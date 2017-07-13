using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YouthOne.Component;

public partial class BaseInfo_Post : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        InitRight();
        grid.DataBound +=new EventHandler(grid_DataBound);
    }

    protected void InitRight()
    {
        if (AuthUser.RoleName != AuthenUserType.Admin && AuthUser.RoleName != AuthenUserType.TW_Admin)
        {
            grid.Columns["OP_COL"].Visible = false;
            btnAddAdmin.Enabled = false;
        }
    }

    void grid_DataBound(object sender, EventArgs e)
    {
        this.grid.GroupBy(this.grid.Columns["CAT_NAME"]);
        //this.grid.ExpandAll();
    }
}
