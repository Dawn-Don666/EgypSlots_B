using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// µ¯»ÉÆ½Ì¨
/// </summary>
public class A_SpringPlatform : A_Platform
{
    public float jumpMag;   //ÌøÔ¾±¶Êý

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.GetContact(0).point.y > transform.position.y)
        {
            A_Player.Instance.jumpMag = jumpMag;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        A_Player.Instance.jumpMag = 1;
    }
}
