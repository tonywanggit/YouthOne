using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YouthOne.Component;
using YouthOne.Component.YouthOneDSTableAdapters;

public partial class Report_YouthInfo : BasePage
{
    MemberTableAdapter mmAdapter = new MemberTableAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["Window"] == "New")
        {
            this.Master.FindControl("rpMenu").Visible = false;
            this.Master.FindControl("nav").Visible = false;
        }

        if (String.IsNullOrEmpty(txtHrCode.Text) && String.IsNullOrEmpty(Request["OID"]))
        {
            //if (mmAdapter.GetMemberRandom().Rows.Count > 0)
            //    BindingReport(mmAdapter.GetMemberRandom().Rows[0]["OID"].ToString());
        }
        else if (!String.IsNullOrEmpty(txtHrCode.Text))
        {
            CreateReport(txtHrCode.Text);
        }
        else
        {
            BindingReport(Request["OID"]);
        }
    }

    /// <summary>
    /// 根据工号创建报表
    /// </summary>
    /// <param name="hrCode"></param>
    private void CreateReport(string hrCode){
        YouthOneDS.MemberDataTable mmTable = mmAdapter.GetMember(hrCode);
        if (mmTable.Rows.Count > 0)
        {
            if (AuthUser.RoleName == AuthenUserType.TZB_Admin && mmTable.Rows[0]["FK_YouthGroup"].ToString() != AuthUser.YouthGroup)
            {
                AlertJS("此团员非本支部，无法查看其信息！");
                return;
            }
            BindingReport(mmTable.Rows[0]["OID"].ToString());
        }
    }

    /// <summary>
    /// 绑定报表
    /// </summary>
    /// <param name="MM_OID"></param>
    private void BindingReport(string MM_OID)
    {
        YouthMemberReport rpt = new YouthMemberReport();
        rpt.SetReportParameter(MM_OID);

        ReportViewerControl1.ReportViewer.Report = rpt;
    }
}
