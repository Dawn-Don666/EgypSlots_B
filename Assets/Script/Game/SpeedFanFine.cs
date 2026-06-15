using DG.Tweening;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����
/// </summary>
public class SpeedFanFine : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("BGLight")]    public GameObject BGQuote;  //������
[UnityEngine.Serialization.FormerlySerializedAs("rewardTxt")]    public Text AbsorbUse;  //��������
[UnityEngine.Serialization.FormerlySerializedAs("anim")]    public SkeletonGraphic Soft;  //�����Ӷ���
[UnityEngine.Serialization.FormerlySerializedAs("reward")]    public Transform Absorb;  //��������

    /// <summary>
    /// ��ʼ����Ʒ��
    /// </summary>
    public void Rake()
    {
        BGQuote.SetActive(false);
        AbsorbUse.gameObject.SetActive(false);
        Soft.AnimationState.SetAnimation(0, "idle", true);
        GetComponent<Button>().interactable = true;
    }

    /// <summary>
    /// չʾ
    /// </summary>
    public void Slow(Transform obj, bool isShowLight = false, int ShowTxt = 0)
    {
        GetComponent<Button>().interactable = false;
        Soft.AnimationState.SetAnimation(0, "open", false);
        obj.SetParent(Absorb,false);  //�����������Reward��
        obj.transform.localScale = new Vector2(0.5f, 0.5f);
        Debug.Log(ShowTxt);
        StartCoroutine(SlowWeekly(obj,isShowLight, ShowTxt));
    }

    IEnumerator SlowWeekly(Transform obj, bool isShowLight, int ShowTxt)
    {
        yield return new WaitForSeconds(0.2f);
        //Debug.Log("<color=green>" + Time.timeScale + "</color>");
        obj.DOScale(new Vector2(1.2f, 1.2f), 0.2f).OnComplete(() =>
        {
            obj.DOScale(new Vector2(1f, 1f), 0.1f);
        });
        yield return new WaitForSeconds(0.7f);
        if (isShowLight)
        {
            BGQuote.SetActive (true);
        }
        if(ShowTxt != 0)
        {
            AbsorbUse.gameObject.SetActive(true);
            AbsorbUse.text = ShowTxt.ToString();
        }
    }

    public void DegreeArray()
    {
        for (int j = 0; j < Absorb.childCount; j++)
            Destroy(Absorb.GetChild(j).gameObject);
    }
}
