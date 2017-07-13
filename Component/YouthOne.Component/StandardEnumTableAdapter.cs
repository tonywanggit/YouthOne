using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Web;

namespace YouthOne.Component.YouthOneDSTableAdapters
{
    public partial class StandardEnumTableAdapter
    {
        private SqlCommand _SpecialSkillSelectSqlComand;
        private SqlCommand SpecialSkillSelectSqlComand
        {
            get
            {
                if (_SpecialSkillSelectSqlComand == null)
                {
                    _SpecialSkillSelectSqlComand = new SqlCommand();
                    _SpecialSkillSelectSqlComand.Connection = this.Connection;
                    _SpecialSkillSelectSqlComand.CommandText = "SELECT * FROM dbo.StandardEnum WHERE SE_TYPE IN('技能等级', '职称') ORDER BY SE_TYPE, SE_ORDER";
                    _SpecialSkillSelectSqlComand.CommandType = System.Data.CommandType.Text;
                }

                return _SpecialSkillSelectSqlComand;
            }
        }

        private SqlCommand _MyDeleteSqlComand;
        private SqlCommand MyDeleteSqlComand
        {
            get
            {
                if (_MyDeleteSqlComand == null)
                {
                    _MyDeleteSqlComand = new SqlCommand();
                    _MyDeleteSqlComand.Connection = this.Connection;
                    _MyDeleteSqlComand.CommandText = "DELETE FROM [dbo].[StandardEnum] WHERE [OID] = @OID";
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
                    _MyUpdateSqlComand.CommandText = "UPDATE [dbo].[StandardEnum] SET SE_TYPE = @SE_TYPE, SE_KEY = @SE_KEY, SE_VALUE = @SE_VALUE WHERE [OID] = @OID";
                    _MyUpdateSqlComand.CommandType = System.Data.CommandType.Text;
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@OID", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "OID", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SE_TYPE", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "SE_TYPE", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SE_KEY", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "SE_KEY", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                    _MyUpdateSqlComand.Parameters.Add(new global::System.Data.SqlClient.SqlParameter("@SE_VALUE", global::System.Data.SqlDbType.NVarChar, 0, global::System.Data.ParameterDirection.Input, 0, 0, "SE_VALUE", global::System.Data.DataRowVersion.Original, false, null, "", "", ""));
                }

                return _MyUpdateSqlComand;
            }
        }

        public virtual int MyInsert(string SE_TYPE, string SE_KEY, string SE_VALUE)
        {
            return Insert(Guid.NewGuid().ToString(), SE_TYPE, SE_KEY, SE_VALUE);
        }

        public virtual int MyUpdate(string OID, string SE_TYPE, string SE_KEY, string SE_VALUE)
        {
            if (OID == null)
            {
                throw new global::System.ArgumentNullException("OID");
            }
            else
            {
                MyUpdateSqlComand.Parameters[0].Value = OID;
            }

            if (SE_TYPE == null)
            {
                throw new global::System.ArgumentNullException("SE_TYPE");
            }
            else
            {
                MyUpdateSqlComand.Parameters[1].Value = SE_TYPE;
            }

            if (SE_KEY == null)
            {
                throw new global::System.ArgumentNullException("SE_KEY");
            }
            else
            {
                MyUpdateSqlComand.Parameters[2].Value = SE_KEY;
            }

            if (SE_VALUE == null)
            {
                throw new global::System.ArgumentNullException("SE_VALUE");
            }
            else
            {
                MyUpdateSqlComand.Parameters[3].Value = SE_VALUE;
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
        /// 缓存版：选出职称、技能等级相关数据
        /// </summary>
        /// <returns></returns>
        public IList<YouthOneDS.StandardEnumRow> GetSpecialSkillCache()
        {
            //YouthOneDS.StandardEnumDataTable dataTable = new YouthOneDS.StandardEnumDataTable();
            //this.Adapter.SelectCommand = SpecialSkillSelectSqlComand;
            //this.Adapter.Fill(dataTable);


            //return dataTable;

            return GetDataCache().Where(x => (x.SE_TYPE == "技能等级" || x.SE_TYPE == "职称")).ToList();
        }

        /// <summary>
        /// 对标准枚举进行缓存
        /// </summary>
        /// <returns></returns>
        public YouthOneDS.StandardEnumDataTable GetDataCache()
        {
            YouthOneDS.StandardEnumDataTable _DT = (YouthOneDS.StandardEnumDataTable)HttpContext.Current.Cache[CacheUtil.CACHE_StandartEnum];

            if (_DT == null)
            {
                _DT = GetData();
                HttpContext.Current.Cache.Insert(CacheUtil.CACHE_StandartEnum, _DT, null, DateTime.Now.AddSeconds(CacheUtil.CONFIG_ExpireSecond), TimeSpan.Zero);
            }

            return _DT;
        }
    }
}
