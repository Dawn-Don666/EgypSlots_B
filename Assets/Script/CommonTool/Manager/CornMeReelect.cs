using System.Runtime.InteropServices;
using UnityEngine;

public class CornMeReelect : MonoBehaviour
{
    public static CornMeReelect instance;

    public string appid;
    //获取IOS函数声明
#if UNITY_IOS
    [System.Runtime.InteropServices.DllImport("__Internal")]
    internal extern static void openRateUsUrl(string appId);
#endif

    private void Awake()
    {
        instance = this;
    }

    public void SpanAPCryBeaver()
    {
#if UNITY_ANDROID || UNITY_EDITOR
        Application.OpenURL("market://details?id=" + appid);
#elif UNITY_IOS
        openRateUsUrl(appid);
#endif
    }
}
