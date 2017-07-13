using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace YouthOne.Component.YouthOneDSTableAdapters
{
    public partial class MemberStaticTableAdapter
    {
        #region 获取到支部月度预算
        private SqlCommand _MySelectSqlComand;
        private SqlCommand MySelectSqlComand
        {
            get
            {
                if (_MySelectSqlComand == null)
                {
                    _MySelectSqlComand = new SqlCommand();
                    _MySelectSqlComand.Connection = this.Connection;
                    _MySelectSqlComand.CommandText = "CreateMemberStatic";
                    _MySelectSqlComand.CommandType = System.Data.CommandType.StoredProcedure;
                    _MySelectSqlComand.Parameters.Add(new SqlParameter("MS_YEAR", System.Data.SqlDbType.Int));
                    _MySelectSqlComand.Parameters.Add(new SqlParameter("MS_MONTH", System.Data.SqlDbType.Int));
                    _MySelectSqlComand.Parameters.Add(new SqlParameter("MY_ADMIN", System.Data.SqlDbType.NVarChar));
                }

                return _MySelectSqlComand;
            }
        }

        /// <summary>
        /// 获取到支部月度预算
        /// </summary>
        /// <returns></returns>
        public void Fill(YouthOneDS.MemberStaticDataTable dataTable, int Year, int Month, string MyAdmin)
        {
            this.Adapter.SelectCommand = MySelectSqlComand;
            this.Adapter.SelectCommand.Parameters[0].Value = Year;
            this.Adapter.SelectCommand.Parameters[1].Value = Month;
            this.Adapter.SelectCommand.Parameters[2].Value = MyAdmin;
            this.Adapter.Fill(dataTable);

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                dataTable.Rows[i]["MS_NUM"] = i + 1;
            }
        }

        #endregion

        #region 获取团员信息统计表中所有的年度及月份
        private SqlCommand _YearSqlComand;
        private SqlCommand YearSqlComand
        {
            get
            {
                if (_YearSqlComand == null)
                {
                    _YearSqlComand = new SqlCommand();
                    _YearSqlComand.Connection = this.Connection;
                    _YearSqlComand.CommandText = "SELECT DISTINCT MS_YEAR, MS_MONTH FROM dbo.MemberStatic "
                                                   + "UNION "
                                                   + "SELECT YEAR(GETDATE()), MONTH(GETDATE()) "
                                                   + "ORDER BY MS_YEAR DESC, MS_MONTH DESC";
                    _YearSqlComand.CommandType = System.Data.CommandType.Text;
                }

                return _YearSqlComand;
            }
        }

        /// <summary>
        /// 获取团员信息统计表中所有的年度
        /// </summary>
        /// <returns></returns>
        public DataTable GetYearAndMonth()
        {
            DataTable dtYearAndMonth = new DataTable();
            this.Adapter.SelectCommand = YearSqlComand;
            this.Adapter.Fill(dtYearAndMonth);

            return dtYearAndMonth;
        }

        #endregion

        #region 备份团员年度数据
        private SqlCommand _YearBackupSqlComand;
        private SqlCommand YearBackupSqlComand
        {
            get
            {
                if (_YearBackupSqlComand == null)
                {
                    _YearBackupSqlComand = new SqlCommand();
                    _YearBackupSqlComand.Connection = this.Connection;
                    _YearBackupSqlComand.CommandText = "BackupMemberData";
                    _YearBackupSqlComand.CommandType = System.Data.CommandType.StoredProcedure;
                    _YearBackupSqlComand.Parameters.Add(new SqlParameter("MY_ADMIN", SqlDbType.NVarChar));
                }

                return _YearBackupSqlComand;
            }
        }

        /// <summary>
        /// 备份团员数据
        /// </summary>
        /// <returns></returns>
        public int BackupMemberData(string MY_ADMIN)
        {
            YearBackupSqlComand.Parameters[0].Value = MY_ADMIN;
            return AdapterHelper.ExecuteCommand(YearBackupSqlComand);
        }

        #endregion
    }
}
