using System.Collections.Generic;

public class ATimerMgr
{
    private readonly List<int> _listTimerIDs;
    private readonly bool _isInit = false;
    
    public ATimerMgr()
    {
        if (_isInit)
        {
            return;
        }

        _isInit = true;
        _listTimerIDs = new List<int>();
    }
    
    /// <summary>
    /// 清理内存对象回收入池。
    /// </summary>
    public void Clear()
    {
        if (!_isInit)
        {
            return;
        }

        for (int i = 0; i < _listTimerIDs.Count; ++i)
        {
            var id = _listTimerIDs[i];
            ATimerModule.Instance.RemoveTimer(id);
        }

        _listTimerIDs.Clear();
    }

    #region UITimer

    public int AddTimer(TimerHandler callback, float time, bool isLoop = false, bool isUnscaled = false,
        params object[] args)
    {
        var id = ATimerModule.Instance.AddTimer(callback, time, isLoop, isUnscaled, args);
        _listTimerIDs.Add(id);
        return id;
    }
    
    public void RemoveTimer(int id)
    {
        ATimerModule.Instance.RemoveTimer(id);
        _listTimerIDs.Remove(id);
    }

    #endregion
}