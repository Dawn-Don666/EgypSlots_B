using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 每日签到
/// </summary>
namespace zeta_framework
{
    public class TeryleneGessoPainAmid: Terylene
    {
        public static TeryleneGessoPainAmid Instance;

        private List<ActivityDailyGiftDB> SpeakInner;

        public TeryleneGessoPainAmid()
        {
            if (Instance == null)
            {
                Instance = this;
            }
        }

        public override void PinGeorgia(JsonData setting)
        {
            if (setting != null)
            {
                SpeakInner = JsonMapper.ToObject<List<ActivityDailyGiftDB>>(setting.ToJson());
            }
            else
            {
                SpeakInner = new();
            }
        }


        /// <summary>
        /// 获取当前应该是第几天签到（从0开始）
        /// </summary>
        /// <returns></returns>
        public int RatSlanderChimp()
        {
            return GeniusPlace % SpeakInner.Count;
        }

        /// <summary>
        /// 获取每日签到所有配置
        /// </summary>
        /// <returns></returns>
        public List<ActivityDailyGiftDB> RatEonGeorgia()
        {
            return SpeakInner;
        }

        /// <summary>
        /// 获取第n天的奖励
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public List<BustAffix> RatLeaderMeChimp(int index)
        {
            List<BustAffix> Orderly= new();
            ActivityDailyGiftDB dailyGift = SpeakInner[index];
            if (!string.IsNullOrEmpty(dailyGift.Euphrates_He))
            {
                Orderly.AddRange(NitrogenAmid.Instance.RatBustAffixMeNo(dailyGift.Euphrates_He));
            }
            if (!string.IsNullOrEmpty(dailyGift.Fire_id) && dailyGift.Fire_Rib > 0)
            {
                BustAffix FireAffix= new(dailyGift.Fire_id, dailyGift.Fire_Rib);
                Orderly.Add(FireAffix);
            }

            return Orderly;
        }

        /// <summary>
        /// 领取签到奖励
        /// </summary>
        public void Trample()
        {
            int index = RatSlanderChimp();
            List<BustAffix> Orderly= RatLeaderMeChimp(index);
            NitrogenAmid.Instance.RunBustAffix(Orderly);
            // 活动设置finish状态
            Discontent();
        }
    }
}

