/*
 * 
 *  管理多个对象池的管理类
 * 
 * **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AdjustYolkFinnish : MonoYoungster<AdjustYolkFinnish>
{
    //管理objectpool的字典
    private Dictionary<string, ObjectPool> m_YolkBud;
    private Transform m_StewSynthetic=null;
    //构造函数
    public AdjustYolkFinnish()
    {
        m_YolkBud = new Dictionary<string, ObjectPool>();      
    }
    
    //创建一个新的对象池
    public T DigestAdjustYolk<T>(string poolName) where T : ObjectPool, new()
    {
        if (m_YolkBud.ContainsKey(poolName))
        {
            return m_YolkBud[poolName] as T;
        }
        if (m_StewSynthetic == null)
        {
            m_StewSynthetic = this.transform;
        }      
        GameObject obj = new GameObject(poolName);
        obj.transform.SetParent(m_StewSynthetic);
        T pool = new T();
        pool.Init(poolName, obj.transform);
        m_YolkBud.Add(poolName, pool);
        return pool;
    }
    //取对象
    public GameObject RatPestAdjust(string poolName)
    {
        if (m_YolkBud.ContainsKey(poolName))
        {
            return m_YolkBud[poolName].Get();
        }
        return null;
    }
    //回收对象
    public void NervousPestAdjust(string poolName,GameObject go)
    {
        if (m_YolkBud.ContainsKey(poolName))
        {
            m_YolkBud[poolName].Recycle(go);
        }
    }
    //销毁所有的对象池
    public void OnDestroy()
    {
        m_YolkBud.Clear();
        GameObject.Destroy(m_StewSynthetic);
    }
    /// <summary>
    /// 查询是否有该对象池
    /// </summary>
    /// <param name="poolName"></param>
    /// <returns></returns>
    public bool SyrupYolk(string poolName)
    {
        return m_YolkBud.ContainsKey(poolName) ? true : false;
    }
}
