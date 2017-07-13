using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace YouthOne.Component.YouthOneDSTableAdapters
{
    public partial class PriseTableAdapter
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
                    _MySelectSqlComand.CommandText = "SELECT * FROM dbo.Prise WHERE FK_Member = @OID ORDER BY PR_DATE";
                    _MySelectSqlComand.CommandType = System.Data.CommandType.Text;
                    _MySelectSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OID", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                }

                return _MySelectSqlComand;
            }
        }

        public int Fill(YouthOneDS.PriseDataTable dataTable, string memberOID)
        {
            this.Adapter.SelectCommand = MySelectSqlComand;
            this.Adapter.SelectCommand.Parameters[0].Value = memberOID;

            if ((this.ClearBeforeFill == true))
            {
                dataTable.Clear();
            }
            int returnValue = this.Adapter.Fill(dataTable);

            if (returnValue < 7)
            {
                for (int i = 7; i > returnValue; i--)
                {
                    dataTable.AddPriseRow(Guid.NewGuid().ToString(), memberOID, "", "", new DateTime(1900, 1, 1), 0, "");
                }
            }

            dataTable.Rows[1]["EX_TEXT"] = "获";
            dataTable.Rows[2]["EX_TEXT"] = "奖";
            dataTable.Rows[3]["EX_TEXT"] = "情";
            dataTable.Rows[4]["EX_TEXT"] = "况";
            dataTable.Rows[dataTable.Rows.Count - 1]["EX_TEXT"] = "E";

            return returnValue;
        }

        public YouthOneDS.PriseDataTable GetData(string memberOID)
        {
            memberOID = memberOID ?? string.Empty;

            YouthOneDS.PriseDataTable dataTable = new YouthOneDS.PriseDataTable();

            this.Adapter.SelectCommand = MySelectSqlComand;
            this.Adapter.SelectCommand.Parameters[0].Value = memberOID;
            this.Adapter.Fill(dataTable);

            return dataTable;
        }

        public int MyInsert(string PR_NAME, decimal PR_VALUE, string PR_UNIT, DateTime PR_DATE, string FK_Member)
        {
            return Insert(Guid.NewGuid().ToString(), FK_Member, PR_NAME, PR_UNIT, PR_DATE, PR_VALUE);
        }


        #region 修改获奖情况

        private SqlCommand _MyUpdateSqlComand;
        private SqlCommand MyUpdateSqlComand
        {
            get
            {
                if (_MyUpdateSqlComand == null)
                {
                    _MyUpdateSqlComand = new SqlCommand();
                    _MyUpdateSqlComand.Connection = this.Connection;
                    _MyUpdateSqlComand.CommandText = "UPDATE dbo.Prise SET PR_DATE=@PR_DATE, PR_NAME=@PR_NAME, PR_UNIT=@PR_UNIT, PR_VALUE=@PR_VALUE WHERE OID = @OID";
                    _MyUpdateSqlComand.CommandType = System.Data.CommandType.Text;
                    _MyUpdateSqlComand.Parameters.Add(new SqlParameter("OID", System.Data.SqlDbType.NVarChar, 50));
                    _MyUpdateSqlComand.Parameters.Add(new SqlParameter("PR_VALUE", System.Data.SqlDbType.Decimal));
                    _MyUpdateSqlComand.Parameters.Add(new SqlParameter("PR_NAME", System.Data.SqlDbType.NVarChar));
                    _MyUpdateSqlComand.Parameters.Add(new SqlParameter("PR_UNIT", System.Data.SqlDbType.NVarChar));
                    _MyUpdateSqlComand.Parameters.Add(new SqlParameter("PR_DATE", System.Data.SqlDbType.DateTime));
                }

                return _MyUpdateSqlComand;
            }
        }

        /// <summary>
        /// 修改获奖情况
        /// </summary>
        /// <param name="YG_OID"></param>
        /// <param name="BA_NUM"></param>
        /// <param name="BA_DESC"></param>
        /// <param name="BA_STATUS"></param>
        /// <param name="OID"></param>
        /// <returns></returns>
        public int MyUpdate(string PR_NAME, decimal PR_VALUE, string PR_UNIT, DateTime PR_DATE, string OID)
        {
            MyUpdateSqlComand.Parameters[0].Value = OID;
            MyUpdateSqlComand.Parameters[1].Value = PR_VALUE;
            MyUpdateSqlComand.Parameters[2].Value = PR_NAME;
            MyUpdateSqlComand.Parameters[3].Value = PR_UNIT;
            MyUpdateSqlComand.Parameters[4].Value = PR_DATE;

            return AdapterHelper.ExecuteCommand(MyUpdateSqlComand);
        }
        #endregion

        #region 删除获奖记录
        private SqlCommand _MyDeleteSqlComand;
        private SqlCommand MyDeleteSqlComand
        {
            get
            {
                if (_MyDeleteSqlComand == null)
                {
                    _MyDeleteSqlComand = new SqlCommand();
                    _MyDeleteSqlComand.Connection = this.Connection;
                    _MyDeleteSqlComand.CommandText = "DELETE FROM [dbo].[Prise] WHERE [OID] = @OID";
                    _MyDeleteSqlComand.CommandType = System.Data.CommandType.Text;
                    _MyDeleteSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OID", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                }

                return _MyDeleteSqlComand;
            }
        }

        public int MyDelete(string OID)
        {
            if ((OID == null))
            {
                throw new global::System.ArgumentNullException("OID");
            }
            else
            {
                MyDeleteSqlComand.Parameters[0].Value = OID;
            }

            return AdapterHelper.ExecuteCommand(MyDeleteSqlComand);
        }
        #endregion

    }
}
