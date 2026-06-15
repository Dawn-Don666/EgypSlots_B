using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 时间道具
/// </summary>
public class A_TimeItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //被碰撞的对象是玩家，则隐藏时间道具
        if (collision.gameObject.tag == "Player")
        {
            ATimeController.Instance.AddTime();
            gameObject.SetActive(false);
        }
    }
}
