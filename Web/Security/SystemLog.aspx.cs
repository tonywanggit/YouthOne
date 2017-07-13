using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Security_SystemLog : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        InitPage();
    }

    protected void InitPage()
    {
        HideSourceCodeTable();
    }

    protected void cmdSearch_Click(object sender, EventArgs e)
    {
        grid.Selection.UnselectAll();
        grid.PageIndex = 0;
        grid.DataBind();
    }

    protected void cmdExport_Click(object sender, EventArgs e)
    {
        gridExport.WriteXlsToResponse(true);
    }
}
