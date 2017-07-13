using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Web;

namespace YouthOne.Component.YouthOneDSTableAdapters
{
    public partial class YouthGroupTableAdapter
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
                    _MyDeleteSqlComand.CommandText = "DELETE FROM [dbo].[YouthGroup] WHERE [OID] = @OID";
                    _MyDeleteSqlComand.CommandType = System.Data.CommandType.Text;
                    _MyDeleteSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OID", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                }

                return _MyDeleteSqlComand;
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
                    _MyInsertSqlComand.CommandText = "NewYouthGroup";
                    _MyInsertSqlComand.CommandType = System.Data.CommandType.StoredProcedure;
                    _MyInsertSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@YG_NAME", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "YG_NAME", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
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
                    _MyUpdateSqlComand.CommandText = "UPDATE [dbo].[YouthGroup] SET YG_NAME = @YG_NAME WHERE [OID] = @OID";
                    _MyUpdateSqlComand.CommandType = System.Data.CommandType.Text;
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OID", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@YG_NAME", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "YG_NAME", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                }

                return _MyUpdateSqlComand;
            }
        }

        private SqlCommand _MySelectZBSqlComand;
        private SqlCommand MySelectZBSqlComand
        {
            get
            {
                if (_MySelectZBSqlComand == null)
                {
                    _MySelectZBSqlComand = new SqlCommand();
                    _MySelectZBSqlComand.Connection = this.Connection;
                    _MySelectZBSqlComand.CommandText = "SELECT * FROM dbo.YouthGroup WHERE YG_LEVEL = 1 OR OID = 'GSTW' ORDER BY YG_LEVEL, YG_ORDER";
                    _MySelectZBSqlComand.CommandType = System.Data.CommandType.Text;
                }

                return _MySelectZBSqlComand;
            }
        }
        #endregion

        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Insert, true)]
        public virtual int MyInsert(string YG_NAME, string PARENT_OID, DateTime CRE_DATE)
        {
            if (YG_NAME == null)
            {
                throw new global::System.ArgumentNullException("YG_NAME");
            }
            else
            {
                MyInsertSqlComand.Parameters[0].Value = YG_NAME;
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

        public virtual int MyUpdate(string OID, string YG_NAME, DateTime CRE_DATE)
        {
            if (OID == null)
            {
                throw new global::System.ArgumentNullException("OID");
            }
            else
            {
                MyUpdateSqlComand.Parameters[0].Value = OID;
            }

            if (YG_NAME == null)
            {
                throw new global::System.ArgumentNullException("YG_NAME");
            }
            else
            {
                MyUpdateSqlComand.Parameters[1].Value = YG_NAME;
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

        /// <summary>
        /// 获取到一级支部信息, 包括公司团委
        /// </summary>
        /// <returns></returns>
        public YouthOneDS.YouthGroupDataTable GetDataZB()
        {
            this.Adapter.SelectCommand = MySelectZBSqlComand;
            YouthOneDS.YouthGroupDataTable dataTable = new YouthOneDS.YouthGroupDataTable();
            this.Adapter.Fill(dataTable);
            return dataTable;
        }

        /// <summary>
        /// 获取到一级支部信息
        /// </summary>
        /// <returns></returns>
        public List<YouthOneDS.YouthGroupRow> GetZB()
        {
            return GetDataZB().Where(x => x.OID != "GSTW").ToList();
        }

        /// <summary>
        /// 获取到排除指定支部的支部缓存数据，主要用于团员转出操作
        /// </summary>
        /// <param name="YG_OID"></param>
        /// <returns></returns>
        public List<YouthOneDS.YouthGroupRow> GetZBSpecial(String YG_OID)
        {
            return GetZBCache().Where(x => x.OID != YG_OID).ToList();

        }

        /// <summary>
        /// 缓存版：获取到所有的支部、分支信息
        /// </summary>
        /// <returns></returns>
        public YouthOneDS.YouthGroupDataTable GetDataCache()
        {
            YouthOneDS.YouthGroupDataTable _DT = (YouthOneDS.YouthGroupDataTable)HttpContext.Current.Cache[CacheUtil.CACHE_YouthGroup];

            if (_DT == null)
            {
                _DT = GetData();
                HttpContext.Current.Cache.Insert(CacheUtil.CACHE_YouthGroup, _DT, null, DateTime.Now.AddSeconds(CacheUtil.CONFIG_ExpireSecond), TimeSpan.Zero);
            }

            return _DT;
        }

        /// <summary>
        /// 缓存版：获取到分支信息
        /// </summary>
        /// <returns></returns>
        public List<YouthOneDS.YouthGroupRow> GetFZCache(string YG_OID)
        {
            if (YG_OID == "GSTW")
                return GetDataCache().ToList();
            else
                return GetDataCache().Where(x => x.PARENT_OID == YG_OID).ToList();

        }

        /// <summary>
        /// 缓存版：获取到一级支部信息
        /// </summary>
        /// <returns></returns>
        public List<YouthOneDS.YouthGroupRow> GetZBCache()
        {
            return GetDataCache().Where(x => x.YG_LEVEL == 1).ToList();
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
                    _GetDataOrderSqlComand.CommandText = "SELECT * FROM [dbo].[YouthGroup] ORDER BY YG_ORDER";
                    _GetDataOrderSqlComand.CommandType = System.Data.CommandType.Text;
                }

                return _GetDataOrderSqlComand;
            }
        }

        /// <summary>
        /// 获取到经过排序的组织信息
        /// </summary>
        /// <returns></returns>
        public YouthOneDS.YouthGroupDataTable GetDataOrder()
        {
            this.Adapter.SelectCommand = GetDataOrderSqlComand;
            YouthOneDS.YouthGroupDataTable dataTable = new YouthOneDS.YouthGroupDataTable();
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
                    _MoveNodeSqlComand.CommandText = "ChangeYouthGroupOrder";
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
