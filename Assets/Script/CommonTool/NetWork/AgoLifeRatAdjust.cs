/***
 * 
 * 网络请求的get对象
 * 
 * **/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class AgoLifeRatAdjust 
{
    //get的url
    public string Shy;
    //get成功的回调
    public Action<UnityWebRequest> RatPartial;
    //get失败的回调
    public Action RatRosy;
    public AgoLifeRatAdjust(string url,Action<UnityWebRequest> success,Action fail)
    {
        Shy = url;
        RatPartial = success;
        RatRosy = fail;
    }
   
}
