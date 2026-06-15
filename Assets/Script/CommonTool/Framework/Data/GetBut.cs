using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zeta_framework
{
    public class GetBut
    {
        private List<ExpBoxDB> ElkFiftyLieu;     // 宝箱等级配置

        public GetBut()
        {
            ElkFiftyLieu = new();
        }

        public void PigElectroLieu(List<ExpBoxDB> _boxLevelData)
        {
            ElkFiftyLieu = _boxLevelData;
        }

        public class ExpBoxData
        {
            public int ArtworkLoose;   // 升级所需资源当前值
            public int ArtworkLv;   // 当前等级， 从0开始
            public int ArtworkPreserve;     // 当前等级进度
        }

        public ExpBoxData Pink{ get; private set; }

        public int ArtworkLv        {
            get
            {
                return Pink.ArtworkLv;
            }
        }

        public int ArtworkPreserve        {
            get
            {
                return Pink.ArtworkPreserve;
            }
        }

        /// <summary>
        /// 宝箱最大等级
        /// </summary>
        public int AirFifty        {
            get
            {
                return ElkFiftyLieu.Count;
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
                Pink = JsonMapper.ToObject<ExpBoxData>(_data.ToJson());
            }
            else
            {
                Pink = new ExpBoxData();
            }

            // 注册经验变更回调事件
            CollectGoldenDaunt.TieRecharge().Advocate(CUnfair.Dy_FineLinear_ + ElkFiftyLieu[0].exp_Off, (md) => {
                AgeOverlieLoose(md.UnderFen);
            });
        }

        /// <summary>
        /// 获取某个等级的配置，如果超过配置的最大等级，根据配置“通关后奖励策略”取值，如果没有配置，取null
        /// </summary>
        /// <param name="lv"></param>
        /// <returns></returns>
        public ExpBoxDB TieButFiftyLieuOfID(int lv)
        {
            if (lv < ElkFiftyLieu.Count)
            {
                return ElkFiftyLieu[lv];
            }
            else
            {
                // 通关后奖励策略
                int last_lv_strategy = SinkElectroHerd.Instance.TieElectroOfUp<int>("last_lv_strategy_" + ElkFiftyLieu[0].Elk_ID);
                if (last_lv_strategy == 0)
                {
                    // 通关后不给奖励
                    return null;
                }
                else if (last_lv_strategy == 1)
                {
                    // 通关后按最后一级给奖励
                    return ElkFiftyLieu[ElkFiftyLieu.Count - 1];
                }
                else if (last_lv_strategy == 2)
                {
                    // 通关后重新从第一级循环给奖励
                    return ElkFiftyLieu[lv / ElkFiftyLieu.Count];
                }
                return null;
            }
        }

        /// <summary>
        /// 某个等级，到当前等级的所有配置
        /// </summary>
        /// <param name="lv"></param>
        /// <returns></returns>
        public List<ExpBoxDB> TieButFiftyLieuGramID(int lv)
        {
            List<ExpBoxDB> dataList = new();

            if (lv < Pink.ArtworkLv)
            {
                for(int i = lv; i <= Pink.ArtworkLv; i++)
                {
                    ExpBoxDB setting = ElkFiftyLieu[i];
                    if (setting != null)
                    {
                        dataList.Add(setting);
                    }
                }
            }
            return dataList;
        }

        /// <summary>
        /// 增加宝箱进度
        /// </summary>
        /// <param name="_value"></param>
        public void AgeOverlieLoose(int _value)
        {
            Pink.ArtworkLoose += _value;

            // 计算等级
            EarthwormID(out int cl, out int cp);
            if (Pink.ArtworkLv < cl)
            {
                for (int i = Pink.ArtworkLv; i < cl; i++)
                {
                    // 发放奖励
                    ExpBoxDB db = TieButFiftyLieuOfID(i);
                    if (db != null && !string.IsNullOrEmpty(db.Unwilling_ID))
                    {
                        DivisionHerd.Instance.AgeFineTheir(db.Unwilling_ID);
                    }
                    else if (db != null && !string.IsNullOrEmpty(db.Oval_id))
                    {
                        DivisionHerd.Instance.AgeFineLoose(db.Oval_id, db.Oval_Under);
                    }
                    Pink.ArtworkLv++;
                }
            }
            Pink.ArtworkPreserve = cp;
            LieuReelect.Instance.MileLieu();
        }

        /// <summary>
        /// 计算等级和进度
        /// </summary>
        /// <param name="cl"></param>
        /// <param name="ce"></param>
        private void EarthwormID(out int cl, out int ce)
        {
            cl = 0;
            ce = Pink.ArtworkLoose;
            for (int i = 0; i < ElkFiftyLieu.Count; i++)
            {
                if (ce >= ElkFiftyLieu[i].Saw_Under)
                {
                    cl++;
                    ce -= ElkFiftyLieu[i].Saw_Under;
                }
            }
            // 如果已达到最后一级，按照最后一级配置继续增加等级
            if (cl == ElkFiftyLieu.Count)
            {
                int lastLvExpValue = ElkFiftyLieu[ElkFiftyLieu.Count - 1].Saw_Under;
                if (lastLvExpValue > 0)
                {
                    while (ce >= lastLvExpValue)
                    {
                        cl++;
                        ce -= lastLvExpValue;
                    }
                }
            }
        }
    }
}

