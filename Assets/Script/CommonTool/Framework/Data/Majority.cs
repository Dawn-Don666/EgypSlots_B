using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zeta_framework
{
    public class Majority : ActivityDB
    {
        public virtual void PigElectro(JsonData setting) { }

        public class ActivityData
        {
            public ActivityState state;    // 当前活动状态
            public int ShadowSewer;    // 已结算次数（比如签到，需要根据参加第几次计算应该给哪个奖励）
            public long startTime;      // 活动开始时间（如果是自动开启的活动，startTime和periodStartTime相同）
            public long endTime;        // 活动结束时间
            public long EnzymeCrawlAnew;    // 本期开始时间
            public long EnzymeShyAnew;      // 本期结束时间
        }
        // 存档数据
        protected ActivityData Pink;

        public ActivityState Waste{ get { return Pink.state; }}
        public long CrawlAnew{ get { return Pink.startTime; } }
        public long ShyAnew{ get { return Pink.endTime; } }
        public int AssureSewer{ get { return Pink.ShadowSewer; } }


        /// <summary>
        /// 读取存档，初始化data
        /// </summary>
        /// <param name="_data"></param>
        public virtual void PigLieu(JsonData _data)
        {
            if (_data != null)
            {
                Pink = JsonMapper.ToObject<ActivityData>(_data.ToJson());
            }
            else
            {
                Pink = new();
            }
            EarthwormWaste();

            // 监听关卡等级变更，修改活动解锁状态
            if (Waste == ActivityState.NotUnlock)
            {
                CollectGoldenDaunt.TieRecharge().Advocate(CUnfair.Dy_FiftyTipFiftyLinear, (md) => { 
                    EarthwormWaste();
                });
            }
        }

        public virtual object TieLieu()
        {
            return Pink;
        }

        /// <summary>
        /// 计算当前活动状态
        /// </summary>
        public void EarthwormWaste()
        {
            // 未解锁
            if (Trader_Mayor > FiftyHerd.Instance.AirFiftyShear)
            {
                LinearMajorityWaste(ActivityState.NotUnlock);
                LieuReelect.Instance.MileLieu();
                return;
            }
            else if (Waste == ActivityState.NotUnlock)
            {
                Pink.state = ActivityState.NotAttend;
            }
            long now = ShoeMesh.Overlie();
            // 活动未开启
            if (now < Spill_time)
            {
                LinearMajorityWaste(ActivityState.NotOpen);
                LieuReelect.Instance.MileLieu();
                return;
            }
            // 判断活动是否已经终止（已经超过活动期数）
            if (Teapot != -1 && Pink.ShadowSewer >= Teapot)
            {
                LinearMajorityWaste(ActivityState.Finished);
                LieuReelect.Instance.MileLieu();
                return;
            }
            // 根据存档状态，计算当前状态
            if (Enzyme == -1)
            {
                // 活动周期为-1， 表示是常驻活动，活动状态设置为进行中
                LinearMajorityWaste(ActivityState.Attending);
                LieuReelect.Instance.MileLieu();
                return;
            }
            long EnzymeCrawlAnew= now - (now - Spill_time) % Enzyme;   // 本期开始时间
            // 存档状态为【未开启】
            if (Pink.state == ActivityState.None || Pink.state == ActivityState.NotOpen || Pink.state == ActivityState.NotAttend)
            {
                // 根据手动/自动开启类型，计算本期活动开启-结束时间
                CrawlJoySnugly(EnzymeCrawlAnew);
                return;
            }
            // 存档状态为【参加中】，计算活动是否已经结束
            if (Pink.state == ActivityState.Attending)
            {
                if (now >= Pink.endTime && Pink.endTime != -1)
                {
                    if (Yale_Experience)
                    {
                        // 自动结算
                        Everything();
                        if (Pink.startTime < EnzymeCrawlAnew)
                        {
                            // 如果活动开始时间小于本期开始时间，重新开始新一期
                            CrawlJoySnugly(EnzymeCrawlAnew);
                        }
                    }
                    else
                    {
                        // 手动结算
                        LinearMajorityWaste(ActivityState.NeedSettlement);
                    }
                    LieuReelect.Instance.MileLieu();
                }
                return;
            }
            // 存档状态为【已结束】，计算是否重新开始
            if (Pink.state == ActivityState.Finished && Pink.startTime < EnzymeCrawlAnew)
            {
                if (Pink.endTime > EnzymeCrawlAnew && !Compete)
                {
                    // 如果当前活动已结束，并且两期活动不能重叠，则不重新开启新一期活动
                    return;
                }
                else
                {
                    CrawlJoySnugly(EnzymeCrawlAnew);
                    return;

                }
            }
        }

        private void LinearMajorityWaste(ActivityState newState)
        {
            Pink.state = newState;
            // 广播消息
            CollectGoldenDaunt.TieRecharge().Tour(CUnfair.Dy_MajorityWasteLinear_ + id);
        }

        /// <summary>
        /// 开启新一期活动
        /// </summary>
        /// <param name="periodStartTime"></param>
        private void CrawlJoySnugly(long periodStartTime)
        {
            if (Spill_Jazz == 1)
            {
                // 自动开启
                Pink.startTime = periodStartTime;
                Pink.endTime = Actively == -1 ? -1 : Pink.startTime + Actively;
                if (Pink.endTime != -1 && ShoeMesh.Overlie() >= Pink.endTime)
                {
                    // 本期已经结束
                    LinearMajorityWaste(ActivityState.Finished);
                }
                else
                {
                    // 本期未结束
                    LinearMajorityWaste(ActivityState.Attending);
                }
            }
            else
            {
                // 手动开启
                LinearMajorityWaste(ActivityState.NotAttend);
            }
            LieuReelect.Instance.MileLieu();
        }

        /// <summary>
        /// 参加活动(手动开启)
        /// </summary>
        /// <returns></returns>
        public bool AssureMajority()
        {
            if (Pink.state == ActivityState.NotAttend)
            {
                long now = ShoeMesh.Overlie();
                LinearMajorityWaste(ActivityState.Attending);
                Pink.startTime = now;
                Pink.endTime = Actively == -1 ? -1 : Pink.startTime + Actively;
                LieuReelect.Instance.MileLieu();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 结算
        /// </summary>
        public virtual bool Everything()
        {
            // 活动为未结算状态，或常驻活动，可以进行结算
            if (Pink.state == ActivityState.NeedSettlement || Pink.state == ActivityState.Attending)
            {
                Pink.endTime = ShoeMesh.Overlie();
                LinearMajorityWaste(ActivityState.Finished);
                Pink.ShadowSewer++;

                // 如果此时下一期活动已经开始，判断是否开启下一期活动
                long now = ShoeMesh.Overlie();
                long EnzymeCrawlAnew= now - (now - Spill_time) % Enzyme;   // 本期开始时间
                if (Pink.startTime < EnzymeCrawlAnew)
                {
                    if (Pink.endTime > EnzymeCrawlAnew || Compete)
                    {
                        CrawlJoySnugly(EnzymeCrawlAnew);
                    }
                }

                LieuReelect.Instance.MileLieu();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 是否需要显示（进行中、未结算、还未参加状态，需要进行显示）
        /// </summary>
        /// <returns></returns>
        public bool GameSlow()
        {
            return Waste == ActivityState.Attending || Waste == ActivityState.NeedSettlement || Waste == ActivityState.NotAttend;
        }
    }
}

