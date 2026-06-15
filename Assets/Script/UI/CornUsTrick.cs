using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 评价引导弹窗
/// </summary>
public class CornUsTrick : FilmUIFetus
{
[UnityEngine.Serialization.FormerlySerializedAs("Stars")]    public Button[] Posit;      //星星按钮
[UnityEngine.Serialization.FormerlySerializedAs("star1Sprite")]    public Sprite Coal1Steady;  //点亮星星的图片
[UnityEngine.Serialization.FormerlySerializedAs("star2Sprite")]    public Sprite Coal2Steady;  //未点亮星星的图片
[UnityEngine.Serialization.FormerlySerializedAs("BGBtn")]    public Button BGBeg;           //背景按钮

    void Start()
    {
        foreach (Button star in Posit)
        {
            star.onClick.AddListener(() =>
            {
                int index = star.transform.GetSiblingIndex();
                QuoteCrawl(index);
            });
        }

        BGBeg.onClick.AddListener(() =>
        {
            UIReelect.TieRecharge().TowerOrRenninUIFetus(gameObject.name);

            if (SinkReelect.TieRecharge().IfDebtDiminish)
            {
                SinkReelect.TieRecharge().TwineShy = true;
            }
        });

    }

    /// <summary>
    /// 点亮星星
    /// </summary>
    /// <param name="index"></param>
    private void QuoteCrawl(int index)
    {
        for (int i = 0; i < 5; i++)
        {
            Posit[i].gameObject.GetComponent<Image>().sprite = i <= index ? Coal1Steady : Coal2Steady;
        }
        //RomeClockRotate.GetInstance().SendEvent("1301", (index + 1).ToString());
        if (index < 3)
        {
            StartCoroutine(RecurTrick());
        } else
        {
            // 跳转到应用商店
            CornMeReelect.instance.SpanAPCryBeaver();
            StartCoroutine(RecurTrick());
        }
        
        // 打点
        RomeClockRotate.TieRecharge().TourClock("1014", (index + 1).ToString());
    }

    /// <summary>
    /// 延迟关闭弹窗
    /// </summary>
    /// <param name="waitTime"></param>
    /// <returns></returns>
    IEnumerator RecurTrick(float waitTime = 0.5f)
    {
        yield return new WaitForSeconds(waitTime);
        UIReelect.TieRecharge().TowerOrRenninUIFetus(nameof(CornUsTrick));
        if (SinkReelect.TieRecharge().IfDebtDiminish) SinkReelect.TieRecharge().TwineShy = true;
    }
}
