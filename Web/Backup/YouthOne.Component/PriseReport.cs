using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace YouthOne.Component
{
    public partial class PriseReport : DevExpress.XtraReports.UI.XtraReport
    {
        public PriseReport()
        {
            InitializeComponent();
        }


        public void SetReportParameter(string memberOID)
        {
            youthOneDS1.Clear();
            priseTableAdapter.Fill(youthOneDS1.Prise, memberOID);
        }

        private void xrTableCell2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //e.PrintAction
            DateTime prDate = (DateTime)GetCurrentColumnValue("PR_DATE");
            if(prDate.Year == 1900){
                XRLabel label = (XRLabel)sender;
                label.Text = "";
            }
        }

        private void xrTableCell4_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            string ext = GetCurrentColumnValue("EX_TEXT") as string;
            if (ext != null && ext == "E")
            {
                XRLabel label = (XRLabel)sender;
                label.Text = "";
                label.Borders = DevExpress.XtraPrinting.BorderSide.Bottom | DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right;
                label.StylePriority.UseBorders = true;

            }
        }
    }
}
