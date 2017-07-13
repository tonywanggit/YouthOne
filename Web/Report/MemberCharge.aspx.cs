using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YouthOne.Component;
using YouthOne.Component.YouthOneDSTableAdapters;
using DevExpress.Web.ASPxEditors;

public partial class Report_MemberCharge : BasePage
{
    private YouthGroupTableAdapter ygAdapter = new YouthGroupTableAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
        BindYouthGroup();

        if (!Page.IsPostBack)
        {
            cbYouthGroup.SelectedIndex = 0;
            cbQuarter.SelectedIndex = GetCurQuarter() - 1;
            InitBJ();
            InitRight();
        }

        BindReport();
    }

    void cbYouthGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        InitBJ();
        BindReport();
    }

    private void InitRight()
    {
        if (AuthUser.YouthGroup != "GSTW")
        {
            foreach (ListEditItem item in cbYouthGroup.Items)
            {
                if (item.Value.ToString() == AuthUser.YouthGroup)
                    cbYouthGroup.SelectedItem = item;
            }

            cbYouthGroup.ReadOnly = true;
        }
        if (AuthUser.RoleName == AuthenUserType.TW_Browser || AuthUser.RoleName == AuthenUserType.TW_Finance)
        {
            btnBJ.Enabled = false;
        }
    }

    private void InitBJ()
    {
        txtBJ1.Value = 0;
        txtBJ2.Value = 0;
        txtBJ3.Value = 0;
    }

    private void BindReport()
    {
        TuanFeiReport rpt = new TuanFeiReport();
        rpt.SetReportParameter(cbYouthGroup.Text, GetYearQuarter()
            , int.Parse(txtBJ1.Value.ToString()), int.Parse(txtBJ2.Value.ToString()), int.Parse(txtBJ3.Value.ToString()));
        ReportViewerControl1.ReportViewer.Report = rpt;
    }

    private void BindYouthGroup()
    {
        cbYouthGroup.DataSource = ygAdapter.GetZBCache();
        cbYouthGroup.TextField = "YG_NAME";
        cbYouthGroup.ValueField = "OID";
        cbYouthGroup.DataBind();
    }

    /// <summary>
    /// 获取到当前季度
    /// </summary>
    /// <returns></returns>
    private int GetCurQuarter()
    {
        if (DateTime.Now.Month < 4)
            return 1;
        else if (DateTime.Now.Month < 7)
            return 2;
        else if (DateTime.Now.Month < 10)
            return 3;
        else
            return 4;
    }

    /// <summary>
    /// 获取到团费缴纳季度
    /// </summary>
    /// <returns></returns>
    private string GetYearQuarter()
    {
        return DateTime.Now.Year + "-" + cbQuarter.Value.ToString();
    }

}
