using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace YouthOne.Component
{
    public partial class TuanFeiReport : DevExpress.XtraReports.UI.XtraReport
    {
        public TuanFeiReport()
        {
            InitializeComponent();
        }

        public void SetReportParameter(string ygName, string mcCode, int bj1, int bj2, int bj3)
        {
            youthOneDS1.Clear();
            memberChargeTableAdapter.Fill(youthOneDS1.MemberCharge, ygName, mcCode, bj1, bj2, bj3); 
        }
    }
}
