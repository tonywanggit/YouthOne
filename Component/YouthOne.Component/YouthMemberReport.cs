using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace YouthOne.Component
{
    public partial class YouthMemberReport : DevExpress.XtraReports.UI.XtraReport
    {
        public YouthMemberReport()
        {
            InitializeComponent();
        }

        public void SetReportParameter(string memberOID)
        {
            xrPictureBox1.DataBindings.Add("ImageUrl", this.DataSource, "Member.EmpID", "~/EmpPicture/{0}.jpg");

            youthOneDS1.Clear();
            memberTableAdapter.Fill(youthOneDS1.Member, memberOID);

            (this.RptPrise.ReportSource as PriseReport).SetReportParameter(memberOID);
        }

        private void FstSchool_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string ext = GetCurrentColumnValue("FstSchool") as string;
            if (String.IsNullOrEmpty(ext))
            {
                XRLabel label = (XRLabel)sender;
                label.Text = "";
            }
        }

        private void LstSchool_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string ext = GetCurrentColumnValue("LstSchool") as string;
            if (String.IsNullOrEmpty(ext))
            {
                XRLabel label = (XRLabel)sender;
                label.Text = "";
            }
        }

        /// <summary>
        /// 申请入党日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xrTableCell46_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DateTime ext = (DateTime)GetCurrentColumnValue("ApplyPartyDate");
            if (ext.Year == 1900)
            {
                XRLabel label = (XRLabel)sender;
                label.Text = "";
            }
        }

        /// <summary>
        /// 入党日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xrTableCell27_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DateTime ext = (DateTime)GetCurrentColumnValue("PartyDate");
            if (ext.Year == 1900)
            {
                XRLabel label = (XRLabel)sender;
                label.Text = "";
            }

        }

        /// <summary>
        /// 出生年月
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xrTableCell19_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DateTime ext = (DateTime)GetCurrentColumnValue("Birthday");
            if (ext.Year == 1900)
            {
                XRLabel label = (XRLabel)sender;
                label.Text = "";
            }
        }

        /// <summary>
        /// 参加工作年月
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xrTableCell22_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DateTime ext = (DateTime)GetCurrentColumnValue("JobDateTime");
            if (ext.Year == 1900)
            {
                XRLabel label = (XRLabel)sender;
                label.Text = "";
            }
        }

        /// <summary>
        /// 入厂年月
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xrTableCell33_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            DateTime ext = (DateTime)GetCurrentColumnValue("ComDateTime");
            if (ext.Year == 1900)
            {
                XRLabel label = (XRLabel)sender;
                label.Text = "";
            }
        }

    }
}
