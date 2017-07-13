using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using YouthOne.Component.YouthOneDSTableAdapters;
using YouthOne.Component;
using System.Web;
using System.Web.Security;

/// <summary>
/// 授权用户信息
/// </summary>
namespace YouthOne.Component
{
    public class AuthenUserType
    {
        /// <summary>
        /// 系统管理员
        /// </summary>
        public const string Admin = "Admin";
        /// <summary>
        /// 团委管理员1
        /// </summary>
        public const string TW_Admin = "TW_Admin";
        /// <summary>
        /// 团委管理员2
        /// </summary>
        public const string TW_Browser = "TW_Browser";
        /// <summary>
        /// 团委财务管理员
        /// </summary>
        public const string TW_Finance = "TW_Finance";
        /// <summary>
        /// 团支部管理员
        /// </summary>
        public const string TZB_Admin = "TZB_Admin";
    }

    public class AuthenUser
    {
        public string UserName;
        public bool IsSystemAdmin;
        public string UserID;
        public string LoginName;
        public string PassWord;
        public string YouthGroup;
        public string RoleName;

        public static Hashtable OnlineAuthenUserList = new Hashtable();
        private static SystemAdminTableAdapter adminAdapter = new SystemAdminTableAdapter();

        /// <summary>
        /// 获取到存储票据的Cookie
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="isPersistent"></param>
        /// <returns></returns>
        public static HttpCookie GetTicketCookie(string loginName, bool isPersistent)
        {
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(1, loginName, DateTime.Now, DateTime.Now.AddMinutes(10), isPersistent, "");
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            if (isPersistent)
            {
                authCookie.Expires = authTicket.Expiration;
            }

            return authCookie;
        }

        /// <summary>
        /// 用户到授权用户的装换
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public static AuthenUser TransformToAuthenUser(YouthOneDS.SystemAdminRow admin)
        {
            if (admin == null) return null;

            AuthenUser authenUser = new AuthenUser();

            authenUser.IsSystemAdmin = admin.LOG_NAME == "admin";
            authenUser.UserID = admin.OID;
            authenUser.UserName = admin.LOG_NAME;
            authenUser.LoginName = admin.LOG_NAME;
            authenUser.PassWord = admin.PAS_WORD;
            authenUser.YouthGroup = admin.YG_OID;
            authenUser.RoleName = admin.ROL_NAME;

            return authenUser;
        }

        /// <summary>
        /// 根据登陆帐号实例化授权用户
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public static AuthenUser GetAuthenUserByLoginName(string loginName)
        {
            loginName = loginName.ToUpper();

            if (AuthenUser.OnlineAuthenUserList.ContainsKey(loginName))
                return AuthenUser.OnlineAuthenUserList[loginName] as AuthenUser;

            YouthOneDS.SystemAdminRow admin = adminAdapter.GetData().SingleOrDefault<YouthOneDS.SystemAdminRow>(x => x.LOG_NAME.ToUpper() == loginName);
            
            
            return TransformToAuthenUser(admin);
        }

        /// <summary>
        /// 将授权用户加入在线列表
        /// </summary>
        /// <param name="authenUser"></param>
        /// <returns></returns>
        public static AuthenUser PushAuthenUserOnline(AuthenUser authenUser)
        {
            string loginName = authenUser.LoginName.ToUpper();

            if (AuthenUser.OnlineAuthenUserList.ContainsKey(loginName))
                return AuthenUser.OnlineAuthenUserList[loginName] as AuthenUser;

            AuthenUser.OnlineAuthenUserList.Add(loginName, authenUser);
            return authenUser;
        }

        /// <summary>
        /// 将授权用户加入在线列表
        /// </summary>
        /// <param name="authenUser"></param>
        /// <returns></returns>
        public static void RemoveAuthenUserOnline(string loginName)
        {
            loginName = loginName.ToUpper();

            if (AuthenUser.OnlineAuthenUserList.ContainsKey(loginName))
                AuthenUser.OnlineAuthenUserList.Remove(loginName);
        }

        /// <summary>
        /// 验证用户的口令
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public static AuthenUser CheckUserPasswrod(string userName, string passWord)
        {
            AuthenUser admin = GetAuthenUserByLoginName(userName);
            if (admin != null && admin.PassWord == AdapterHelper.Hash(passWord))
            {
                return PushAuthenUserOnline(admin);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据Cookies中的用户名自动登录
        /// </summary>
        public static AuthenUser AutoLogin(string userName)
        {
            AuthenUser admin = GetAuthenUserByLoginName(userName);

            return PushAuthenUserOnline(admin);
        }

        /// <summary>
        /// 获取到当前用户
        /// </summary>
        /// <returns></returns>
        public static AuthenUser GetCurrentUser()
        {
            HttpCookie authCookies = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookies == null || String.IsNullOrEmpty(authCookies.Value))
            {
                return null;
            }
            else
            {
                FormsAuthenticationTicket authenTicket = FormsAuthentication.Decrypt(authCookies.Value);
                return AuthenUser.AutoLogin(authenTicket.Name);
            }
        }

        /// <summary>
        /// 更改登录密码
        /// </summary>
        /// <param name="newPassword"></param>
        public static void ChangePassword(string newPassword)
        {
            newPassword = AdapterHelper.Hash(newPassword);
            adminAdapter.ChangePassword(GetCurrentUser().UserID, newPassword);
            GetCurrentUser().PassWord = newPassword;
        }
    }

}
