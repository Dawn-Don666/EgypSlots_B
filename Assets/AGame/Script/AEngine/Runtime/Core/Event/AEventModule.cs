using System;

/// <summary>
/// 事件管理器。
/// </summary>
public class AEventModule
{
    /// <summary>
    /// 分发注册器。
    /// </summary>
    private static AEventDispatcher Dispatcher = new AEventDispatcher();
    
    public static AEventDispatcher GetDispatcher()
    {
        return Dispatcher;
    }
    
    /// <summary>
    /// 清除事件。
    /// </summary>
    public static void Init()
    {
        // ADebug.Log("事件模块初始化");
        Dispatcher.ClearEventTable();
    }

    #region Add Event
    public static bool AddEvent(string eventType, Action handler)
    {
        return Dispatcher.AddEventListener(eventType, handler);
        
    }

    public static bool AddEvent<T>(string eventType, Action<T> handler)
    {
        return Dispatcher.AddEventListener(eventType, handler);
    }

    public static bool AddEvent<T1, T2>(string eventType, Action<T1, T2> handler)
    {
        return Dispatcher.AddEventListener(eventType, handler);
    }

    public static bool AddEvent<T1, T2, T3>(string eventType, Action<T1, T2, T3> handler)
    {
        return Dispatcher.AddEventListener(eventType, handler);
    }

    public static bool AddEvent<T1, T2, T3, T4>(string eventType, Action<T1, T2, T3, T4> handler)
    {
        return Dispatcher.AddEventListener(eventType, handler);
    }

    public static bool AddEvent<T1, T2, T3, T4, T5>(string eventType, Action<T1, T2, T3, T4, T5> handler)
    {
        return Dispatcher.AddEventListener(eventType, handler);
    }
    
    public static bool AddEvent<T1, T2, T3, T4, T5, T6>(string eventType, Action<T1, T2, T3, T4, T5, T6> handler)
    {
        return Dispatcher.AddEventListener(eventType, handler);
    }
    #endregion

    #region Remove Event
    public static void RemoveEvent(string eventType, Action handler)
    {
        Dispatcher.RemoveEventListener(eventType, handler);
    }
    public static void RemoveEvent<T>(string eventType, Action<T> handler)
    {
        Dispatcher.RemoveEventListener(eventType, handler);
    }
    
    public static void RemoveEvent<T1, T2>(string eventType, Action<T1, T2> handler)
    {
        Dispatcher.RemoveEventListener(eventType, handler);
    }
    
public static void RemoveEvent<T1, T2, T3>(string eventType, Action<T1, T2, T3> handler)
    {
    Dispatcher.RemoveEventListener(eventType, handler);
    }
    
public static void RemoveEvent<T1, T2, T3, T4>(string eventType, Action<T1, T2, T3, T4> handler)
    {
        Dispatcher.RemoveEventListener(eventType, handler);
    }
    
    public static void RemoveEvent<T1, T2, T3, T4, T5>(string eventType, Action<T1, T2, T3, T4, T5> handler)
    {
        Dispatcher.RemoveEventListener(eventType, handler);
    }
    
    public static void RemoveEvent<T1, T2, T3, T4, T5, T6>(string eventType, Action<T1, T2, T3, T4, T5, T6> handler)
    {
        Dispatcher.RemoveEventListener(eventType, handler);
    }
    
    /// <summary>
    /// 移除事件监听。
    /// </summary>
    /// <param name="eventType">事件类型。</param>
    /// <param name="handler">事件处理回调。</param>
    public static void RemoveEvent(string eventType, Delegate handler)
    {
        Dispatcher.RemoveEventListener(eventType, handler);
    }
    #endregion
    
    #region Send Event
    public static void Send(string eventType)
    {
        Dispatcher.Send(eventType);
    }
    
    public static void Send<T>(string eventType, T arg)
    {
        Dispatcher.Send(eventType, arg);
    }

    public static void Send<T1, T2>(string eventType, T1 arg1, T2 arg2)
    {
        Dispatcher.Send(eventType, arg1, arg2);
    }
    
    public static void Send<T1, T2, T3>(string eventType, T1 arg1, T2 arg2, T3 arg3)
    {
        Dispatcher.Send(eventType, arg1, arg2, arg3);
    }
    
    public static void Send<T1, T2, T3, T4>(string eventType, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
    {
        Dispatcher.Send(eventType, arg1, arg2, arg3, arg4);
    }
    
    public static void Send<T1, T2, T3, T4, T5>(string eventType, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
    {
        Dispatcher.Send(eventType, arg1, arg2, arg3, arg4, arg5);
    }
    
    public static void Send<T1, T2, T3, T4, T5, T6>(string eventType, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
    {
        Dispatcher.Send(eventType, arg1, arg2, arg3, arg4, arg5, arg6);
    }
    
    #endregion
}