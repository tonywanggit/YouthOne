using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using YouthOne.Component.YouthOneDSTableAdapters;

namespace YouthOne.Component
{
    public partial class MemberStaticReport : DevExpress.XtraReports.UI.XtraReport
    {
        MemberStaticTableAdapter msAdapter = new MemberStaticTableAdapter();

        public MemberStaticReport()
        {
            InitializeComponent();
        }


        public void SetReportParameter(int Year, int Month, string MyAdmin)
        {
            msAdapter.Fill(youthOneDS1.MemberStatic, Year, Month, MyAdmin);
        }
    }
}
