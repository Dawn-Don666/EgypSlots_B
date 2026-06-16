using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace zeta_framework
{
    /// <summary>
    /// 排行榜管理
    /// </summary>
    public class OpenAmid : IAmid
    {
        public static OpenAmid Instance;

        public Dictionary<string, Open> Noble;
        public string[] BulkThumb;

        public OpenAmid(JsonData setting, JsonData rewardReward) {
            if (Instance == null)
            {
                Instance = this;
            }

            Noble = new Dictionary<string, Open>();
            if (setting != null)
            {
                List<Open> list = JsonMapper.ToObject<List<Open>>(setting.ToJson());    // 排行榜配置数据
                List<RankRewardDB> Orderly= JsonMapper.ToObject<List<RankRewardDB>>(rewardReward.ToJson());    // 排行榜奖励
                foreach (Open rank in list)
                {
                    string Move_He= rank.Move_He;
                    rank.PinFanwise(new List<RankRewardDB>(Orderly.Where(item => item.Move_He == Move_He)));
                    Noble.Add(Move_He, rank);
                }
            }

            BikeAcreThumb();
        }
       
        public void Bike(JsonData data)
        {
            foreach(string rank_id in Noble.Keys)
            {
                Noble[rank_id].PinTang(data != null && data.ContainsKey(rank_id) ? data[rank_id] : null);
            }
        }

        public Dictionary<string, object> RatPrevalentTang()
        {
            Dictionary<string, object> Full= new();
            foreach (string rank_id in Noble.Keys)
            {
                Full.Add(rank_id, Noble[rank_id].Full);
            }

            return Full;
        }

        // 从文档中读取用户名
        private void BikeAcreThumb()
        {
            TextAsset Face= Resources.Load<TextAsset>("LocationJson/UserName");
            BulkThumb = Face.text.Split("\n");
        }

        public Open RatOpenMeNo(string rank_id)
        {
            Noble.TryGetValue(rank_id, out Open rank);
            return rank;
        }
    }
}

