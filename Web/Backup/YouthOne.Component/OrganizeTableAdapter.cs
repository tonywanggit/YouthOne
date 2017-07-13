using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Web;

namespace YouthOne.Component.YouthOneDSTableAdapters
{
    public partial class OrganizeTableAdapter
    {
        #region 命令成员及属性
        private SqlCommand _MyDeleteSqlComand;
        private SqlCommand MyDeleteSqlComand
        {
            get
            {
                if (_MyDeleteSqlComand == null)
                {
                    _MyDeleteSqlComand = new SqlCommand();
                    _MyDeleteSqlComand.Connection = this.Connection;
                    _MyDeleteSqlComand.CommandText = "DELETE FROM [dbo].[Organize] WHERE [OID] = @OID";
                    _MyDeleteSqlComand.CommandType = System.Data.CommandType.Text;
                    _MyDeleteSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OID", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                }

                return _MyDeleteSqlComand;
            }
        }

        private SqlCommand _WorkGroupSelectSqlComand;
        private SqlCommand WorkGroupSelectSqlComand
        {
            get
            {
                if (_WorkGroupSelectSqlComand == null)
                {
                    _WorkGroupSelectSqlComand = new SqlCommand();
                    _WorkGroupSelectSqlComand.Connection = this.Connection;
                    _WorkGroupSelectSqlComand.CommandText = "SELECT * FROM [dbo].[Organize] WHERE [PARENT_OID] = @PARENT_OID ORDER BY OG_ORDER";
                    _WorkGroupSelectSqlComand.CommandType = System.Data.CommandType.Text;
                    _WorkGroupSelectSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PARENT_OID", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PARENT_OID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                }

                return _WorkGroupSelectSqlComand;
            }
        }

        private SqlCommand _MyInsertSqlComand;
        private SqlCommand MyInsertSqlComand
        {
            get
            {
                if (_MyInsertSqlComand == null)
                {
                    _MyInsertSqlComand = new SqlCommand();
                    _MyInsertSqlComand.Connection = this.Connection;
                    _MyInsertSqlComand.CommandText = "NewOrganize";
                    _MyInsertSqlComand.CommandType = System.Data.CommandType.StoredProcedure;
                    _MyInsertSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OG_NAME", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OG_NAME", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                    _MyInsertSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PARENT_OID", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PARENT_OID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                }

                return _MyInsertSqlComand;
            }
        }

        private SqlCommand _MyUpdateSqlComand;
        private SqlCommand MyUpdateSqlComand
        {
            get
            {
                if (_MyUpdateSqlComand == null)
                {
                    _MyUpdateSqlComand = new SqlCommand();
                    _MyUpdateSqlComand.Connection = this.Connection;
                    _MyUpdateSqlComand.CommandText = "UPDATE [dbo].[Organize] SET OG_NAME = @OG_NAME WHERE [OID] = @OID";
                    _MyUpdateSqlComand.CommandType = System.Data.CommandType.Text;
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OID", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OG_NAME", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OG_NAME", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                }

                return _MyUpdateSqlComand;
            }
        }
        #endregion

        public int MyInsert(string OG_NAME, string PARENT_OID, DateTime CRE_DATE)
        {
            if (OG_NAME == null)
            {
                throw new global::System.ArgumentNullException("OG_NAME");
            }
            else
            {
                MyInsertSqlComand.Parameters[0].Value = OG_NAME;
            }

            if (PARENT_OID == null)
            {
                throw new global::System.ArgumentNullException("PARENT_OID");
            }
            else
            {
                MyInsertSqlComand.Parameters[1].Value = PARENT_OID;
            }

            return AdapterHelper.ExecuteCommand(MyInsertSqlComand);
        }

        public int MyUpdate(string OID, string OG_NAME, DateTime CRE_DATE)
        {
            if (OID == null)
            {
                throw new global::System.ArgumentNullException("OID");
            }
            else
            {
                MyUpdateSqlComand.Parameters[0].Value = OID;
            }

            if (OG_NAME == null)
            {
                throw new global::System.ArgumentNullException("OG_NAME");
            }
            else
            {
                MyUpdateSqlComand.Parameters[1].Value = OG_NAME;
            }

            return AdapterHelper.ExecuteCommand(MyUpdateSqlComand);
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

        public YouthOneDS.OrganizeDataTable GetWorkGroup(string PARENT_OID)
        {
            YouthOneDS.OrganizeDataTable dataTable = new YouthOneDS.OrganizeDataTable();

            if ((PARENT_OID == null))
            {
                throw new global::System.ArgumentNullException("PARENT_OID");
            }
            else
            {
                WorkGroupSelectSqlComand.Parameters[0].Value = PARENT_OID;
            }

            this.Adapter.SelectCommand = WorkGroupSelectSqlComand;
            this.Adapter.SelectCommand.Parameters[0].Value = PARENT_OID;
            this.Adapter.Fill(dataTable);

            return dataTable;
        }

        /// <summary>
        /// 对标准枚举进行缓存
        /// </summary>
        /// <returns></returns>
        public YouthOneDS.OrganizeDataTable GetDataCache()
        {
            YouthOneDS.OrganizeDataTable _DT = (YouthOneDS.OrganizeDataTable)HttpContext.Current.Cache[CacheUtil.CACHE_Organize];

            if (_DT == null)
            {
                _DT = GetData();
                HttpContext.Current.Cache.Insert(CacheUtil.CACHE_Organize, _DT, null, DateTime.Now.AddSeconds(CacheUtil.CONFIG_ExpireSecond), TimeSpan.Zero);
            }

            return _DT;
        }

        /// <summary>
        /// 缓存版：根据部门找到作业区或班组
        /// </summary>
        /// <param name="PARENT_OID"></param>
        /// <returns></returns>
        public List<YouthOneDS.OrganizeRow> GetWorkGroupCache(string deptName)
        {
            string deptID = GetDataCache().SingleOrDefault(x => x.OG_NAME == deptName).OID;

            return GetDataCache().Where(x => x.PARENT_OID == deptID).ToList();
        }

        /// <summary>
        /// 缓存版：获取到部门
        /// </summary>
        /// <returns></returns>
        public List<YouthOneDS.OrganizeRow> GetDeptCache()
        {
            return GetDataCache().Where(x => x.OG_LEVEL == 1).ToList();
        }

        private SqlCommand _GetDataOrderSqlComand;
        private SqlCommand GetDataOrderSqlComand
        {
            get
            {
                if (_GetDataOrderSqlComand == null)
                {
                    _GetDataOrderSqlComand = new SqlCommand();
                    _GetDataOrderSqlComand.Connection = this.Connection;
                    _GetDataOrderSqlComand.CommandText = "SELECT * FROM [dbo].[Organize] ORDER BY OG_ORDER";
                    _GetDataOrderSqlComand.CommandType = System.Data.CommandType.Text;
                }

                return _GetDataOrderSqlComand;
            }
        }

        /// <summary>
        /// 获取到经过排序的组织信息
        /// </summary>
        /// <returns></returns>
        public YouthOneDS.OrganizeDataTable GetDataOrder()
        {
            this.Adapter.SelectCommand = GetDataOrderSqlComand;
            YouthOneDS.OrganizeDataTable dataTable = new YouthOneDS.OrganizeDataTable();
            this.Adapter.Fill(dataTable);
            return dataTable;
        }


        private SqlCommand _MoveNodeSqlComand;
        private SqlCommand MoveNodeSqlComand
        {
            get
            {
                if (_MoveNodeSqlComand == null)
                {
                    _MoveNodeSqlComand = new SqlCommand();
                    _MoveNodeSqlComand.Connection = this.Connection;
                    _MoveNodeSqlComand.CommandText = "ChangeOrganizeOrder";
                    _MoveNodeSqlComand.CommandType = System.Data.CommandType.StoredProcedure;
                    _MoveNodeSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@MOVE_OID", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "MOVE_OID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                    _MoveNodeSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@AFTER_OID", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "AFTER_OID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                    _MoveNodeSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PARENT_OID", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PARENT_OID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                }

                return _MoveNodeSqlComand;
            }
        }


        /// <summary>
        /// 改变组织的顺序
        /// </summary>
        /// <param name="MOVE_OID">需要改变顺序的组织</param>
        /// <param name="AFTER_OID">调整后组织的前一个节点</param>
        /// <param name="PARENT_OID">父节点</param>
        /// <returns></returns>
        public int MoveNode(string MOVE_OID, string AFTER_OID, string PARENT_OID)
        {
            MoveNodeSqlComand.Parameters[0].Value = MOVE_OID;
            MoveNodeSqlComand.Parameters[1].Value = AFTER_OID;
            MoveNodeSqlComand.Parameters[2].Value = PARENT_OID;

            return AdapterHelper.ExecuteCommand(MoveNodeSqlComand);
        }
    }
}
