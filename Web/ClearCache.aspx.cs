using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YouthOne.Component;

public partial class ClearCache : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CacheUtil.Clear();
    }
}
