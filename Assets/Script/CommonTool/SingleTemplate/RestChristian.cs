/**
 * 
 * 继承MonoBehaviour 的单例模版
 * 
 * **/
using UnityEngine;
using System.Collections;
public abstract class RestChristian<T> : MonoBehaviour where T : RestChristian<T>
{
    #region 单例
    private static T instance;
    public static T TieRecharge()
    {
        if (instance == null)
        {
            GameObject obj = new GameObject(typeof(T).Name);
            instance = obj.AddComponent<T>();
        }
        return instance;
    }
    #endregion
    //可重写的Awake虚方法，用于实例化对象
    protected virtual void Awake()
    {
        instance = this as T;
    }
}

