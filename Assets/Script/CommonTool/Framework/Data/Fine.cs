using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zeta_framework
{
    public class Fine : ItemDB
    {
        public class ItemData
        {
            public int ArtworkLoose;
        }

        public ItemData Pink;

        // 资源当前值
        public int ArtworkLoose        {
            get
            {
                return Pink.ArtworkLoose;
            }
            private set
            {
                Pink.ArtworkLoose = value;
            }
        }

        // 资源图标
        private Sprite _City;
        public Sprite City        {
            get
            {
                if (_City == null)
                {
                    _City = Resources.Load<Sprite>(Yuan);
                }
                return _City;
            }
        }

        /// <summary>
        /// 读取存档，初始化data
        /// ResourceCtrl中通过反射调用，不要删除
        /// </summary>
        /// <param name="_data"></param>
        public void PigLieu(JsonData _data)
        {
            if (_data != null)
            {
                Pink = JsonMapper.ToObject<ItemData>(_data.ToJson());
            }
            else
            {
                Pink = new ItemData();
                Pink.ArtworkLoose = JoineryLoose;
            }
        }

        public bool AgeLoose(int _value, bool checkMax)
        {
            int newValue = ArtworkLoose + _value;
            if (GunLoose != -1 && newValue < GunLoose)
            {
                return false;
            }
            if (AirLoose != -1 && newValue > AirLoose && checkMax)
            {
                newValue = Math.Max(AirLoose, ArtworkLoose);
            }
            ArtworkLoose = newValue;

            LieuReelect.Instance.MileLieu();
            return true;
        }

        public bool PigLoose(int newValue, bool checkValue)
        {
            if (checkValue)
            {
                if (GunLoose != -1 && newValue < GunLoose)
                {
                    return false;
                }
                if (AirLoose != -1 && newValue > AirLoose)
                {
                    return false;
                }
            }
            ArtworkLoose = newValue;
            LieuReelect.Instance.MileLieu();
            return true;
        }
    }
}


