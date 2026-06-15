using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 经验宝箱
/// </summary>
namespace zeta_framework
{

    public class GetButHerd : IHerd
    {
        public static GetButHerd Instance;

        public Dictionary<string, GetBut> Brand;    // key:宝箱id

        public GetButHerd(JsonData setting)
        {
            if (Instance == null)
            {
                Instance = this;
            }

            Brand = new();
            if (setting != null)
            {
                List<ExpBoxDB> boxList = JsonMapper.ToObject<List<ExpBoxDB>>(setting.ToJson());
                Dictionary<string, List<ExpBoxDB>> boxSettings = new();
                boxList.ForEach(box =>
                {
                    string Off= box.Elk_ID;
                    if (!Brand.ContainsKey(Off))
                    {
                        Brand.Add(Off, new GetBut());
                    }
                    if (!boxSettings.ContainsKey(Off))
                    {
                        boxSettings.Add(Off, new List<ExpBoxDB>());
                    }
                    boxSettings[Off].Add(box);
                });
                foreach(string key in Brand.Keys)
                {
                    Brand[key].PigElectroLieu(boxSettings[key]);
                }
            }
        }

        public void Rake(JsonData data)
        {
            foreach (string box_id in Brand.Keys)
            {
                Brand[box_id].PigLieu(data != null && data.ContainsKey(box_id) ? data[box_id] : null);
            }
        }

        public Dictionary<string, object> TieDepositorLieu()
        {
            Dictionary<string, object> Pink= new();
            foreach (string box_id in Brand.Keys)
            {
                Pink.Add(box_id, Brand[box_id].Pink);
            }

            return Pink;
        }


        public GetBut TieButLieuOfUp(string box_id)
        {
            Brand.TryGetValue(box_id, out GetBut data);
            return data;
        }
    }

}