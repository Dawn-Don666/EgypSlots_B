using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 奖励预制体
/// </summary>
public class RewardPrefab : MonoBehaviour
{
    public Image img;
    public Sprite diamondSpr;   //钻石精灵图
    public Sprite cashSpr;   //绿钞精灵图

    public void Start()
    {
        if (CommonUtil.IsApple() && GameManager.GetInstance().platform == E_Platform.IOS )
        {
            img.sprite = diamondSpr;
        }
        else
        {
            img.sprite = cashSpr;
        }
        img.SetNativeSize();
    }
}
