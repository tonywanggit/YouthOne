using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace YouthOne.Component.YouthOneDSTableAdapters
{
    public partial class BudgetApplyTableAdapter
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
                    _MySelectSqlComand.CommandText = "SelectBudgetApply";
                    _MySelectSqlComand.CommandType = System.Data.CommandType.StoredProcedure;
                    _MySelectSqlComand.Parameters.Add(new SqlParameter("YG_OID", System.Data.SqlDbType.NVarChar, 50));
                    _MySelectSqlComand.Parameters.Add(new SqlParameter("Month", System.Data.SqlDbType.Int));
                    _MySelectSqlComand.Parameters.Add(new SqlParameter("Status", System.Data.SqlDbType.NVarChar, 50));
                }

                return _MySelectSqlComand;
            }
        }

        /// <summary>
        /// 获取到支部月度预算
        /// </summary>
        /// <returns></returns>
        public YouthOneDS.BudgetApplyDataTable MySelect(string YG_OID, int Month, string Status)
        {
            YouthOneDS.BudgetApplyDataTable dataTable = new YouthOneDS.BudgetApplyDataTable();
            this.Adapter.SelectCommand = MySelectSqlComand;
            this.Adapter.SelectCommand.Parameters[0].Value = (YG_OID == null ? "GSWT" : YG_OID);
            this.Adapter.SelectCommand.Parameters[1].Value = Month;
            this.Adapter.SelectCommand.Parameters[2].Value = Status;
            this.Adapter.Fill(dataTable);

            return dataTable;
        }

        #endregion


        #region 新增支部月度预算
        /// <summary>
        /// 新增支部月度预算
        /// </summary>
        /// <returns></returns>
        public int MyInsert(string YG_OID, DateTime CRE_DATE, decimal BA_NUM, string YJ_MONTH, string BA_DESC)
        {
            return Insert(Guid.NewGuid().ToString(), YG_OID, CRE_DATE, BA_NUM, BA_DESC, "等待审核中",
                AuthenUser.GetCurrentUser().UserID, "", new DateTime(1900, 1, 1), YJ_MONTH, String.Empty);
        }

        #endregion


        #region 修改支部月度预算

        private SqlCommand _MyUpdateSqlComand;
        private SqlCommand MyUpdateSqlComand
        {
            get
            {
                if (_MyUpdateSqlComand == null)
                {
                    _MyUpdateSqlComand = new SqlCommand();
                    _MyUpdateSqlComand.Connection = this.Connection;
                    _MyUpdateSqlComand.CommandText = "UpdateBudgetApply";
                    _MyUpdateSqlComand.CommandType = System.Data.CommandType.StoredProcedure;
                    _MyUpdateSqlComand.Parameters.Add(new SqlParameter("OID", System.Data.SqlDbType.NVarChar, 50));
                    _MyUpdateSqlComand.Parameters.Add(new SqlParameter("BA_NUM", System.Data.SqlDbType.Decimal));
                    _MyUpdateSqlComand.Parameters.Add(new SqlParameter("BA_DESC", System.Data.SqlDbType.NVarChar, 50));
                    _MyUpdateSqlComand.Parameters.Add(new SqlParameter("BA_STATUS", System.Data.SqlDbType.NVarChar, 10));
                    _MyUpdateSqlComand.Parameters.Add(new SqlParameter("ADMIN", System.Data.SqlDbType.NVarChar, 50));
                    _MyUpdateSqlComand.Parameters.Add(new SqlParameter("YJ_MONTH", System.Data.SqlDbType.NVarChar, 5));
                    _MyUpdateSqlComand.Parameters.Add(new SqlParameter("SP_REASON", System.Data.SqlDbType.NVarChar, 500));
                }

                return _MyUpdateSqlComand;
            }
        }

        /// <summary>
        /// 修改支部月度预算
        /// </summary>
        /// <param name="YG_OID"></param>
        /// <param name="BA_NUM"></param>
        /// <param name="BA_DESC"></param>
        /// <param name="BA_STATUS"></param>
        /// <param name="OID"></param>
        /// <returns></returns>
        public int MyUpdate(string YG_OID, DateTime CRE_DATE, decimal BA_NUM, string YJ_MONTH, string BA_DESC, string BA_STATUS, string SP_REASON, string OID)
        {
            string roleName = AuthenUser.GetCurrentUser().RoleName;

            //if (roleName != AuthenUserType.TW_Finance && roleName != AuthenUserType.Admin && roleName != AuthenUserType.TW_Admin
            //    && BA_STATUS != "等待审核中")
            //    return 0;

            MyUpdateSqlComand.Parameters[0].Value = OID;
            MyUpdateSqlComand.Parameters[1].Value = BA_NUM;
            MyUpdateSqlComand.Parameters[2].Value = BA_DESC;
            MyUpdateSqlComand.Parameters[3].Value = BA_STATUS;
            MyUpdateSqlComand.Parameters[4].Value = AuthenUser.GetCurrentUser().UserID;
            MyUpdateSqlComand.Parameters[5].Value = YJ_MONTH;
            MyUpdateSqlComand.Parameters[6].Value = String.IsNullOrEmpty(SP_REASON) ? String.Empty : SP_REASON;

            return AdapterHelper.ExecuteCommand(MyUpdateSqlComand);
        }

        /// <summary>
        /// 修改支部月度预算: 支部管理员使用
        /// </summary>
        /// <param name="YG_OID"></param>
        /// <param name="CRE_DATE"></param>
        /// <param name="BA_NUM"></param>
        /// <param name="YJ_MONTH"></param>
        /// <param name="BA_DESC"></param>
        /// <param name="OID"></param>
        /// <returns></returns>
        public int MyUpdate(string YG_OID, DateTime CRE_DATE, decimal BA_NUM, string YJ_MONTH, string BA_DESC, string OID)
        {
            return MyUpdate(YG_OID, CRE_DATE, BA_NUM, YJ_MONTH, BA_DESC, "等待审核中", String.Empty, OID);
        }
        #endregion

        #region 删除支部月度预算

        private SqlCommand _MyDeleteSqlComand;
        private SqlCommand MyDeleteSqlComand
        {
            get
            {
                if (_MyDeleteSqlComand == null)
                {
                    _MyDeleteSqlComand = new SqlCommand();
                    _MyDeleteSqlComand.Connection = this.Connection;
                    _MyDeleteSqlComand.CommandText = "DELETE dbo.BudgetApply WHERE OID = @OID";
                    _MyDeleteSqlComand.CommandType = System.Data.CommandType.Text;
                    _MyDeleteSqlComand.Parameters.Add(new SqlParameter("OID", System.Data.SqlDbType.NVarChar, 50));
                }

                return _MyDeleteSqlComand;
            }
        }

        /// <summary>
        /// 删除支部月度预算
        /// </summary>
        /// <param name="OID"></param>
        /// <returns></returns>
        public int MyDelete(string OID)
        {
            MyDeleteSqlComand.Parameters[0].Value = OID;

            return AdapterHelper.ExecuteCommand(MyDeleteSqlComand);
        }
        #endregion



        #region 获取到等待审核中的预算申请总数

        private SqlCommand _NoAuditApplySqlComand;
        private SqlCommand NoAuditApplySqlComand
        {
            get
            {
                if (_NoAuditApplySqlComand == null)
                {
                    _NoAuditApplySqlComand = new SqlCommand();
                    _NoAuditApplySqlComand.Connection = this.Connection;
                    _NoAuditApplySqlComand.CommandText = "SELECT COUNT(*) AS NUM FROM BudgetApply WHERE BA_STATUS = '等待审核中'";
                    _NoAuditApplySqlComand.CommandType = System.Data.CommandType.Text;
                }

                return _NoAuditApplySqlComand;
            }
        }

        /// <summary>
        /// 获取到等待审核中的预算申请总数
        /// </summary>
        /// <param name="OID"></param>
        /// <returns></returns>
        public int GetNoAuditApplyNum()
        {
            return AdapterHelper.ExecuteScalar<Int32>(NoAuditApplySqlComand);
        }
        #endregion


        #region 获取到等待审核中的预算申请总数

        private SqlCommand _AuditApplySqlComand;
        private SqlCommand AuditApplySqlComand
        {
            get
            {
                if (_AuditApplySqlComand == null)
                {
                    _AuditApplySqlComand = new SqlCommand();
                    _AuditApplySqlComand.Connection = this.Connection;
                    _AuditApplySqlComand.CommandText = "SELECT COUNT(*) NUM FROM BudgetApplyAudit A INNER JOIN BudgetApply B ON A.BA_OID = B.OID AND B.YG_OID = @YG_OID";
                    _AuditApplySqlComand.CommandType = System.Data.CommandType.Text;
                    _AuditApplySqlComand.Parameters.Add(new SqlParameter("YG_OID", System.Data.SqlDbType.NVarChar, 50));
                }

                return _AuditApplySqlComand;
            }
        }

        /// <summary>
        /// 获取到本支部已审批预算总数
        /// </summary>
        /// <param name="OID"></param>
        /// <returns></returns>
        public int GetAuditApplyNum(string YG_OID)
        {
            AuditApplySqlComand.Parameters[0].Value = YG_OID;
            return AdapterHelper.ExecuteScalar<Int32>(AuditApplySqlComand);
        }
        #endregion


        #region 删除本支部已审批预算通知

        private SqlCommand _DeleteAuditApplySqlComand;
        private SqlCommand DeleteAuditApplySqlComand
        {
            get
            {
                if (_DeleteAuditApplySqlComand == null)
                {
                    _DeleteAuditApplySqlComand = new SqlCommand();
                    _DeleteAuditApplySqlComand.Connection = this.Connection;
                    _DeleteAuditApplySqlComand.CommandText = "DELETE A FROM BudgetApplyAudit A INNER JOIN BudgetApply B ON A.BA_OID = B.OID WHERE B.YG_OID = @YG_OID";
                    _DeleteAuditApplySqlComand.CommandType = System.Data.CommandType.Text;
                    _DeleteAuditApplySqlComand.Parameters.Add(new SqlParameter("YG_OID", System.Data.SqlDbType.NVarChar, 50));
                }

                return _DeleteAuditApplySqlComand;
            }
        }

        /// <summary>
        /// 删除本支部已审批预算通知
        /// </summary>
        /// <param name="OID"></param>
        /// <returns></returns>
        public int DeleteAuditApply(string YG_OID)
        {
            DeleteAuditApplySqlComand.Parameters[0].Value = YG_OID;
            return AdapterHelper.ExecuteCommand(DeleteAuditApplySqlComand);
        }
        #endregion

    }
}
