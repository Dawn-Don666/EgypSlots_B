using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zeta_framework
{
    public class BustAffixAmid
    {
        public static BustAffixAmid Instance;

        public Dictionary<string, List<BustAffix>> itemAccord;

        public BustAffixAmid(JsonData setting)
        {
            if (Instance == null)
            {
                Instance = this;
            }

            Dictionary<string, List<BustAffix>> itemAccord= new Dictionary<string, List<BustAffix>>();
            List<BustAffix> itemGroupList = JsonMapper.ToObject<List<BustAffix>>(setting.ToJson());
            foreach (BustAffix itemGroup in itemGroupList)
            {
                if (!itemAccord.ContainsKey(itemGroup.id))
                {
                    itemAccord.Add(itemGroup.id, new List<BustAffix>());
                }
                itemAccord[itemGroup.id].Add(itemGroup);
            }
            this.itemAccord = itemAccord;
        }

    }
}
