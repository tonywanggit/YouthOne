using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YouthOne.Component;
using YouthOne.Component.YouthOneDSTableAdapters;
using System.Data;

public partial class Report_MemberStatic : BasePage
{
    private MemberStaticTableAdapter msAdapter = new MemberStaticTableAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
        BindYear();

        if (!Page.IsPostBack && !Page.IsCallback)
        {
            cmbYear.SelectedIndex = cmbYear.Items.IndexOfText(DateTime.Now.Year.ToString());

            BindMonth();
            cmbMonth.SelectedIndex = cmbMonth.Items.IndexOfText(DateTime.Now.Month.ToString());

            BindReport();
        }

        btnBackup.Click += new EventHandler(btnBackup_Click);
        InitRight();
    }

    private void InitRight()
    {
        if (AuthUser.RoleName != AuthenUserType.Admin && AuthUser.RoleName != AuthenUserType.TW_Admin)
            btnBackup.Enabled = false;
    }

    void btnBackup_Click(object sender, EventArgs e)
    {
        msAdapter.BackupMemberData(AuthUser.UserID);
    }

    protected void cmbYear_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindMonth();
        BindReport();
    }

    protected void BindReport()
    {
        MemberStaticReport rpt = new MemberStaticReport();
        rpt.SetReportParameter(int.Parse(cmbYear.Value.ToString()), int.Parse(cmbMonth.Value.ToString()), AuthUser.LoginName);
        ReportViewerControl1.ReportViewer.Report = rpt;
    }

    protected void BindYear()
    {
        DataTable dtYear = msAdapter.GetYearAndMonth();
        if (dtYear != null && dtYear.Rows.Count > 0)
        {
            for (int i = 0; i < dtYear.Rows.Count; i++)
            {
                string msYear = dtYear.Rows[i]["MS_YEAR"].ToString();
                if(cmbYear.Items.FindByText(msYear) == null)
                    cmbYear.Items.Add(msYear, msYear);
            }
        }
    }

    protected void BindMonth()
    {
        DataTable dtYear = msAdapter.GetYearAndMonth();
        if (dtYear != null && dtYear.Rows.Count > 0)
        {
            for (int i = 0; i < dtYear.Rows.Count; i++)
            {
                string selYear = cmbYear.Value.ToString();
                string msYear = dtYear.Rows[i]["MS_YEAR"].ToString();
                string msMonth = dtYear.Rows[i]["MS_MONTH"].ToString();

                if (selYear == msYear && cmbMonth.Items.FindByText(msMonth) == null)
                    cmbMonth.Items.Add(msMonth, msMonth);
            }
        }
    }
}
