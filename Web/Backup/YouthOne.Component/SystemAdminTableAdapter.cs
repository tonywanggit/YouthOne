using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace YouthOne.Component.YouthOneDSTableAdapters
{
    public partial class SystemAdminTableAdapter
    {
        private SqlCommand _MyDeleteSqlComand;
        private SqlCommand MyDeleteSqlComand
        {
            get
            {
                if (_MyDeleteSqlComand == null)
                {
                    _MyDeleteSqlComand = new SqlCommand();
                    _MyDeleteSqlComand.Connection = this.Connection;
                    _MyDeleteSqlComand.CommandText = "DELETE FROM [dbo].[SystemAdmin] WHERE [OID] = @OID";
                    _MyDeleteSqlComand.CommandType = System.Data.CommandType.Text;
                    _MyDeleteSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OID", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                }

                return _MyDeleteSqlComand;
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
                    _MyUpdateSqlComand.CommandText = "UpdateAdmin";
                    _MyUpdateSqlComand.CommandType = System.Data.CommandType.StoredProcedure;
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OID", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@LOG_NAME", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "LOG_NAME", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@PAS_WORD", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "PAS_WORD", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@ROL_NAME", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "ROL_NAME", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@YG_OID", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "YG_OID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                }

                return _MyUpdateSqlComand;
            }
        }

        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Insert, true)]
        public virtual int MyInsert(string LOG_NAME, string PAS_WORD, string ROL_NAME, string YG_OID)
        {
            string passWord = AdapterHelper.Hash(PAS_WORD);
            return Insert(Guid.NewGuid().ToString(), LOG_NAME, passWord, ROL_NAME, DateTime.Now, YG_OID);
        }

        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Delete, true)]
        public virtual int MyDelete(string OID)
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

        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Update, true)]
        public virtual int MyUpdate(string OID, string LOG_NAME, string PAS_WORD, string ROL_NAME, string YG_OID)
        {
            if (OID == null)
            {
                throw new global::System.ArgumentNullException("OID");
            }
            else
            {
                MyUpdateSqlComand.Parameters[0].Value = OID;
            }

            if (LOG_NAME == null)
            {
                throw new global::System.ArgumentNullException("LOG_NAME");
            }
            else
            {
                MyUpdateSqlComand.Parameters[1].Value = LOG_NAME;
            }

            if (PAS_WORD == null)
            {
                throw new global::System.ArgumentNullException("PAS_WORD");
            }
            else if (PAS_WORD == "PAS_WORD_TONY")
            {
                MyUpdateSqlComand.Parameters[2].Value = PAS_WORD;
            }
            else
            {
                //--对密码进行加密存储
                MyUpdateSqlComand.Parameters[2].Value = AdapterHelper.Hash(PAS_WORD);
            }

            if (ROL_NAME == null)
            {
                throw new global::System.ArgumentNullException("ROL_NAME");
            }
            else
            {
                MyUpdateSqlComand.Parameters[3].Value = ROL_NAME;
            }

            if (YG_OID == null)
            {
                throw new global::System.ArgumentNullException("YG_OID");
            }
            else
            {
                MyUpdateSqlComand.Parameters[4].Value = YG_OID;
            }

            return AdapterHelper.ExecuteCommand(MyUpdateSqlComand);
        }

        #region 获取到所有的系统管理员
        private SqlCommand _MySelectSqlComand;
        private SqlCommand MySelectSqlComand
        {
            get
            {
                if (_MySelectSqlComand == null)
                {
                    _MySelectSqlComand = new SqlCommand();
                    _MySelectSqlComand.Connection = this.Connection;
                    _MySelectSqlComand.CommandText = "SELECT * FROM dbo.SystemAdmin ORDER BY LOG_NAME";
                    _MySelectSqlComand.CommandType = System.Data.CommandType.Text;
                }

                return _MySelectSqlComand;
            }
        }

        /// <summary>
        /// 获取到所有的系统管理员
        /// </summary>
        /// <returns></returns>
        public YouthOneDS.SystemAdminDataTable MySelect()
        {
            YouthOneDS.SystemAdminDataTable dataTable = new YouthOneDS.SystemAdminDataTable();
            this.Adapter.SelectCommand = MySelectSqlComand;
            this.Adapter.Fill(dataTable);

            return dataTable;
        }

        #endregion

        #region 修改用户登录密码
        private SqlCommand _ChangePasswordComand;
        private SqlCommand ChangePasswordComand
        {
            get
            {
                if (_ChangePasswordComand == null)
                {
                    _ChangePasswordComand = new SqlCommand();
                    _ChangePasswordComand.Connection = this.Connection;
                    _ChangePasswordComand.CommandText = "UPDATE SystemAdmin SET PAS_WORD = @PAS_WORD WHERE OID = @OID";
                    _ChangePasswordComand.CommandType = System.Data.CommandType.Text;
                    _ChangePasswordComand.Parameters.Add(new SqlParameter("PAS_WORD", System.Data.SqlDbType.NVarChar, 50));
                    _ChangePasswordComand.Parameters.Add(new SqlParameter("OID", System.Data.SqlDbType.NVarChar, 50));
                }

                return _ChangePasswordComand;
            }
        }

        /// <summary>
        /// 修改用户登录密码
        /// </summary>
        /// <returns></returns>
        public int ChangePassword(string OID, string PAS_WORD)
        {
            ChangePasswordComand.Parameters[0].Value = PAS_WORD;
            ChangePasswordComand.Parameters[1].Value = OID;

            return AdapterHelper.ExecuteCommand(ChangePasswordComand);
        }

        #endregion
    
    }
}
