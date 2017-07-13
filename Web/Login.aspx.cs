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
    /// �˷������ڵ���
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

                //--��¼��¼����������Cookie��һ����
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
                errorLabel.Text = "��¼ʧ�ܣ������û��������룡";
            }
        }
        catch (System.Exception ex)
        {
            errorLabel.Text = "��¼ʧ��: " + ex.Message;
        }
    }

    /// <summary>
    /// ��֤�û�����
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
            AdapterHelper.WriteLog(LogType.System, "��¼", authenUser.UserName, authenUser.UserID.ToString(), authenUser.UserName);
            return true;
        }
    }
}
