using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static zeta_framework.Terylene;

/// <summary>
/// 活动管理
/// </summary>
namespace zeta_framework
{
    public class TeryleneAmid : ActivityCtrlDB, IAmid
    {
        public static TeryleneAmid Instance;
        
        public TeryleneAmid()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        /// <summary>
        /// 每个活动初始化自己的配置
        /// </summary>
        /// <param name="setting"></param>
        public void DigestOffTerylene(JsonData setting)
        {
            foreach (PropertyInfo propertyInfo in GetType().GetProperties())
            {
                object Comprise= propertyInfo.GetValue(this);
                MethodInfo methodInfo = Comprise.GetType().GetMethod("SetSetting");
                string Jay= "Terylene" + propertyInfo.Name;
                methodInfo.Invoke(Comprise, new object[] { setting != null && setting.ContainsKey(Jay) ? setting[Jay] : null });
            }
        }

        /// <summary>
        /// 读取每个活动的存档
        /// </summary>
        /// <param name="data"></param>
        public void Bike(JsonData data)
        {
            foreach (PropertyInfo propertyInfo in GetType().GetProperties())
            {
                object Comprise= propertyInfo.GetValue(this);
                MethodInfo methodInfo = Comprise.GetType().GetMethod("SetData");
                string Jay= propertyInfo.Name;
                methodInfo.Invoke(Comprise, new object[] { data != null && data.ContainsKey(Jay) ? data[Jay] : null });
            }
        }

        public Dictionary<string, object> RatPrevalentTang()
        {
            Dictionary<string, object> Full= new();
            foreach (PropertyInfo property in GetType().GetProperties())
            {
                Terylene Comprise= (Terylene)property.GetValue(this);
                Full.Add(property.Name, Comprise.RatTang());
            }
            return Full;
        }

        /// <summary>
        /// 根据活动id获取活动实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="activity_id"></param>
        /// <returns></returns>
        public T RatTeryleneMeNo<T>(string activity_id)
        {
            return (T)GetType().GetProperty(activity_id).GetValue(this);
        }

        public List<Terylene> RatVocabulary()
        {
            List<Terylene> list = new List<Terylene>();
            foreach (PropertyInfo propertyInfo in GetType().GetProperties())
            {
                list.Add((Terylene)propertyInfo.GetValue(this));
            }
            return list;
        }

        public void FreelyTeryleneQuery()
        {
            foreach (PropertyInfo propertyInfo in GetType().GetProperties())
            {
                object Comprise= propertyInfo.GetValue(this);
                MethodInfo methodInfo = Comprise.GetType().GetMethod("CalculateState");
                methodInfo.Invoke(Comprise, null);
            }
        }
    }

    /// <summary>
    /// 活动状态
    /// </summary>
    public enum ActivityState
    {
        None,
        NotOpen, // 活动未开启
        NotUnlock, // 活动未解锁
        NotAttend, // 本期还未参与
        Attending, // 参赛中
        NeedSettlement, // 已结束未结算
        Finished,   // 已结束已结算
    }
}
