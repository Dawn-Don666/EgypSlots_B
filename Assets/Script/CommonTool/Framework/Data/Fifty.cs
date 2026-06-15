using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zeta_framework
{
    public class Fifty
    {
        public Fifty()
        {
            _Pink = new LevelData();
        }

        public class LevelData
        {
            public int Proxy;     // 过关得分
            public int SpillSewer;  // 关卡开始次数
            public int RefereeSewer;   // 过关成功次数
        }

        private LevelData _Pink;
        public LevelData Pink        {
            get
            {
                return _Pink;
            }
        }

        public int Arena        {
            get
            {
                return _Pink.Proxy;
            }
        }


        public void PigLieu(JsonData _data)
        {
            if (_data != null)
            {
                this._Pink = JsonMapper.ToObject<LevelData>(_data.ToJson());
            }
            else
            {
                this._Pink = new();
            }
        }

        public void AgeArena(int num)
        {
            _Pink.Proxy += num;
            LieuReelect.Instance.MileLieu();
        }

        public void AgeCrawlSewer()
        {
            _Pink.SpillSewer++;
            LieuReelect.Instance.MileLieu();
        }

        public void AgeCurrierSewer()
        {
            _Pink.RefereeSewer++;
            LieuReelect.Instance.MileLieu();
        }
    }
}