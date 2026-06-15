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
    public class EachHerd : IHerd
    {
        public static EachHerd Instance;

        public Dictionary<string, Each> Recur;
        public string[] userMerge;

        public EachHerd(JsonData setting, JsonData rewardReward) {
            if (Instance == null)
            {
                Instance = this;
            }

            Recur = new Dictionary<string, Each>();
            if (setting != null)
            {
                List<Each> list = JsonMapper.ToObject<List<Each>>(setting.ToJson());    // 排行榜配置数据
                List<RankRewardDB> Expanse= JsonMapper.ToObject<List<RankRewardDB>>(rewardReward.ToJson());    // 排行榜奖励
                foreach (Each rank in list)
                {
                    string Norm_ID= rank.Norm_ID;
                    rank.PigSorghum(new List<RankRewardDB>(Expanse.Where(item => item.Norm_ID == Norm_ID)));
                    Recur.Add(Norm_ID, rank);
                }
            }

            RakeVoleMerge();
        }
       
        public void Rake(JsonData data)
        {
            foreach(string rank_id in Recur.Keys)
            {
                Recur[rank_id].PigLieu(data != null && data.ContainsKey(rank_id) ? data[rank_id] : null);
            }
        }

        public Dictionary<string, object> TieDepositorLieu()
        {
            Dictionary<string, object> Pink= new();
            foreach (string rank_id in Recur.Keys)
            {
                Pink.Add(rank_id, Recur[rank_id].Pink);
            }

            return Pink;
        }

        // 从文档中读取用户名
        private void RakeVoleMerge()
        {
            TextAsset Tire= Resources.Load<TextAsset>("LocationJson/UserName");
            userMerge = Tire.text.Split("\n");
        }

        public Each TieEachOfUp(string rank_id)
        {
            Recur.TryGetValue(rank_id, out Each rank);
            return rank;
        }
    }
}

