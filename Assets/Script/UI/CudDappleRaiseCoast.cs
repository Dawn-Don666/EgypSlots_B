using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��������ҳ��
/// </summary>
public class CudDappleRaiseCoast : AeroUIOnset
{
[UnityEngine.Serialization.FormerlySerializedAs("step1")]    public GameObject Flee1;
[UnityEngine.Serialization.FormerlySerializedAs("step2")]    public GameObject Flee2;
[UnityEngine.Serialization.FormerlySerializedAs("step1Btn")]
    public Button Flee1Pul;
[UnityEngine.Serialization.FormerlySerializedAs("step2Btn")]    public Button Flee2Pul;

    private void Start()
    {
        Flee1Pul.onClick.AddListener(Soul1PulFaith);
        Flee2Pul.onClick.AddListener(Soul2PulFaith);
    }

    public void WithSoul1()
    {
        HalfTangFinnish.SetBool("IsNewPlayer", false);
        Flee1.SetActive(true);
        Flee2.SetActive(false);
    }

    void WithSoul2()
    {
        Flee1.SetActive(false);
        Flee2.SetActive(true);
    }

    void Soul1PulFaith()
    {
        Flee1.SetActive(false);
        //���������������
        CashDrakeSeaman.RatRuminate().TakeAtJustDrake("1002");
        UIFinnish.RatRuminate().WithUIOnset(nameof(CashOutPanel));
        StartCoroutine(WithSoul2UnityMeaty());
    }

    IEnumerator WithSoul2UnityMeaty()
    {
        yield return new WaitForSeconds(1.5f);
        WithSoul2();
    }

    void Soul2PulFaith()
    {
        AIGamePlusManager.RatRuminate().SendEvent("5gnvqb");
        UIFinnish.RatRuminate().RatCoastMeForm(nameof(PestCoast)).GetComponent<PestCoast>().OnSpinBtnClick();
        CaputUIEach(nameof(CudDappleRaiseCoast));
    }
}