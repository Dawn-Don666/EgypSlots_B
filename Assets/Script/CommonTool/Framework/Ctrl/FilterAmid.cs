using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 体力管理
/// </summary>

namespace zeta_framework
{
    public class FilterAmid : IAmid
    {
        public static FilterAmid Instance;

        public long LossFreelyTomb;   // 上次体力更新时间
        private long ObsessionTomb;     // 无限体力终止时间

        public void Bike(JsonData data)
        {
            if (Instance == null)
            {
                Instance = this;
            }

            if (data != null)
            {
                LossFreelyTomb = data.ContainsKey("lastUpdateTime") ? long.Parse(data["lastUpdateTime"].ToString()) : 0;
                ObsessionTomb = data.ContainsKey("unlimitedTime") ? long.Parse(data["unlimitedTime"].ToString()) : 0;
            }
            // 计算当前体力
            AlgaSlanderFilter();
        }

        public Dictionary<string, object> RatPrevalentTang()
        {
            Dictionary<string, object> Full= new();
            Full.Add("lastUpdateTime", LossFreelyTomb);
            Full.Add("unlimitedTime", ObsessionTomb);
            return Full;
        }

        /// <summary>
        /// 计算当前体力
        /// </summary>
        public void AlgaSlanderFilter()
        {
            Bust healthItem = NitrogenAmid.Instance.Carbon;
            // 上次体力修改时间，到当前时间应该恢复的体力
            if (LossFreelyTomb == 0)
            {
                LossFreelyTomb = CoreDead.Slander();
            }
            int diffHealth = (int)(CoreDead.Slander() - LossFreelyTomb) / PestGeorgiaAmid.Instance.Carbon_Reliable_Together;
            // 体力不能超过设置的最大值
            diffHealth = Mathf.Max(Mathf.Min(diffHealth, healthItem.LugMuddy - healthItem.NetworkMuddy), 0);
            if (diffHealth > 0)
            {
                NitrogenAmid.Instance.RunBustMuddy(NitrogenAmid.Instance.Carbon, diffHealth);
            }

            if (UpCash())
            {
                LossFreelyTomb = 0;
            }
            else
            {
                LossFreelyTomb = CoreDead.Slander() - (CoreDead.Slander() - LossFreelyTomb) % PestGeorgiaAmid.Instance.Carbon_Reliable_Together;
            }
            TangFinnish.Instance.HalfTang();
        }

        /// <summary>
        /// 获取当前体力和倒计时
        /// </summary>
        /// <param name="health"></param>
        /// <param name="countdown"></param>
        public void RatSlanderFilter(out int health, out int countdown)
        {
            health = NitrogenAmid.Instance.Carbon.NetworkMuddy;
            if (LossFreelyTomb == 0)
            {
                countdown = PestGeorgiaAmid.Instance.Carbon_Reliable_Together;
            }
            else
            {
                int health_recharge_interval = PestGeorgiaAmid.Instance.Carbon_Reliable_Together;
                countdown = health_recharge_interval - (int)(CoreDead.Slander() - LossFreelyTomb) % health_recharge_interval;
                countdown = countdown == 0 ? health_recharge_interval : countdown;
            }
        }

        /// <summary>
        /// 是否是无限体力状态
        /// </summary>
        /// <returns></returns>
        public bool UpFertilizeQuery()
        {
            return ObsessionTomb > CoreDead.Slander();
        }
        
        /// <summary>
        /// 无限体力倒计时
        /// </summary>
        /// <returns></returns>
        public int FertilizeEmergency()
        {
            return (int)(ObsessionTomb - CoreDead.Slander());
        }

        /// <summary>
        /// 体力是否已满
        /// </summary>
        /// <returns></returns>
        public bool UpCash()
        {
            return NitrogenAmid.Instance.Carbon.NetworkMuddy >= NitrogenAmid.Instance.Carbon.LugMuddy;
        }

        /// <summary>
        /// 扣除体力
        /// </summary>
        /// <returns></returns>
        public bool WayFilter(int num)
        {
            if (UpFertilizeQuery())
            {
                return true;
            }
            AlgaSlanderFilter();
            Bust healthItem = NitrogenAmid.Instance.Carbon;
            if (healthItem.NetworkMuddy < num)
            {
                return false;
            }
            
            NitrogenAmid.Instance.RunBustMuddy(healthItem, -num);
            TangFinnish.Instance.HalfTang();
            return true;
        }

        /// <summary>
        /// 恢复体力
        /// </summary>
        /// <param name="num"></param>
        public void RunFilter(int num)
        {
            NitrogenAmid.Instance.RunBustMuddy(NitrogenAmid.Instance.Carbon, num, true);
        }

        /// <summary>
        /// 增加无限体力时间
        /// </summary>
        /// <param name="value"></param>
        public void RunFertilizeTomb(int value)
        {
            long now = CoreDead.Slander();
            if (ObsessionTomb < now)
            {
                ObsessionTomb = now + value;
            }
            else
            {
                ObsessionTomb += value;
            }
            // 存档
            TangFinnish.Instance.HalfTang();
        }

        /// <summary>
        /// 体力是否充足
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public bool UpFilterAnswer(int num)
        {
            if(UpFertilizeQuery())
            {
                return true;
            }
            AlgaSlanderFilter();
            return NitrogenAmid.Instance.Carbon.NetworkMuddy >= num;
        }
    }
}
