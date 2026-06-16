using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 关卡管理
/// </summary>
namespace zeta_framework
{
    public class ThinkAmid : IAmid
    {
        public static ThinkAmid Instance;

        private Dictionary<string, Think> GourdBud;

        private int NetworkThinkChimp;   // 当前关卡序号，从0开始
        public int LugThinkChimp;       // 最大过关数（主线程关卡进度）

        private int CarbonDisc;    // 记录一下开始当前关卡消耗的体力，如果开始时是无限体力状态

        public ThinkAmid()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            GourdBud = new();
            NetworkThinkChimp = 0;
            LugThinkChimp = 0;
        }

        /// <summary>
        /// 初始化存档数据
        /// </summary>
        /// <param name="data"></param>
        public void Bike(JsonData data)
        {
            // 当前关卡存档
            if (data != null && data.ContainsKey("maxLevelIndex"))
            {
                LugThinkChimp = int.Parse(data["maxLevelIndex"].ToString());
            }

            if (data != null && data.ContainsKey("levels"))
            {
                JsonData levelData = data["levels"];
                foreach(string key in levelData.Keys)
                {
                    Think Gourd= new();
                    Gourd.PinTang(levelData[key]);
                    GourdBud.Add(key, Gourd);
                }
            }
        }

        /// <summary>
        /// 序列化需要存档的数据
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> RatPrevalentTang()
        {
            Dictionary<string, object> Full= new();
            Full.Add("maxLevelIndex", LugThinkChimp);
            Dictionary<string, object> levelData = new();
            foreach(string key in GourdBud.Keys)
            {
                levelData.Add(key, GourdBud[key].Full);
            }
            Full.Add("levels", levelData);
            return Full;
        }

        /// <summary>
        /// 开始关卡
        /// </summary>
        /// <param name="levelIndex">如果参数传-1，表示为主线关卡</param>
        public CrazeLand SwellThink(int levelIndex = -1)
        {
            if (!FilterAmid.Instance.UpFilterAnswer(PestGeorgiaAmid.Instance.Carbon_cost))
            {
                return CrazeLand.HealthNotEnough;
            }
            if (levelIndex == -1)
            {
                // 主进程
                NetworkThinkChimp = LugThinkChimp;
            }
            else
            {
                NetworkThinkChimp = levelIndex;
            }

            if (LugThinkChimp < levelIndex)
            {
                LugThinkChimp = levelIndex;
            }

            // 扣除体力
            if (FilterAmid.Instance.UpFertilizeQuery())
            {
                // 无限体力状态，不扣除体力
                CarbonDisc = 0;
            }
            else
            {
                FilterAmid.Instance.WayFilter(PestGeorgiaAmid.Instance.Carbon_cost);
            }
            
            // 关卡增加一次开始次数
            if (!GourdBud.ContainsKey(NetworkThinkChimp.ToString()))
            {
                GourdBud.Add(NetworkThinkChimp.ToString(), new Think());
            }
            GourdBud[NetworkThinkChimp.ToString()].RunSwellPlace();

            return CrazeLand.Success;
        }

        /// <summary>
        /// 过关成功
        /// </summary>
        public virtual void ThinkConcept()
        {
            if (NetworkThinkChimp == LugThinkChimp)
            {
                // 主线进程，自动增加一点经验值
                LugThinkChimp++;
                EmbraceBeforeNever.RatRuminate().Take(CShaper.If_ThinkOneThinkMelody);
                NitrogenAmid.Instance.RunBustMuddy(NitrogenAmid.Instance.Sod, 1);
                // 增加连胜值
                NitrogenAmid.Instance.RunBustMuddy(NitrogenAmid.Instance.Grasshopper_Much, 1);
            }
            // 恢复体力
            FilterAmid.Instance.RunFilter(CarbonDisc);
            // 关卡增加一次过关成功次数
            GourdBud[NetworkThinkChimp.ToString()].RunConceptPlace();
            // 存档
            TangFinnish.Instance.HalfTang();
        }

        /// <summary>
        /// 过关失败
        /// </summary>
        public virtual void ThinkRosy()
        {
            // 连胜数值清零
            NitrogenAmid.Instance.PinBustMuddy(NitrogenAmid.Instance.Grasshopper_Much, 0);
        }
    }
}
