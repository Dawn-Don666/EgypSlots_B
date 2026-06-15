using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 关卡管理
/// </summary>
namespace zeta_framework
{
    public class FiftyHerd : IHerd
    {
        public static FiftyHerd Instance;

        private Dictionary<string, Fifty> MayorRat;

        private int ArtworkFiftyShear;   // 当前关卡序号，从0开始
        public int AirFiftyShear;       // 最大过关数（主线程关卡进度）

        private int WhollyHawk;    // 记录一下开始当前关卡消耗的体力，如果开始时是无限体力状态

        public FiftyHerd()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            MayorRat = new();
            ArtworkFiftyShear = 0;
            AirFiftyShear = 0;
        }

        /// <summary>
        /// 初始化存档数据
        /// </summary>
        /// <param name="data"></param>
        public void Rake(JsonData data)
        {
            // 当前关卡存档
            if (data != null && data.ContainsKey("maxLevelIndex"))
            {
                AirFiftyShear = int.Parse(data["maxLevelIndex"].ToString());
            }

            if (data != null && data.ContainsKey("levels"))
            {
                JsonData levelData = data["levels"];
                foreach(string key in levelData.Keys)
                {
                    Fifty Mayor= new();
                    Mayor.PigLieu(levelData[key]);
                    MayorRat.Add(key, Mayor);
                }
            }
        }

        /// <summary>
        /// 序列化需要存档的数据
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> TieDepositorLieu()
        {
            Dictionary<string, object> Pink= new();
            Pink.Add("maxLevelIndex", AirFiftyShear);
            Dictionary<string, object> levelData = new();
            foreach(string key in MayorRat.Keys)
            {
                levelData.Add(key, MayorRat[key].Pink);
            }
            Pink.Add("levels", levelData);
            return Pink;
        }

        /// <summary>
        /// 开始关卡
        /// </summary>
        /// <param name="levelIndex">如果参数传-1，表示为主线关卡</param>
        public UsualDeaf CrawlFifty(int levelIndex = -1)
        {
            if (!CombatHerd.Instance.BeCombatHunter(SinkElectroHerd.Instance.Wholly_Mood))
            {
                return UsualDeaf.HealthNotEnough;
            }
            if (levelIndex == -1)
            {
                // 主进程
                ArtworkFiftyShear = AirFiftyShear;
            }
            else
            {
                ArtworkFiftyShear = levelIndex;
            }

            if (AirFiftyShear < levelIndex)
            {
                AirFiftyShear = levelIndex;
            }

            // 扣除体力
            if (CombatHerd.Instance.BeSugarlikeWaste())
            {
                // 无限体力状态，不扣除体力
                WhollyHawk = 0;
            }
            else
            {
                CombatHerd.Instance.JawCombat(SinkElectroHerd.Instance.Wholly_Mood);
            }
            
            // 关卡增加一次开始次数
            if (!MayorRat.ContainsKey(ArtworkFiftyShear.ToString()))
            {
                MayorRat.Add(ArtworkFiftyShear.ToString(), new Fifty());
            }
            MayorRat[ArtworkFiftyShear.ToString()].AgeCrawlSewer();

            return UsualDeaf.Success;
        }

        /// <summary>
        /// 过关成功
        /// </summary>
        public virtual void FiftyCurrier()
        {
            if (ArtworkFiftyShear == AirFiftyShear)
            {
                // 主线进程，自动增加一点经验值
                AirFiftyShear++;
                CollectGoldenDaunt.TieRecharge().Tour(CUnfair.Dy_FiftyTipFiftyLinear);
                DivisionHerd.Instance.AgeFineLoose(DivisionHerd.Instance.Saw, 1);
                // 增加连胜值
                DivisionHerd.Instance.AgeFineLoose(DivisionHerd.Instance.Devastation_Flag, 1);
            }
            // 恢复体力
            CombatHerd.Instance.AgeCombat(WhollyHawk);
            // 关卡增加一次过关成功次数
            MayorRat[ArtworkFiftyShear.ToString()].AgeCurrierSewer();
            // 存档
            LieuReelect.Instance.MileLieu();
        }

        /// <summary>
        /// 过关失败
        /// </summary>
        public virtual void FiftyCorp()
        {
            // 连胜数值清零
            DivisionHerd.Instance.PigFineLoose(DivisionHerd.Instance.Devastation_Flag, 0);
        }
    }
}
