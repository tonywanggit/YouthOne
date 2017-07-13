using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace YouthOne.Component.YouthOneDSTableAdapters
{
    public partial class MemberChargeTableAdapter
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
                    _MySelectSqlComand.CommandText = "CreateMemberCharge";
                    _MySelectSqlComand.CommandType = System.Data.CommandType.StoredProcedure;
                    _MySelectSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@YG_NAME", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "YG_NAME", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                    _MySelectSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MC_CODE", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MC_CODE", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                    _MySelectSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BJ_1", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BJ_1", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                    _MySelectSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BJ_2", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BJ_2", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                    _MySelectSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@BJ_3", global::System.Data.SqlDbType.Int, 0, global::System.Data.ParameterDirection.Input, 0, 0, "BJ_3", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                }

                return _MySelectSqlComand;
            }
        }

        /// <summary>
        /// 根据支部名称和缴费季度获取支部的缴费数据
        /// </summary>
        /// <param name="dataTable"></param>
        /// <param name="ygName"></param>
        /// <param name="mcCode"></param>
        /// <returns></returns>
        public int Fill(YouthOneDS.MemberChargeDataTable dataTable, string ygName, string mcCode, int bj1, int bj2, int bj3)
        {
            this.Adapter.SelectCommand = MySelectSqlComand;
            this.Adapter.SelectCommand.Parameters[0].Value = ygName;
            this.Adapter.SelectCommand.Parameters[1].Value = mcCode;
            this.Adapter.SelectCommand.Parameters[2].Value = bj1;
            this.Adapter.SelectCommand.Parameters[3].Value = bj2;
            this.Adapter.SelectCommand.Parameters[4].Value = bj3;

            if ((this.ClearBeforeFill == true))
            {
                dataTable.Clear();
            }
            int returnValue = this.Adapter.Fill(dataTable);
            return returnValue;
        }
    }
}
