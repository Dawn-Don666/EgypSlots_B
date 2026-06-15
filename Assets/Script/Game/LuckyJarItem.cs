using DG.Tweening;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 礼物
/// </summary>
public class LuckyJarItem : MonoBehaviour
{
    public GameObject BGLight;  //背景光
    public Text rewardTxt;  //奖励数量
    public SkeletonGraphic anim;  //开罐子动画
    public Transform reward;  //奖励物体

    /// <summary>
    /// 初始化礼品盒
    /// </summary>
    public void Init()
    {
        BGLight.SetActive(false);
        rewardTxt.gameObject.SetActive(false);
        anim.AnimationState.SetAnimation(0, "idle", true);
        GetComponent<Button>().interactable = true;
    }

    /// <summary>
    /// 展示
    /// </summary>
    public void Show(Transform obj, bool isShowLight = false, int ShowTxt = 0)
    {
        GetComponent<Button>().interactable = false;
        anim.AnimationState.SetAnimation(0, "open", false);
        obj.SetParent(reward,false);  //奖励物体放在Reward下
        obj.transform.localScale = new Vector2(0.5f, 0.5f);
        Debug.Log(ShowTxt);
        StartCoroutine(ShowReward(obj,isShowLight, ShowTxt));
    }

    IEnumerator ShowReward(Transform obj, bool isShowLight, int ShowTxt)
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
            BGLight.SetActive (true);
        }
        if(ShowTxt != 0)
        {
            rewardTxt.gameObject.SetActive(true);
            rewardTxt.text = ShowTxt.ToString();
        }
    }

    public void DeleteRward()
    {
        for (int j = 0; j < reward.childCount; j++)
            Destroy(reward.GetChild(j).gameObject);
    }
}
