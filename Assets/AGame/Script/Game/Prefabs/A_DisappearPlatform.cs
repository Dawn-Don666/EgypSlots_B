using DG.Tweening;
using System.Collections;
using UnityEngine;

/// <summary>
/// 自动消失的平台
/// </summary>
public class A_DisappearPlatform : A_Platform
{
    public float disappearTime = 3f;    //自动消失时间

    public override void Init(int platformId)
    {
        base.Init(platformId);
        //初始化时停止之前的动画 并设置透明度为1
        GetComponent<SpriteRenderer>().DOKill();
        Color color = GetComponent<SpriteRenderer>().color;
        color.a = 1;
        GetComponent<SpriteRenderer>().color = color;
        GetComponent<BoxCollider2D>().enabled = true;   //可以产生碰撞
    }

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.tag == "Player" && collision.GetContact(0).point.y > transform.position.y)
        {
            StartCoroutine(Disappear());
        }
    }

    /// <summary>
    /// 自动消失
    /// </summary>
    /// <returns></returns>
    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(disappearTime - 1);
        GetComponent<SpriteRenderer>().DOFade(0, 1).OnComplete(() =>
        {
            GetComponent<BoxCollider2D>().enabled = false;  //变透明后不再能碰撞
        });    //1秒内变透明
    }
}
