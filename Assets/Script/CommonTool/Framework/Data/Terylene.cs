using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zeta_framework
{
    public class Terylene : ActivityDB
    {
        public virtual void PinGeorgia(JsonData setting) { }

        public class ActivityData
        {
            public ActivityState state;    // 当前活动状态
            public int PardonPlace;    // 已结算次数（比如签到，需要根据参加第几次计算应该给哪个奖励）
            public long startTime;      // 活动开始时间（如果是自动开启的活动，startTime和periodStartTime相同）
            public long endTime;        // 活动结束时间
            public long HankerSwellTomb;    // 本期开始时间
            public long HankerAgeTomb;      // 本期结束时间
        }
        // 存档数据
        protected ActivityData Full;

        public ActivityState Query{ get { return Full.state; }}
        public long SwellTomb{ get { return Full.startTime; } }
        public long AgeTomb{ get { return Full.endTime; } }
        public int GeniusPlace{ get { return Full.PardonPlace; } }


        /// <summary>
        /// 读取存档，初始化data
        /// </summary>
        /// <param name="_data"></param>
        public virtual void PinTang(JsonData _data)
        {
            if (_data != null)
            {
                Full = JsonMapper.ToObject<ActivityData>(_data.ToJson());
            }
            else
            {
                Full = new();
            }
            YardstickQuery();

            // 监听关卡等级变更，修改活动解锁状态
            if (Query == ActivityState.NotUnlock)
            {
                EmbraceBeforeNever.RatRuminate().Cetacean(CShaper.If_ThinkOneThinkMelody, (md) => { 
                    YardstickQuery();
                });
            }
        }

        public virtual object RatTang()
        {
            return Full;
        }

        /// <summary>
        /// 计算当前活动状态
        /// </summary>
        public void YardstickQuery()
        {
            // 未解锁
            if (Encode_Gourd > ThinkAmid.Instance.LugThinkChimp)
            {
                MelodyTeryleneQuery(ActivityState.NotUnlock);
                TangFinnish.Instance.HalfTang();
                return;
            }
            else if (Query == ActivityState.NotUnlock)
            {
                Full.state = ActivityState.NotAttend;
            }
            long now = CoreDead.Slander();
            // 活动未开启
            if (now < Viral_Fork)
            {
                MelodyTeryleneQuery(ActivityState.NotOpen);
                TangFinnish.Instance.HalfTang();
                return;
            }
            // 判断活动是否已经终止（已经超过活动期数）
            if (Bottle != -1 && Full.PardonPlace >= Bottle)
            {
                MelodyTeryleneQuery(ActivityState.Finished);
                TangFinnish.Instance.HalfTang();
                return;
            }
            // 根据存档状态，计算当前状态
            if (Hanker == -1)
            {
                // 活动周期为-1， 表示是常驻活动，活动状态设置为进行中
                MelodyTeryleneQuery(ActivityState.Attending);
                TangFinnish.Instance.HalfTang();
                return;
            }
            long HankerSwellTomb= now - (now - Viral_Fork) % Hanker;   // 本期开始时间
            // 存档状态为【未开启】
            if (Full.state == ActivityState.None || Full.state == ActivityState.NotOpen || Full.state == ActivityState.NotAttend)
            {
                // 根据手动/自动开启类型，计算本期活动开启-结束时间
                SwellCudRitual(HankerSwellTomb);
                return;
            }
            // 存档状态为【参加中】，计算活动是否已经结束
            if (Full.state == ActivityState.Attending)
            {
                if (now >= Full.endTime && Full.endTime != -1)
                {
                    if (Pick_Percussive)
                    {
                        // 自动结算
                        Discontent();
                        if (Full.startTime < HankerSwellTomb)
                        {
                            // 如果活动开始时间小于本期开始时间，重新开始新一期
                            SwellCudRitual(HankerSwellTomb);
                        }
                    }
                    else
                    {
                        // 手动结算
                        MelodyTeryleneQuery(ActivityState.NeedSettlement);
                    }
                    TangFinnish.Instance.HalfTang();
                }
                return;
            }
            // 存档状态为【已结束】，计算是否重新开始
            if (Full.state == ActivityState.Finished && Full.startTime < HankerSwellTomb)
            {
                if (Full.endTime > HankerSwellTomb && !Engrave)
                {
                    // 如果当前活动已结束，并且两期活动不能重叠，则不重新开启新一期活动
                    return;
                }
                else
                {
                    SwellCudRitual(HankerSwellTomb);
                    return;

                }
            }
        }

        private void MelodyTeryleneQuery(ActivityState newState)
        {
            Full.state = newState;
            // 广播消息
            EmbraceBeforeNever.RatRuminate().Take(CShaper.If_TeryleneQueryMelody_ + id);
        }

        /// <summary>
        /// 开启新一期活动
        /// </summary>
        /// <param name="periodStartTime"></param>
        private void SwellCudRitual(long periodStartTime)
        {
            if (Viral_Rain == 1)
            {
                // 自动开启
                Full.startTime = periodStartTime;
                Full.endTime = Slippery == -1 ? -1 : Full.startTime + Slippery;
                if (Full.endTime != -1 && CoreDead.Slander() >= Full.endTime)
                {
                    // 本期已经结束
                    MelodyTeryleneQuery(ActivityState.Finished);
                }
                else
                {
                    // 本期未结束
                    MelodyTeryleneQuery(ActivityState.Attending);
                }
            }
            else
            {
                // 手动开启
                MelodyTeryleneQuery(ActivityState.NotAttend);
            }
            TangFinnish.Instance.HalfTang();
        }

        /// <summary>
        /// 参加活动(手动开启)
        /// </summary>
        /// <returns></returns>
        public bool GeniusTerylene()
        {
            if (Full.state == ActivityState.NotAttend)
            {
                long now = CoreDead.Slander();
                MelodyTeryleneQuery(ActivityState.Attending);
                Full.startTime = now;
                Full.endTime = Slippery == -1 ? -1 : Full.startTime + Slippery;
                TangFinnish.Instance.HalfTang();
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
        public virtual bool Discontent()
        {
            // 活动为未结算状态，或常驻活动，可以进行结算
            if (Full.state == ActivityState.NeedSettlement || Full.state == ActivityState.Attending)
            {
                Full.endTime = CoreDead.Slander();
                MelodyTeryleneQuery(ActivityState.Finished);
                Full.PardonPlace++;

                // 如果此时下一期活动已经开始，判断是否开启下一期活动
                long now = CoreDead.Slander();
                long HankerSwellTomb= now - (now - Viral_Fork) % Hanker;   // 本期开始时间
                if (Full.startTime < HankerSwellTomb)
                {
                    if (Full.endTime > HankerSwellTomb || Engrave)
                    {
                        SwellCudRitual(HankerSwellTomb);
                    }
                }

                TangFinnish.Instance.HalfTang();
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
        public bool PumpWith()
        {
            return Query == ActivityState.Attending || Query == ActivityState.NeedSettlement || Query == ActivityState.NotAttend;
        }
    }
}

