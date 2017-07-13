using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Web;

namespace YouthOne.Component.YouthOneDSTableAdapters
{
    public partial class NotificationTableAdapter
    {
        public const string NF_CACHE_KEY = "Notification_CACHE";
        /// <summary>
        /// 清除通知缓存
        /// </summary>
        public static void ClearNfCache()
        {
            if (HttpContext.Current.Cache[NF_CACHE_KEY] != null)
                HttpContext.Current.Cache.Remove(NF_CACHE_KEY);
        }

        #region 创建并查出通知
        private SqlCommand _MySelectSqlComand;
        private SqlCommand MySelectSqlComand
        {
            get
            {
                if (_MySelectSqlComand == null)
                {
                    _MySelectSqlComand = new SqlCommand();
                    _MySelectSqlComand.Connection = this.Connection;
                    _MySelectSqlComand.CommandText = "CreateNotification";
                    _MySelectSqlComand.CommandType = System.Data.CommandType.StoredProcedure;
                }

                return _MySelectSqlComand;
            }
        }

        public YouthOneDS.NotificationDataTable MySelect()
        {
            YouthOneDS.NotificationDataTable dataTable = new YouthOneDS.NotificationDataTable();
            this.Adapter.SelectCommand = MySelectSqlComand;
            this.Adapter.Fill(dataTable);

            return dataTable;
        }
        #endregion

        #region 对通知进行操作
        private SqlCommand _NotifiationOpSqlComand;
        private SqlCommand NotifiationOpSqlComand
        {
            get
            {
                if (_NotifiationOpSqlComand == null)
                {
                    _NotifiationOpSqlComand = new SqlCommand();
                    _NotifiationOpSqlComand.Connection = this.Connection;
                    _NotifiationOpSqlComand.CommandText = "NotificationOperation";
                    _NotifiationOpSqlComand.CommandType = System.Data.CommandType.StoredProcedure;
                    _NotifiationOpSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@NF_OID", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "NF_OID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                    _NotifiationOpSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@NF_CAT", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "NF_CAT", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                    _NotifiationOpSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OP_ADMIN", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OP_ADMIN", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                    _NotifiationOpSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MM_OID", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MM_OID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                    _NotifiationOpSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@YG_OID", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "YG_OID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                    _NotifiationOpSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@FZ_OID", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "FZ_OID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                    _NotifiationOpSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@DP_NAME", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "DP_NAME", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                    _NotifiationOpSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@KS_NAME", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "KS_NAME", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                    _NotifiationOpSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PS_NAME", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PS_NAME", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                }

                return _NotifiationOpSqlComand;
            }
        }

        /// <summary>
        /// 对通知进行操作
        /// </summary>
        public int NotifiationOp(string NF_OID, string NF_CAT, string OP_ADMIN, string MM_OID
            , string YG_OID, string FZ_OID, string DP_NAME, string KS_NAME, string PS_NAME)
        {
            NotifiationOpSqlComand.Parameters[0].Value = NF_OID;
            NotifiationOpSqlComand.Parameters[1].Value = NF_CAT;
            NotifiationOpSqlComand.Parameters[2].Value = OP_ADMIN;
            NotifiationOpSqlComand.Parameters[3].Value = MM_OID;
            NotifiationOpSqlComand.Parameters[4].Value = YG_OID;
            NotifiationOpSqlComand.Parameters[5].Value = FZ_OID;
            NotifiationOpSqlComand.Parameters[6].Value = DP_NAME;
            NotifiationOpSqlComand.Parameters[7].Value = KS_NAME;
            NotifiationOpSqlComand.Parameters[8].Value = PS_NAME;

            ClearNfCache();

            return AdapterHelper.ExecuteCommand(NotifiationOpSqlComand);
        }
        #endregion

        /// <summary>
        /// 根据团组织OID和通知类别获取到通知信息
        /// </summary>
        /// <param name="YG_OID"></param>
        /// <returns></returns>
        public List<YouthOneDS.NotificationRow> GetNotificationRows(string YG_OID, string CAT_NAME)
        {
            List<YouthOneDS.NotificationRow> nfRows = (List<YouthOneDS.NotificationRow>)HttpContext.Current.Cache["Notification_CACHE"];
            if (nfRows == null)
            {
                nfRows = MySelect().OrderBy(x => x.MM_NAME).ToList();
                HttpContext.Current.Cache.Insert(NF_CACHE_KEY, nfRows, null, DateTime.Now.AddSeconds(60), TimeSpan.Zero);
            }

            if (YG_OID != "GSTW")
                nfRows = nfRows.Where(x => x.NF_YG_OID == YG_OID).ToList();

            if (!String.IsNullOrEmpty(CAT_NAME))
                nfRows = nfRows.Where(x => x.NF_CAT == CAT_NAME ).ToList();

            return nfRows;
        }
    }
}
