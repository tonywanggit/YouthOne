using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace YouthOne.Component
{
    /// <summary>
    /// 缓存帮助类
    /// </summary>
    public static class CacheUtil
    {
        private static List<string> CacheList;

        /// <summary>
        /// 缓存过期时间
        /// </summary>
        public const int CONFIG_ExpireSecond = 60;

        /// <summary>
        /// 标准枚举值
        /// </summary>
        public const string CACHE_StandartEnum = "CACHE_StandartEnum";
        /// <summary>
        /// 岗位
        /// </summary>
        public const string CACHE_Post = "CACHE_Post";
        /// <summary>
        /// 组织
        /// </summary>
        public const string CACHE_Organize = "CACHE_Organize";
        /// <summary>
        /// 团支部
        /// </summary>
        public const string CACHE_YouthGroup = "CACHE_YouthGroup";

        static CacheUtil(){
            CacheList.Add(CACHE_StandartEnum);
            CacheList.Add(CACHE_Post);
            CacheList.Add(CACHE_Organize);
            CacheList.Add(CACHE_YouthGroup);
        }

        /// <summary>
        /// 清除所有的缓存
        /// </summary>
        public static void Clear(){
            foreach (string item in CacheList)
	        {
                HttpContext.Current.Cache.Remove(item);
	        }
        }

        /// <summary>
        /// 清除单个缓存
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }
    }
}
