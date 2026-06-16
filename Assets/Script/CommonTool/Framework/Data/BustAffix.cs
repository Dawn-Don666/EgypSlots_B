using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zeta_framework
{
    public class BustAffix : ItemGroupDB
    {
        public BustAffix()
        {

        }
        public BustAffix(string item_id, int item_num)
        {
            this.Fire_id = item_id;
            this.Fire_Rib = item_num;
        }

        public Bust Bust=> (Bust)NitrogenAmid.Instance.GetType().GetProperty(Fire_id).GetValue(NitrogenAmid.Instance);

        public string BustFirIon        {
            get
            {
                if (Fire_id.Equals("unlimit_health"))
                {
                    // 无限体力需要转为分钟
                    return (Fire_Rib / 60) + "m";
                }
                return Fire_Rib.ToString();
            }
        }
    }
}

