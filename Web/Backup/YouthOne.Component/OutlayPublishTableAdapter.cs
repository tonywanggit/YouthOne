using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace YouthOne.Component.YouthOneDSTableAdapters
{
    public partial class OutlayPublishTableAdapter
    {
        #region 获取到年度金费下发情况
        private SqlCommand _MySelectSqlComand;
        private SqlCommand MySelectSqlComand
        {
            get
            {
                if (_MySelectSqlComand == null)
                {
                    _MySelectSqlComand = new SqlCommand();
                    _MySelectSqlComand.Connection = this.Connection;
                    _MySelectSqlComand.CommandText = "GetOutlayPublish";
                    _MySelectSqlComand.CommandType = System.Data.CommandType.StoredProcedure;
                    _MySelectSqlComand.Parameters.Add(new SqlParameter("YG_OID", System.Data.SqlDbType.NVarChar, 50));
                }

                return _MySelectSqlComand;
            }
        }

        /// <summary>
        /// 获取到年度金费下发情况 包含公司团委
        /// </summary>
        /// <returns></returns>
        public YouthOneDS.OutlayPublishDataTable MySelect(string YG_OID)
        {
            YouthOneDS.OutlayPublishDataTable dataTable = new YouthOneDS.OutlayPublishDataTable();
            this.Adapter.SelectCommand = MySelectSqlComand;
            this.Adapter.SelectCommand.Parameters[0].Value = YG_OID;
            this.Adapter.Fill(dataTable);

            return dataTable;
        }

        /// <summary>
        /// 获取到年度金费下发情况 不包含公司团委
        /// </summary>
        /// <param name="YG_OID"></param>
        /// <returns></returns>
        public YouthOneDS.OutlayPublishDataTable MySelectNoGSTW(string YG_OID)
        {
            YouthOneDS.OutlayPublishDataTable dataTable = MySelect(YG_OID);

            YouthOne.Component.YouthOneDS.OutlayPublishRow row = dataTable.SingleOrDefault(x => x.YG_OID == "GSTW");
            if (dataTable != null && row != null)
                dataTable.Rows.Remove(row);

            return dataTable;
        }

        #endregion

        #region 更新支部的年度金费
        private SqlCommand _MyUpdateCommand;
        private SqlCommand MyUpdateCommand
        {
            get
            {
                if (_MyUpdateCommand == null)
                {
                    _MyUpdateCommand = new SqlCommand();
                    _MyUpdateCommand.Connection = this.Connection;
                    _MyUpdateCommand.CommandText = "UPDATE dbo.OutlayPublish SET OP_ADMIN = @OP_ADMIN, OL_NUM = @OL_NUM WHERE OID = @OID";
                    _MyUpdateCommand.CommandType = System.Data.CommandType.Text;
                    _MyUpdateCommand.Parameters.Add(new SqlParameter("OID", System.Data.SqlDbType.NVarChar, 50));
                    _MyUpdateCommand.Parameters.Add(new SqlParameter("OP_ADMIN", System.Data.SqlDbType.NVarChar, 50));
                    _MyUpdateCommand.Parameters.Add(new SqlParameter("OL_NUM", System.Data.SqlDbType.Decimal));
                }

                return _MyUpdateCommand;
            }
        }

        /// <summary>
        /// 更新支部的年度金费
        /// </summary>
        /// <param name="OID"></param>
        /// <param name="OP_ADMIN"></param>
        /// <param name="OL_NUM"></param>
        /// <returns></returns>
        public int MyUpdate(string YG_NAME, decimal OL_NUM, string OID)
        {
            MyUpdateCommand.Parameters[0].Value = OID;
            MyUpdateCommand.Parameters[1].Value = AuthenUser.GetCurrentUser().UserID;
            MyUpdateCommand.Parameters[2].Value = OL_NUM;

            return AdapterHelper.ExecuteCommand(MyUpdateCommand);
        }
        #endregion


        #region 发放单个支部的经费
        private SqlCommand _PublishOneCommand;
        private SqlCommand PublishOneCommand
        {
            get
            {
                if (_PublishOneCommand == null)
                {
                    _PublishOneCommand = new SqlCommand();
                    _PublishOneCommand.Connection = this.Connection;
                    _PublishOneCommand.CommandText = "OutlayPublishOne";
                    _PublishOneCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    _PublishOneCommand.Parameters.Add(new SqlParameter("OL_OID", System.Data.SqlDbType.NVarChar, 50));
                    _PublishOneCommand.Parameters.Add(new SqlParameter("OP_ADMIN", System.Data.SqlDbType.NVarChar, 50));
                }

                return _PublishOneCommand;
            }
        }

        /// <summary>
        /// 发放单个支部的经费
        /// </summary>
        /// <param name="OID"></param>
        /// <param name="OP_ADMIN"></param>
        /// <param name="OL_NUM"></param>
        /// <returns></returns>
        public int PublishOne(string OL_OID)
        {
            PublishOneCommand.Parameters[0].Value = OL_OID;
            PublishOneCommand.Parameters[1].Value = AuthenUser.GetCurrentUser().UserID;

            return AdapterHelper.ExecuteCommand(PublishOneCommand);
        }
        #endregion

        #region 发放所有支部的经费
        private SqlCommand _PublishAllCommand;
        private SqlCommand PublishAllCommand
        {
            get
            {
                if (_PublishAllCommand == null)
                {
                    _PublishAllCommand = new SqlCommand();
                    _PublishAllCommand.Connection = this.Connection;
                    _PublishAllCommand.CommandText = "OutlayPublishAll";
                    _PublishAllCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    _PublishAllCommand.Parameters.Add(new SqlParameter("OP_ADMIN", System.Data.SqlDbType.NVarChar, 50));
                }

                return _PublishAllCommand;
            }
        }

        /// <summary>
        /// 发放单个支部的经费
        /// </summary>
        /// <param name="OID"></param>
        /// <param name="OP_ADMIN"></param>
        /// <param name="OL_NUM"></param>
        /// <returns></returns>
        public int PublishAll()
        {
            PublishAllCommand.Parameters[0].Value = AuthenUser.GetCurrentUser().UserID;

            return AdapterHelper.ExecuteCommand(PublishAllCommand);
        }
        #endregion

        #region 记录经费结余备注
        private SqlCommand _OutlayRemarkCommand;
        private SqlCommand OutlayRemarkCommand
        {
            get
            {
                if (_OutlayRemarkCommand == null)
                {
                    _OutlayRemarkCommand = new SqlCommand();
                    _OutlayRemarkCommand.Connection = this.Connection;
                    _OutlayRemarkCommand.CommandText = "UPDATE OutlayPublish SET OL_DESC = @OL_DESC WHERE OID = @OID";
                    _OutlayRemarkCommand.CommandType = System.Data.CommandType.Text;
                    _OutlayRemarkCommand.Parameters.Add(new SqlParameter("OID", System.Data.SqlDbType.NVarChar, 50));
                    _OutlayRemarkCommand.Parameters.Add(new SqlParameter("OL_DESC", System.Data.SqlDbType.NVarChar, 50));
                }

                return _OutlayRemarkCommand;
            }
        }

        /// <summary>
        /// 发放单个支部的经费
        /// </summary>
        /// <param name="OID"></param>
        /// <param name="OP_ADMIN"></param>
        /// <param name="OL_NUM"></param>
        /// <returns></returns>
        public int OutlayRemark(string YG_NAME, decimal OL_SL, decimal OL_ZC, decimal OL_LEFT, string OL_DESC, string OID)
        {
            OutlayRemarkCommand.Parameters[0].Value = OID;
            OutlayRemarkCommand.Parameters[1].Value = OL_DESC;

            return AdapterHelper.ExecuteCommand(OutlayRemarkCommand);
        }
        #endregion
    }
}
