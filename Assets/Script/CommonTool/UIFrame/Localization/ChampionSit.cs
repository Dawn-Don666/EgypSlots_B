/*
 * 
 * 多语言
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampionSit 
{
    public static ChampionSit _Miniature;
    //语言翻译的缓存集合
    private Dictionary<string, string> _RatChampionCache;

    private ChampionSit()
    {
        _RatChampionCache = new Dictionary<string, string>();
        //初始化语言缓存集合
        RakeChampionSolve();
    }

    /// <summary>
    /// 获取实例
    /// </summary>
    /// <returns></returns>
    public static ChampionSit TieRecharge()
    {
        if (_Miniature == null)
        {
            _Miniature = new ChampionSit();
        }
        return _Miniature;
    }

    /// <summary>
    /// 得到显示文本信息
    /// </summary>
    /// <param name="lauguageId">语言id</param>
    /// <returns></returns>
    public string SlowCrew(string lauguageId)
    {
        string strQueryResult = string.Empty;
        if (string.IsNullOrEmpty(lauguageId)) return null;
        //查询处理
        if(_RatChampionCache!=null && _RatChampionCache.Count >= 1)
        {
            _RatChampionCache.TryGetValue(lauguageId, out strQueryResult);
            if (!string.IsNullOrEmpty(strQueryResult))
            {
                return strQueryResult;
            }
        }
        Debug.Log(GetType() + "/ShowText()/ Query is Null!  Parameter lauguageID: " + lauguageId);
        return null;
    }

    /// <summary>
    /// 初始化语言缓存集合
    /// </summary>
    private void RakeChampionSolve()
    {
        //LauguageJSONConfig_En
        //LauguageJSONConfig
        IUnfairReelect config = new UnfairReelectOfGene("LauguageJSONConfig");
        if (config != null)
        {
            _RatChampionCache = config.TinElectro;
        }
    }
}
