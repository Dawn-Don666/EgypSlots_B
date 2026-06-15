using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 每日签到
/// </summary>
namespace zeta_framework
{
    public class MajorityAdmitSoilHerd: Majority
    {
        public static MajorityAdmitSoilHerd Instance;

        private List<ActivityDailyGiftDB> GreekPella;

        public MajorityAdmitSoilHerd()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public override void PigElectro(JsonData setting)
        {
            if (setting != null)
            {
                GreekPella = JsonMapper.ToObject<List<ActivityDailyGiftDB>>(setting.ToJson());
            }
            else
            {
                GreekPella = new();
            }
        }


        /// <summary>
        /// 获取当前应该是第几天签到（从0开始）
        /// </summary>
        /// <returns></returns>
        public int TieOverlieShear()
        {
            return AssureSewer % GreekPella.Count;
        }

        /// <summary>
        /// 获取每日签到所有配置
        /// </summary>
        /// <returns></returns>
        public List<ActivityDailyGiftDB> TieCarElectro()
        {
            return GreekPella;
        }

        /// <summary>
        /// 获取第n天的奖励
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public List<FineTheir> TieWeeklyOfShear(int index)
        {
            List<FineTheir> Expanse= new();
            ActivityDailyGiftDB dailyGift = GreekPella[index];
            if (!string.IsNullOrEmpty(dailyGift.Unwilling_ID))
            {
                Expanse.AddRange(DivisionHerd.Instance.TieFineTheirOfUp(dailyGift.Unwilling_ID));
            }
            if (!string.IsNullOrEmpty(dailyGift.Oval_id) && dailyGift.Oval_Lay > 0)
            {
                FineTheir OvalTheir= new(dailyGift.Oval_id, dailyGift.Oval_Lay);
                Expanse.Add(OvalTheir);
            }

            return Expanse;
        }

        /// <summary>
        /// 领取签到奖励
        /// </summary>
        public void Despite()
        {
            int index = TieOverlieShear();
            List<FineTheir> Expanse= TieWeeklyOfShear(index);
            DivisionHerd.Instance.AgeFineTheir(Expanse);
            // 活动设置finish状态
            Everything();
        }
    }
}

