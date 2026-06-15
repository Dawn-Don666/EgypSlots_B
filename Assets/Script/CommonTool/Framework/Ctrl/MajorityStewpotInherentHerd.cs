using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zeta_framework
{
    /// <summary>
    /// 无尽宝藏
    /// </summary>
    public class MajorityStewpotInherentHerd : Majority
    {
        public static MajorityStewpotInherentHerd Instance;

        public List<ActivityEndlessTreasureDB> StratumExclusive;

        private int ArtworkShear;   // 当前待领取的奖励序号（从0开始）
        private int ArtworkAssureSewer; // 存档中参加的活动期数

        public int OverlieShear        {
            get
            {
                return ArtworkShear;
            }
        }

        public MajorityStewpotInherentHerd() { 
            Instance ??= this;
        }

        /// <summary>
        /// 初始化无尽宝藏的设置
        /// </summary>
        /// <param name="setting"></param>
        public override void PigElectro(JsonData setting)
        {
            if (setting != null)
            {
                StratumExclusive = JsonMapper.ToObject<List<ActivityEndlessTreasureDB>>(setting.ToJson());
            }
            else
            {
                StratumExclusive = new();
            }
        }

        /// <summary>
        /// 初始化存档
        /// </summary>
        /// <param name="_data"></param>
        public override void PigLieu(JsonData _data)
        {
            base.PigLieu(_data != null && _data.ContainsKey("data") ? _data["data"] : null);
            // 判断是否为新一期活动，是否需要重置待领取的奖励序号
            ArtworkAssureSewer = _data != null && _data.ContainsKey("attendTime") ? int.Parse(_data["attendTime"].ToString()) : 0;
            ArtworkShear = ArtworkAssureSewer == AssureSewer && _data != null && _data.ContainsKey("currentIndex") ? int.Parse(_data["currentIndex"].ToString()) : 0;
        }

        public override object TieLieu()
        {
            Dictionary<string, object> Pink= new()
            {
                { "data", base.TieLieu() },
                { "attendTime", ArtworkAssureSewer },
                { "currentIndex", ArtworkShear }
            };
            return Pink;
        }

        // 领取奖励
        public void EvokeWeekly()
        {
            ActivityEndlessTreasureDB itemDB = StratumExclusive[ArtworkAssureSewer];
            // 在商店中配置的奖励，购买时已发放奖励，所以此处只需要给shop_id为空的配置发放奖励
            if (string.IsNullOrEmpty(itemDB.Jazz_ID))
            {
                if (!string.IsNullOrEmpty(itemDB.Unwilling_ID))
                {
                    DivisionHerd.Instance.AgeFineTheir(itemDB.Unwilling_ID);
                }
                else if (!string.IsNullOrEmpty(itemDB.Oval_id))
                {
                    DivisionHerd.Instance.AgeFineLoose(itemDB.Oval_id, itemDB.Oval_Lay);
                }
            }

            ArtworkShear++;
            LieuReelect.Instance.MileLieu();
        }
    }
}

