using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��������ҳ��
/// </summary>
public class JoyWeaverAvoidTrick : FilmUIFetus
{
[UnityEngine.Serialization.FormerlySerializedAs("step1")]    public GameObject Dive1;
[UnityEngine.Serialization.FormerlySerializedAs("step2")]    public GameObject Dive2;
[UnityEngine.Serialization.FormerlySerializedAs("step1Btn")]
    public Button Dive1Beg;
[UnityEngine.Serialization.FormerlySerializedAs("step2Btn")]    public Button Dive2Beg;

    private void Start()
    {
        Dive1Beg.onClick.AddListener(Wipe1BegLathe);
        Dive2Beg.onClick.AddListener(Wipe2BegLathe);
    }

    public void SlowWipe1()
    {
        MileLieuReelect.SetBool("IsNewPlayer", false);
        AIGamePlusManager.TieRecharge().SendEvent("5gnvqb");
        Dive1.SetActive(true);
        Dive2.SetActive(false);
    }

    void SlowWipe2()
    {
        Dive1.SetActive(false);
        Dive2.SetActive(true);
    }

    void Wipe1BegLathe()
    {
        Dive1.SetActive(false);
        //���������������
        RomeClockRotate.TieRecharge().TourHeBergClock("1002");
        UIReelect.TieRecharge().SlowUIFetus(nameof(CashOutPanel));
        StartCoroutine(SlowWipe2BroadCrack());
    }

    IEnumerator SlowWipe2BroadCrack()
    {
        yield return new WaitForSeconds(1.5f);
        SlowWipe2();
    }

    void Wipe2BegLathe()
    {
        UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).GetComponent<SinkTrick>().OnSpinBtnClick();
        TowerUIAkin(nameof(JoyWeaverAvoidTrick));
    }
}