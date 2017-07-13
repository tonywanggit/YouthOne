using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Reflection;
using DevExpress.Web.ASPxHeadline;
using DevExpress.Web.ASPxSiteMapControl;
using DevExpress.Web.ASPxEditors;
using YouthOne.Component;
using YouthOne.Component.YouthOneDSTableAdapters;
using System.Collections;

public partial class BasePage : System.Web.UI.Page {
	protected enum DemoPageStatus { Default, New, Updated };
    public const string DefaultThemeName = "Glass";
	const int InvalidHighlightIndex = Int32.MinValue;

    private Dictionary<string, DemoPageStatus> DemoPageGroupsStatus = null;
    private Dictionary<string, DemoPageStatus> DemoPageItemsStatus = null;
	private Dictionary<string, int> DemoPageHighlightedIndex = null;
    private Dictionary<string, List<string>> DemoPageSourceCodeFiles = null;
    private Dictionary<string, Unit> DemoPageCustomSourceCodeWidth = null;
    private string cssLink = "";
    private string demoName = "";

    //--�˴��洢
    private static XmlDocument demoXmlDocument = null;
    //--�˴����ӱ�����ʾÿ������Ա�Ĳ˵�XML
    private XmlDocument menuXmlDocument = null;

    //--����Ԥ������
    private int budgetApply = 0;
    //--��֧����������Ԥ������
    private int budgetApplyAudit = 0;

    //--Ԥ������������
    BudgetApplyTableAdapter baAdapter = new BudgetApplyTableAdapter();

    //--֪ͨ������
    NotificationTableAdapter nfAdapter = new NotificationTableAdapter();

    private AuthenUser authenUser = new AuthenUser();

    protected string CSSLink {
        set { cssLink = value; }
    }
    protected string DemoName {
        get { return demoName; }
    }

    public XmlDocument GetDemoXmlDocument(Page page) {
        //if(BasePage.demoXmlDocument == null) {
            BasePage.demoXmlDocument = new XmlDocument();
			BasePage.demoXmlDocument.Load(page.MapPath("~/App_Data/Demos.xml"));
        //}

        //--�˴���ȡ�����Ի��˵�
        if (menuXmlDocument == null)
        {
            menuXmlDocument = ProcessXmlDocumentRight(BasePage.demoXmlDocument);
        }

        return menuXmlDocument;
    }

    /// <summary>
    /// ���̶��˵����ӻ�Ĳ˵������֧������
    /// </summary>
    /// <param name="xmlDoc"></param>
    /// <returns></returns>
    public XmlDocument ProcessXmlDocumentRight(XmlDocument xmlDoc)
    {
        //--�ȸ��ݹ̶��˵�����һ���û���XML
        XmlDocument xmlMenu = new XmlDocument();
        xmlMenu.LoadXml(xmlDoc.InnerXml);

        //xmlMenu.SelectSingleNode("")

        //--��ȡ����Ա����˵�
        XmlNodeList nodeList = xmlMenu.SelectNodes("Demos/DemoGroup");
        XmlNode nodeMember = null;
        foreach (XmlNode item in nodeList)
        {
            String groupName = GetAttributeValue(item.Attributes, "Text");
            if(groupName == "��Ա����"){
                nodeMember = item;
            }
            if(authenUser.RoleName == AuthenUserType.TZB_Admin)
            {
                if (groupName == "������Ϣ")
                {
                    item.ParentNode.RemoveChild(item);
                }
                else if (groupName == "ϵͳ����")
                {
                    List<XmlNode> lstXmlNode = new List<XmlNode>();
                    foreach (XmlNode sysItem in item.ChildNodes)
                    {
                        String sysItemName = GetAttributeValue(sysItem.Attributes, "Text");
                        if (sysItemName == "�ʻ�����" || sysItemName == "��־���")
                        {
                            //item.RemoveChild(sysItem);
                            lstXmlNode.Add(sysItem);
                        }
                    }
                    foreach (XmlNode nodeRemove in lstXmlNode)
                    {
                        item.RemoveChild(nodeRemove);
                    }
                }
            }
        }

        //--����ǹ�˾��ί����Ա����ȡ�����е�һ��֧���������ȡ������Ͻ��֧��
        YouthGroupTableAdapter ygAdapder = new YouthGroupTableAdapter();
        YouthOneDS.YouthGroupDataTable ygTable = ygAdapder.GetDataZB();
        EnumerableRowCollection<YouthOneDS.YouthGroupRow> rows;
        if (authenUser.YouthGroup == "GSTW")
        {
            rows = ygTable.Where<YouthOneDS.YouthGroupRow>(x => x.YG_LEVEL == 1);
        }
        else
        {
            rows = ygTable.Where<YouthOneDS.YouthGroupRow>(x => x.OID == authenUser.YouthGroup);
        }

        //--����֧���������Ӳ˵���
        if (nodeMember != null && rows != null)
        {
            if(AuthUser.RoleName != AuthenUserType.TZB_Admin)
                AddMemberMenu(xmlMenu, nodeMember, "GSTW", "��˾��ί");

            foreach (YouthOneDS.YouthGroupRow row in rows)
            {
                AddMemberMenu(xmlMenu, nodeMember, row.OID, row.YG_NAME);
            }
        }
        return xmlMenu;
    }

    /// <summary>
    /// ������֧������˵�
    /// </summary>
    /// <param name="menuTree"></param>
    /// <param name="ygNode"></param>
    /// <param name="ygOID"></param>
    /// <param name="ygName"></param>
    private void AddMemberMenu(XmlDocument menuTree, XmlNode ygNode, string ygOID, string ygName)
    {
        XmlElement node = menuTree.CreateElement("Demo");
        XmlAttribute attText = menuTree.CreateAttribute("Text");
        XmlAttribute attNavigateUrl = menuTree.CreateAttribute("NavigateUrl");
        XmlAttribute attTitle = menuTree.CreateAttribute("Title");

        attText.InnerText = ygName;
        attNavigateUrl.InnerText = String.Format("~/Member/MemberList.aspx?OID={0}", ygOID);
        attTitle.InnerText = String.Format("��Ա������Ϣ����ϵͳ - ֧����Ա����", ygName);

        node.Attributes.Append(attText);
        node.Attributes.Append(attNavigateUrl);
        node.Attributes.Append(attTitle);

        ygNode.AppendChild(node);
    }

	public UnboundSiteMapProvider SiteMapProvider {
		get {
			//if(!IsSiteMapCreated)
				Application["DemoUnboundProvider"] = CreateSiteMapProvider();
			return (UnboundSiteMapProvider)Application["DemoUnboundProvider"];
		}
	}
	public bool IsSiteMapCreated { get { return Application["DemoUnboundProvider"] != null; } }

    protected string GetThemeCookieName() {
        string cookieName = "EsbCurrentTheme";
        string path = Page.Request.ApplicationPath;

        int startPos = path.IndexOf("ASPx");
        if(startPos != -1) {
            int endPos = path.IndexOf("/", startPos);
            if(endPos != -1)
                cookieName = path.Substring(startPos, endPos - startPos);
        }
        return cookieName;
    }

    public AuthenUser AuthUser
    {
        get { return authenUser; }
        set { authenUser = value; }
    }


    /* Page PreInit */
    protected void Page_PreInit(object sender, EventArgs e) {
        //Tony ��ʼ����Ȩ
        InitAuthenUser();

        string themeName = DefaultThemeName;
        if (Page.Request.Cookies[GetThemeCookieName()] != null) {
            themeName = Page.Request.Cookies[GetThemeCookieName()].Value;
        }

        string clientScriptBlock = "var DXCurrentThemeCookieName = \"" + GetThemeCookieName() + "\";";
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "DXCurrentThemeCookieName", clientScriptBlock, true);
        this.Theme = themeName;
        
    }
	/* Page Init */
	protected override void OnInit(EventArgs e) {
        base.OnInit(e);

		ClearDemoProperties();
        InitDemoProperties();  
	}
    /* Page Load */
    protected override void OnLoad(EventArgs e) {
        base.OnLoad(e);

        Response.Expires = -1;

        RegisterScript("Utilities", "~/Scripts/Utilities.js");
        RegisterCSSLink("~/CSS/styles.css");
        if (!string.IsNullOrEmpty(this.cssLink))
            RegisterCSSLink(this.cssLink);
    }

    /// <summary>
    /// ��ʼ����Ȩ�û�
    /// </summary>
    void InitAuthenUser()
    {
        authenUser = AuthenUser.GetCurrentUser();

        if (authenUser == null)
        {
            AdapterHelper.AlertLogout();
        }
    }

    /// <summary>
    /// ��ʾJS��ʾ��
    /// </summary>
    /// <param name="message"></param>
    protected void AlertJS(String message)
    {
        string strAll = "<SCRIPT lanquage='JScript'>window.alert('" + message + "');<" + "/SCRIPT>";

        Response.Write(strAll);
        //Response.End();

        //string clientScriptBlock = "window.alert('" + message + "');";
        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "YouthOneAlert", clientScriptBlock, true);
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "YouthOneAlert", clientScriptBlock, true);
    }

    /// <summary>
    /// ����Դ��쿴��
    /// </summary>
    protected void HideSourceCodeTable()
    {
        //Form.FindControl("tblSourceCode").Visible = false;
    }

    /// <summary>
    /// ���ߣ�Tony 2011-02-23
    /// ���ܣ�����ASPxComboBox��None��ʾֵ��һ����ASPxComboBox��DataBound�¼��н��е���
    /// </summary>
    /// <param name="cb"></param>
    /// <param name="noneDisplayText"></param>
    protected void SetASPxComboBoxNullItem(ASPxComboBox cb, string noneDisplayText)
    {
        if (string.IsNullOrEmpty(noneDisplayText))
            noneDisplayText = "(none)";

        if (cb != null)
        {
            ListEditItem item = new ListEditItem("noneDisplayText", "");
            cb.Items.Insert(0, item);
            cb.SelectedItem = item;
        }
    }

    /// <summary>
    /// Ϊҳ��������־ģ��
    /// </summary>
    /// <param name="logType"></param>
    /// <param name="opName"></param>
    /// <param name="opDesc"></param>
    protected void WriteLog(LogType logType, String opName, String opDesc)
    {
        AdapterHelper.WriteLog(logType, opName, opDesc, AuthenUser.GetCurrentUser().UserID, AuthenUser.GetCurrentUser().UserName);
    }


    /* Functions */
	public void PrepareStatusHeadlineGroups(ASPxHeadline sender) {
		PrepareStatusHeadlineCore(sender, this.DemoPageGroupsStatus);
    }
	public void PrepareStatusHeadlineItems(ASPxHeadline sender) {
        PrepareStatusHeadlineCore(sender, this.DemoPageItemsStatus);
    }
    protected virtual bool IsCurrentPage(object oUrl) {
        if (oUrl == null)
            return false;

        bool result = false;
        string url = oUrl.ToString();
        if (url.ToLower() == Page.Request.AppRelativeCurrentExecutionFilePath.ToLower())
            result = true;
        return result;
    }
    /* Events */
    protected virtual void TraceEvent(HtmlGenericControl memo, object sender, EventArgs args, string eventName) {
        string s = "";
        PropertyInfo[] properties = args.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
        foreach (PropertyInfo propertyInfo in properties) {
            s += propertyInfo.Name + " = " +
                (propertyInfo.PropertyType.IsValueType ? propertyInfo.GetValue(args, null) : "[" + propertyInfo.PropertyType.Name + "]") + "<br />";
        }

        memo.InnerHtml += "<table cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tr><td valign=\"top\" style=\"width: 100px;\">Sender:</td><td valign=\"top\">" +
            (sender as Control).ID +
            "</td></tr><tr><td valign=\"top\">EventType:</td><td valign=\"top\"><b>" +
            eventName + "</b></td></tr><tr><td valign=\"top\">Arguments:</td><td valign=\"top\">" +
            s + "</td></tr></table><br />";
    }
    protected virtual void ClearEvents(HtmlGenericControl memo) {
        memo.InnerHtml = "";
    }
    protected virtual void InitEvents(HtmlGenericControl memo) {
        Page.ClientScript.RegisterStartupScript(typeof(BasePage), "ScrollEvents", "document.getElementById('" + memo.ClientID + "').scrollTop = 100000;", true);
    }
    protected virtual bool GetStatus(object dataItem, string name) {
        IHierarchyData hierarchyData = (dataItem as IHierarchyData);
        XmlElement xmlElement = hierarchyData.Item as XmlElement;
        return GetStatusCore(xmlElement, name);
    }

    /* Private Functions */
	private void PrepareStatusHeadlineCore(ASPxHeadline hl, Dictionary<string, DemoPageStatus> colStatus) {
		if(hl != null && colStatus != null && colStatus.ContainsKey(GetStatusKey(hl.ContentText, hl.NavigateUrl))) {
			switch(colStatus[GetStatusKey(hl.ContentText, hl.NavigateUrl)]) {
                case DemoPageStatus.New:
                    hl.TailImage.Url = "~/Images/New.png";
                    hl.TailImage.Width = Unit.Pixel(20);
                    hl.TailImage.Height = Unit.Pixel(11);
                    break;
                case DemoPageStatus.Updated:
                    hl.TailImage.Url = "~/Images/Updated.png";
                    hl.TailImage.Width = Unit.Pixel(34);
                    hl.TailImage.Height = Unit.Pixel(11);
                    break;
            }
        }
    }
	private string GetStatusKey(string text, string url) {
		return text + "-" + url;
	}
	protected DemoPageStatus GetItemStatus(DevExpress.Web.ASPxNavBar.NavBarItem item) {
		string key = GetStatusKey(item.Text, item.NavigateUrl);
		if(DemoPageItemsStatus.ContainsKey(key)) return DemoPageItemsStatus[key];
		return DemoPageStatus.Default;
	}
	public bool IsHighlighted(DevExpress.Web.ASPxNavBar.NavBarItem item) {
		string key = GetStatusKey(item.Text, item.NavigateUrl);
		return DemoPageHighlightedIndex.ContainsKey(key);
	}
	public int GetHighlightedIndex(DevExpress.Web.ASPxNavBar.NavBarItem item) {
		string key = GetStatusKey(item.Text, item.NavigateUrl);
		return DemoPageHighlightedIndex[key];
	}
    public List<string> GetSourceCodeFiles(DevExpress.Web.ASPxNavBar.NavBarItem item) {
        if (item == null) return null;
        string key = GetStatusKey(item.Text, item.NavigateUrl);
        return DemoPageSourceCodeFiles.ContainsKey(key) ? DemoPageSourceCodeFiles[key] : null;
    }
    public Unit GetCustomSourceCodeWidth(DevExpress.Web.ASPxNavBar.NavBarItem item) {
        if (item == null) return Unit.Empty;
        string key = GetStatusKey(item.Text, item.NavigateUrl);
        return DemoPageCustomSourceCodeWidth.ContainsKey(key) ? DemoPageCustomSourceCodeWidth[key] : Unit.Empty;
    }
    private void ClearDemoProperties() {
        this.DemoPageGroupsStatus = null;
        this.DemoPageItemsStatus = null;
        this.DemoPageHighlightedIndex = null;
        this.DemoPageSourceCodeFiles = null;
        this.DemoPageCustomSourceCodeWidth = null;
    }

    //--��ʼ���˵�����
    private void InitDemoProperties() {
        this.DemoPageGroupsStatus = new Dictionary<string, DemoPageStatus>();
        this.DemoPageItemsStatus = new Dictionary<string, DemoPageStatus>();
        this.DemoPageHighlightedIndex = new Dictionary<string, int>();
        this.DemoPageSourceCodeFiles = new Dictionary<string,List<string>>();
        this.DemoPageCustomSourceCodeWidth = new Dictionary<string, Unit>();

        //--��ȡ������Ԥ������
        this.budgetApply = this.baAdapter.GetNoAuditApplyNum();

        //--�������֧������Ա������֧����������Ԥ������
        this.budgetApplyAudit = (AuthUser.RoleName == AuthenUserType.TZB_Admin )? this.baAdapter.GetAuditApplyNum(AuthUser.YouthGroup) : 0;

		XmlDocument xmlDoc = GetDemoXmlDocument(Page);
        if (string.IsNullOrEmpty(DemoName)) {
            this.demoName = "ASPxperience";
            if (xmlDoc.DocumentElement.Attributes["Name"] != null)
                this.demoName = xmlDoc.DocumentElement.Attributes["Name"].Value;
        }
        foreach (XmlNode node in xmlDoc.SelectNodes("//DemoGroup")) {
            AddPageStatus(this.DemoPageGroupsStatus, node);
            foreach (XmlNode nodeItem in node.SelectNodes("Demo")) {
                AddPageStatus(this.DemoPageItemsStatus, nodeItem);
                AddPageHighlightedIndex(this.DemoPageHighlightedIndex, nodeItem);
                AddPageSourceCodeFiles(this.DemoPageSourceCodeFiles, nodeItem);
                AddPageCustomSourceCodeWidth(this.DemoPageCustomSourceCodeWidth, nodeItem);
            }
        }

    }

    /// <summary>
    /// ���ѹ��� ����NEW��ʾ����
    /// </summary>
    /// <param name="ret"></param>
    /// <param name="node"></param>
    private void AddPageStatus(Dictionary<string, DemoPageStatus> ret, XmlNode node) {
        string url = GetAttributeValue(node.Attributes, "NavigateUrl");
        string text = GetAttributeValue(node.Attributes, "Text");
        DemoPageStatus status = DemoPageStatus.Default;
        //if (GetStatusCore(node, "IsNew"))
        //    status = DemoPageStatus.New;
        //else if (GetStatusCore(node, "IsUpdated"))
        //    status = DemoPageStatus.Updated;

        if (text == "���ѹ���" || text == "Ԥ���걨")
        {
            //--������ί����Ա��˵�������δ������Ԥ�����룬����Ҫ��NEW������ʾ
            if (budgetApply > 0 && authenUser.RoleName == AuthenUserType.TW_Finance)
                status = DemoPageStatus.New;

            //--������֧������Ա��˵���������������Ԥ�����룬����Ҫ��NEW������ʾ
            if (budgetApplyAudit > 0 && authenUser.RoleName == AuthenUserType.TZB_Admin)
                status = DemoPageStatus.New;
        }

        if (text == "��ί�ſ�" || text == "���¶�̬")
        {
            if(nfAdapter.GetNotificationRows(AuthUser.YouthGroup, null).Count > 0)
                status = DemoPageStatus.New;
        }


        ret.Add(GetStatusKey(text, url), status);
    }
	private void AddPageHighlightedIndex(Dictionary<string, int> ret, XmlNode node) {
		int index = GetHighlightedIndexCore(node, "HighlightedIndex");
		if(index == InvalidHighlightIndex) return;

		string url = GetAttributeValue(node.Attributes, "NavigateUrl");
		string text = GetAttributeValue(node.Attributes, "Text");
		ret.Add(GetStatusKey(text, url), index);
	}
    private void AddPageSourceCodeFiles(Dictionary<string, List<string>> ret, XmlNode node) {
        List<string> files = null;

        foreach(XmlNode nodeFiles in node.SelectNodes("SourceCodeFiles")) {
            foreach(XmlNode nodeFile in nodeFiles.SelectNodes("File")) {
                string fileName = GetAttributeValue(nodeFile.Attributes, "Name");
                if(!string.IsNullOrEmpty(fileName)) {
                    if(files == null)
                        files = new List<string>();
                    files.Add(fileName);
                }
            }
        }

        if(files != null) {
            string url = GetAttributeValue(node.Attributes, "NavigateUrl");
            string text = GetAttributeValue(node.Attributes, "Text");
            ret.Add(GetStatusKey(text, url), files);
        }
    }
    private void AddPageCustomSourceCodeWidth(Dictionary<string, Unit> ret, XmlNode node) {
        foreach(XmlNode nodeFiles in node.SelectNodes("SourceCodeFiles")) {
            XmlAttribute widthAttribute = nodeFiles.Attributes["PageControlWidth"];
            if(widthAttribute != null) {
                Unit width = Unit.Parse(widthAttribute.Value);
                if(!width.IsEmpty) {
                    string url = GetAttributeValue(node.Attributes, "NavigateUrl");
                    string text = GetAttributeValue(node.Attributes, "Text");
                    ret.Add(GetStatusKey(text, url), width);
                    break;
                }
            }
        }
    }
    private bool GetStatusCore(XmlElement element, string name) {
        bool ret = false;

        string value = GetAttributeValue(element.Attributes, name);
        bool.TryParse(value, out ret);
        return ret;
    }
    private bool GetStatusCore(XmlNode node, string name) {
        bool ret = false;
        string value = GetAttributeValue(node.Attributes, name);
        bool.TryParse(value, out ret);
        return ret;
    }
	private int GetHighlightedIndexCore(XmlNode node, string name) {
		int ret = InvalidHighlightIndex;
		string value = GetAttributeValue(node.Attributes, name);
		if(!int.TryParse(value, out ret)) return InvalidHighlightIndex;
		return ret;
	}
    private string GetAttributeValue(XmlAttributeCollection attributes, string name) {
        if (attributes[name] != null)
            return attributes[name].Value;
        else
            return "";
    }
    private void RegisterScript(string key, string url) {
        Page.ClientScript.RegisterClientScriptInclude(key, Page.ResolveUrl(url));
    }
    private void RegisterCSSLink(string url) {
        HtmlLink link = new HtmlLink();
        Page.Header.Controls.Add(link);
        link.EnableViewState = false;
        link.Attributes.Add("type", "text/css");
        link.Attributes.Add("rel", "stylesheet");
        link.Href = url;
    }
	protected UnboundSiteMapProvider CreateSiteMapProvider() {
		UnboundSiteMapProvider provider = new UnboundSiteMapProvider("", "");

		SiteMapNode categoryDemoNode = provider.RootNode;
		foreach(XmlNode groupNode in GetDemoXmlDocument(Page).SelectNodes("//DemoGroup[not(@Visible=\"false\")]")) {
			bool groupIsNew = false;
			if(groupNode.Attributes["IsNew"] != null) {
				string value = groupNode.Attributes["IsNew"].Value;
				bool.TryParse(value, out groupIsNew);
			}
			bool groupIsUpdated = false;
			if(groupNode.Attributes["IsUpdated"] != null) {
				string value = groupNode.Attributes["IsUpdated"].Value;
				bool.TryParse(value, out groupIsUpdated);
			}

			System.Collections.Specialized.NameValueCollection attributes = new System.Collections.Specialized.NameValueCollection();
			attributes.Add("IsNew", groupIsNew.ToString());
			attributes.Add("IsUpdated", groupIsUpdated.ToString());
			SiteMapNode groupDemoNode = provider.CreateNode("", groupNode.Attributes["Text"].Value, "", null, attributes);

			bool beginCategory = false;
			if(groupNode.Attributes["BeginCategory"] != null &&
					bool.TryParse(groupNode.Attributes["BeginCategory"].Value, out beginCategory) &&
					beginCategory) {
				categoryDemoNode = provider.CreateNode("", "");
				provider.AddSiteMapNode(categoryDemoNode, provider.RootNode);
			}

			provider.AddSiteMapNode(groupDemoNode, categoryDemoNode);

			foreach(XmlNode itemNode in groupNode.SelectNodes("Demo")) {
				bool itemIsNew = false;
				if(itemNode.Attributes["IsNew"] != null) {
					string value = itemNode.Attributes["IsNew"].Value;
					bool.TryParse(value, out itemIsNew);
				}
				bool itemIsUpdated = false;
				if(itemNode.Attributes["IsUpdated"] != null) {
					string value = itemNode.Attributes["IsUpdated"].Value;
					bool.TryParse(value, out itemIsUpdated);
				}
				attributes = new System.Collections.Specialized.NameValueCollection();
				attributes.Add("IsNew", itemIsNew.ToString());
				attributes.Add("IsUpdated", itemIsUpdated.ToString());
				SiteMapNode itemDemoNode = provider.CreateNode(itemNode.Attributes["NavigateUrl"].Value, itemNode.Attributes["Text"].Value, "", null, attributes);
				provider.AddSiteMapNode(itemDemoNode, groupDemoNode);
			}
		}
		return provider;
	}
	public virtual void EnsureSiteMapIsBound() { }
}

public partial class AppearancePage : BasePage {
    protected virtual void mSelector_ItemDataBound(object source, DevExpress.Web.ASPxMenu.MenuItemEventArgs e) {
        if (GetStatus(e.Item.DataItem, "IsUpdated"))
            e.Item.Text += " <span style=\"color: #2D9404;\">(updated)</span>";
        if (GetStatus(e.Item.DataItem, "IsNew"))
            e.Item.Text += " <span style=\"color: #BD0808;\">(new)</span>";
    }
    protected virtual string GetHeaderTitle(string title, string text) {
        string name = title + " - ";
        if (text.IndexOf("<") > 0)
            name += text.Substring(0, text.IndexOf("<"));
        else
            name += text;
        return name;
    }
    protected virtual string GetCurrentAppearanceName() {
        return "Appearances/" + GetCurrentAppearanceNameCore() + ".ascx";
    }
    protected virtual string GetCurrentAppearanceNameCore() {
        string name = Page.Request.QueryString["Name"];
        if (String.IsNullOrEmpty(name))
            name = "";
        return name;
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          