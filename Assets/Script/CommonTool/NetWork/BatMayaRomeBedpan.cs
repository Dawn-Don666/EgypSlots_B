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
public class BatMayaRomeBedpan 
{
    //post请求地址
    public string URL;
    //post的数据表单
    public WWWForm Akin;
    //post成功回调
    public Action<UnityWebRequest> RomeImmerse;
    //post失败回调
    public Action RomeCorp;
    public BatMayaRomeBedpan(string url,WWWForm  form,Action<UnityWebRequest> success,Action fail)
    {
        URL = url;
        Akin = form;
        RomeImmerse = success;
        RomeCorp = fail;
    }
}
