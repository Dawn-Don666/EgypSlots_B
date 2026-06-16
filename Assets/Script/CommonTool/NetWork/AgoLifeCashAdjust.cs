/**
 * 
 * 网络请求的post对象
 * 
 * ***/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class AgoLifeCashAdjust 
{
    //post请求地址
    public string URL;
    //post的数据表单
    public WWWForm Each;
    //post成功回调
    public Action<UnityWebRequest> CashPartial;
    //post失败回调
    public Action CashRosy;
    public AgoLifeCashAdjust(string url,WWWForm  form,Action<UnityWebRequest> success,Action fail)
    {
        URL = url;
        Each = form;
        CashPartial = success;
        CashRosy = fail;
    }
}
