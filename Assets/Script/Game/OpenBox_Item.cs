using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 开箱子预制体的奖励
/// </summary>
public class OpenBox_Item : MonoBehaviour
{
    public Image image;
    public Sprite diamondSpr;
    public Sprite coinSpr;

    //根据不同模式显示不同奖励图标
    public void SetSpr()
    {
        if (CommonUtil.IsApple() && GameManager.GetInstance().platform == E_Platform.IOS)
        {
            image.sprite = diamondSpr;
        }
        else
        {
            image.sprite = coinSpr;
        }
        image.SetNativeSize();
    }
}
