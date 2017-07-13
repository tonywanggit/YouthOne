using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YouthOne.Component.YouthOneDSTableAdapters;
using DevExpress.Web.ASPxGridView;
using YouthOne.Component;
using DevExpress.Web.ASPxGridView.Rendering;

public partial class Outlay_OutLayPublish : BasePage
{
    private OutlayPublishTableAdapter opAdapter = new OutlayPublishTableAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
        InitPage();
        InitRight();
    }

    private void InitPage()
    {
        Session["OutLayPublish_YG_OID"] = AuthUser.YouthGroup;

        grid.HtmlCommandCellPrepared += new DevExpress.Web.ASPxGridView.ASPxGridViewTableCommandCellEventHandler(grid_HtmlCommandCellPrepared);
        grid.CustomCallback += new ASPxGridViewCustomCallbackEventHandler(grid_CustomCallback);
        grid.HtmlEditFormCreated += new ASPxGridViewEditFormEventHandler(grid_HtmlEditFormCreated);

        btnAllPublish.Click += new EventHandler(btnAllPublish_Click);
    }

    void grid_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if(grid.GetRowValues(grid.EditingRowVisibleIndex, "OL_STATUS").ToString() == "已发放")
            e.EditForm.Enabled = false;
    }

    private void InitRight()
    {
        if (AuthUser.RoleName != AuthenUserType.TW_Finance && AuthUser.RoleName != AuthenUserType.Admin)
        {
            grid.Columns["OP_COL"].Visible = false;
            btnAllPublish.Enabled = false;
        }
    }

    void btnAllPublish_Click(object sender, EventArgs e)
    {
        opAdapter.PublishAll();
        grid.DataBind();
    }

    void grid_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        String[] cbArray = e.Parameters.Split('|');

        opAdapter.PublishOne(cbArray[0]);
        WriteLog(LogType.Bussiness, "经费下发", "下发支部：" + cbArray[1]);
        grid.DataBind();
    }

    void grid_HtmlCommandCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableCommandCellEventArgs e)
    {
        if (grid.EditingRowVisibleIndex == e.VisibleIndex) return; 

        if (e.CommandCellType == GridViewTableCommandCellType.Data)
        {
            if (grid.GetRowValuesByKeyValue(e.KeyValue, "OL_STATUS").ToString() == "已发放"
                || ( AuthUser.RoleName != AuthenUserType.TW_Finance && AuthUser.RoleName != AuthenUserType.Admin && AuthUser.RoleName != AuthenUserType.TW_Admin)
                )
            {
                if (e.Cell.Controls.Count > 1)
                {
                    e.Cell.Controls[1].Visible = false;
                }

                ((System.Web.UI.WebControls.HyperLink)(e.Cell.Controls[0].Controls[0])).Enabled = false;
                ((DevExpress.Web.ASPxGridView.Rendering.GridViewCommandColumnButtonControl)(e.Cell.Controls[0])).Enabled = false;
            }

        }
    }
}
