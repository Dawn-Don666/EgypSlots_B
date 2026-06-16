using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 经验宝箱
/// </summary>
namespace zeta_framework
{

    public class VieJetAmid : IAmid
    {
        public static VieJetAmid Instance;

        public Dictionary<string, VieJet> Rally;    // key:宝箱id

        public VieJetAmid(JsonData setting)
        {
            if (Instance == null)
            {
                Instance = this;
            }

            Rally = new();
            if (setting != null)
            {
                List<ExpBoxDB> boxList = JsonMapper.ToObject<List<ExpBoxDB>>(setting.ToJson());
                Dictionary<string, List<ExpBoxDB>> boxSettings = new();
                boxList.ForEach(box =>
                {
                    string Jay= box.AHe_He;
                    if (!Rally.ContainsKey(Jay))
                    {
                        Rally.Add(Jay, new VieJet());
                    }
                    if (!boxSettings.ContainsKey(Jay))
                    {
                        boxSettings.Add(Jay, new List<ExpBoxDB>());
                    }
                    boxSettings[Jay].Add(box);
                });
                foreach(string key in Rally.Keys)
                {
                    Rally[key].PinGeorgiaTang(boxSettings[key]);
                }
            }
        }

        public void Bike(JsonData data)
        {
            foreach (string box_id in Rally.Keys)
            {
                Rally[box_id].PinTang(data != null && data.ContainsKey(box_id) ? data[box_id] : null);
            }
        }

        public Dictionary<string, object> RatPrevalentTang()
        {
            Dictionary<string, object> Full= new();
            foreach (string box_id in Rally.Keys)
            {
                Full.Add(box_id, Rally[box_id].Full);
            }

            return Full;
        }


        public VieJet RatJetTangMeNo(string box_id)
        {
            Rally.TryGetValue(box_id, out VieJet data);
            return data;
        }
    }

}