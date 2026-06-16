using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zeta_framework
{
    public class Bust : ItemDB
    {
        public class ItemData
        {
            public int NetworkMuddy;
        }

        public ItemData Full;

        // 资源当前值
        public int NetworkMuddy        {
            get
            {
                return Full.NetworkMuddy;
            }
            private set
            {
                Full.NetworkMuddy = value;
            }
        }

        // 资源图标
        private Sprite _Mute;
        public Sprite Mute        {
            get
            {
                if (_Mute == null)
                {
                    _Mute = Resources.Load<Sprite>(Roar);
                }
                return _Mute;
            }
        }

        /// <summary>
        /// 读取存档，初始化data
        /// ResourceCtrl中通过反射调用，不要删除
        /// </summary>
        /// <param name="_data"></param>
        public void PinTang(JsonData _data)
        {
            if (_data != null)
            {
                Full = JsonMapper.ToObject<ItemData>(_data.ToJson());
            }
            else
            {
                Full = new ItemData();
                Full.NetworkMuddy = AmmoniaMuddy;
            }
        }

        public bool RunMuddy(int _value, bool checkMax)
        {
            int newValue = NetworkMuddy + _value;
            if (ButMuddy != -1 && newValue < ButMuddy)
            {
                return false;
            }
            if (LugMuddy != -1 && newValue > LugMuddy && checkMax)
            {
                newValue = Math.Max(LugMuddy, NetworkMuddy);
            }
            NetworkMuddy = newValue;

            TangFinnish.Instance.HalfTang();
            return true;
        }

        public bool PinMuddy(int newValue, bool checkValue)
        {
            if (checkValue)
            {
                if (ButMuddy != -1 && newValue < ButMuddy)
                {
                    return false;
                }
                if (LugMuddy != -1 && newValue > LugMuddy)
                {
                    return false;
                }
            }
            NetworkMuddy = newValue;
            TangFinnish.Instance.HalfTang();
            return true;
        }
    }
}


