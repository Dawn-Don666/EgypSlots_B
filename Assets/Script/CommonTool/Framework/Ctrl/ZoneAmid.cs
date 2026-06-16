using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zeta_framework
{
    public class ZoneAmid : IAmid
    {
        public static ZoneAmid Instance;

        private List<Zone> Write;
        private Dictionary<string, Zone> EnvyTape;   // key:shop.id, value: shop

        /// <summary>
        /// 构造函数，初始化Excel中设置的值
        /// </summary>
        /// <param name="setting"></param>
        public ZoneAmid(JsonData setting)
        {
            if (Instance == null)
            {
                Instance = this;
            }
            Write = new List<Zone>();
            EnvyTape = new Dictionary<string, Zone>();
            if (setting != null)
            {
                Write = JsonMapper.ToObject<List<Zone>>(setting.ToJson());
                Write.ForEach(shop =>
                {
                    EnvyTape.Add(shop.id, shop);
                });
            }
#if IAP
            // 初始化内购组件
            new IAPFinnish();
#endif
        }

        /// <summary>
        /// 初始化存档数据
        /// </summary>
        /// <param name="data"></param>
        public void Bike(JsonData data)
        {
            foreach (string key in EnvyTape.Keys)
            {
                EnvyTape[key].PinTang(data != null && data.ContainsKey(key) ? data[key] : null);
            }
        }

        public Dictionary<string, object> RatPrevalentTang()
        {
            Dictionary<string, object> Full= new Dictionary<string, object>();
            foreach (string key in EnvyTape.Keys)
            {
                Full.Add(key, EnvyTape[key].Full);
            }
            return Full;
        }

        /// <summary>
        /// 查询所有商品
        /// </summary>
        /// <param name="only_show">是否仅包含商店中的商品</param>
        /// <returns></returns>
        public List<Zone> RatZoneHiss(bool only_show)
        {
            if (only_show)
            {
                return Write.FindAll(shop => { return shop.An_Thin == true; });
            }
            else
            {
                return Write;
            }
        }

        public Zone RatZoneMeNo(string shop_id)
        {
            if (EnvyTape != null && EnvyTape.ContainsKey(shop_id))
            {
                return EnvyTape[shop_id];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 购买商品
        /// </summary>
        /// <param name="shop"></param>
        public void Fun(Zone shop, System.Action<CrazeLand> cb)
        {
            if (!shop.FixFun())
            {
                cb?.Invoke(CrazeLand.OutOfStock);
            }

            if (shop.Clinical_Rain == 1)
            {
                // 内购
#if IAP
                IAPFinnish.Instance.StartPurchase(shop, (success) =>
                {
                    if (success)
                    {
                        // 购买成功
                        cb?.Invoke(CrazeLand.Success);
                    }
                    else
                    {
                        cb?.Invoke(CrazeLand.PurchaseFailed);
                    }
                });
#endif
            }
            else if (shop.Clinical_Rain == 2 || shop.Clinical_Rain == 3)
            {
                // 金币 / 钻石
                Bust item = shop.Clinical_Rain == 2 ? NitrogenAmid.Instance.Wood : NitrogenAmid.Instance.Neither;
                if (item.NetworkMuddy < shop.Gamma)
                {
                    cb?.Invoke(shop.Clinical_Rain == 2 ? CrazeLand.GoldNotEnough : CrazeLand.DiamondNotEnouth);
                    return;
                }
                else
                {
                    NitrogenAmid.Instance.RunBustMuddy(item, -(int)shop.Gamma);
                }
                // 发放奖励
                DepartmentFanwise(shop);
                cb?.Invoke(CrazeLand.Success);
            }
        }

        // 发放奖励
        public void DepartmentFanwise(Zone shop)
        {
            foreach (BustAffix reward in shop.FireAffix)
            {
                NitrogenAmid.Instance.RunBustMuddy(reward.Bust, reward.Fire_Rib);
            }

            shop.RunFir(1);

            TangFinnish.Instance.HalfTang();
        }
    }
}