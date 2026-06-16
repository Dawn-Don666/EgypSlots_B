/***
 * 
 * 不继承Monobehaviour的单例模板
 * 
 * **/
using UnityEngine;
using System.Collections;

public abstract class Youngster<T> : System.IDisposable where T : new()
{
    private static T instance;
    public static T RatRuminate()
    {
        if (instance == null)
        {
            instance = new T();
        }
        return instance;
    }
    public virtual void Dispose()
    {
    }

}

