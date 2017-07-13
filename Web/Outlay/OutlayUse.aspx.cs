using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YouthOne.Component;
using YouthOne.Component.YouthOneDSTableAdapters;
using DevExpress.Web.ASPxGridView.Rendering;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;


public partial class Outlay_OutlayUse : BasePage
{
    private YouthGroupTableAdapter ygAdapter = new YouthGroupTableAdapter();
    private OutlayUseTableAdapter ouAdapter = new OutlayUseTableAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack && !Page.IsCallback)
            BindYG();

        InitPage();
        InitRight();
    }

    private void InitPage()
    {
        grid.CustomCallback += new DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventHandler(grid_CustomCallback);
        grid.HtmlCommandCellPrepared += new DevExpress.Web.ASPxGridView.ASPxGridViewTableCommandCellEventHandler(grid_HtmlCommandCellPrepared);
        grid.HtmlEditFormCreated += new ASPxGridViewEditFormEventHandler(grid_HtmlEditFormCreated);
    }

    private void InitRight()
    {
        if (AuthUser.RoleName == AuthenUserType.TZB_Admin)
        {
            grid.Columns["OP_COL"].Visible = false;
            btnAdd.Enabled = false;
        }
    }

    void grid_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (grid.GetRowValues(grid.EditingRowVisibleIndex, "OU_STATUS").ToString() == "1")
            e.EditForm.Enabled = false;
    }

    void grid_HtmlCommandCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableCommandCellEventArgs e)
    {
        if (grid.EditingRowVisibleIndex == e.VisibleIndex) return; 

        if (e.CommandCellType == GridViewTableCommandCellType.Data)
        {
            if (grid.GetRowValuesByKeyValue(e.KeyValue, "OU_STATUS").ToString() == "1"
                || (AuthUser.RoleName != AuthenUserType.TW_Finance && AuthUser.RoleName != AuthenUserType.Admin && AuthUser.RoleName != AuthenUserType.TW_Admin)
                )
            {
                if (e.Cell.Controls.Count > 1)
                    e.Cell.Controls[1].Visible = false;


                ((System.Web.UI.WebControls.HyperLink)(e.Cell.Controls[0].Controls[0])).Enabled = false;
                ((DevExpress.Web.ASPxGridView.Rendering.GridViewCommandColumnButtonControl)(e.Cell.Controls[0])).Enabled = false;
            }
        }
    }

    void grid_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        string[] paramsArray = e.Parameters.Split('|');
        string YG_OID = paramsArray[0];
        string OU_TYPE = paramsArray[1];
        decimal OU_NUM = decimal.Parse(paramsArray[2]);
        string OU_DESC = paramsArray[3];

        ouAdapter.NewOutLayUse(YG_OID, OU_TYPE, OU_NUM, OU_DESC, AuthUser.UserID);
        grid.DataBind();
    }

    void BindYG()
    {
        List<YouthOneDS.YouthGroupRow> lstFZ = ygAdapter.GetZBCache();
        YouthOneDS.YouthGroupDataTable tbFZ = new YouthOneDS.YouthGroupDataTable();
        YouthOneDS.YouthGroupRow row = tbFZ.AddYouthGroupRow("ALL", "所有支部", 0, "", DateTime.Now);
        YouthOneDS.YouthGroupRow rowGSTW = tbFZ.AddYouthGroupRow("GSTW", "公司团委", 0, "", DateTime.Now);


        lstFZ.Insert(0, rowGSTW);
        cbYouthGroup.DataSource = lstFZ;
        cbYouthGroup.TextField = "YG_NAME";
        cbYouthGroup.ValueField = "OID";
        cbYouthGroup.DataBind();
        cbYouthGroup.SelectedIndex = 0;

        lstFZ.Insert(0, row);
        cbFZ.DataSource = lstFZ;
        cbFZ.TextField = "YG_NAME";
        cbFZ.ValueField = "OID";
        cbFZ.DataBind();

        if (AuthUser.YouthGroup != "GSTW")
        {
            foreach (ListEditItem item in cbFZ.Items)
            {
                if (item.Value.ToString() == AuthUser.YouthGroup)
                    cbFZ.SelectedItem = item;
            }

            cbFZ.ReadOnly = true;
        }
        else
        {
            if (!String.IsNullOrEmpty(Request["YG_OID"]))
            {
                foreach (ListEditItem item in cbFZ.Items)
                {
                    if (item.Value.ToString() == Request["YG_OID"])
                        cbFZ.SelectedItem = item;
                }
            }
            else
            {
                cbFZ.SelectedIndex = 0;
            }
        }
    }
}
