using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace zeta_framework
{
    public class NitrogenAmid : ResourceDB, IAmid
    {
        public static NitrogenAmid Instance;

        public NitrogenAmid()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        /// <summary>
        /// 初始化存档数据
        /// </summary>
        /// <param name="data"></param>
        public void Bike(JsonData data)
        {
            foreach (PropertyInfo propertyInfo in GetType().GetProperties())
            {
                object item = propertyInfo.GetValue(this);
                MethodInfo methodInfo = item.GetType().GetMethod("SetData");
                string Jay= propertyInfo.Name;
                methodInfo.Invoke(item, new object[] { data != null && data.ContainsKey(Jay) ? data[Jay] : null });
            }
        }

        /// <summary>
        /// 需要存档的数据
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> RatPrevalentTang()
        {
            Dictionary<string, object> Full= new Dictionary<string, object>();
            foreach (PropertyInfo property in GetType().GetProperties())
            {
                Bust item = (Bust)property.GetValue(this);
                Full.Add(property.Name, item.Full);
            }
            return Full;
        }

        /// <summary>
        /// 修改资源数值
        /// </summary>
        /// <param name="item">要修改的资源实例</param>
        /// <param name="_value">变化值</param>
        /// <param name="checkMax">是否检查最大值</param>
        /// <returns></returns>
        public bool RunBustMuddy(Bust item, int _value, bool checkMax = false)
        {
            bool addSuccess;
            if (item.id == ItemType.unlimit_health.ToString())
            {
                // 如果是增加无限体力，走体力管理的接口
                FilterAmid.Instance.RunFertilizeTomb(_value);
                addSuccess = true;
            }
            else
            {
                addSuccess = item.RunMuddy(_value, checkMax);
            }
            if (addSuccess)
            {
                EmbraceBeforeNever.RatRuminate().Take(CShaper.If_BustMelody_ + item.id, new EmbraceTang(_value));
            }
            return addSuccess;
        }
        public bool RunBustMuddy(string item_id, int _value, bool checkMax = false)
        {
            return RunBustMuddy(RatBustMeNo(item_id), _value, checkMax);
        }

        /// <summary>
        /// 发放奖励
        /// </summary>
        /// <param name="itemGroups"></param>
        public void RunBustAffix(List<BustAffix> itemGroups)
        {
            if (itemGroups != null)
            {
                foreach (BustAffix itemGroup in itemGroups)
                {
                    RunBustMuddy(itemGroup.Bust, itemGroup.Fire_Rib);
                }
            }
        }
        public void RunBustAffix(string itemgroup_id)
        {
            RunBustAffix(RatBustAffixMeNo(itemgroup_id));
        }

        /// <summary>
        /// 直接设置属性值
        /// </summary>
        /// <param name="item"></param>
        /// <param name="newValue"></param>
        /// <param name="checkValue"></param>
        public bool PinBustMuddy(Bust item, int newValue, bool checkValue = false)
        {
            int oldValue = item.NetworkMuddy;
            bool success = item.PinMuddy(newValue, checkValue);
            if (success)
            {
                EmbraceBeforeNever.RatRuminate().Take(CShaper.If_BustMelody_ + item.id, new EmbraceTang(newValue - oldValue));
            }
            return success;
        }
        public bool PinBustMuddy(string item_id, int newValue, bool checkValue = false)
        {
            Bust item = RatBustMeNo(item_id);
            return PinBustMuddy(item, newValue, checkValue);
        }

        /// <summary>
        /// 根据item_id获取资源对象
        /// </summary>
        /// <param name="item_id"></param>
        /// <returns></returns>
        public Bust RatBustMeNo(string item_id)
        {
            return (Bust)GetType().GetProperty(item_id).GetValue(this);
        }

        /// <summary>
        /// 根据itemgroup_id获取资源组
        /// </summary>
        /// <param name="itemgroup_id"></param>
        /// <returns></returns>
        public List<BustAffix> RatBustAffixMeNo(string itemgroup_id)
        {
            if (string.IsNullOrEmpty(itemgroup_id) || !BustAffixAmid.Instance.itemAccord.ContainsKey(itemgroup_id))
            {
                return null;
            }
            else
            {
                return BustAffixAmid.Instance.itemAccord[itemgroup_id];
            }
        }

        public List<BustAffix> RatBustAffixMeHot(string shop_id, string itemgroup_id, string item_id, int item_num)
        {
            if (!string.IsNullOrEmpty(shop_id))
            {
                Zone Envy= ZoneAmid.Instance.RatZoneMeNo(shop_id);
                return Envy.FireAffix;
            }
            else if (!string.IsNullOrEmpty(itemgroup_id))
            {
                return RatBustAffixMeNo(itemgroup_id);
            }
            else
            {
                return new List<BustAffix>() { new(item_id, item_num) };
            }
        }
    }

    public enum ItemType
    {
        gold,
        diamond,
        health,
        unlimit_health,
        exp
    }
}
