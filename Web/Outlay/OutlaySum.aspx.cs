using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YouthOne.Component;

public partial class Outlay_OutlaySum : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["OutlaySum_YG_OID"] = AuthUser.YouthGroup;
        InitRight();
    }

    private void InitRight()
    {
        if (AuthUser.RoleName != AuthenUserType.TW_Finance && AuthUser.RoleName != AuthenUserType.Admin)
            grid.Columns["OP_COL"].Visible = false;
    }
}
