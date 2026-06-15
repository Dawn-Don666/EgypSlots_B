using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zeta_framework
{
    public class TubeHerd : IHerd
    {
        public static TubeHerd Instance;

        private List<Tube> Vivid;
        private Dictionary<string, Tube> JazzBind;   // key:shop.id, value: shop

        /// <summary>
        /// 构造函数，初始化Excel中设置的值
        /// </summary>
        /// <param name="setting"></param>
        public TubeHerd(JsonData setting)
        {
            if (Instance == null)
            {
                Instance = this;
            }
            Vivid = new List<Tube>();
            JazzBind = new Dictionary<string, Tube>();
            if (setting != null)
            {
                Vivid = JsonMapper.ToObject<List<Tube>>(setting.ToJson());
                Vivid.ForEach(shop =>
                {
                    JazzBind.Add(shop.id, shop);
                });
            }
#if IAP
            // 初始化内购组件
            new IAPReelect();
#endif
        }

        /// <summary>
        /// 初始化存档数据
        /// </summary>
        /// <param name="data"></param>
        public void Rake(JsonData data)
        {
            foreach (string key in JazzBind.Keys)
            {
                JazzBind[key].PigLieu(data != null && data.ContainsKey(key) ? data[key] : null);
            }
        }

        public Dictionary<string, object> TieDepositorLieu()
        {
            Dictionary<string, object> Pink= new Dictionary<string, object>();
            foreach (string key in JazzBind.Keys)
            {
                Pink.Add(key, JazzBind[key].Pink);
            }
            return Pink;
        }

        /// <summary>
        /// 查询所有商品
        /// </summary>
        /// <param name="only_show">是否仅包含商店中的商品</param>
        /// <returns></returns>
        public List<Tube> TieTubeFond(bool only_show)
        {
            if (only_show)
            {
                return Vivid.FindAll(shop => { return shop.If_Deny == true; });
            }
            else
            {
                return Vivid;
            }
        }

        public Tube TieTubeOfUp(string shop_id)
        {
            if (JazzBind != null && JazzBind.ContainsKey(shop_id))
            {
                return JazzBind[shop_id];
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
        public void Fir(Tube shop, System.Action<UsualDeaf> cb)
        {
            if (!shop.EggFir())
            {
                cb?.Invoke(UsualDeaf.OutOfStock);
            }

            if (shop.Seacoast_Jazz == 1)
            {
                // 内购
#if IAP
                IAPReelect.Instance.StartPurchase(shop, (success) =>
                {
                    if (success)
                    {
                        // 购买成功
                        cb?.Invoke(UsualDeaf.Success);
                    }
                    else
                    {
                        cb?.Invoke(UsualDeaf.PurchaseFailed);
                    }
                });
#endif
            }
            else if (shop.Seacoast_Jazz == 2 || shop.Seacoast_Jazz == 3)
            {
                // 金币 / 钻石
                Fine item = shop.Seacoast_Jazz == 2 ? DivisionHerd.Instance.Meal : DivisionHerd.Instance.Gazette;
                if (item.ArtworkLoose < shop.Vigor)
                {
                    cb?.Invoke(shop.Seacoast_Jazz == 2 ? UsualDeaf.GoldNotEnough : UsualDeaf.DiamondNotEnouth);
                    return;
                }
                else
                {
                    DivisionHerd.Instance.AgeFineLoose(item, -(int)shop.Vigor);
                }
                // 发放奖励
                CongenitalSorghum(shop);
                cb?.Invoke(UsualDeaf.Success);
            }
        }

        // 发放奖励
        public void CongenitalSorghum(Tube shop)
        {
            foreach (FineTheir reward in shop.OvalTheir)
            {
                DivisionHerd.Instance.AgeFineLoose(reward.Fine, reward.Oval_Lay);
            }

            shop.AgeDry(1);

            LieuReelect.Instance.MileLieu();
        }
    }
}