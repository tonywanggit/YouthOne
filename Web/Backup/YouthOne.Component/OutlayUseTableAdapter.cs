using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace YouthOne.Component.YouthOneDSTableAdapters
{
    public partial class OutlayUseTableAdapter
    {
        #region 获取到年度金费使用情况
        private SqlCommand _MySelectSqlComand;
        private SqlCommand MySelectSqlComand
        {
            get
            {
                if (_MySelectSqlComand == null)
                {
                    _MySelectSqlComand = new SqlCommand();
                    _MySelectSqlComand.Connection = this.Connection;
                    _MySelectSqlComand.CommandText = "GetOutlayUse";
                    _MySelectSqlComand.CommandType = System.Data.CommandType.StoredProcedure;
                    _MySelectSqlComand.Parameters.Add(new SqlParameter("YG_OID", System.Data.SqlDbType.NVarChar, 50));
                }

                return _MySelectSqlComand;
            }
        }

        /// <summary>
        /// 获取到年度金费使用情况
        /// </summary>
        /// <returns></returns>
        public YouthOneDS.OutlayUseDataTable MySelect(string YG_OID)
        {
            YouthOneDS.OutlayUseDataTable dataTable = new YouthOneDS.OutlayUseDataTable();
            this.Adapter.SelectCommand = MySelectSqlComand;
            this.Adapter.SelectCommand.Parameters[0].Value = (YG_OID == null ? "GSWT" : YG_OID);
            this.Adapter.Fill(dataTable);

            return dataTable;
        }

        #endregion

        #region 记录经费使用明细
        private SqlCommand _NewOutlayUseComand;
        private SqlCommand NewOutlayUseComand
        {
            get
            {
                if (_NewOutlayUseComand == null)
                {
                    _NewOutlayUseComand = new SqlCommand();
                    _NewOutlayUseComand.Connection = this.Connection;
                    _NewOutlayUseComand.CommandText = "NewOutlayUse";
                    _NewOutlayUseComand.CommandType = System.Data.CommandType.StoredProcedure;
                    _NewOutlayUseComand.Parameters.Add(new SqlParameter("YG_OID", System.Data.SqlDbType.NVarChar, 50));
                    _NewOutlayUseComand.Parameters.Add(new SqlParameter("OU_TYPE", System.Data.SqlDbType.NVarChar, 10));
                    _NewOutlayUseComand.Parameters.Add(new SqlParameter("OU_NUM", System.Data.SqlDbType.Decimal));
                    _NewOutlayUseComand.Parameters.Add(new SqlParameter("OU_DESC", System.Data.SqlDbType.NVarChar, 50));
                    _NewOutlayUseComand.Parameters.Add(new SqlParameter("OU_ADMIN", System.Data.SqlDbType.NVarChar, 50));
                }

                return _NewOutlayUseComand;
            }
        }

        /// <summary>
        /// 记录经费使用明细
        /// </summary>
        /// <returns></returns>
        public int NewOutLayUse(string YG_OID, string OU_TYPE, decimal OU_NUM, string OU_DESC, string OU_ADMIN)
        {
            NewOutlayUseComand.Parameters[0].Value = YG_OID;
            NewOutlayUseComand.Parameters[1].Value = OU_TYPE;
            NewOutlayUseComand.Parameters[2].Value = OU_NUM;
            NewOutlayUseComand.Parameters[3].Value = OU_DESC;
            NewOutlayUseComand.Parameters[4].Value = OU_ADMIN;

            return AdapterHelper.ExecuteCommand(NewOutlayUseComand);
        }

        #endregion

        #region 删除经费使用明细
        private SqlCommand _MyDeleteCommand;
        private SqlCommand MyDeleteCommand
        {
            get
            {
                if (_MyDeleteCommand == null)
                {
                    _MyDeleteCommand = new SqlCommand();
                    _MyDeleteCommand.Connection = this.Connection;
                    _MyDeleteCommand.CommandText = "DeleteOutlayUse";
                    _MyDeleteCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    _MyDeleteCommand.Parameters.Add(new SqlParameter("OID", System.Data.SqlDbType.NVarChar, 50));
                }

                return _MyDeleteCommand;
            }
        }

        /// <summary>
        /// 删除经费使用明细
        /// </summary>
        /// <returns></returns>
        public int DeleteOutlayUse(string OID)
        {
            MyDeleteCommand.Parameters[0].Value = OID;

            return AdapterHelper.ExecuteCommand(MyDeleteCommand);
        }

        #endregion

        #region 修改经费使用明细
        private SqlCommand _MyUpdateCommand;
        private SqlCommand MyUpdateCommand
        {
            get
            {
                if (_MyUpdateCommand == null)
                {
                    _MyUpdateCommand = new SqlCommand();
                    _MyUpdateCommand.Connection = this.Connection;
                    _MyUpdateCommand.CommandText = "UpdateOutlayUse";
                    _MyUpdateCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    _MyUpdateCommand.Parameters.Add(new SqlParameter("OID", System.Data.SqlDbType.NVarChar, 50));
                    _MyUpdateCommand.Parameters.Add(new SqlParameter("OU_SL", System.Data.SqlDbType.Decimal));
                    _MyUpdateCommand.Parameters.Add(new SqlParameter("OU_ZC", System.Data.SqlDbType.Decimal));
                }

                return _MyUpdateCommand;
            }
        }

        /// <summary>
        /// 修改经费使用明细
        /// </summary>
        /// <returns></returns>
        public int MyUpdate(string YG_NAME, decimal OU_JY, decimal OU_SL, decimal OU_ZC, string OU_DESC, DateTime OU_DATE, string OID)
        {
            MyUpdateCommand.Parameters[0].Value = OID;
            MyUpdateCommand.Parameters[1].Value = OU_SL;
            MyUpdateCommand.Parameters[2].Value = OU_ZC;

            return AdapterHelper.ExecuteCommand(MyUpdateCommand);
        }

        #endregion
    }
}
