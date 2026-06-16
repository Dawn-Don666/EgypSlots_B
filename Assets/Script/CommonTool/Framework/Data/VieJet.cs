using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zeta_framework
{
    public class VieJet
    {
        private List<ExpBoxDB> AidThinkTang;     // 宝箱等级配置

        public VieJet()
        {
            AidThinkTang = new();
        }

        public void PinGeorgiaTang(List<ExpBoxDB> _boxLevelData)
        {
            AidThinkTang = _boxLevelData;
        }

        public class ExpBoxData
        {
            public int NetworkMuddy;   // 升级所需资源当前值
            public int NetworkOf;   // 当前等级， 从0开始
            public int NetworkAcoustic;     // 当前等级进度
        }

        public ExpBoxData Full{ get; private set; }

        public int NetworkOf        {
            get
            {
                return Full.NetworkOf;
            }
        }

        public int NetworkAcoustic        {
            get
            {
                return Full.NetworkAcoustic;
            }
        }

        /// <summary>
        /// 宝箱最大等级
        /// </summary>
        public int LugThink        {
            get
            {
                return AidThinkTang.Count;
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
                Full = JsonMapper.ToObject<ExpBoxData>(_data.ToJson());
            }
            else
            {
                Full = new ExpBoxData();
            }

            // 注册经验变更回调事件
            EmbraceBeforeNever.RatRuminate().Cetacean(CShaper.If_BustMelody_ + AidThinkTang[0].Sod_Jay, (md) => {
                RunSlanderMuddy(md.MiamiKea);
            });
        }

        /// <summary>
        /// 获取某个等级的配置，如果超过配置的最大等级，根据配置“通关后奖励策略”取值，如果没有配置，取null
        /// </summary>
        /// <param name="lv"></param>
        /// <returns></returns>
        public ExpBoxDB RatJetThinkTangMeOf(int lv)
        {
            if (lv < AidThinkTang.Count)
            {
                return AidThinkTang[lv];
            }
            else
            {
                // 通关后奖励策略
                int last_lv_strategy = PestGeorgiaAmid.Instance.RatGeorgiaMeNo<int>("last_lv_strategy_" + AidThinkTang[0].AHe_He);
                if (last_lv_strategy == 0)
                {
                    // 通关后不给奖励
                    return null;
                }
                else if (last_lv_strategy == 1)
                {
                    // 通关后按最后一级给奖励
                    return AidThinkTang[AidThinkTang.Count - 1];
                }
                else if (last_lv_strategy == 2)
                {
                    // 通关后重新从第一级循环给奖励
                    return AidThinkTang[lv / AidThinkTang.Count];
                }
                return null;
            }
        }

        /// <summary>
        /// 某个等级，到当前等级的所有配置
        /// </summary>
        /// <param name="lv"></param>
        /// <returns></returns>
        public List<ExpBoxDB> RatJetThinkTangDivaOf(int lv)
        {
            List<ExpBoxDB> dataList = new();

            if (lv < Full.NetworkOf)
            {
                for(int i = lv; i <= Full.NetworkOf; i++)
                {
                    ExpBoxDB setting = AidThinkTang[i];
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
        public void RunSlanderMuddy(int _value)
        {
            Full.NetworkMuddy += _value;

            // 计算等级
            YardstickOf(out int cl, out int cp);
            if (Full.NetworkOf < cl)
            {
                for (int i = Full.NetworkOf; i < cl; i++)
                {
                    // 发放奖励
                    ExpBoxDB db = RatJetThinkTangMeOf(i);
                    if (db != null && !string.IsNullOrEmpty(db.Euphrates_He))
                    {
                        NitrogenAmid.Instance.RunBustAffix(db.Euphrates_He);
                    }
                    else if (db != null && !string.IsNullOrEmpty(db.Fire_id))
                    {
                        NitrogenAmid.Instance.RunBustMuddy(db.Fire_id, db.Fire_Miami);
                    }
                    Full.NetworkOf++;
                }
            }
            Full.NetworkAcoustic = cp;
            TangFinnish.Instance.HalfTang();
        }

        /// <summary>
        /// 计算等级和进度
        /// </summary>
        /// <param name="cl"></param>
        /// <param name="ce"></param>
        private void YardstickOf(out int cl, out int ce)
        {
            cl = 0;
            ce = Full.NetworkMuddy;
            for (int i = 0; i < AidThinkTang.Count; i++)
            {
                if (ce >= AidThinkTang[i].Sod_Miami)
                {
                    cl++;
                    ce -= AidThinkTang[i].Sod_Miami;
                }
            }
            // 如果已达到最后一级，按照最后一级配置继续增加等级
            if (cl == AidThinkTang.Count)
            {
                int lastLvExpValue = AidThinkTang[AidThinkTang.Count - 1].Sod_Miami;
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

