using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace YouthOne.Component.YouthOneDSTableAdapters
{
    public partial class MemberImportTableAdapter
    {
        #region 根据OID获取到团员信息
        private SqlCommand _GetMemberOIDComand;
        private SqlCommand GetMemberOIDComand
        {
            get
            {
                if (_GetMemberOIDComand == null)
                {
                    _GetMemberOIDComand = new SqlCommand();
                    _GetMemberOIDComand.Connection = this.Connection;
                    _GetMemberOIDComand.CommandText = "SELECT * FROM dbo.MemberImport WHERE OID = @OID";
                    _GetMemberOIDComand.CommandType = System.Data.CommandType.Text;
                    _GetMemberOIDComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OID", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                }

                return _GetMemberOIDComand;
            }
        }


        /// <summary>
        /// 根据OID获取到团员信息
        /// </summary>
        /// <param name="HrCode"></param>
        /// <returns></returns>
        public YouthOneDS.MemberImportDataTable GetMember(string MI_OID)
        {
            YouthOneDS.MemberImportDataTable dataTable = new YouthOneDS.MemberImportDataTable();

            this.Adapter.SelectCommand = GetMemberOIDComand;
            this.Adapter.SelectCommand.Parameters[0].Value = MI_OID;
            this.Adapter.Fill(dataTable);

            return dataTable;
        }

        #endregion


        #region 获取到导入团员信息
        private SqlCommand _MySelectSqlComand;
        private SqlCommand MySelectSqlComand
        {
            get
            {
                if (_MySelectSqlComand == null)
                {
                    _MySelectSqlComand = new SqlCommand();
                    _MySelectSqlComand.Connection = this.Connection;
                    _MySelectSqlComand.CommandText = "SELECT * FROM dbo.MemberImport ORDER BY ExcelNum";
                    _MySelectSqlComand.CommandType = System.Data.CommandType.Text;
                }

                return _MySelectSqlComand;
            }
        }


        /// <summary>
        /// 根据OID获取到团员信息
        /// </summary>
        /// <param name="HrCode"></param>
        /// <returns></returns>
        public YouthOneDS.MemberImportDataTable MySelect()
        {
            YouthOneDS.MemberImportDataTable dataTable = new YouthOneDS.MemberImportDataTable();

            this.Adapter.SelectCommand = MySelectSqlComand;
            this.Adapter.Fill(dataTable);

            return dataTable;
        }

        #endregion

        #region 删除导入数据
        private SqlCommand _MyDeleteSqlComand;
        private SqlCommand MyDeleteSqlComand
        {
            get
            {
                if (_MyDeleteSqlComand == null)
                {
                    _MyDeleteSqlComand = new SqlCommand();
                    _MyDeleteSqlComand.Connection = this.Connection;
                    _MyDeleteSqlComand.CommandText = "DELETE FROM [dbo].[MemberImport] WHERE [OID] = @OID";
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


        #region 清空导入数据
        private SqlCommand _TruncateTableComand;
        private SqlCommand TruncateTableComand
        {
            get
            {
                if (_TruncateTableComand == null)
                {
                    _TruncateTableComand = new SqlCommand();
                    _TruncateTableComand.Connection = this.Connection;
                    _TruncateTableComand.CommandText = "TRUNCATE TABLE [dbo].[MemberImport]";
                    _TruncateTableComand.CommandType = System.Data.CommandType.Text;
                }

                return _TruncateTableComand;
            }
        }

        /// <summary>
        /// 清空导入数据
        /// </summary>
        /// <returns></returns>
        public int TruncateTable()
        {
            return AdapterHelper.ExecuteCommand(TruncateTableComand);
        }
        #endregion
    }
}
