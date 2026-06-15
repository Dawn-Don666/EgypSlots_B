using System;
using UnityEngine;

/// <summary>
/// UI基类。
/// </summary>
public class AUIBase : MonoBehaviour
{
      /// <summary>
      /// UI类型。
      /// </summary>
      public enum UIType
      {
            /// <summary>
            /// 类型无。
            /// </summary>
            None,

            /// <summary>
            /// 类型Windows。
            /// </summary>
            Window,

            /// <summary>
            /// 类型Widget。
            /// </summary>
            Widget,
      }
      
      /// <summary>
      /// 自定义数据集。
      /// </summary>
      protected System.Object[] _userDatas;
        
      /// <summary>
      /// 自定义数据。
      /// </summary>
      public System.Object UserData
      {
            get
            {
                  if (_userDatas != null && _userDatas.Length >= 1)
                  {
                        return _userDatas[0];
                  }
                  else
                  {
                        return null;
                  }
            }
      }

      /// <summary>
      /// 自定义数据集。
      /// </summary>
      public System.Object[] UserDatas => _userDatas;
      
      /// <summary>
      /// UI类型。
      /// </summary>
      public virtual UIType Type => UIType.None;
      
      /// <summary>
      /// 窗口创建。
      /// </summary>
      public virtual void OnCreate()
      {
            
      }
      
      public virtual void OnClose()
      {
            RemoveAllUIEvent();
            RemoveAllUITimer();
      }
      
      /// <summary>
      /// 窗口刷新。
      /// </summary>
      public virtual void OnRefresh()
      {
      }
      
      private AEventMgr _eventMgr;

      protected AEventMgr EventMgr
      {
            get
            {
                  if (_eventMgr == null)
                  {
                        _eventMgr = new AEventMgr();
                  }

                  return _eventMgr;
            }
      }
      
      #region Add Event
      protected void AddUIEvent(string eventType, Action handler)
      {
            EventMgr.AddEvent(eventType, handler);
      }

      protected void AddUIEvent<T>(string eventType, Action<T> handler)
      {
            EventMgr.AddEvent(eventType, handler);
      }

      protected void AddUIEvent<T1, T2>(string eventType, Action<T1, T2> handler)
      {
            EventMgr.AddEvent(eventType, handler);
      }

      protected void AddUIEvent<T1, T2, T3>(string eventType, Action<T1, T2, T3> handler)
      {
            EventMgr.AddEvent(eventType, handler);
      }

      protected void AddUIEvent<T1, T2, T3, T4>(string eventType, Action<T1, T2, T3, T4> handler)
      {
            EventMgr.AddEvent(eventType, handler);
      }
      
      protected void AddUIEvent<T1, T2, T3, T4, T5>(string eventType, Action<T1, T2, T3, T4, T5> handler)
      {
            EventMgr.AddEvent(eventType, handler);
      }
      
      protected void AddUIEvent<T1, T2, T3, T4, T5, T6>(string eventType, Action<T1, T2, T3, T4, T5, T6> handler)
      {
            EventMgr.AddEvent(eventType, handler);
      }
      #endregion
      
      #region Remove Event
      protected void RemoveUIEvent(string eventType, Action handler)
      {
            EventMgr.RemoveEvent(eventType, handler);
      }
      
      protected void RemoveAllUIEvent()
      {
            if (_eventMgr != null)
            {
                  _eventMgr.Clear();
            }
      }
      #endregion
      
      #region Send Event
      public void SendUIEvent(string eventType)
      {
            AEventModule.Send(eventType);
      }
    
      public void SendUIEvent<T>(string eventType, T arg)
      {
            AEventModule.Send(eventType, arg);
      }

      public void SendUIEvent<T1, T2>(string eventType, T1 arg1, T2 arg2)
      {
            AEventModule.Send(eventType, arg1, arg2);
      }
    
      public void SendUIEvent<T1, T2, T3>(string eventType, T1 arg1, T2 arg2, T3 arg3)
      {
            AEventModule.Send(eventType, arg1, arg2, arg3);
      }
    
      public void SendUIEvent<T1, T2, T3, T4>(string eventType, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
      {
            AEventModule.Send(eventType, arg1, arg2, arg3, arg4);
      }
    
      public void SendUIEvent<T1, T2, T3, T4, T5>(string eventType, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
      {
            AEventModule.Send(eventType, arg1, arg2, arg3, arg4, arg5);
      }
      
      public void SendUIEvent<T1, T2, T3, T4, T5, T6>(string eventType, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
      {
            AEventModule.Send(eventType, arg1, arg2, arg3, arg4, arg5, arg6);
      }
      
      #endregion

      #region UITimer
      private ATimerMgr _timerMgr;

      protected ATimerMgr TimerMgr
      {
            get
            {
                  if (_timerMgr == null)
                  {
                        _timerMgr = new ATimerMgr();
                  }
                  
                  return _timerMgr;
            }
      }

      protected int AddTimer(TimerHandler callback, float time, bool isLoop = false, bool isUnscaled = false,
            params object[] args)
      {
            return TimerMgr.AddTimer(callback, time, isLoop, isUnscaled, args);
      }
      
      protected void RemoveTimer(int id)
      {
            TimerMgr.RemoveTimer(id);
      }
      
      protected void RemoveAllUITimer()
      {
            if (_timerMgr != null)
            {
                  _timerMgr.Clear();
            }
      }
      #endregion
}