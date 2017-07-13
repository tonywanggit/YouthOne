using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Security.Cryptography;
using YouthOne.Component.YouthOneDSTableAdapters;
using System.Web;

namespace YouthOne.Component
{
    public enum LogType{
        System,
        Bussiness
    }

    /// <summary>
    /// 执行命令
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public class AdapterHelper
    {
        private static SystemLogTableAdapter logAdapter = new SystemLogTableAdapter();

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static int ExecuteCommand(SqlCommand command)
        {
            global::System.Data.ConnectionState previousConnectionState = command.Connection.State;
            if (((command.Connection.State & global::System.Data.ConnectionState.Open)
                        != global::System.Data.ConnectionState.Open))
            {
                command.Connection.Open();
            }
            try
            {
                int returnValue = command.ExecuteNonQuery();
                return returnValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed))
                {
                    command.Connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行命令, 获得第一行第一列的值
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static T ExecuteScalar<T>(SqlCommand command) where T : struct
        {
            global::System.Data.ConnectionState previousConnectionState = command.Connection.State;
            if (((command.Connection.State & global::System.Data.ConnectionState.Open)
                        != global::System.Data.ConnectionState.Open))
            {
                command.Connection.Open();
            }
            try
            {
                object returnValue = command.ExecuteScalar();
                return (T)returnValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed))
                {
                    command.Connection.Close();
                }
            }
        }

        /// <summary>
        /// MD5散列
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String Hash(String str)
        {
            if (String.IsNullOrEmpty(str)) throw new ArgumentNullException("str");

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            Byte[] by = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            return BitConverter.ToString(by).Replace("-", "");
        }

        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="opName"></param>
        /// <param name="opDesc"></param>
        /// <param name="saID"></param>
        /// <param name="saName"></param>
        public static void WriteLog(LogType logType, String opName, String opDesc, String saID, String saName)
        {
            String opType = logType == LogType.System ? "系统日志" : "操作日志";
            logAdapter.Insert(Guid.NewGuid().ToString(), saID, saName, DateTime.Now, opType, opName, opDesc);
        }

        /// <summary>
        /// 用于记录已登录用户的日志
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="opName"></param>
        /// <param name="opDesc"></param>
        public static void WriteLog(LogType logType, String opName, String opDesc)
        {
            AuthenUser admin = AuthenUser.GetCurrentUser();
            if (admin == null)
            {
                AlertLogout();
            }
            else
            {
                WriteLog(logType, opName, opDesc, admin.UserID, admin.LoginName);
            }
        }

        /// <summary>
        /// 提示授权失效
        /// </summary>
        public static void AlertLogout()
        {
            string strValue = "您的授权已经失效，请重新登陆！";
            string strAll = "<SCRIPT lanquage='JScript'>window.alert('" + strValue + "');window.location.href='/YouthOne/Logout.aspx'<" + "/SCRIPT>";

            HttpContext.Current.Response.Write(strAll);
            HttpContext.Current.Response.End();
        }
    }
}
