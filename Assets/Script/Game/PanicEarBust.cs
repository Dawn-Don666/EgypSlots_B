using DG.Tweening;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ����
/// </summary>
public class PanicEarBust : MonoBehaviour
{
[UnityEngine.Serialization.FormerlySerializedAs("BGLight")]    public GameObject BGPlace;  //������
[UnityEngine.Serialization.FormerlySerializedAs("rewardTxt")]    public Text BetrayOwe;  //��������
[UnityEngine.Serialization.FormerlySerializedAs("anim")]    public SkeletonGraphic Peck;  //�����Ӷ���
[UnityEngine.Serialization.FormerlySerializedAs("reward")]    public Transform Betray;  //��������

    /// <summary>
    /// ��ʼ����Ʒ��
    /// </summary>
    public void Bike()
    {
        BGPlace.SetActive(false);
        BetrayOwe.gameObject.SetActive(false);
        Peck.AnimationState.SetAnimation(0, "idle", true);
        GetComponent<Button>().interactable = true;
    }

    /// <summary>
    /// չʾ
    /// </summary>
    public void With(Transform obj, bool isShowLight = false, int ShowTxt = 0)
    {
        GetComponent<Button>().interactable = false;
        Peck.AnimationState.SetAnimation(0, "open", false);
        obj.SetParent(Betray,false);  //�����������Reward��
        obj.transform.localScale = new Vector2(0.5f, 0.5f);
        Debug.Log(ShowTxt);
        StartCoroutine(WithLeader(obj,isShowLight, ShowTxt));
    }

    IEnumerator WithLeader(Transform obj, bool isShowLight, int ShowTxt)
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
            BGPlace.SetActive (true);
        }
        if(ShowTxt != 0)
        {
            BetrayOwe.gameObject.SetActive(true);
            BetrayOwe.text = ShowTxt.ToString();
        }
    }

    public void AlkaliEmbed()
    {
        for (int j = 0; j < Betray.childCount; j++)
            Destroy(Betray.GetChild(j).gameObject);
    }
}
