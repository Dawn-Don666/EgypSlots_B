using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
public class CashDrakeSeaman : MonoYoungster<CashDrakeSeaman>
{
    public string version = "1.2";
    public string PestLand= AgoSateHit.instance.PestLand;
    //channel
#if UNITY_IOS
    private string Besiege= "AppStore";
#elif UNITY_ANDROID
    private string Channel = "GooglePlay";
#else
    private string Channel = "GooglePlay";
#endif


    private void OnApplicationPause(bool pause)
    {
        CashDrakeSeaman.RatRuminate().GritPestAcoustic();
    }

    public Text Face;

    protected override void Awake()
    {
        base.Awake();

        version = Application.version;
        StartCoroutine(nameof(PickEmbrace));
    }
    IEnumerator PickEmbrace()
    {
        while (true)
        {
            yield return new WaitForSeconds(120f);
            CashDrakeSeaman.RatRuminate().GritPestAcoustic();
        }
    }
    private void Start()
    {
        if (HalfTangFinnish.GetInt("event_day") != DateTime.Now.Day && HalfTangFinnish.GetString("user_servers_id").Length != 0)
        {
            HalfTangFinnish.SetInt("event_day", DateTime.Now.Day);
        }
    }
    public void TakeAtJustDrake(string event_id)
    {
        TakeDrake(event_id);
    }
    public void GritPestAcoustic(List<string> valueList = null)
    {
        if (HalfTangFinnish.GetDouble(CShaper.Go_GlaciationLifeExam) == 0)
        {
            HalfTangFinnish.SetDouble(CShaper.Go_GlaciationLifeExam, HalfTangFinnish.GetDouble(CShaper.Go_LifeExam));
        }
        if (HalfTangFinnish.GetDouble(CShaper.Go_GlaciationMerge) == 0)
        {
            HalfTangFinnish.SetDouble(CShaper.Go_GlaciationMerge, HalfTangFinnish.GetDouble(CShaper.Go_Merge));
        }
        if (valueList == null)
        {
            valueList = new List<string>() {
                CashOutManager.RatRuminate().Money.ToString(),
                HalfTang.TangBland.ToString(), 
                HalfTang.BaskPlace.ToString()
                //HalfTangFinnish.GetInt(SlotConfig.sv_SlotSpinCount).ToString()
            };
        }

        if (HalfTangFinnish.GetString(CShaper.Go_BroadPuzzleNo) == null)
        {
            return;
        }
        WWWForm wwwForm = new WWWForm();
        wwwForm.AddField("gameCode", PestLand);
        wwwForm.AddField("userId", HalfTangFinnish.GetString(CShaper.Go_BroadPuzzleNo));

        wwwForm.AddField("gameVersion", version);

        wwwForm.AddField("channel", Besiege);

        for (int i = 0; i < valueList.Count; i++)
        {
            wwwForm.AddField("resource" + (i + 1), valueList[i]);
        }



        StartCoroutine(TakeCash(AgoSateHit.instance.AeroShy + "/api/client/game_progress", wwwForm,
        (error) =>
        {
            Debug.Log(error);
        },
        (message) =>
        {
            Debug.Log(message);
        }));
    }
    public void TakeDrake(string event_id, string p1 = null, string p2 = null, string p3 = null)
    {
        if (Face != null)
        {
            if (int.Parse(event_id) < 9100 && int.Parse(event_id) >= 9000)
            {
                if (p1 == null)
                {
                    p1 = "";
                }
                Face.text += "\n" + DateTime.Now.ToString() + "id:" + event_id + "  p1:" + p1;
            }
        }
        if (HalfTangFinnish.GetString(CShaper.Go_BroadPuzzleNo) == null)
        {
            AgoSateHit.instance.Speck();
            return;
        }
        WWWForm wwwForm = new WWWForm();
        wwwForm.AddField("gameCode", PestLand);
        wwwForm.AddField("userId", HalfTangFinnish.GetString(CShaper.Go_BroadPuzzleNo));
        //Debug.Log("userId:" + HalfTangFinnish.GetString(CShaper.sv_LocalServerId));
        wwwForm.AddField("version", version);
        //Debug.Log("version:" + version);
        wwwForm.AddField("channel", Besiege);
        //Debug.Log("channel:" + channal);
        wwwForm.AddField("operateId", event_id);
        Debug.Log("operateId:" + event_id);


        if (p1 != null)
        {
            wwwForm.AddField("params1", p1);
        }
        if (p2 != null)
        {
            wwwForm.AddField("params2", p2);
        }
        if (p3 != null)
        {
            wwwForm.AddField("params3", p3);
        }
        StartCoroutine(TakeCash(AgoSateHit.instance.AeroShy + "/api/client/log", wwwForm,
        (error) =>
        {
            Debug.Log(error);
        },
        (message) =>
        {
            Debug.Log(message);
        }));
    }
    IEnumerator TakeCash(string _url, WWWForm wwwForm, Action<string> fail, Action<string> success)
    {
        //Debug.Log(SerializeDictionaryToJsonString(dic));
        using UnityWebRequest request = UnityWebRequest.Post(_url, wwwForm);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isNetworkError)
        {
            fail(request.error);
            EraFeature();
            request.Dispose();
        }
        else
        {
            success(request.downloadHandler.text);
            EraFeature();
            request.Dispose();
        }
    }
    private void EraFeature()
    {
        StopCoroutine(nameof(TakeCash));
    }


}