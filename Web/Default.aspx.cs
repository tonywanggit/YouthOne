using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.Data;
using DevExpress.Web.ASPxGridView;
using YouthOne.Component.YouthOneDSTableAdapters;
using DevExpress.Web.ASPxClasses;
using YouthOne.Component;

public partial class _DefaultPage : BasePage {

    NotificationTableAdapter _NTAdapter = new NotificationTableAdapter();
    private OrganizeTableAdapter ogAdapter = new OrganizeTableAdapter();
    private YouthGroupTableAdapter ygAdapter = new YouthGroupTableAdapter();

    protected void Page_Load(object sender, EventArgs e) {

        CSSLink = "~/CSS/Default.css"; // Register css file
        if (Theme == "Youthful")
            CSSLink = "~/CSS/DefaultYouthful.css"; // Register css file


        InitPage();

        if (!Page.IsPostBack && !Page.IsCallback)
        {
            BindNFGrid();
        }
    }

    protected void InitPage()
    {
        grid.HtmlCommandCellPrepared += new DevExpress.Web.ASPxGridView.ASPxGridViewTableCommandCellEventHandler(grid_HtmlCommandCellPrepared);
        grid.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(grid_CustomCallback);
        btnSearch.Click += new EventHandler(btnSearch_Click);

        if (!Page.IsPostBack && !Page.IsCallback)
        {
            gridPost.DataBound += new EventHandler(gridPost_DataBound);
        }
    }

    void btnSearch_Click(object sender, EventArgs e)
    {
        BindNFGrid();
    }

    protected void BindNFGrid()
    {
        string catType = cbNotification.Value == null ? "" : cbNotification.Value.ToString();
        grid.DataSource = _NTAdapter.GetNotificationRows(AuthUser.YouthGroup, catType);
        grid.DataBind();
    }

    void gridPost_DataBound(object sender, EventArgs e)
    {
        this.gridPost.GroupBy(this.gridPost.Columns["CAT_NAME"]);
        this.gridPost.ExpandAll();
    }

    void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        string[] cbArray = e.Parameters.Split('|');
        string opType = cbArray[0];
        string mmOID = cbArray[1];
        string nfOID = cbArray[2];
        string empName = cbArray[3];
        string ygOID = string.Empty;
        string fzOID = string.Empty;
        string dpName = string.Empty;
        string ksName = string.Empty;
        string psName = string.Empty;

        if (opType == "转入")
        {
            ygOID = cbArray[4];
            fzOID = cbArray[5];
            dpName = cbArray[6];
            ksName = cbArray[7];
            psName = cbArray[8];
        }

        _NTAdapter.NotifiationOp(nfOID, opType, AuthUser.UserID, mmOID, ygOID, fzOID, dpName, ksName, psName);

        WriteLog(LogType.Bussiness, opType, string.Format("管理员『{0}』对『{1}』执行了{2}动作。", AuthUser.LoginName, empName, opType));
        BindNFGrid();
    }

    void grid_HtmlCommandCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableCommandCellEventArgs e)
    {
        if (e.CommandCellType == GridViewTableCommandCellType.Data)
        {
            if (grid.GetRowValuesByKeyValue(e.KeyValue, "NF_CAT").ToString() == "生日"
                || AuthUser.RoleName == AuthenUserType.TW_Browser
                || AuthUser.RoleName == AuthenUserType.TW_Finance    
                )
            {
                e.Cell.Controls[0].Visible = false;
            }
        }
    }

    protected void grid_OnPageIndexChanged(object sender, EventArgs e)
    {
        BindNFGrid();
    }

    protected void cbWorkGroup_Callback(object source, CallbackEventArgsBase e)
    {
        cbWorkGroup.DataSource = ogAdapter.GetWorkGroupCache(e.Parameter);
        cbWorkGroup.TextField = "OG_NAME";
        cbWorkGroup.ValueField = "OG_NAME";
        cbWorkGroup.DataBind();
    }


    protected void cbYouthGroupFZ_Callback(object source, CallbackEventArgsBase e)
    {
        cbYouthGroupFZ.DataSource = ygAdapter.GetFZCache(e.Parameter);
        cbYouthGroupFZ.TextField = "YG_NAME";
        cbYouthGroupFZ.ValueField = "OID";
        cbYouthGroupFZ.DataBind();
    }


    protected bool IsImageVisible(object visible) {
        if (visible != null)
            return bool.Parse(visible.ToString());
        return true;
    }
}
