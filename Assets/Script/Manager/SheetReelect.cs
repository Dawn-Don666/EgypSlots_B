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
public class SheetReelect : MonoBehaviour
{
    // ����ʵ��
    public static SheetReelect Recharge{ get; private set; }

    // ��ʱ��������
    private class TimerData
    {
        public string Off;
        public int BluffJupiter;
        public int MechanizeJupiter;
        public bool IfOpen;
        public UnityAction Emergent;
        public Text BarrierCrew;
        public DateTime startTime;
        public Coroutine Conqueror;
    }

    // ���л�ļ�ʱ��
    private Dictionary<string, TimerData> InjuryPlight= new Dictionary<string, TimerData>();

    // �洢��ֹͣ�ļ�ʱ����ֵ��ȷ��IsTimerComplete����true
    private HashSet<string> AwesomePlight= new HashSet<string>();

    // �洢��ǰ׺
    private const string PLAYER_PREFS_PREFIX= "Timer_";

    private void Awake()
    {
        if (Recharge != null && Recharge != this)
        {
            Destroy(gameObject);
            return;
        }

        Recharge = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// �����¼�ʱ��
    /// </summary>
    public void CrawlSheet(string key, int seconds, bool isLoop, UnityAction callback, Text displayText = null)
    {
        // ���������ͬkey�ļ�ʱ������ֹͣ
        if (InjuryPlight.ContainsKey(key))
        {
            StopSheet(key);
        }

        // ��ֹͣ�������Ƴ�
        AwesomePlight.Remove(key);

        // �����¼�ʱ��
        TimerData Delta= new TimerData()
        {
            Off = key,
            BluffJupiter = seconds,
            MechanizeJupiter = seconds,
            IfOpen = isLoop,
            Emergent = callback,
            BarrierCrew = displayText,
            startTime = DateTime.UtcNow
        };

        // ��ʼ��ʱЭ��
        Delta.Conqueror = StartCoroutine(SexSheet(Delta));
        InjuryPlight[key] = Delta;

        // ���浽PlayerPrefs
        MileSheetAnWeaverPrefs(Delta);
    }

    /// <summary>
    /// ����ʱ���Ƿ������
    /// </summary>
    public bool BeSheetCrescent(string key)
    {
        // �����ʱ��������ֹͣ������true
        if (AwesomePlight.Contains(key)) return true;

        // �����ʱ���������У��϶�δ���
        if (InjuryPlight.ContainsKey(key)) return false;

        // ���PlayerPrefs���Ƿ��д洢
        string playerPrefsKey = PLAYER_PREFS_PREFIX + key;
        if (!PlayerPrefs.HasKey(playerPrefsKey)) return true;

        // ��PlayerPrefs��������
        string[] Pink= PlayerPrefs.GetString(playerPrefsKey).Split('|');
        DateTime startTime = DateTime.FromBinary(Convert.ToInt64(Pink[0]));
        int BluffJupiter= int.Parse(Pink[1]);
        bool IfOpen= bool.Parse(Pink[2]);

        // ���㾭����ʱ��
        TimeSpan elapsed = DateTime.UtcNow - startTime;
        int elapsedSeconds = (int)elapsed.TotalSeconds;

        // ��鵥�μ�ʱ���Ƿ����
        if (!IfOpen) return elapsedSeconds >= BluffJupiter;

        // ѭ����ʱ��������ɣ����Ǳ�ֹͣ��
        return false;
    }

    /// <summary>
    /// ����δ��ɵļ�ʱ��
    /// </summary>
    public void PilingSheet(string key, UnityAction callback, Text displayText = null)
    {
        // �����ʱ�������Ϊֹͣ�����Ƴ����
        AwesomePlight.Remove(key);

        // ���PlayerPrefs���Ƿ��д洢
        string playerPrefsKey = PLAYER_PREFS_PREFIX + key;
        if (!PlayerPrefs.HasKey(playerPrefsKey))
        {
            Debug.LogWarning($"û���ҵ��ɻָ��ļ�ʱ��: {key}");
            return;
        }

        // ��PlayerPrefs��������
        string[] Pink= PlayerPrefs.GetString(playerPrefsKey).Split('|');
        DateTime startTime = DateTime.FromBinary(Convert.ToInt64(Pink[0]));
        int BluffJupiter= int.Parse(Pink[1]);
        bool IfOpen= bool.Parse(Pink[2]);

        // ���㾭����ʱ��
        TimeSpan elapsed = DateTime.UtcNow - startTime;
        int elapsedSeconds = (int)elapsed.TotalSeconds;

        // �������μ�ʱ���������
        if (!IfOpen && elapsedSeconds >= BluffJupiter)
        {
            // ֱ��ִ�лص�������
            callback?.Invoke();
            PlayerPrefs.DeleteKey(playerPrefsKey);
            return;
        }

        // ����ѭ����ʱ����ι������
        int loopCount = 0;
        if (IfOpen && elapsedSeconds >= BluffJupiter)
        {
            loopCount = elapsedSeconds / BluffJupiter;
            for (int i = 0; i < loopCount; i++)
            {
                callback?.Invoke();
            }
        }

        // ����ʣ��ʱ��
        int remaining = BluffJupiter - (elapsedSeconds % BluffJupiter);

        // �����¼�ʱ��
        TimerData Delta= new TimerData()
        {
            Off = key,
            BluffJupiter = BluffJupiter,
            MechanizeJupiter = remaining,
            IfOpen = IfOpen,
            Emergent = callback,
            BarrierCrew = displayText,
            startTime = startTime.AddSeconds(loopCount * BluffJupiter)
        };

        // ��ʼ��ʱЭ��
        Delta.Conqueror = StartCoroutine(SexSheet(Delta));
        InjuryPlight[key] = Delta;

        // ���´洢
        MileSheetAnWeaverPrefs(Delta);
    }

    /// <summary>
    /// ֹͣ��ʱ��
    /// </summary>
    public void StopSheet(string key)
    {
        if (InjuryPlight.TryGetValue(key, out TimerData timer))
        {
            // ֹͣЭ��
            if (timer.Conqueror != null)
            {
                StopCoroutine(timer.Conqueror);
            }

            // �Ƴ���ʱ��
            InjuryPlight.Remove(key);
        }

        // ���ӵ�ֹͣ���ϣ�ȷ��IsTimerComplete����true
        AwesomePlight.Add(key);

        // �Ƴ�PlayerPrefs�洢
        PlayerPrefs.DeleteKey(PLAYER_PREFS_PREFIX + key);
    }

    /// <summary>
    /// ���м�ʱ��Э��
    /// </summary>
    private IEnumerator SexSheet(TimerData timer)
    {
        while (timer.MechanizeJupiter > 0)
        {
            // ����UI��ʾ
            if (timer.BarrierCrew != null)
            {
                timer.BarrierCrew.text = ButterAnew(timer.MechanizeJupiter);
            }

            // �ȴ�һ��
            yield return new WaitForSeconds(1f);

            // ����ʣ��ʱ��
            timer.MechanizeJupiter--;

            // ���´洢
            MileSheetAnWeaverPrefs(timer);
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
        timer.Emergent?.Invoke();

        // ����UI��ʾ
        if (timer.BarrierCrew != null)
        {
            timer.BarrierCrew.text = ButterAnew(0);
        }

        if (timer.IfOpen)
        {
            // ����ѭ����ʱ��
            timer.MechanizeJupiter = timer.BluffJupiter;
            timer.startTime = DateTime.UtcNow;
            timer.Conqueror = StartCoroutine(SexSheet(timer));
            MileSheetAnWeaverPrefs(timer);
        }
        else
        {
            // �Ƴ����μ�ʱ��
            InjuryPlight.Remove(timer.Off);
            PlayerPrefs.DeleteKey(PLAYER_PREFS_PREFIX + timer.Off);
        }
    }

    /// <summary>
    /// ��ʽ��ʱ��Ϊhh:mm:ss
    /// </summary>
    private string ButterAnew(int totalSeconds)
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
    private void MileSheetAnWeaverPrefs(TimerData timer)
    {
        string playerPrefsKey = PLAYER_PREFS_PREFIX + timer.Off;
        string Pink= $"{timer.startTime.ToBinary()}|{timer.BluffJupiter}|{timer.IfOpen}";
        PlayerPrefs.SetString(playerPrefsKey, Pink);
    }

    /// <summary>
    /// �������м�ʱ��
    /// </summary>
    private void OnDestroy()
    {
        if (Recharge == this)
        {
            // ֹͣ����Э��
            foreach (var timer in InjuryPlight.Values)
            {
                if (timer.Conqueror != null)
                {
                    StopCoroutine(timer.Conqueror);
                }
            }
            Recharge = null;
        }
    }
}