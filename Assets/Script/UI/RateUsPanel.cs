using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 评价引导弹窗
/// </summary>
public class RateUsPanel : BaseUIForms
{
    public Button[] Stars;      //星星按钮
    public Sprite star1Sprite;  //点亮星星的图片
    public Sprite star2Sprite;  //未点亮星星的图片
    public Button BGBtn;           //背景按钮

    void Start()
    {
        foreach (Button star in Stars)
        {
            star.onClick.AddListener(() =>
            {
                int index = star.transform.GetSiblingIndex();
                LightStart(index);
            });
        }

        BGBtn.onClick.AddListener(() =>
        {
            UIManager.GetInstance().CloseOrReturnUIForms(gameObject.name);

            if (GameManager.GetInstance().isAutoSpinning)
            {
                GameManager.GetInstance().roundEnd = true;
            }
        });

    }

    /// <summary>
    /// 点亮星星
    /// </summary>
    /// <param name="index"></param>
    private void LightStart(int index)
    {
        for (int i = 0; i < 5; i++)
        {
            Stars[i].gameObject.GetComponent<Image>().sprite = i <= index ? star1Sprite : star2Sprite;
        }
        //PostEventScript.GetInstance().SendEvent("1301", (index + 1).ToString());
        if (index < 3)
        {
            StartCoroutine(closePanel());
        } else
        {
            // 跳转到应用商店
            RateUsManager.instance.OpenAPPinMarket();
            StartCoroutine(closePanel());
        }
        
        // 打点
        PostEventScript.GetInstance().SendEvent("1014", (index + 1).ToString());
    }

    /// <summary>
    /// 延迟关闭弹窗
    /// </summary>
    /// <param name="waitTime"></param>
    /// <returns></returns>
    IEnumerator closePanel(float waitTime = 0.5f)
    {
        yield return new WaitForSeconds(waitTime);
        UIManager.GetInstance().CloseOrReturnUIForms(nameof(RateUsPanel));
        if (GameManager.GetInstance().isAutoSpinning) GameManager.GetInstance().roundEnd = true;
    }
}
