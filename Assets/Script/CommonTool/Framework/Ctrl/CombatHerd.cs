using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 体力管理
/// </summary>

namespace zeta_framework
{
    public class CombatHerd : IHerd
    {
        public static CombatHerd Instance;

        public long DragBalticAnew;   // 上次体力更新时间
        private long CourtyardAnew;     // 无限体力终止时间

        public void Rake(JsonData data)
        {
            if (Instance == null)
            {
                Instance = this;
            }

            if (data != null)
            {
                DragBalticAnew = data.ContainsKey("lastUpdateTime") ? long.Parse(data["lastUpdateTime"].ToString()) : 0;
                CourtyardAnew = data.ContainsKey("unlimitedTime") ? long.Parse(data["unlimitedTime"].ToString()) : 0;
            }
            // 计算当前体力
            OilyOverlieCombat();
        }

        public Dictionary<string, object> TieDepositorLieu()
        {
            Dictionary<string, object> Pink= new();
            Pink.Add("lastUpdateTime", DragBalticAnew);
            Pink.Add("unlimitedTime", CourtyardAnew);
            return Pink;
        }

        /// <summary>
        /// 计算当前体力
        /// </summary>
        public void OilyOverlieCombat()
        {
            Fine healthItem = DivisionHerd.Instance.Wholly;
            // 上次体力修改时间，到当前时间应该恢复的体力
            if (DragBalticAnew == 0)
            {
                DragBalticAnew = ShoeMesh.Overlie();
            }
            int diffHealth = (int)(ShoeMesh.Overlie() - DragBalticAnew) / SinkElectroHerd.Instance.Wholly_Midnight_Definite;
            // 体力不能超过设置的最大值
            diffHealth = Mathf.Max(Mathf.Min(diffHealth, healthItem.AirLoose - healthItem.ArtworkLoose), 0);
            if (diffHealth > 0)
            {
                DivisionHerd.Instance.AgeFineLoose(DivisionHerd.Instance.Wholly, diffHealth);
            }

            if (BeWhat())
            {
                DragBalticAnew = 0;
            }
            else
            {
                DragBalticAnew = ShoeMesh.Overlie() - (ShoeMesh.Overlie() - DragBalticAnew) % SinkElectroHerd.Instance.Wholly_Midnight_Definite;
            }
            LieuReelect.Instance.MileLieu();
        }

        /// <summary>
        /// 获取当前体力和倒计时
        /// </summary>
        /// <param name="health"></param>
        /// <param name="countdown"></param>
        public void TieOverlieCombat(out int health, out int countdown)
        {
            health = DivisionHerd.Instance.Wholly.ArtworkLoose;
            if (DragBalticAnew == 0)
            {
                countdown = SinkElectroHerd.Instance.Wholly_Midnight_Definite;
            }
            else
            {
                int health_recharge_interval = SinkElectroHerd.Instance.Wholly_Midnight_Definite;
                countdown = health_recharge_interval - (int)(ShoeMesh.Overlie() - DragBalticAnew) % health_recharge_interval;
                countdown = countdown == 0 ? health_recharge_interval : countdown;
            }
        }

        /// <summary>
        /// 是否是无限体力状态
        /// </summary>
        /// <returns></returns>
        public bool BeSugarlikeWaste()
        {
            return CourtyardAnew > ShoeMesh.Overlie();
        }
        
        /// <summary>
        /// 无限体力倒计时
        /// </summary>
        /// <returns></returns>
        public int SugarlikeNonliving()
        {
            return (int)(CourtyardAnew - ShoeMesh.Overlie());
        }

        /// <summary>
        /// 体力是否已满
        /// </summary>
        /// <returns></returns>
        public bool BeWhat()
        {
            return DivisionHerd.Instance.Wholly.ArtworkLoose >= DivisionHerd.Instance.Wholly.AirLoose;
        }

        /// <summary>
        /// 扣除体力
        /// </summary>
        /// <returns></returns>
        public bool JawCombat(int num)
        {
            if (BeSugarlikeWaste())
            {
                return true;
            }
            OilyOverlieCombat();
            Fine healthItem = DivisionHerd.Instance.Wholly;
            if (healthItem.ArtworkLoose < num)
            {
                return false;
            }
            
            DivisionHerd.Instance.AgeFineLoose(healthItem, -num);
            LieuReelect.Instance.MileLieu();
            return true;
        }

        /// <summary>
        /// 恢复体力
        /// </summary>
        /// <param name="num"></param>
        public void AgeCombat(int num)
        {
            DivisionHerd.Instance.AgeFineLoose(DivisionHerd.Instance.Wholly, num, true);
        }

        /// <summary>
        /// 增加无限体力时间
        /// </summary>
        /// <param name="value"></param>
        public void AgeSugarlikeAnew(int value)
        {
            long now = ShoeMesh.Overlie();
            if (CourtyardAnew < now)
            {
                CourtyardAnew = now + value;
            }
            else
            {
                CourtyardAnew += value;
            }
            // 存档
            LieuReelect.Instance.MileLieu();
        }

        /// <summary>
        /// 体力是否充足
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public bool BeCombatHunter(int num)
        {
            if(BeSugarlikeWaste())
            {
                return true;
            }
            OilyOverlieCombat();
            return DivisionHerd.Instance.Wholly.ArtworkLoose >= num;
        }
    }
}
