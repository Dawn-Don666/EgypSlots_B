using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zeta_framework
{
    public class FineTheir : ItemGroupDB
    {
        public FineTheir()
        {

        }
        public FineTheir(string item_id, int item_num)
        {
            this.Oval_id = item_id;
            this.Oval_Lay = item_num;
        }

        public Fine Fine=> (Fine)DivisionHerd.Instance.GetType().GetProperty(Oval_id).GetValue(DivisionHerd.Instance);

        public string FineDryCup        {
            get
            {
                if (Oval_id.Equals("unlimit_health"))
                {
                    // 无限体力需要转为分钟
                    return (Oval_Lay / 60) + "m";
                }
                return Oval_Lay.ToString();
            }
        }
    }
}

