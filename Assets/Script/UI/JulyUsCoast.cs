using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 评价引导弹窗
/// </summary>
public class JulyUsCoast : AeroUIOnset
{
[UnityEngine.Serialization.FormerlySerializedAs("Stars")]    public Button[] Pride;      //星星按钮
[UnityEngine.Serialization.FormerlySerializedAs("star1Sprite")]    public Sprite Thus1Import;  //点亮星星的图片
[UnityEngine.Serialization.FormerlySerializedAs("star2Sprite")]    public Sprite Thus2Import;  //未点亮星星的图片
[UnityEngine.Serialization.FormerlySerializedAs("BGBtn")]    public Button BGPul;           //背景按钮

    void Start()
    {
        foreach (Button star in Pride)
        {
            star.onClick.AddListener(() =>
            {
                int index = star.transform.GetSiblingIndex();
                PlaceSwell(index);
            });
        }

        BGPul.onClick.AddListener(() =>
        {
            UIFinnish.RatRuminate().CaputOrDugoutUIOnset(gameObject.name);

            if (PestFinnish.RatRuminate().AnAutoSpoonful)
            {
                PestFinnish.RatRuminate().MountAge = true;
            }
        });

    }

    /// <summary>
    /// 点亮星星
    /// </summary>
    /// <param name="index"></param>
    private void PlaceSwell(int index)
    {
        for (int i = 0; i < 5; i++)
        {
            Pride[i].gameObject.GetComponent<Image>().sprite = i <= index ? Thus1Import : Thus2Import;
        }
        //CashDrakeSeaman.GetInstance().SendEvent("1301", (index + 1).ToString());
        if (index < 3)
        {
            StartCoroutine(BlessCoast());
        } else
        {
            // 跳转到应用商店
            JulyMeFinnish.instance.PaceAPPinSecure();
            StartCoroutine(BlessCoast());
        }
        
        // 打点
        CashDrakeSeaman.RatRuminate().TakeDrake("1014", (index + 1).ToString());
    }

    /// <summary>
    /// 延迟关闭弹窗
    /// </summary>
    /// <param name="waitTime"></param>
    /// <returns></returns>
    IEnumerator BlessCoast(float waitTime = 0.5f)
    {
        yield return new WaitForSeconds(waitTime);
        UIFinnish.RatRuminate().CaputOrDugoutUIOnset(nameof(JulyUsCoast));
        if (PestFinnish.RatRuminate().AnAutoSpoonful) PestFinnish.RatRuminate().MountAge = true;
    }
}
