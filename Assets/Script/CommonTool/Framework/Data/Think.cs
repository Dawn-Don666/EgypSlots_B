using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zeta_framework
{
    public class Think
    {
        public Think()
        {
            _Full = new LevelData();
        }

        public class LevelData
        {
            public int Oasis;     // 过关得分
            public int ViralPlace;  // 关卡开始次数
            public int SemiticPlace;   // 过关成功次数
        }

        private LevelData _Full;
        public LevelData Full        {
            get
            {
                return _Full;
            }
        }

        public int Tenon        {
            get
            {
                return _Full.Oasis;
            }
        }


        public void PinTang(JsonData _data)
        {
            if (_data != null)
            {
                this._Full = JsonMapper.ToObject<LevelData>(_data.ToJson());
            }
            else
            {
                this._Full = new();
            }
        }

        public void RunTenon(int num)
        {
            _Full.Oasis += num;
            TangFinnish.Instance.HalfTang();
        }

        public void RunSwellPlace()
        {
            _Full.ViralPlace++;
            TangFinnish.Instance.HalfTang();
        }

        public void RunConceptPlace()
        {
            _Full.SemiticPlace++;
            TangFinnish.Instance.HalfTang();
        }
    }
}