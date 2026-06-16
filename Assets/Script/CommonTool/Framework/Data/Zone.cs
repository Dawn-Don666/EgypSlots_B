using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zeta_framework
{
    public class Zone : ShopDB
    {
        public class ShopData
        {
            public int NetworkFir;  // 已使用数量（仅对每日限购商品有效）
        }

        public ShopData Full;

        public int NetworkFir        {
            get
            {
                return Full.NetworkFir;
            }
            set
            {
                Full.NetworkFir = value;
            }
        }

        public List<BustAffix> FireAffix        {
            get
            {
                if (string.IsNullOrEmpty(Euphrates_He) || !BustAffixAmid.Instance.itemAccord.ContainsKey(Euphrates_He))
                {
                    return null;
                }
                else
                {
                    return BustAffixAmid.Instance.itemAccord[Euphrates_He];
                }
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
                Full = JsonMapper.ToObject<ShopData>(_data.ToJson());
            }
            else
            {
                Full = new ShopData();
            }
        }

        public bool RunFir(int _num = 1)
        {
            if (num > 0 && NetworkFir + _num > num)
            {
                return false;
            }
            else
            {
                NetworkFir += _num;
                return true;
            }
        }

        /// <summary>
        /// 该商品当前是否可购买
        /// </summary>
        /// <returns></returns>
        public bool FixFun(int _num = 1)
        {
            if (num > 0 && NetworkFir + _num > num)
            {
                // 是否已达到当天的限购数量
                return false;
            }
            //TODO 其他条件，比如是否解锁等

            return true;
        }
    }
}