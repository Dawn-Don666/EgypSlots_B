using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zeta_framework
{
    public class Tube : ShopDB
    {
        public class ShopData
        {
            public int ArtworkDry;  // 已使用数量（仅对每日限购商品有效）
        }

        public ShopData Pink;

        public int ArtworkDry        {
            get
            {
                return Pink.ArtworkDry;
            }
            set
            {
                Pink.ArtworkDry = value;
            }
        }

        public List<FineTheir> OvalTheir        {
            get
            {
                if (string.IsNullOrEmpty(Unwilling_ID) || !FineTheirHerd.Instance.OvalSquirt.ContainsKey(Unwilling_ID))
                {
                    return null;
                }
                else
                {
                    return FineTheirHerd.Instance.OvalSquirt[Unwilling_ID];
                }
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
                Pink = JsonMapper.ToObject<ShopData>(_data.ToJson());
            }
            else
            {
                Pink = new ShopData();
            }
        }

        public bool AgeDry(int _num = 1)
        {
            if (num > 0 && ArtworkDry + _num > num)
            {
                return false;
            }
            else
            {
                ArtworkDry += _num;
                return true;
            }
        }

        /// <summary>
        /// 该商品当前是否可购买
        /// </summary>
        /// <returns></returns>
        public bool EggFir(int _num = 1)
        {
            if (num > 0 && ArtworkDry + _num > num)
            {
                // 是否已达到当天的限购数量
                return false;
            }
            //TODO 其他条件，比如是否解锁等

            return true;
        }
    }
}