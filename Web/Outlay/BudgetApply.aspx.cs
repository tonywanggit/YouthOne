using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YouthOne.Component.YouthOneDSTableAdapters;
using YouthOne.Component;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class Outlay_BudgetApply : BasePage
{
    private YouthGroupTableAdapter ygAdapter = new YouthGroupTableAdapter();
    private BudgetApplyTableAdapter baAdapter = new BudgetApplyTableAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack && !Page.IsCallback)
        {
            BindComBox();

            if (AuthUser.RoleName == AuthenUserType.TW_Finance)
            {
                cbMonth.SelectedIndex = 0;
                cbStatus.SelectedIndex = 3;
            }
            baAdapter.DeleteAuditApply(AuthUser.YouthGroup);
        }
        InitPage();
    }

    void InitPage()
    {
        grid.InitNewRow += new DevExpress.Web.Data.ASPxDataInitNewRowEventHandler(grid_InitNewRow);
        grid.HtmlCommandCellPrepared += new DevExpress.Web.ASPxGridView.ASPxGridViewTableCommandCellEventHandler(grid_HtmlCommandCellPrepared);
        grid.DataBound += new EventHandler(grid_DataBound);
        grid.HtmlEditFormCreated += new ASPxGridViewEditFormEventHandler(grid_HtmlEditFormCreated);
    }

    void grid_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (AuthenUserType.TZB_Admin == AuthUser.RoleName)
        {
            e.EditForm.Controls[0].Controls[3].Visible = false;
            e.EditForm.Controls[0].Controls[4].Visible = false;
        }
        
        if (grid.IsNewRowEditing)
        {
            return;
        }

        if (grid.GetRowValues(grid.EditingRowVisibleIndex, "BA_STATUS").ToString() != "等待审核中" 
                && AuthUser.RoleName != AuthenUserType.TW_Finance
                && AuthUser.RoleName != AuthenUserType.TW_Admin
                && AuthUser.RoleName != AuthenUserType.Admin
            )
            e.EditForm.Enabled = false;
    }

    void grid_DataBound(object sender, EventArgs e)
    {
        if (AuthUser.RoleName == AuthenUserType.TW_Finance || AuthUser.RoleName == AuthenUserType.Admin)
            (grid.Columns["BA_STATUS"] as GridViewDataComboBoxColumn).ReadOnly = false;
    }

    void grid_HtmlCommandCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableCommandCellEventArgs e)
    {
        //--Admin和团委管理员不做限制，如果当前行在编辑中则不控制按钮
        if (AuthUser.RoleName == AuthenUserType.Admin || grid.EditingRowVisibleIndex == e.VisibleIndex) return; 

        if (e.CommandCellType == GridViewTableCommandCellType.Data)
        {
            if (grid.GetRowValuesByKeyValue(e.KeyValue, "BA_STATUS").ToString() == "等待审核中")
            {
                //--只有团支部管理员有删除权限
                if (e.Cell.Controls.Count > 1)
                {
                    e.Cell.Controls[1].Visible = (AuthUser.RoleName == AuthenUserType.TZB_Admin);
                }

                //--团委管理员2无法进行编辑
                if (e.Cell.Controls.Count > 0 && AuthUser.RoleName == AuthenUserType.TW_Browser)
                {
                    ((System.Web.UI.WebControls.HyperLink)(e.Cell.Controls[0].Controls[0])).Enabled = false;
                    ((DevExpress.Web.ASPxGridView.Rendering.GridViewCommandColumnButtonControl)(e.Cell.Controls[0])).Enabled = false;
                }
            }
            else
            {
                //--审核通过就不允许删除
                if (e.Cell.Controls.Count > 1)
                {
                    e.Cell.Controls[1].Visible = false;
                }

                //--审核通过就不允许编辑
                if (e.Cell.Controls.Count > 0)
                {
                    ((System.Web.UI.WebControls.HyperLink)(e.Cell.Controls[0].Controls[0])).Enabled = false;
                    ((DevExpress.Web.ASPxGridView.Rendering.GridViewCommandColumnButtonControl)(e.Cell.Controls[0])).Enabled = false;
                }

            }
        }
    }

    void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["YG_OID"] = AuthUser.YouthGroup;
        e.NewValues["BA_STATUS"] = "等待审核中";
        e.NewValues["CRE_DATE"] = DateTime.Now;
    }

    void BindComBox()
    {
        List<YouthOneDS.YouthGroupRow> lstFZ = ygAdapter.GetZBCache();
        YouthOneDS.YouthGroupDataTable tbFZ = new YouthOneDS.YouthGroupDataTable();
        YouthOneDS.YouthGroupRow row = tbFZ.AddYouthGroupRow("GSTW", "所有支部", 0, "", DateTime.Now);

        lstFZ.Insert(0, row);

        cbYouthGroup.DataSource = lstFZ;
        cbYouthGroup.TextField = "YG_NAME";
        cbYouthGroup.ValueField = "OID";
        cbYouthGroup.DataBind();

        cbYouthGroup.SelectedIndex = 0;

        if (AuthUser.YouthGroup != "GSTW")
        {
            foreach (ListEditItem item in cbYouthGroup.Items)
            {
                if (item.Value.ToString() == AuthUser.YouthGroup)
                    cbYouthGroup.SelectedItem = item;
            }

            cbYouthGroup.ReadOnly = true;
        }
        else
        {
            btnAdd.Enabled = false;
        }

        cbMonth.SelectedIndex = DateTime.Now.Month; 
    }
}
