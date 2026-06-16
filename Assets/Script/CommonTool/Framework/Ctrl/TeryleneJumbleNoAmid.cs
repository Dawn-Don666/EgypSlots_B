using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 去广告活动
/// </summary>

namespace zeta_framework
{
    public class TeryleneJumbleNoAmid: Terylene
    {
        public static TeryleneJumbleNoAmid Instance;

        public const string ZoneNo= "s_remove_ad"; // 去广告在商店中的配置(Zone)

        public TeryleneJumbleNoAmid()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }


        /// <summary>
        /// 去广告功能是否已经生效
        /// </summary>
        /// <returns></returns>
        public bool UpEnigma()
        {
            return NitrogenAmid.Instance.remove_Ax.NetworkMuddy > 0;
        }

        /// <summary>
        /// 购买去广告
        /// </summary>
        /// <param name="cb"></param>
        public void Fun(System.Action<CrazeLand> cb)
        {
            if (UpEnigma())
            {
                cb?.Invoke(CrazeLand.Success);
            }

            Zone shopItem = ZoneAmid.Instance.RatZoneMeNo(ZoneNo);
            ZoneAmid.Instance.Fun(shopItem, (errorCode) => { 
                if (errorCode == CrazeLand.Success)
                {
                    // 购买成功，给奖励
                    NitrogenAmid.Instance.RunBustAffix(shopItem.gp_Due);
                    // 活动状态改为Finish
                    Discontent();
                    cb?.Invoke(CrazeLand.Success);
                }
                else
                {
                    // 购买失败，直接返回
                    cb?.Invoke(errorCode);
                }
            });
        }

        /// <summary>
        /// 去广告活动的所有奖励
        /// </summary>
        /// <returns></returns>
        public List<BustAffix> RatFanwise() {
            Zone shopItem = ZoneAmid.Instance.RatZoneMeNo(ZoneNo);
            return shopItem.FireAffix;
        }
    }
}

