using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System;
public class RomeClockRotate : RestChristian<RomeClockRotate>
{
    public string version = "1.2";
    public string SinkDeaf= BatSizeSit.instance.SinkDeaf;
    //channel
#if UNITY_IOS
    private string Declare= "AppStore";
#elif UNITY_ANDROID
    private string Channel = "GooglePlay";
#else
    private string Channel = "GooglePlay";
#endif


    private void OnApplicationPause(bool pause)
    {
        RomeClockRotate.TieRecharge().sendSinkPreserve();
    }

    public Text Tire;

    protected override void Awake()
    {
        base.Awake();

        version = Application.version;
        StartCoroutine(nameof(YaleCollect));
    }
    IEnumerator YaleCollect()
    {
        while (true)
        {
            yield return new WaitForSeconds(120f);
            RomeClockRotate.TieRecharge().sendSinkPreserve();
        }
    }
    private void Start()
    {
        if (MileLieuReelect.GetInt("event_day") != DateTime.Now.Day && MileLieuReelect.GetString("user_servers_id").Length != 0)
        {
            MileLieuReelect.SetInt("event_day", DateTime.Now.Day);
        }
    }
    public void TourHeBergClock(string event_id)
    {
        TourClock(event_id);
    }
    public void sendSinkPreserve(List<string> valueList = null)
    {
        if (MileLieuReelect.GetDouble(CUnfair.Of_OccasionalTileGain) == 0)
        {
            MileLieuReelect.SetDouble(CUnfair.Of_OccasionalTileGain, MileLieuReelect.GetDouble(CUnfair.Of_TileGain));
        }
        if (MileLieuReelect.GetDouble(CUnfair.Of_OccasionalHappy) == 0)
        {
            MileLieuReelect.SetDouble(CUnfair.Of_OccasionalHappy, MileLieuReelect.GetDouble(CUnfair.Of_Happy));
        }
        if (valueList == null)
        {
            valueList = new List<string>() {
                MileLieuReelect.GetInt(CUnfair.Of_OccasionalTileGain).ToString(),
                MileLieuReelect.GetInt(CUnfair.Of_SeismicRoamLevels).ToString(),
                MileLieuReelect.GetString(CUnfair.Of_OccasionalHappy),
                MileLieuReelect.GetFloat(CUnfair.Of_CompoSinkAnew).ToString()
                //MileLieuReelect.GetInt(SlotConfig.sv_SlotSpinCount).ToString()
            };
        }

        if (MileLieuReelect.GetString(CUnfair.Of_FeverInjectUp) == null)
        {
            return;
        }
        WWWForm wwwForm = new WWWForm();
        wwwForm.AddField("gameCode", SinkDeaf);
        wwwForm.AddField("userId", MileLieuReelect.GetString(CUnfair.Of_FeverInjectUp));

        wwwForm.AddField("gameVersion", version);

        wwwForm.AddField("channel", Declare);

        for (int i = 0; i < valueList.Count; i++)
        {
            wwwForm.AddField("resource" + (i + 1), valueList[i]);
        }



        StartCoroutine(TourRome(BatSizeSit.instance.FilmToe + "/api/client/game_progress", wwwForm,
        (error) =>
        {
            Debug.Log(error);
        },
        (message) =>
        {
            Debug.Log(message);
        }));
    }
    public void TourClock(string event_id, string p1 = null, string p2 = null, string p3 = null)
    {
        if (Tire != null)
        {
            if (int.Parse(event_id) < 9100 && int.Parse(event_id) >= 9000)
            {
                if (p1 == null)
                {
                    p1 = "";
                }
                Tire.text += "\n" + DateTime.Now.ToString() + "id:" + event_id + "  p1:" + p1;
            }
        }
        if (MileLieuReelect.GetString(CUnfair.Of_FeverInjectUp) == null)
        {
            BatSizeSit.instance.Diver();
            return;
        }
        WWWForm wwwForm = new WWWForm();
        wwwForm.AddField("gameCode", SinkDeaf);
        wwwForm.AddField("userId", MileLieuReelect.GetString(CUnfair.Of_FeverInjectUp));
        //Debug.Log("userId:" + MileLieuReelect.GetString(CUnfair.sv_LocalServerId));
        wwwForm.AddField("version", version);
        //Debug.Log("version:" + version);
        wwwForm.AddField("channel", Declare);
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
        StartCoroutine(TourRome(BatSizeSit.instance.FilmToe + "/api/client/log", wwwForm,
        (error) =>
        {
            Debug.Log(error);
        },
        (message) =>
        {
            Debug.Log(message);
        }));
    }
    IEnumerator TourRome(string _url, WWWForm wwwForm, Action<string> fail, Action<string> success)
    {
        //Debug.Log(SerializeDictionaryToJsonString(dic));
        using UnityWebRequest request = UnityWebRequest.Post(_url, wwwForm);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isNetworkError)
        {
            fail(request.error);
            ArcPivotal();
            request.Dispose();
        }
        else
        {
            success(request.downloadHandler.text);
            ArcPivotal();
            request.Dispose();
        }
    }
    private void ArcPivotal()
    {
        StopCoroutine(nameof(TourRome));
    }


}