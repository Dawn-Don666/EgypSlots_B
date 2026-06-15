using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zeta_framework
{
    public class FineTheirHerd
    {
        public static FineTheirHerd Instance;

        public Dictionary<string, List<FineTheir>> OvalSquirt;

        public FineTheirHerd(JsonData setting)
        {
            if (Instance == null)
            {
                Instance = this;
            }

            Dictionary<string, List<FineTheir>> OvalSquirt= new Dictionary<string, List<FineTheir>>();
            List<FineTheir> itemGroupList = JsonMapper.ToObject<List<FineTheir>>(setting.ToJson());
            foreach (FineTheir itemGroup in itemGroupList)
            {
                if (!OvalSquirt.ContainsKey(itemGroup.id))
                {
                    OvalSquirt.Add(itemGroup.id, new List<FineTheir>());
                }
                OvalSquirt[itemGroup.id].Add(itemGroup);
            }
            this.OvalSquirt = OvalSquirt;
        }

    }
}
