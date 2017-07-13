using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YouthOne.Component;

public partial class BaseInfo_StandardEnum : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        InitPage();
        InitRight();

    }

    protected void InitRight()
    {
        if (AuthUser.RoleName != AuthenUserType.Admin && AuthUser.RoleName != AuthenUserType.TW_Admin)
        {
            grid.Columns["OP_COL"].Visible = false;
            btnAddAdmin.Enabled = false;
        }
    }

    protected void InitPage()
    {
        this.grid.DataBound += new EventHandler(grid_DataBound);
    }

    void grid_DataBound(object sender, EventArgs e)
    {
        this.grid.GroupBy(this.grid.Columns["SE_TYPE"]);
    }
}
