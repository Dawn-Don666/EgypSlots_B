using Coffee.UIExtensions;
using Spine.Unity;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// FiveFSBoard上的单个格子
/// </summary>
public class FiveFSBoardGrid : MonoBehaviour
{
    private EFiveFSGridStateType state;     //格子状态
    private int number;     //格子数字

    public UIParticle star;    //闪烁粒子效果

    public GameObject normalTxt;    //正常状态文字
    public GameObject boostTxt;     //乌云状态文字
    public GameObject winTxt;    //胜利状态文字

    /// <summary>
    /// 设置格子的状态
    /// </summary>
    /// <param name="state">格子状态</param>
    public void SetGridState(EFiveFSGridStateType state)
    {
        this.state = state;
        switch (state)
        {
            case EFiveFSGridStateType.Normal:   //正常状态
                normalTxt.SetActive(true);
                boostTxt.SetActive(false);
                winTxt.SetActive(false);
                //设置皮肤
                GetComponent<SkeletonGraphic>().Skeleton.SetSkin("Normal");
                GetComponent<SkeletonGraphic>().Skeleton.SetSlotsToSetupPose();
                GetComponent<SkeletonGraphic>().AnimationState.Apply(GetComponent<SkeletonGraphic>().Skeleton);
                GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "Appear", false);    //播放出现动画
                break;
            case EFiveFSGridStateType.Selected: //被选中状态（获得状态）
                star.Play();
                StartCoroutine(SetAnimation(state));
                break;
            case EFiveFSGridStateType.Destroyed:    //被摧毁状态
                star.Play();
                StartCoroutine(SetAnimation(state));
                break;
        }
    }

    /// <summary>
    /// 设置格子的动画效果
    /// </summary>
    /// <returns></returns>
    IEnumerator SetAnimation(EFiveFSGridStateType state)
    {
        yield return new WaitForSeconds(0.2f);
        if (state == EFiveFSGridStateType.Selected)
        {
            normalTxt.SetActive(false);
            boostTxt.SetActive(false);
            winTxt.SetActive(true);
            GetComponent<SkeletonGraphic>().Skeleton.SetSkin("Win");
        }
        else if (state == EFiveFSGridStateType.Destroyed)
        {
            normalTxt.SetActive(false);
            boostTxt.SetActive(true);
            winTxt.SetActive(false);
            GetComponent<SkeletonGraphic>().Skeleton.SetSkin("Delete");
        }

        //应用皮肤
        GetComponent<SkeletonGraphic>().Skeleton.SetSlotsToSetupPose();
        GetComponent<SkeletonGraphic>().AnimationState.Apply(GetComponent<SkeletonGraphic>().Skeleton);
        GetComponent<SkeletonGraphic>().AnimationState.SetAnimation(0, "idle", true);
    }

    /// <summary>
    /// 设置格子的数字
    /// </summary>
    /// <param name="number">格子数字</param>
    public void SetGridNumber(int number)
    {
        this.number = number;
        normalTxt.GetComponent<Text>().text = ((float)number / 1000).ToString() + "K";
        boostTxt.GetComponent<Text>().text = ((float)number / 1000).ToString() + "K";
        winTxt.GetComponent<Text>().text = ((float)number / 1000).ToString() + "K";
    }

    /// <summary>
    /// 得到格子的状态
    /// </summary>
    /// <returns></returns>
    public EFiveFSGridStateType GetState()
    {
        return state;
    }

    /// <summary>
    /// 得到格子的数字
    /// </summary>
    /// <returns></returns>
    public int GetNumber()
    {
        return number;
    }
}
