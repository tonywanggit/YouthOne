using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Web;

namespace YouthOne.Component.YouthOneDSTableAdapters
{
    public partial class PostTableAdapter
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
                    _MyDeleteSqlComand.CommandText = "DELETE FROM [dbo].[Post] WHERE [OID] = @OID";
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
                    _MyUpdateSqlComand.CommandText = "UPDATE [dbo].[Post] SET CAT_NAME = @CAT_NAME, POST_NAME = @POST_NAME WHERE [OID] = @OID";
                    _MyUpdateSqlComand.CommandType = System.Data.CommandType.Text;
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OID", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@CAT_NAME", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "CAT_NAME", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@POST_NAME", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "POST_NAME", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                }

                return _MyUpdateSqlComand;
            }
        }

        public virtual int MyInsert(string CAT_NAME, string POST_NAME)
        {
            return Insert(Guid.NewGuid().ToString(), DateTime.Now, CAT_NAME, POST_NAME);
        }

        public virtual int MyUpdate(string OID, string CAT_NAME, string POST_NAME)
        {
            if (OID == null)
            {
                throw new global::System.ArgumentNullException("OID");
            }
            else
            {
                MyUpdateSqlComand.Parameters[0].Value = OID;
            }

            if (CAT_NAME == null)
            {
                throw new global::System.ArgumentNullException("CAT_NAME");
            }
            else
            {
                MyUpdateSqlComand.Parameters[1].Value = CAT_NAME;
            }

            if (POST_NAME == null)
            {
                throw new global::System.ArgumentNullException("POST_NAME");
            }
            else
            {
                MyUpdateSqlComand.Parameters[2].Value = POST_NAME;
            }

            return AdapterHelper.ExecuteCommand(MyUpdateSqlComand);
        }

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

        /// <summary>
        /// 对岗位进行缓存
        /// </summary>
        /// <returns></returns>
        public YouthOneDS.PostDataTable GetDataCache()
        {
            YouthOneDS.PostDataTable _DT = (YouthOneDS.PostDataTable)HttpContext.Current.Cache[CacheUtil.CACHE_Post];

            if (_DT == null)
            {
                _DT = GetData();
                HttpContext.Current.Cache.Insert(CacheUtil.CACHE_Post, _DT, null, DateTime.Now.AddSeconds(CacheUtil.CONFIG_ExpireSecond), TimeSpan.Zero);
            }

            return _DT;
        }
    }
}
