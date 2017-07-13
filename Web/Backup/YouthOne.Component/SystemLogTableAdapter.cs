using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace YouthOne.Component.YouthOneDSTableAdapters
{
    public partial class SystemLogTableAdapter
    {
        private SqlCommand _MySelectSqlComand;
        private SqlCommand MySelectSqlComand
        {
            get
            {
                if (_MySelectSqlComand == null)
                {
                    _MySelectSqlComand = new SqlCommand();
                    _MySelectSqlComand.Connection = this.Connection;
                    _MySelectSqlComand.CommandText = "SearchSystemLog";
                    _MySelectSqlComand.CommandType = System.Data.CommandType.StoredProcedure;
                    _MySelectSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LogType", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LogType", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                    _MySelectSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@TimeScope", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "TimeScope", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                }

                return _MySelectSqlComand;
            }
        }

        /// <summary>
        /// 查询到指定条件的日志信息
        /// </summary>
        /// <returns></returns>
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Select, true)]
        public virtual YouthOneDS.SystemLogDataTable GetData(string logType, string timeScope)
        {
            YouthOneDS.SystemLogDataTable dataTable = new YouthOneDS.SystemLogDataTable();

            if (String.IsNullOrEmpty(logType))
            {
                logType = "所有日志";
            }

            if (String.IsNullOrEmpty(timeScope))
            {
                timeScope = "当天";
            }

            this.Adapter.SelectCommand = MySelectSqlComand;
            this.Adapter.SelectCommand.Parameters[0].Value = logType;
            this.Adapter.SelectCommand.Parameters[1].Value = timeScope;
            this.Adapter.Fill(dataTable);

            return dataTable;
        }
    }
}
