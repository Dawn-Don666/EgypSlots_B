using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 去广告活动
/// </summary>

namespace zeta_framework
{
    public class MajorityMobileHeHerd: Majority
    {
        public static MajorityMobileHeHerd Instance;

        public const string TubeUp= "s_remove_ad"; // 去广告在商店中的配置(Tube)

        public MajorityMobileHeHerd()
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
        public bool BeMethyl()
        {
            return DivisionHerd.Instance.remove_No.ArtworkLoose > 0;
        }

        /// <summary>
        /// 购买去广告
        /// </summary>
        /// <param name="cb"></param>
        public void Fir(System.Action<UsualDeaf> cb)
        {
            if (BeMethyl())
            {
                cb?.Invoke(UsualDeaf.Success);
            }

            Tube shopItem = TubeHerd.Instance.TieTubeOfUp(TubeUp);
            TubeHerd.Instance.Fir(shopItem, (errorCode) => { 
                if (errorCode == UsualDeaf.Success)
                {
                    // 购买成功，给奖励
                    DivisionHerd.Instance.AgeFineTheir(shopItem.gp_pid);
                    // 活动状态改为Finish
                    Everything();
                    cb?.Invoke(UsualDeaf.Success);
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
        public List<FineTheir> TieSorghum() {
            Tube shopItem = TubeHerd.Instance.TieTubeOfUp(TubeUp);
            return shopItem.OvalTheir;
        }
    }
}

