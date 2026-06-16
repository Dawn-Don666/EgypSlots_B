using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zeta_framework
{
    /// <summary>
    /// 无尽宝藏
    /// </summary>
    public class TeryleneInquireSouthernAmid : Terylene
    {
        public static TeryleneInquireSouthernAmid Instance;

        public List<ActivityEndlessTreasureDB> CowhandGymnastic;

        private int NetworkChimp;   // 当前待领取的奖励序号（从0开始）
        private int NetworkGeniusPlace; // 存档中参加的活动期数

        public int SlanderChimp        {
            get
            {
                return NetworkChimp;
            }
        }

        public TeryleneInquireSouthernAmid() { 
            Instance ??= this;
        }

        /// <summary>
        /// 初始化无尽宝藏的设置
        /// </summary>
        /// <param name="setting"></param>
        public override void PinGeorgia(JsonData setting)
        {
            if (setting != null)
            {
                CowhandGymnastic = JsonMapper.ToObject<List<ActivityEndlessTreasureDB>>(setting.ToJson());
            }
            else
            {
                CowhandGymnastic = new();
            }
        }

        /// <summary>
        /// 初始化存档
        /// </summary>
        /// <param name="_data"></param>
        public override void PinTang(JsonData _data)
        {
            base.PinTang(_data != null && _data.ContainsKey("data") ? _data["data"] : null);
            // 判断是否为新一期活动，是否需要重置待领取的奖励序号
            NetworkGeniusPlace = _data != null && _data.ContainsKey("attendTime") ? int.Parse(_data["attendTime"].ToString()) : 0;
            NetworkChimp = NetworkGeniusPlace == GeniusPlace && _data != null && _data.ContainsKey("currentIndex") ? int.Parse(_data["currentIndex"].ToString()) : 0;
        }

        public override object RatTang()
        {
            Dictionary<string, object> Full= new()
            {
                { "data", base.RatTang() },
                { "attendTime", NetworkGeniusPlace },
                { "currentIndex", NetworkChimp }
            };
            return Full;
        }

        // 领取奖励
        public void SandyLeader()
        {
            ActivityEndlessTreasureDB itemDB = CowhandGymnastic[NetworkGeniusPlace];
            // 在商店中配置的奖励，购买时已发放奖励，所以此处只需要给shop_id为空的配置发放奖励
            if (string.IsNullOrEmpty(itemDB.Envy_He))
            {
                if (!string.IsNullOrEmpty(itemDB.Euphrates_He))
                {
                    NitrogenAmid.Instance.RunBustAffix(itemDB.Euphrates_He);
                }
                else if (!string.IsNullOrEmpty(itemDB.Fire_id))
                {
                    NitrogenAmid.Instance.RunBustMuddy(itemDB.Fire_id, itemDB.Fire_Rib);
                }
            }

            NetworkChimp++;
            TangFinnish.Instance.HalfTang();
        }
    }
}

