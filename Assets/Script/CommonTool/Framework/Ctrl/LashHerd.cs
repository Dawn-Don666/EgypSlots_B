using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zeta_framework
{
    public class LashHerd : IHerd
    {
        public static LashHerd Instance;

        private List<Lash> Shade;
        private Dictionary<string, Lash> skinBind;     // 所有皮肤，key:皮肤id
        private Dictionary<string, List<Lash>> BareTrait;  // 所有皮肤分类，key：皮肤分类
        private Dictionary<string, Lash> InjuryLash;    // 当前使用的皮肤, key:皮肤分类

        /// <summary>
        /// 构造函数，初始化Excel中设置的值
        /// </summary>
        /// <param name="setting"></param>
        public LashHerd(JsonData setting)
        {
            if (Instance == null)
            {
                Instance = this;
            }
            Shade = new();
            skinBind = new();
            BareTrait = new();
            InjuryLash = new();
            if (setting != null)
            {
                Shade = JsonMapper.ToObject<List<Lash>>(setting.ToJson());
                Shade.ForEach(skin =>
                {
                    skinBind.Add(skin.Oval_id, skin);
                    // 皮肤分类
                    if (!BareTrait.ContainsKey(skin.Bare_Jazz))
                    {
                        BareTrait.Add(skin.Bare_Jazz, new());
                    }
                    BareTrait[skin.Bare_Jazz].Add(skin);
                    // 当前正在使用的皮肤，默认使用第一个
                    if (!InjuryLash.ContainsKey(skin.Bare_Jazz))
                    {
                        InjuryLash.Add(skin.Bare_Jazz, skin);
                    }
                });
            }

            // 向资源管理器中注册经验变更回调事件
            CollectGoldenDaunt.TieRecharge().Advocate(CUnfair.Dy_FineLinear_ + DivisionHerd.Instance.Saw.id, (md) =>
            {
                SpruceOfExp();
            });
        }

        /// <summary>
        /// 初始化存档数据
        /// </summary>
        /// <param name="data"></param>
        public void Rake(JsonData data)
        {
            foreach (string key in skinBind.Keys)
            {
                skinBind[key].PigLieu(data != null && data.ContainsKey(key) ? data[key] : null);
                // 当前使用中的皮肤
                if (data != null && data.ContainsKey(key) && data[key].ContainsKey("actived") && bool.Parse(data[key]["actived"].ToString()))
                {
                    NegateLash(skinBind[key]);
                }
            }
        }

        public Dictionary<string, object> TieDepositorLieu()
        {
            Dictionary<string, object> Pink= new();
            foreach (string key in skinBind.Keys)
            {
                Pink.Add(key, skinBind[key].Pink);
            }
            return Pink;
        }

        /// <summary>
        /// 获取所有所有分类及分类下的皮肤
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<Lash>> TieCarSkinsMeUser()
        {
            return BareTrait;
        }

        /// <summary>
        /// 获取某个分类下的所有皮肤
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Lash> TieEjectOfUser(string skin_type)
        {
            if (BareTrait.ContainsKey(skin_type))
            {
                return BareTrait[skin_type];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 解锁/购买皮肤
        /// </summary>
        /// <param name="skin"></param>
        /// <param name="cb"></param>
        public void SpruceLash(Lash skin, System.Action<UsualDeaf> cb)
        {
            if (skin.Trader_Jazz == 1)
            {
                // 过关自动解锁
                int exp = DivisionHerd.Instance.Saw.ArtworkLoose + 1;
                if (int.Parse(skin.Trader_Under) <= exp)
                {
                    DivisionHerd.Instance.AgeFineLoose(skin.Oval_id, 1);
                    // 存档
                    LieuReelect.Instance.MileLieu();
                    cb?.Invoke(UsualDeaf.Success);
                }
                else
                {
                    cb?.Invoke(UsualDeaf.ExpNotEnouth);
                }

            }
            else if (skin.Trader_Jazz == 2)
            {
                // 金币解锁
                if (DivisionHerd.Instance.Meal.ArtworkLoose < int.Parse(skin.Trader_Under))
                {
                    cb.Invoke(UsualDeaf.GoldNotEnough);
                }
                else
                {
                    DivisionHerd.Instance.AgeFineLoose(DivisionHerd.Instance.Meal, -int.Parse(skin.Trader_Under));
                    DivisionHerd.Instance.AgeFineLoose(skin.Oval_id, 1);
                    // 存档
                    LieuReelect.Instance.MileLieu();
                    cb?.Invoke(UsualDeaf.Success);
                }
            }
            else if (skin.Trader_Jazz == 3)
            {
                // 购买解锁
                Tube Jazz= TubeHerd.Instance.TieTubeOfUp(skin.Trader_Under);
                TubeHerd.Instance.Fir(Jazz, (errorCode) =>
                {
                    cb?.Invoke(errorCode);
                });
            }
            else if (skin.Trader_Jazz == 4)
            {
                DivisionHerd.Instance.AgeFineLoose(skin.Oval_id, 1);
                // 存档
                LieuReelect.Instance.MileLieu();
                cb?.Invoke(UsualDeaf.Success);
            }
        }


        /// <summary>
        /// 使用某个皮肤
        /// </summary>
        /// <param name="skin"></param>
        /// <returns></returns>
        public bool NegateLash(Lash skin)
        {
            if (!skin.Statuary)
            {
                return false;
            }
            if (InjuryLash != null && InjuryLash.ContainsKey(skin.Bare_Jazz))
            {
                InjuryLash[skin.Bare_Jazz].PigNegate(false);
            }
            skin.PigNegate(true);
            InjuryLash[skin.Bare_Jazz] = skin;
            // 存档
            LieuReelect.Instance.MileLieu();

            return true;
        }

        /// <summary>
        /// 用户经验变更后，查看是否有皮肤可以自动解锁
        /// </summary>
        private void SpruceOfExp()
        {
            int exp = DivisionHerd.Instance.Saw.ArtworkLoose + 1;
            Shade.ForEach(skin =>
            {
                if (skin.Trader_Jazz == 1 && skin.Statuary && int.Parse(skin.Trader_Under) <= exp)
                {
                    SpruceLash(skin, null);
                }
            });
        }
    }
}