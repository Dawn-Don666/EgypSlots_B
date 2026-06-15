using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// 高级计时器管理器
/// 支持跨游戏会话的计时器持久化和恢复
/// </summary>
public class TimerManager : MonoBehaviour
{
    // 单例实例
    public static TimerManager Instance { get; private set; }

    // 计时器数据类
    private class TimerData
    {
        public string key;
        public int totalSeconds;
        public int remainingSeconds;
        public bool isLoop;
        public UnityAction callback;
        public Text displayText;
        public DateTime startTime;
        public Coroutine coroutine;
    }

    // 所有活动的计时器
    private Dictionary<string, TimerData> activeTimers = new Dictionary<string, TimerData>();

    // 存储被停止的计时器键值，确保IsTimerComplete返回true
    private HashSet<string> stoppedTimers = new HashSet<string>();

    // 存储键前缀
    private const string PLAYER_PREFS_PREFIX = "Timer_";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 开启新计时器
    /// </summary>
    public void StartTimer(string key, int seconds, bool isLoop, UnityAction callback, Text displayText = null)
    {
        // 如果已有相同key的计时器，先停止
        if (activeTimers.ContainsKey(key))
        {
            StopTimer(key);
        }

        // 从停止集合中移除
        stoppedTimers.Remove(key);

        // 创建新计时器
        TimerData timer = new TimerData()
        {
            key = key,
            totalSeconds = seconds,
            remainingSeconds = seconds,
            isLoop = isLoop,
            callback = callback,
            displayText = displayText,
            startTime = DateTime.UtcNow
        };

        // 开始计时协程
        timer.coroutine = StartCoroutine(RunTimer(timer));
        activeTimers[key] = timer;

        // 保存到PlayerPrefs
        SaveTimerToPlayerPrefs(timer);
    }

    /// <summary>
    /// 检查计时器是否已完成
    /// </summary>
    public bool IsTimerComplete(string key)
    {
        // 如果计时器被主动停止，返回true
        if (stoppedTimers.Contains(key)) return true;

        // 如果计时器仍在运行，肯定未完成
        if (activeTimers.ContainsKey(key)) return false;

        // 检查PlayerPrefs中是否有存储
        string playerPrefsKey = PLAYER_PREFS_PREFIX + key;
        if (!PlayerPrefs.HasKey(playerPrefsKey)) return true;

        // 从PlayerPrefs加载数据
        string[] data = PlayerPrefs.GetString(playerPrefsKey).Split('|');
        DateTime startTime = DateTime.FromBinary(Convert.ToInt64(data[0]));
        int totalSeconds = int.Parse(data[1]);
        bool isLoop = bool.Parse(data[2]);

        // 计算经过的时间
        TimeSpan elapsed = DateTime.UtcNow - startTime;
        int elapsedSeconds = (int)elapsed.TotalSeconds;

        // 检查单次计时器是否过期
        if (!isLoop) return elapsedSeconds >= totalSeconds;

        // 循环计时器永不完成（除非被停止）
        return false;
    }

    /// <summary>
    /// 继续未完成的计时器
    /// </summary>
    public void ResumeTimer(string key, UnityAction callback, Text displayText = null)
    {
        // 如果计时器被标记为停止，先移除标记
        stoppedTimers.Remove(key);

        // 检查PlayerPrefs中是否有存储
        string playerPrefsKey = PLAYER_PREFS_PREFIX + key;
        if (!PlayerPrefs.HasKey(playerPrefsKey))
        {
            Debug.LogWarning($"没有找到可恢复的计时器: {key}");
            return;
        }

        // 从PlayerPrefs加载数据
        string[] data = PlayerPrefs.GetString(playerPrefsKey).Split('|');
        DateTime startTime = DateTime.FromBinary(Convert.ToInt64(data[0]));
        int totalSeconds = int.Parse(data[1]);
        bool isLoop = bool.Parse(data[2]);

        // 计算经过的时间
        TimeSpan elapsed = DateTime.UtcNow - startTime;
        int elapsedSeconds = (int)elapsed.TotalSeconds;

        // 处理单次计时器过期情况
        if (!isLoop && elapsedSeconds >= totalSeconds)
        {
            // 直接执行回调并清理
            callback?.Invoke();
            PlayerPrefs.DeleteKey(playerPrefsKey);
            return;
        }

        // 处理循环计时器多次过期情况
        int loopCount = 0;
        if (isLoop && elapsedSeconds >= totalSeconds)
        {
            loopCount = elapsedSeconds / totalSeconds;
            for (int i = 0; i < loopCount; i++)
            {
                callback?.Invoke();
            }
        }

        // 计算剩余时间
        int remaining = totalSeconds - (elapsedSeconds % totalSeconds);

        // 创建新计时器
        TimerData timer = new TimerData()
        {
            key = key,
            totalSeconds = totalSeconds,
            remainingSeconds = remaining,
            isLoop = isLoop,
            callback = callback,
            displayText = displayText,
            startTime = startTime.AddSeconds(loopCount * totalSeconds)
        };

        // 开始计时协程
        timer.coroutine = StartCoroutine(RunTimer(timer));
        activeTimers[key] = timer;

        // 更新存储
        SaveTimerToPlayerPrefs(timer);
    }

    /// <summary>
    /// 停止计时器
    /// </summary>
    public void StopTimer(string key)
    {
        if (activeTimers.TryGetValue(key, out TimerData timer))
        {
            // 停止协程
            if (timer.coroutine != null)
            {
                StopCoroutine(timer.coroutine);
            }

            // 移除计时器
            activeTimers.Remove(key);
        }

        // 添加到停止集合，确保IsTimerComplete返回true
        stoppedTimers.Add(key);

        // 移除PlayerPrefs存储
        PlayerPrefs.DeleteKey(PLAYER_PREFS_PREFIX + key);
    }

    /// <summary>
    /// 运行计时器协程
    /// </summary>
    private IEnumerator RunTimer(TimerData timer)
    {
        while (timer.remainingSeconds > 0)
        {
            // 更新UI显示
            if (timer.displayText != null)
            {
                timer.displayText.text = FormatTime(timer.remainingSeconds);
            }

            // 等待一秒
            yield return new WaitForSeconds(1f);

            // 更新剩余时间
            timer.remainingSeconds--;

            // 更新存储
            SaveTimerToPlayerPrefs(timer);
        }

        // 时间结束处理
        OnTimerCompleted(timer);
    }

    /// <summary>
    /// 计时器完成处理
    /// </summary>
    private void OnTimerCompleted(TimerData timer)
    {
        // 执行回调
        timer.callback?.Invoke();

        // 更新UI显示
        if (timer.displayText != null)
        {
            timer.displayText.text = FormatTime(0);
        }

        if (timer.isLoop)
        {
            // 重置循环计时器
            timer.remainingSeconds = timer.totalSeconds;
            timer.startTime = DateTime.UtcNow;
            timer.coroutine = StartCoroutine(RunTimer(timer));
            SaveTimerToPlayerPrefs(timer);
        }
        else
        {
            // 移除单次计时器
            activeTimers.Remove(timer.key);
            PlayerPrefs.DeleteKey(PLAYER_PREFS_PREFIX + timer.key);
        }
    }

    /// <summary>
    /// 格式化时间为hh:mm:ss
    /// </summary>
    private string FormatTime(int totalSeconds)
    {
        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds % 3600) / 60;
        int seconds = totalSeconds % 60;
        //return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        return $"{minutes:D2}:{seconds:D2}";
    }

    /// <summary>
    /// 保存计时器到PlayerPrefs
    /// </summary>
    private void SaveTimerToPlayerPrefs(TimerData timer)
    {
        string playerPrefsKey = PLAYER_PREFS_PREFIX + timer.key;
        string data = $"{timer.startTime.ToBinary()}|{timer.totalSeconds}|{timer.isLoop}";
        PlayerPrefs.SetString(playerPrefsKey, data);
    }

    /// <summary>
    /// 清理所有计时器
    /// </summary>
    private void OnDestroy()
    {
        if (Instance == this)
        {
            // 停止所有协程
            foreach (var timer in activeTimers.Values)
            {
                if (timer.coroutine != null)
                {
                    StopCoroutine(timer.coroutine);
                }
            }
            Instance = null;
        }
    }
}