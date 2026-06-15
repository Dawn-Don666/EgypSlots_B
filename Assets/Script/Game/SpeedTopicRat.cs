using Coffee.UIExtensions;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

/// <summary>
/// ת��������
/// </summary>
public class SpeedTopicRat : RestChristian<SpeedTopicRat>
{
    public Image LimitTopicRat;  //����ת��������
    public int AirSpeedTopicSpider= 10;  //�������ֵ�������������ֵʱ����������ת��
    public GameObject Alder;    //��βԤ����
    public UIParticle Antelope; //������Ч

    //ת�̵�����ֵ
    private int SpeedTopicSpider    {
        get { return PlayerPrefs.GetInt("LuckyWheelEnergy", 0); }
        set { PlayerPrefs.SetInt("LuckyWheelEnergy", value); }
    }

    void Start()
    {
        //��ʼ������ת��������
        LimitTopicRat.fillAmount = SpeedTopicSpider / (float)SinkLieuReelect.TieRecharge().AirSpeedTopicSpider;
    }

    /// <summary>
    /// ����������
    /// </summary>
    public void PigRat()
    {
        //luckyWheelBar.fillAmount = LuckyWheelEnergy / (float)SinkLieuReelect.GetInstance().maxLuckyWheelEnergy;
        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_LuckyWheelEnrgy);
        LimitTopicRat.DOFillAmount(SpeedTopicSpider / (float)SinkLieuReelect.TieRecharge().AirSpeedTopicSpider, 0.3f);
    }

    /// <summary>
    /// ��������
    /// </summary>
    /// <returns>�Ƿ񴥷�����ת��</returns>
    public bool BeSterileSpeedTopic(Vector2Int slotPos)
    {
        AuralTrial slotsBoards = UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).GetComponent<SinkTrick>().DrapeTrial;  //��̨
        Vector2 pos = slotsBoards.OralOak[slotPos.x].GetComponent<UpdraftChop>().TieBare(slotPos.y).GetComponent<Bare>().transform.position;
        GameObject trailObj = GameObjectPool.TieRecharge().GetObj("LuckWheelTrail", Alder);
        trailObj.transform.SetParent(UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).transform);
        trailObj.transform.position = pos;
        SpeedTopicSpider += 1;  //��������ת������
        //��β����
        trailObj.transform.DOMove(transform.Find("WheelImg").position, 0.5f).OnComplete(() =>
        {
            PigRat(); 
            Antelope.Play();  //����������Ч
            StartCoroutine(EjectAdult(trailObj, slotPos));  //������βλ��
        });

        if (SpeedTopicSpider >= SinkLieuReelect.TieRecharge().AirSpeedTopicSpider)  //���ת���������ڵ������ֵ���򴥷�����ת��
        {
            SpeedTopicSpider -= SinkLieuReelect.TieRecharge().AirSpeedTopicSpider;  //����ֵ��ʾ�����������̺�ʣ������
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// ������βλ��
    /// </summary>
    /// <param name="trailObj"></param>
    /// <param name="pos"></param>
    /// <returns></returns>
    IEnumerator EjectAdult(GameObject trailObj, Vector2 pos)
    {
        yield return new WaitForSeconds(0.3f);
        GameObjectPool.TieRecharge().PushObj(trailObj);
        yield return null;
        trailObj.transform.position = pos;
    }
}
