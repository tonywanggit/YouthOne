using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Collections.Generic;
using YouthOne.Component;

public partial class LoginPage : System.Web.UI.Page{

    private HttpCookie authCookie;

	protected void Page_Load(object sender, EventArgs e) {
        if (!Page.IsPostBack && !Page.IsCallback)
        {
            if (Request.Cookies["YouthOneLoginName"] != null && !String.IsNullOrEmpty(Request.Cookies["YouthOneLoginName"].Value))
            {
                txtUsername.Text = HttpUtility.UrlDecode(Request.Cookies["YouthOneLoginName"].Value);
            }
        }

	}

    /// <summary>
    /// 此方法用于调试
    /// </summary>
    protected void AutoLogin()
    {
        string loginName = "admin";
        AuthenUser.PushAuthenUserOnline(AuthenUser.GetAuthenUserByLoginName(loginName));

        authCookie = AuthenUser.GetTicketCookie(loginName, chkPersist.Checked);
        Response.Cookies.Add(authCookie);
        Response.Redirect(FormsAuthentication.GetRedirectUrl(txtUsername.Text, false));
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (AuthenUserToken(txtUsername.Text, txtPassword.Text))
            {
                authCookie = AuthenUser.GetTicketCookie(txtUsername.Text, chkPersist.Checked);
                Response.Cookies.Add(authCookie);

                //--记录登录名，保存在Cookie中一个月
                HttpCookie loginCookie = new HttpCookie("YouthOneLoginName", HttpUtility.UrlEncode(txtUsername.Text));
                loginCookie.Expires = DateTime.Now.AddMonths(1);
                Response.Cookies.Add(loginCookie);

                String redirectUrl = FormsAuthentication.GetRedirectUrl(txtUsername.Text, false);
                if (redirectUrl.EndsWith("Logout.aspx"))
                    redirectUrl = "~/Default.aspx";

                Response.Redirect(redirectUrl);
            }
            else
            {
                errorLabel.Text = "登录失败，请检查用户名和密码！";
            }
        }
        catch (System.Exception ex)
        {
            errorLabel.Text = "登录失败: " + ex.Message;
        }
    }

    /// <summary>
    /// 验证用户口令
    /// </summary>
    /// <param name="userName"></param>
    private Boolean AuthenUserToken(string userName, string passWord)
    {
        AuthenUser authenUser = AuthenUser.CheckUserPasswrod(userName, passWord);

        if (null == authenUser)
        {
            return false;
        }
        else
        {
            AdapterHelper.WriteLog(LogType.System, "登录", authenUser.UserName, authenUser.UserID.ToString(), authenUser.UserName);
            return true;
        }
    }
}
