using UnityEngine;

/// <summary>
/// 履带平台
/// </summary>
public class A_ConveyorPlatform : A_Platform
{
    public float conveyorVelocity;  //履带的速度

    public Sprite leftSprite;  //左履带精灵图
    public Sprite rightSprite;  //右履带精灵图

    private int direction;  //履带方向，0为左，1为右

    public override void Init(int platformId)
    {
        base.Init(platformId);
        direction = Random.Range(0, 2);  //随机选择方向
        //设置履带精灵图
        if (direction == 0 )    GetComponent<SpriteRenderer>().sprite = leftSprite;
        else                    GetComponent<SpriteRenderer>().sprite = rightSprite;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && collision.GetContact(0).point.y > transform.position.y)
        {
            if (direction == 0)
            {
                A_Player.Instance.deltaVeloumn = new Vector2(-conveyorVelocity, 0);
            }
            else 
            {
                A_Player.Instance.deltaVeloumn = new Vector2(conveyorVelocity, 0);
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        A_Player.Instance.deltaVeloumn = Vector2.zero;
    }
}
