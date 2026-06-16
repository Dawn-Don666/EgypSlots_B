/*
 * 
 * 多语言
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EqualityHit 
{
    public static EqualityHit _Inclusive;
    //语言翻译的缓存集合
    private Dictionary<string, string> _BudLauguageLotus;

    private EqualityHit()
    {
        _BudLauguageLotus = new Dictionary<string, string>();
        //初始化语言缓存集合
        BikeEqualityLotus();
    }

    /// <summary>
    /// 获取实例
    /// </summary>
    /// <returns></returns>
    public static EqualityHit RatRuminate()
    {
        if (_Inclusive == null)
        {
            _Inclusive = new EqualityHit();
        }
        return _Inclusive;
    }

    /// <summary>
    /// 得到显示文本信息
    /// </summary>
    /// <param name="lauguageId">语言id</param>
    /// <returns></returns>
    public string WithPoet(string lauguageId)
    {
        string strQueryResult = string.Empty;
        if (string.IsNullOrEmpty(lauguageId)) return null;
        //查询处理
        if(_BudLauguageLotus!=null && _BudLauguageLotus.Count >= 1)
        {
            _BudLauguageLotus.TryGetValue(lauguageId, out strQueryResult);
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
    private void BikeEqualityLotus()
    {
        //LauguageJSONConfig_En
        //LauguageJSONConfig
        IShaperFinnish config = new ShaperFinnishMeLuce("LauguageJSONConfig");
        if (config != null)
        {
            _BudLauguageLotus = config.WebGeorgia;
        }
    }
}
