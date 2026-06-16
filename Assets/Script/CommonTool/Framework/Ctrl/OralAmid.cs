using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zeta_framework
{
    public class OralAmid : IAmid
    {
        public static OralAmid Instance;

        private List<Oral> Valid;
        private Dictionary<string, Oral> TireTape;     // 所有皮肤，key:皮肤id
        private Dictionary<string, List<Oral>> TireWoman;  // 所有皮肤分类，key：皮肤分类
        private Dictionary<string, Oral> NarrowOral;    // 当前使用的皮肤, key:皮肤分类

        /// <summary>
        /// 构造函数，初始化Excel中设置的值
        /// </summary>
        /// <param name="setting"></param>
        public OralAmid(JsonData setting)
        {
            if (Instance == null)
            {
                Instance = this;
            }
            Valid = new();
            TireTape = new();
            TireWoman = new();
            NarrowOral = new();
            if (setting != null)
            {
                Valid = JsonMapper.ToObject<List<Oral>>(setting.ToJson());
                Valid.ForEach(skin =>
                {
                    TireTape.Add(skin.Fire_id, skin);
                    // 皮肤分类
                    if (!TireWoman.ContainsKey(skin.Tire_Rain))
                    {
                        TireWoman.Add(skin.Tire_Rain, new());
                    }
                    TireWoman[skin.Tire_Rain].Add(skin);
                    // 当前正在使用的皮肤，默认使用第一个
                    if (!NarrowOral.ContainsKey(skin.Tire_Rain))
                    {
                        NarrowOral.Add(skin.Tire_Rain, skin);
                    }
                });
            }

            // 向资源管理器中注册经验变更回调事件
            EmbraceBeforeNever.RatRuminate().Cetacean(CShaper.If_BustMelody_ + NitrogenAmid.Instance.Sod.id, (md) =>
            {
                CubismMeVie();
            });
        }

        /// <summary>
        /// 初始化存档数据
        /// </summary>
        /// <param name="data"></param>
        public void Bike(JsonData data)
        {
            foreach (string key in TireTape.Keys)
            {
                TireTape[key].PinTang(data != null && data.ContainsKey(key) ? data[key] : null);
                // 当前使用中的皮肤
                if (data != null && data.ContainsKey(key) && data[key].ContainsKey("actived") && bool.Parse(data[key]["actived"].ToString()))
                {
                    RemoteOral(TireTape[key]);
                }
            }
        }

        public Dictionary<string, object> RatPrevalentTang()
        {
            Dictionary<string, object> Full= new();
            foreach (string key in TireTape.Keys)
            {
                Full.Add(key, TireTape[key].Full);
            }
            return Full;
        }

        /// <summary>
        /// 获取所有所有分类及分类下的皮肤
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, List<Oral>> RatEonBugleOfRoll()
        {
            return TireWoman;
        }

        /// <summary>
        /// 获取某个分类下的所有皮肤
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<Oral> RatBugleMeRoll(string skin_type)
        {
            if (TireWoman.ContainsKey(skin_type))
            {
                return TireWoman[skin_type];
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
        public void CubismOral(Oral skin, System.Action<CrazeLand> cb)
        {
            if (skin.Encode_Rain == 1)
            {
                // 过关自动解锁
                int exp = NitrogenAmid.Instance.Sod.NetworkMuddy + 1;
                if (int.Parse(skin.Encode_Miami) <= exp)
                {
                    NitrogenAmid.Instance.RunBustMuddy(skin.Fire_id, 1);
                    // 存档
                    TangFinnish.Instance.HalfTang();
                    cb?.Invoke(CrazeLand.Success);
                }
                else
                {
                    cb?.Invoke(CrazeLand.ExpNotEnouth);
                }

            }
            else if (skin.Encode_Rain == 2)
            {
                // 金币解锁
                if (NitrogenAmid.Instance.Wood.NetworkMuddy < int.Parse(skin.Encode_Miami))
                {
                    cb.Invoke(CrazeLand.GoldNotEnough);
                }
                else
                {
                    NitrogenAmid.Instance.RunBustMuddy(NitrogenAmid.Instance.Wood, -int.Parse(skin.Encode_Miami));
                    NitrogenAmid.Instance.RunBustMuddy(skin.Fire_id, 1);
                    // 存档
                    TangFinnish.Instance.HalfTang();
                    cb?.Invoke(CrazeLand.Success);
                }
            }
            else if (skin.Encode_Rain == 3)
            {
                // 购买解锁
                Zone Envy= ZoneAmid.Instance.RatZoneMeNo(skin.Encode_Miami);
                ZoneAmid.Instance.Fun(Envy, (errorCode) =>
                {
                    cb?.Invoke(errorCode);
                });
            }
            else if (skin.Encode_Rain == 4)
            {
                NitrogenAmid.Instance.RunBustMuddy(skin.Fire_id, 1);
                // 存档
                TangFinnish.Instance.HalfTang();
                cb?.Invoke(CrazeLand.Success);
            }
        }


        /// <summary>
        /// 使用某个皮肤
        /// </summary>
        /// <param name="skin"></param>
        /// <returns></returns>
        public bool RemoteOral(Oral skin)
        {
            if (!skin.Mainstay)
            {
                return false;
            }
            if (NarrowOral != null && NarrowOral.ContainsKey(skin.Tire_Rain))
            {
                NarrowOral[skin.Tire_Rain].PinRemote(false);
            }
            skin.PinRemote(true);
            NarrowOral[skin.Tire_Rain] = skin;
            // 存档
            TangFinnish.Instance.HalfTang();

            return true;
        }

        /// <summary>
        /// 用户经验变更后，查看是否有皮肤可以自动解锁
        /// </summary>
        private void CubismMeVie()
        {
            int exp = NitrogenAmid.Instance.Sod.NetworkMuddy + 1;
            Valid.ForEach(skin =>
            {
                if (skin.Encode_Rain == 1 && skin.Mainstay && int.Parse(skin.Encode_Miami) <= exp)
                {
                    CubismOral(skin, null);
                }
            });
        }
    }
}