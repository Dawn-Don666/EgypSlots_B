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
public class PanicAtlasEke : MonoYoungster<PanicAtlasEke>
{
    public Image NakedAtlasEke;  //����ת��������
    public int maxPanicAtlasSelect= 10;  //�������ֵ�������������ֵʱ����������ת��
    public GameObject Maize;    //��βԤ����
    public UIParticle Alienate; //������Ч

    //ת�̵�����ֵ
    private int PanicAtlasSelect    {
        get { return PlayerPrefs.GetInt("LuckyWheelEnergy", 0); }
        set { PlayerPrefs.SetInt("LuckyWheelEnergy", value); }
    }

    void Start()
    {
        //��ʼ������ת��������
        NakedAtlasEke.fillAmount = PanicAtlasSelect / (float)PestTangFinnish.RatRuminate().maxPanicAtlasSelect;
    }

    /// <summary>
    /// ����������
    /// </summary>
    public void PinEke()
    {
        //luckyWheelBar.fillAmount = LuckyWheelEnergy / (float)PestTangFinnish.GetInstance().maxLuckyWheelEnergy;
        RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_LuckyWheelEnrgy);
        NakedAtlasEke.DOFillAmount(PanicAtlasSelect / (float)PestTangFinnish.RatRuminate().maxPanicAtlasSelect, 0.3f);
    }

    /// <summary>
    /// ��������
    /// </summary>
    /// <returns>�Ƿ񴥷�����ת��</returns>
    public bool UpControlPanicAtlas(Vector2Int slotPos)
    {
        ShelfBongo slotsBoards = UIFinnish.RatRuminate().RatCoastMeForm(nameof(PestCoast)).GetComponent<PestCoast>().StuffBongo;  //��̨
        Vector2 pos = slotsBoards.DoorKey[slotPos.x].GetComponent<HollandWeek>().RatPose(slotPos.y).GetComponent<Pose>().transform.position;
        GameObject trailObj = GameObjectPool.RatRuminate().GetObj("LuckWheelTrail", Maize);
        trailObj.transform.SetParent(UIFinnish.RatRuminate().RatCoastMeForm(nameof(PestCoast)).transform);
        trailObj.transform.position = pos;
        PanicAtlasSelect += 1;  //��������ת������
        //��β����
        trailObj.transform.DOMove(transform.Find("WheelImg").position, 0.5f).OnComplete(() =>
        {
            PinEke(); 
            Alienate.Play();  //����������Ч
            StartCoroutine(LegalDiver(trailObj, slotPos));  //������βλ��
        });

        if (PanicAtlasSelect >= PestTangFinnish.RatRuminate().maxPanicAtlasSelect)  //���ת���������ڵ������ֵ���򴥷�����ת��
        {
            PanicAtlasSelect -= PestTangFinnish.RatRuminate().maxPanicAtlasSelect;  //����ֵ��ʾ�����������̺�ʣ������
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
    IEnumerator LegalDiver(GameObject trailObj, Vector2 pos)
    {
        yield return new WaitForSeconds(0.3f);
        GameObjectPool.RatRuminate().PushObj(trailObj);
        yield return null;
        trailObj.transform.position = pos;
    }
}
