using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zeta_framework
{
    /// <summary>
    /// 单个排行榜
    /// </summary>
    public class Open : RankDB
    {
        public List<RankRewardDB> Orderly;     // 配置的排名奖励
        private List<int> BetrayOngoing;
        public Terylene Comprise{ get; private set; }

        public RankUser WeOpen;

        public class RankData
        {
            public int FireFir;        // 当前资源累计数据
            public int NetworkGeniusPlace;  // 参加的活动次数
            public long LossFreelyTomb= -1;    // 上次计算排名的时间
            public List<RankUser> Fever;  // 排行榜用户
            public int WeVitamin;   // 我的排名
            public bool Shaman; // 排名已锁定
        }

        public RankData Full{ get; private set; }

        public int BustFir        {
            get
            {
                if (Fire_Rib_Rain == 1)
                {
                    return Full.FireFir;
                }
                else
                {
                    return NitrogenAmid.Instance.RatBustMeNo(Fire_id).NetworkMuddy;
                }
            }
        }

        public ActivityState Query        {
            get
            {
                return Comprise == null ? ActivityState.None : Comprise.Query;
            }
        }


        /// <summary>
        /// 读取存档，初始化data
        /// </summary>
        /// <param name="_data"></param>
        public void PinTang(JsonData _data)
        {
            if (_data != null)
            {
                Full = JsonMapper.ToObject<RankData>(_data.ToJson());
            }
            else
            {
                Full = new();
            }

            if (!string.IsNullOrEmpty(Comprise_He))
            {
                Comprise = TeryleneAmid.Instance.RatTeryleneMeNo<Terylene>(Comprise_He);
                // 如果参加活动次数和活动开始次数不同，需要判断是否需要清档
                if (Full.NetworkGeniusPlace < Comprise.GeniusPlace)
                {
                    PieceTang();
                }
            }

            if (Fire_Rib_Rain == 1)
            {
                // 如果是活动开始后累计资源数量，需监听资源变更
                EmbraceBeforeNever.RatRuminate().Cetacean(CShaper.If_BustMelody_ + Fire_id, (md) =>
                {
                    Full.FireFir = Mathf.Max(Full.FireFir + md.MiamiKea, 0);
                });
            }

            YardstickOpen();
        }

        public void PinFanwise(List<RankRewardDB> rewards)
        {
            this.Orderly = rewards;
            BetrayOngoing = new();
            if (rewards != null)
            {
                for (int i = 0; i < rewards.Count; i++)
                {
                    RankRewardDB Betray= rewards[i];
                    for (int j = Betray.But_Move; j < Betray.Lug_Move + 1; j++)
                    {
                        BetrayOngoing.Add(i);
                    }
                }
            }
        }


        /// <summary>
        /// 计算用户排名
        /// </summary>
        public void YardstickOpen()
        {
            if (Comprise.Query == ActivityState.NotUnlock || Comprise.Query == ActivityState.NotOpen)
            {
                return;
            }

            if (Full.LossFreelyTomb == -1 || Full.Fever == null)
            {
                Full.LossFreelyTomb = Comprise.SwellTomb;
                // 用户列表
                Full.Fever = new();
                DisuseDead.ScholarLapis(OpenAmid.Instance.BulkThumb);
                for (int i = 0; i < Lug_Vibrant; i++)
                {
                    Full.Fever.Add(new RankUser(i, null, OpenAmid.Instance.BulkThumb[i], 0, -1));
                }
            }

            if (Full.Shaman)
            {
                // 如果活动已结束，排名已锁定，排行榜不再变化
                WeOpen = new(Full.WeVitamin, null, "You", BustFir, -1);
                WeOpen.BetrayChimp = WeOpen.Vibrant < BetrayOngoing.Count ? BetrayOngoing[WeOpen.Vibrant] : -1;

                TangFinnish.Instance.HalfTang();
                return;
            }

            // 计算其他用户的item_num
            long now = CoreDead.Slander();
            int startSeconds = (int)(Mathf.Min(now, Comprise.AgeTomb) - Comprise.SwellTomb);    // 活动开始时长
            int GuessEngrave= Comprise.Slippery;  // 活动总时长
            int deltaSeconds = (int)(CoreDead.Slander() - Full.LossFreelyTomb);
            int deltaItemNum = deltaSeconds * Orderly[0].Fire_Rib / GuessEngrave;
            foreach (RankUser user in Full.Fever)
            {
                user.FireFir += Random.Range(0, deltaItemNum);
            }
            // 排序
            Full.Fever.Sort((a, b) => b.FireFir.CompareTo(a.FireFir));
            Full.LossFreelyTomb = CoreDead.Slander();

            // 计算“我的”排名
            int WeVitamin= int.MaxValue;
            // 先计算“我的”itemNum在用户表中的排名
            for (int i = 0; i < Full.Fever.Count; i++)
            {
                if (Full.FireFir >= Full.Fever[i].FireFir)
                {
                    WeVitamin = i;
                    break;
                }
            }

            // 根据奖励配置中每个档位的最大、最小资源数，计算“我的”资源数是否在奖励配置范围内
            if (Full.FireFir > 0)
            {
                foreach (int rewardIndex in BetrayOngoing)
                {
                    RankRewardDB Betray= Orderly[rewardIndex];
                    int minItemNum = startSeconds * Betray.Fire_Rib / GuessEngrave;
                    if (BustFir >= minItemNum && WeVitamin == int.MaxValue)
                    {
                        WeVitamin = Mathf.Clamp(WeVitamin, Orderly[rewardIndex].But_Move - 1, Orderly[rewardIndex].Lug_Move - 1);
                        break;
                    }
                }
            }

            // 根据“我的”排名和item_num，调整其他用户item_num
            if (WeVitamin != int.MaxValue)
            {
                int lastItemNum = BustFir;
                for (int i = WeVitamin - 1; i >= 0; i--)
                {
                    if (Full.Fever[i].FireFir <= lastItemNum)
                    {
                        lastItemNum = Random.Range(lastItemNum + 1, (int)(lastItemNum * 1.2f));
                        Full.Fever[i].FireFir = lastItemNum;
                    }
                }
                lastItemNum = BustFir;
                for (int i = WeVitamin; i < Full.Fever.Count; i++)
                {
                    if (Full.Fever[i].FireFir > lastItemNum)
                    {
                        lastItemNum = Mathf.Max(0, Random.Range((int)(lastItemNum * 0.9), lastItemNum));
                        Full.Fever[i].FireFir = lastItemNum;
                    }
                }
                Full.Fever.Sort((a, b) => b.FireFir.CompareTo(a.FireFir));
            }
            for (int i = 0; i < Full.Fever.Count; i++)
            {
                if (WeVitamin == int.MaxValue && i < Full.Fever.Count - 1 && BustFir >= Full.Fever[i + 1].FireFir)
                {
                    WeVitamin = i + 1;
                }
                Full.Fever[i].Vibrant = Full.Fever[i].FireFir > BustFir ? i : i + 1;
                Full.Fever[i].BetrayChimp = Full.Fever[i].Vibrant < BetrayOngoing.Count ? BetrayOngoing[i] : -1;
            }
            Full.WeVitamin = WeVitamin;
            WeOpen = new(WeVitamin, null, "You", BustFir, -1);
            WeOpen.BetrayChimp = WeOpen.Vibrant < BetrayOngoing.Count ? BetrayOngoing[WeOpen.Vibrant] : -1;

            if (Comprise.Query == ActivityState.NeedSettlement || Comprise.Query == ActivityState.Finished)
            {
                Full.Shaman = true;
            }
            TangFinnish.Instance.HalfTang();
        }


        /// <summary>
        /// 获取某个排名名次的奖励
        /// </summary>
        /// <param name="ranking"></param>
        /// <returns></returns>
        public List<BustAffix> RatLeaderMeVitamin(int ranking)
        {
            return BetrayOngoing.Count > ranking ? NitrogenAmid.Instance.RatBustAffixMeNo(Orderly[BetrayOngoing[ranking]].Euphrates_He) : null;
        }


        /// <summary>
        /// 结算
        /// </summary>
        /// <returns></returns>
        public List<BustAffix> Discontent()
        {
            if (Comprise != null)
            {
                Comprise.Discontent();
            }
            List<BustAffix> Orderly= RatLeaderMeVitamin(WeOpen.Vibrant);
            NitrogenAmid.Instance.RunBustAffix(Orderly);

            PieceTang();

            return Orderly;
        }

        /// <summary>
        /// 活动结束后，清档
        /// </summary>
        private void PieceTang()
        {
            Full.FireFir = 0;
            Full.NetworkGeniusPlace = Comprise.GeniusPlace;
            Full.LossFreelyTomb = -1;
            Full.Fever = null;
            Full.WeVitamin = int.MaxValue;
            Full.Shaman = false;
            if (Aloft_Fire)
            {
                NitrogenAmid.Instance.PinBustMuddy(Fire_id, 0);
            }
            TangFinnish.Instance.HalfTang();
        }
    }

    public class RankUser
    {
        public int Vibrant; // 排名，从0开始
        public string Ribbon;   // 头像
        public string BulkForm; // 用户名
        public int FireFir;     // 用户资源数量
        public int BetrayChimp; // 奖励索引，-1表示没有奖励
        public List<BustAffix> Orderly; // 奖励

        public RankUser()
        {
        }

        public RankUser(int ranking, string avatar, string userName, int itemNum, int rewardIndex = -1)
        {
            this.Vibrant = ranking;
            this.Ribbon = avatar;
            this.BulkForm = userName;
            this.FireFir = itemNum;
            this.BetrayChimp = rewardIndex;
        }
    }

}

