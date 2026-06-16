using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// �߼���ʱ��������
/// ֧�ֿ���Ϸ�Ự�ļ�ʱ���־û��ͻָ�
/// </summary>
public class ShellFinnish : MonoBehaviour
{
    // ����ʵ��
    public static ShellFinnish Ruminate{ get; private set; }

    // ��ʱ��������
    private class TimerData
    {
        public string Jay;
        public int GuessEngrave;
        public int UnskilledEngrave;
        public bool AnToll;
        public UnityAction Colonize;
        public Text SoloistPoet;
        public DateTime startTime;
        public Coroutine Grassland;
    }

    // ���л�ļ�ʱ��
    private Dictionary<string, TimerData> NarrowVoyage= new Dictionary<string, TimerData>();

    // �洢��ֹͣ�ļ�ʱ����ֵ��ȷ��IsTimerComplete����true
    private HashSet<string> OverlapVoyage= new HashSet<string>();

    // �洢��ǰ׺
    private const string PLAYER_PREFS_PREFIX= "Timer_";

    private void Awake()
    {
        if (Ruminate != null && Ruminate != this)
        {
            Destroy(gameObject);
            return;
        }

        Ruminate = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// �����¼�ʱ��
    /// </summary>
    public void SwellShell(string key, int seconds, bool isLoop, UnityAction callback, Text displayText = null)
    {
        // ���������ͬkey�ļ�ʱ������ֹͣ
        if (NarrowVoyage.ContainsKey(key))
        {
            LureShell(key);
        }

        // ��ֹͣ�������Ƴ�
        OverlapVoyage.Remove(key);

        // �����¼�ʱ��
        TimerData Their= new TimerData()
        {
            Jay = key,
            GuessEngrave = seconds,
            UnskilledEngrave = seconds,
            AnToll = isLoop,
            Colonize = callback,
            SoloistPoet = displayText,
            startTime = DateTime.UtcNow
        };

        // ��ʼ��ʱЭ��
        Their.Grassland = StartCoroutine(SkiShell(Their));
        NarrowVoyage[key] = Their;

        // ���浽PlayerPrefs
        HalfShellOnDapplePerch(Their);
    }

    /// <summary>
    /// ����ʱ���Ƿ������
    /// </summary>
    public bool UpShellEndeavor(string key)
    {
        // �����ʱ��������ֹͣ������true
        if (OverlapVoyage.Contains(key)) return true;

        // �����ʱ���������У��϶�δ���
        if (NarrowVoyage.ContainsKey(key)) return false;

        // ���PlayerPrefs���Ƿ��д洢
        string playerPrefsKey = PLAYER_PREFS_PREFIX + key;
        if (!PlayerPrefs.HasKey(playerPrefsKey)) return true;

        // ��PlayerPrefs��������
        string[] Full= PlayerPrefs.GetString(playerPrefsKey).Split('|');
        DateTime startTime = DateTime.FromBinary(Convert.ToInt64(Full[0]));
        int GuessEngrave= int.Parse(Full[1]);
        bool AnToll= bool.Parse(Full[2]);

        // ���㾭����ʱ��
        TimeSpan elapsed = DateTime.UtcNow - startTime;
        int elapsedSeconds = (int)elapsed.TotalSeconds;

        // ��鵥�μ�ʱ���Ƿ����
        if (!AnToll) return elapsedSeconds >= GuessEngrave;

        // ѭ����ʱ��������ɣ����Ǳ�ֹͣ��
        return false;
    }

    /// <summary>
    /// ����δ��ɵļ�ʱ��
    /// </summary>
    public void RefillShell(string key, UnityAction callback, Text displayText = null)
    {
        // �����ʱ�������Ϊֹͣ�����Ƴ����
        OverlapVoyage.Remove(key);

        // ���PlayerPrefs���Ƿ��д洢
        string playerPrefsKey = PLAYER_PREFS_PREFIX + key;
        if (!PlayerPrefs.HasKey(playerPrefsKey))
        {
            Debug.LogWarning($"û���ҵ��ɻָ��ļ�ʱ��: {key}");
            return;
        }

        // ��PlayerPrefs��������
        string[] Full= PlayerPrefs.GetString(playerPrefsKey).Split('|');
        DateTime startTime = DateTime.FromBinary(Convert.ToInt64(Full[0]));
        int GuessEngrave= int.Parse(Full[1]);
        bool AnToll= bool.Parse(Full[2]);

        // ���㾭����ʱ��
        TimeSpan elapsed = DateTime.UtcNow - startTime;
        int elapsedSeconds = (int)elapsed.TotalSeconds;

        // �������μ�ʱ���������
        if (!AnToll && elapsedSeconds >= GuessEngrave)
        {
            // ֱ��ִ�лص�������
            callback?.Invoke();
            PlayerPrefs.DeleteKey(playerPrefsKey);
            return;
        }

        // ����ѭ����ʱ����ι������
        int loopCount = 0;
        if (AnToll && elapsedSeconds >= GuessEngrave)
        {
            loopCount = elapsedSeconds / GuessEngrave;
            for (int i = 0; i < loopCount; i++)
            {
                callback?.Invoke();
            }
        }

        // ����ʣ��ʱ��
        int remaining = GuessEngrave - (elapsedSeconds % GuessEngrave);

        // �����¼�ʱ��
        TimerData Their= new TimerData()
        {
            Jay = key,
            GuessEngrave = GuessEngrave,
            UnskilledEngrave = remaining,
            AnToll = AnToll,
            Colonize = callback,
            SoloistPoet = displayText,
            startTime = startTime.AddSeconds(loopCount * GuessEngrave)
        };

        // ��ʼ��ʱЭ��
        Their.Grassland = StartCoroutine(SkiShell(Their));
        NarrowVoyage[key] = Their;

        // ���´洢
        HalfShellOnDapplePerch(Their);
    }

    /// <summary>
    /// ֹͣ��ʱ��
    /// </summary>
    public void LureShell(string key)
    {
        if (NarrowVoyage.TryGetValue(key, out TimerData timer))
        {
            // ֹͣЭ��
            if (timer.Grassland != null)
            {
                StopCoroutine(timer.Grassland);
            }

            // �Ƴ���ʱ��
            NarrowVoyage.Remove(key);
        }

        // ���ӵ�ֹͣ���ϣ�ȷ��IsTimerComplete����true
        OverlapVoyage.Add(key);

        // �Ƴ�PlayerPrefs�洢
        PlayerPrefs.DeleteKey(PLAYER_PREFS_PREFIX + key);
    }

    /// <summary>
    /// ���м�ʱ��Э��
    /// </summary>
    private IEnumerator SkiShell(TimerData timer)
    {
        while (timer.UnskilledEngrave > 0)
        {
            // ����UI��ʾ
            if (timer.SoloistPoet != null)
            {
                timer.SoloistPoet.text = BonitoTomb(timer.UnskilledEngrave);
            }

            // �ȴ�һ��
            yield return new WaitForSeconds(1f);

            // ����ʣ��ʱ��
            timer.UnskilledEngrave--;

            // ���´洢
            HalfShellOnDapplePerch(timer);
        }

        // ʱ���������
        OnTimerCompleted(timer);
    }

    /// <summary>
    /// ��ʱ����ɴ���
    /// </summary>
    private void OnTimerCompleted(TimerData timer)
    {
        // ִ�лص�
        timer.Colonize?.Invoke();

        // ����UI��ʾ
        if (timer.SoloistPoet != null)
        {
            timer.SoloistPoet.text = BonitoTomb(0);
        }

        if (timer.AnToll)
        {
            // ����ѭ����ʱ��
            timer.UnskilledEngrave = timer.GuessEngrave;
            timer.startTime = DateTime.UtcNow;
            timer.Grassland = StartCoroutine(SkiShell(timer));
            HalfShellOnDapplePerch(timer);
        }
        else
        {
            // �Ƴ����μ�ʱ��
            NarrowVoyage.Remove(timer.Jay);
            PlayerPrefs.DeleteKey(PLAYER_PREFS_PREFIX + timer.Jay);
        }
    }

    /// <summary>
    /// ��ʽ��ʱ��Ϊhh:mm:ss
    /// </summary>
    private string BonitoTomb(int totalSeconds)
    {
        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds % 3600) / 60;
        int seconds = totalSeconds % 60;
        //return $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        return $"{minutes:D2}:{seconds:D2}";
    }

    /// <summary>
    /// �����ʱ����PlayerPrefs
    /// </summary>
    private void HalfShellOnDapplePerch(TimerData timer)
    {
        string playerPrefsKey = PLAYER_PREFS_PREFIX + timer.Jay;
        string Full= $"{timer.startTime.ToBinary()}|{timer.GuessEngrave}|{timer.AnToll}";
        PlayerPrefs.SetString(playerPrefsKey, Full);
    }

    /// <summary>
    /// �������м�ʱ��
    /// </summary>
    private void OnDestroy()
    {
        if (Ruminate == this)
        {
            // ֹͣ����Э��
            foreach (var timer in NarrowVoyage.Values)
            {
                if (timer.Grassland != null)
                {
                    StopCoroutine(timer.Grassland);
                }
            }
            Ruminate = null;
        }
    }
}