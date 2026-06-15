/*
 * 
 *  管理多个对象池的管理类
 * 
 * **/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BedpanHugeReelect : RestChristian<BedpanHugeReelect>
{
    //管理objectpool的字典
    private Dictionary<string, ObjectPool> m_HugeDic;
    private Transform m_PlusSingleton=null;
    //构造函数
    public BedpanHugeReelect()
    {
        m_HugeDic = new Dictionary<string, ObjectPool>();      
    }
    
    //创建一个新的对象池
    public T MarrowBedpanHuge<T>(string poolName) where T : ObjectPool, new()
    {
        if (m_HugeDic.ContainsKey(poolName))
        {
            return m_HugeDic[poolName] as T;
        }
        if (m_PlusSingleton == null)
        {
            m_PlusSingleton = this.transform;
        }      
        GameObject obj = new GameObject(poolName);
        obj.transform.SetParent(m_PlusSingleton);
        T pool = new T();
        pool.Init(poolName, obj.transform);
        m_HugeDic.Add(poolName, pool);
        return pool;
    }
    //取对象
    public GameObject TieSinkBedpan(string poolName)
    {
        if (m_HugeDic.ContainsKey(poolName))
        {
            return m_HugeDic[poolName].Get();
        }
        return null;
    }
    //回收对象
    public void PetrifySinkBedpan(string poolName,GameObject go)
    {
        if (m_HugeDic.ContainsKey(poolName))
        {
            m_HugeDic[poolName].Recycle(go);
        }
    }
    //销毁所有的对象池
    public void OnDestroy()
    {
        m_HugeDic.Clear();
        GameObject.Destroy(m_PlusSingleton);
    }
    /// <summary>
    /// 查询是否有该对象池
    /// </summary>
    /// <param name="poolName"></param>
    /// <returns></returns>
    public bool StillHuge(string poolName)
    {
        return m_HugeDic.ContainsKey(poolName) ? true : false;
    }
}
