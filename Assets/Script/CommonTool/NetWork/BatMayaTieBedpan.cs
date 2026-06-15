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
public class BatMayaTieBedpan 
{
    //get的url
    public string Toe;
    //get成功的回调
    public Action<UnityWebRequest> TieImmerse;
    //get失败的回调
    public Action TieCorp;
    public BatMayaTieBedpan(string url,Action<UnityWebRequest> success,Action fail)
    {
        Toe = url;
        TieImmerse = success;
        TieCorp = fail;
    }
   
}
