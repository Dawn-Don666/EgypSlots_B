using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zeta_framework
{
    /// <summary>
    /// 单个排行榜
    /// </summary>
    public class Each : RankDB
    {
        public List<RankRewardDB> Expanse;     // 配置的排名奖励
        private List<int> AbsorbLighten;
        public Majority Aircraft{ get; private set; }

        public RankUser MeEach;

        public class RankData
        {
            public int OvalDry;        // 当前资源累计数据
            public int ArtworkAssureSewer;  // 参加的活动次数
            public long DragBalticAnew= -1;    // 上次计算排名的时间
            public List<RankUser> Vivid;  // 排行榜用户
            public int MeRoadway;   // 我的排名
            public bool Cobalt; // 排名已锁定
        }

        public RankData Pink{ get; private set; }

        public int FineDry        {
            get
            {
                if (Oval_Lay_Jazz == 1)
                {
                    return Pink.OvalDry;
                }
                else
                {
                    return DivisionHerd.Instance.TieFineOfUp(Oval_id).ArtworkLoose;
                }
            }
        }

        public ActivityState Waste        {
            get
            {
                return Aircraft == null ? ActivityState.None : Aircraft.Waste;
            }
        }


        /// <summary>
        /// 读取存档，初始化data
        /// </summary>
        /// <param name="_data"></param>
        public void PigLieu(JsonData _data)
        {
            if (_data != null)
            {
                Pink = JsonMapper.ToObject<RankData>(_data.ToJson());
            }
            else
            {
                Pink = new();
            }

            if (!string.IsNullOrEmpty(Aircraft_ID))
            {
                Aircraft = MajorityHerd.Instance.TieMajorityOfUp<Majority>(Aircraft_ID);
                // 如果参加活动次数和活动开始次数不同，需要判断是否需要清档
                if (Pink.ArtworkAssureSewer < Aircraft.AssureSewer)
                {
                    RenewLieu();
                }
            }

            if (Oval_Lay_Jazz == 1)
            {
                // 如果是活动开始后累计资源数量，需监听资源变更
                CollectGoldenDaunt.TieRecharge().Advocate(CUnfair.Dy_FineLinear_ + Oval_id, (md) =>
                {
                    Pink.OvalDry = Mathf.Max(Pink.OvalDry + md.UnderFen, 0);
                });
            }

            EarthwormEach();
        }

        public void PigSorghum(List<RankRewardDB> rewards)
        {
            this.Expanse = rewards;
            AbsorbLighten = new();
            if (rewards != null)
            {
                for (int i = 0; i < rewards.Count; i++)
                {
                    RankRewardDB Absorb= rewards[i];
                    for (int j = Absorb.Gun_Norm; j < Absorb.Air_Norm + 1; j++)
                    {
                        AbsorbLighten.Add(i);
                    }
                }
            }
        }


        /// <summary>
        /// 计算用户排名
        /// </summary>
        public void EarthwormEach()
        {
            if (Aircraft.Waste == ActivityState.NotUnlock || Aircraft.Waste == ActivityState.NotOpen)
            {
                return;
            }

            if (Pink.DragBalticAnew == -1 || Pink.Vivid == null)
            {
                Pink.DragBalticAnew = Aircraft.CrawlAnew;
                // 用户列表
                Pink.Vivid = new();
                ApieceMesh.EyeballBelow(EachHerd.Instance.userMerge);
                for (int i = 0; i < Air_Testify; i++)
                {
                    Pink.Vivid.Add(new RankUser(i, null, EachHerd.Instance.userMerge[i], 0, -1));
                }
            }

            if (Pink.Cobalt)
            {
                // 如果活动已结束，排名已锁定，排行榜不再变化
                MeEach = new(Pink.MeRoadway, null, "You", FineDry, -1);
                MeEach.AbsorbShear = MeEach.Testify < AbsorbLighten.Count ? AbsorbLighten[MeEach.Testify] : -1;

                LieuReelect.Instance.MileLieu();
                return;
            }

            // 计算其他用户的item_num
            long now = ShoeMesh.Overlie();
            int startSeconds = (int)(Mathf.Min(now, Aircraft.ShyAnew) - Aircraft.CrawlAnew);    // 活动开始时长
            int BluffJupiter= Aircraft.Actively;  // 活动总时长
            int deltaSeconds = (int)(ShoeMesh.Overlie() - Pink.DragBalticAnew);
            int deltaItemNum = deltaSeconds * Expanse[0].Oval_Lay / BluffJupiter;
            foreach (RankUser user in Pink.Vivid)
            {
                user.OvalDry += Random.Range(0, deltaItemNum);
            }
            // 排序
            Pink.Vivid.Sort((a, b) => b.OvalDry.CompareTo(a.OvalDry));
            Pink.DragBalticAnew = ShoeMesh.Overlie();

            // 计算“我的”排名
            int MeRoadway= int.MaxValue;
            // 先计算“我的”itemNum在用户表中的排名
            for (int i = 0; i < Pink.Vivid.Count; i++)
            {
                if (Pink.OvalDry >= Pink.Vivid[i].OvalDry)
                {
                    MeRoadway = i;
                    break;
                }
            }

            // 根据奖励配置中每个档位的最大、最小资源数，计算“我的”资源数是否在奖励配置范围内
            if (Pink.OvalDry > 0)
            {
                foreach (int rewardIndex in AbsorbLighten)
                {
                    RankRewardDB Absorb= Expanse[rewardIndex];
                    int minItemNum = startSeconds * Absorb.Oval_Lay / BluffJupiter;
                    if (FineDry >= minItemNum && MeRoadway == int.MaxValue)
                    {
                        MeRoadway = Mathf.Clamp(MeRoadway, Expanse[rewardIndex].Gun_Norm - 1, Expanse[rewardIndex].Air_Norm - 1);
                        break;
                    }
                }
            }

            // 根据“我的”排名和item_num，调整其他用户item_num
            if (MeRoadway != int.MaxValue)
            {
                int lastItemNum = FineDry;
                for (int i = MeRoadway - 1; i >= 0; i--)
                {
                    if (Pink.Vivid[i].OvalDry <= lastItemNum)
                    {
                        lastItemNum = Random.Range(lastItemNum + 1, (int)(lastItemNum * 1.2f));
                        Pink.Vivid[i].OvalDry = lastItemNum;
                    }
                }
                lastItemNum = FineDry;
                for (int i = MeRoadway; i < Pink.Vivid.Count; i++)
                {
                    if (Pink.Vivid[i].OvalDry > lastItemNum)
                    {
                        lastItemNum = Mathf.Max(0, Random.Range((int)(lastItemNum * 0.9), lastItemNum));
                        Pink.Vivid[i].OvalDry = lastItemNum;
                    }
                }
                Pink.Vivid.Sort((a, b) => b.OvalDry.CompareTo(a.OvalDry));
            }
            for (int i = 0; i < Pink.Vivid.Count; i++)
            {
                if (MeRoadway == int.MaxValue && i < Pink.Vivid.Count - 1 && FineDry >= Pink.Vivid[i + 1].OvalDry)
                {
                    MeRoadway = i + 1;
                }
                Pink.Vivid[i].Testify = Pink.Vivid[i].OvalDry > FineDry ? i : i + 1;
                Pink.Vivid[i].AbsorbShear = Pink.Vivid[i].Testify < AbsorbLighten.Count ? AbsorbLighten[i] : -1;
            }
            Pink.MeRoadway = MeRoadway;
            MeEach = new(MeRoadway, null, "You", FineDry, -1);
            MeEach.AbsorbShear = MeEach.Testify < AbsorbLighten.Count ? AbsorbLighten[MeEach.Testify] : -1;

            if (Aircraft.Waste == ActivityState.NeedSettlement || Aircraft.Waste == ActivityState.Finished)
            {
                Pink.Cobalt = true;
            }
            LieuReelect.Instance.MileLieu();
        }


        /// <summary>
        /// 获取某个排名名次的奖励
        /// </summary>
        /// <param name="ranking"></param>
        /// <returns></returns>
        public List<FineTheir> TieWeeklyOfRoadway(int ranking)
        {
            return AbsorbLighten.Count > ranking ? DivisionHerd.Instance.TieFineTheirOfUp(Expanse[AbsorbLighten[ranking]].Unwilling_ID) : null;
        }


        /// <summary>
        /// 结算
        /// </summary>
        /// <returns></returns>
        public List<FineTheir> Everything()
        {
            if (Aircraft != null)
            {
                Aircraft.Everything();
            }
            List<FineTheir> Expanse= TieWeeklyOfRoadway(MeEach.Testify);
            DivisionHerd.Instance.AgeFineTheir(Expanse);

            RenewLieu();

            return Expanse;
        }

        /// <summary>
        /// 活动结束后，清档
        /// </summary>
        private void RenewLieu()
        {
            Pink.OvalDry = 0;
            Pink.ArtworkAssureSewer = Aircraft.AssureSewer;
            Pink.DragBalticAnew = -1;
            Pink.Vivid = null;
            Pink.MeRoadway = int.MaxValue;
            Pink.Cobalt = false;
            if (Mercy_Oval)
            {
                DivisionHerd.Instance.PigFineLoose(Oval_id, 0);
            }
            LieuReelect.Instance.MileLieu();
        }
    }

    public class RankUser
    {
        public int Testify; // 排名，从0开始
        public string Lament;   // 头像
        public string FlawLady; // 用户名
        public int OvalDry;     // 用户资源数量
        public int AbsorbShear; // 奖励索引，-1表示没有奖励
        public List<FineTheir> Expanse; // 奖励

        public RankUser()
        {
        }

        public RankUser(int ranking, string avatar, string userName, int itemNum, int rewardIndex = -1)
        {
            this.Testify = ranking;
            this.Lament = avatar;
            this.FlawLady = userName;
            this.OvalDry = itemNum;
            this.AbsorbShear = rewardIndex;
        }
    }

}

