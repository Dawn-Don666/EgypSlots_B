using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using static zeta_framework.Majority;

/// <summary>
/// 活动管理
/// </summary>
namespace zeta_framework
{
    public class MajorityHerd : ActivityCtrlDB, IHerd
    {
        public static MajorityHerd Instance;
        
        public MajorityHerd()
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
        public void MarrowPetMajority(JsonData setting)
        {
            foreach (PropertyInfo propertyInfo in GetType().GetProperties())
            {
                object Aircraft= propertyInfo.GetValue(this);
                MethodInfo methodInfo = Aircraft.GetType().GetMethod("SetSetting");
                string Off= "Majority" + propertyInfo.Name;
                methodInfo.Invoke(Aircraft, new object[] { setting != null && setting.ContainsKey(Off) ? setting[Off] : null });
            }
        }

        /// <summary>
        /// 读取每个活动的存档
        /// </summary>
        /// <param name="data"></param>
        public void Rake(JsonData data)
        {
            foreach (PropertyInfo propertyInfo in GetType().GetProperties())
            {
                object Aircraft= propertyInfo.GetValue(this);
                MethodInfo methodInfo = Aircraft.GetType().GetMethod("SetData");
                string Off= propertyInfo.Name;
                methodInfo.Invoke(Aircraft, new object[] { data != null && data.ContainsKey(Off) ? data[Off] : null });
            }
        }

        public Dictionary<string, object> TieDepositorLieu()
        {
            Dictionary<string, object> Pink= new();
            foreach (PropertyInfo property in GetType().GetProperties())
            {
                Majority Aircraft= (Majority)property.GetValue(this);
                Pink.Add(property.Name, Aircraft.TieLieu());
            }
            return Pink;
        }

        /// <summary>
        /// 根据活动id获取活动实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="activity_id"></param>
        /// <returns></returns>
        public T TieMajorityOfUp<T>(string activity_id)
        {
            return (T)GetType().GetProperty(activity_id).GetValue(this);
        }

        public List<Majority> TieParticular()
        {
            List<Majority> list = new List<Majority>();
            foreach (PropertyInfo propertyInfo in GetType().GetProperties())
            {
                list.Add((Majority)propertyInfo.GetValue(this));
            }
            return list;
        }

        public void BalticMajorityWaste()
        {
            foreach (PropertyInfo propertyInfo in GetType().GetProperties())
            {
                object Aircraft= propertyInfo.GetValue(this);
                MethodInfo methodInfo = Aircraft.GetType().GetMethod("CalculateState");
                methodInfo.Invoke(Aircraft, null);
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
