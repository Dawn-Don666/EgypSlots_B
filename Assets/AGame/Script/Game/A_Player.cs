using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���
/// </summary>
public class A_Player : ASingletonBehaviour<A_Player>
{
    public float offsetFloor;    //��ʼ���ذ��λ��
    public float offsetLeftWall; //��ʼ������ǽ��λ��
    public float moveSpeed; //����ƶ��ٶ�

    public float minJumpForce;  //��С��Ծ��
    public float maxJumpForce;  //�����Ծ��

    bool isPlay = false; //����Ƿ�ʼ��Ϸ

    private bool isRight; //����Ƿ������ƶ�
    private Rigidbody2D rigidbody2d; //��ҵĸ������
    [HideInInspector]
    public Vector3 deltaVeloumn = new Vector3(0, 0); //�����ٶ�
    [HideInInspector]
    public float jumpMag = 1; //��Ծ������
    
    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// ��ʼ�����λ��
    /// </summary>
    public void Init()
    {
        transform.position = new Vector3(A_Walls.Instance.leftWall.position.x + offsetLeftWall, A_Walls.Instance.floor.position.y + offsetFloor, 0);
    }

    public void Play()
    {
        isPlay = true;
        rigidbody2d.gravityScale = 1;   //��ʼ��Ϸ1������
    }

    public void Pause() 
    {
        isPlay = false;
        rigidbody2d.gravityScale = 0;   //��ͣ��Ϸû������
    }

    /// <summary>
    /// �����Ծ
    /// </summary>
    /// <param name="forceProportion">��������</param>
    public void Jump(float forceProportion)
    {
        if (isPlay == false) return;
        rigidbody2d.AddForce(new Vector2(0,
            (forceProportion * (maxJumpForce + (A_SaveData.Instance.A_Player_JumpLevel - 1) * A_Config.upgrade_AddJumpForce - minJumpForce) + minJumpForce) * jumpMag));
    }
    
    public void JumpByAuto(float forceProportion)
    {
        if (isPlay == false) return;
        float force = (forceProportion * (maxJumpForce + (A_SaveData.Instance.A_Player_JumpLevel - 1) * A_Config.upgrade_AddJumpForce - minJumpForce) + minJumpForce) * jumpMag;
        //Debug.Log($"跳跃力: {force}, 预估高度: {force * force / (2 * 9.81f)} 米");
        rigidbody2d.AddForce(new Vector2(0, force));
    }

    private void FixedUpdate()
    {
        //���������������
        if (isPlay)
        {
            if (isRight)
            {
                rigidbody2d.velocity = new Vector3(moveSpeed, rigidbody2d.velocity.y, 0) + deltaVeloumn;
            }
            else
            {
                rigidbody2d.velocity = new Vector3(-moveSpeed, rigidbody2d.velocity.y, 0) + deltaVeloumn;
            }
        }
        else
        {
            rigidbody2d.velocity = new Vector3(0, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //������ǽ��������
        if (collision.gameObject.tag == "RightWall")
        {
            A_VibrationManager.Instance.Shake(A_ShakeType.Medium);
            GetComponent<SpriteRenderer>().flipX = true;
            A_GamePanel.Instance.powBarFollowPlayer.offset = new Vector3(-1.1f, 0, 0);
            isRight = false;
        }
        //������ǽ��������
        else if (collision.gameObject.tag == "LeftWall")
        {
            A_VibrationManager.Instance.Shake(A_ShakeType.Medium);
            GetComponent<SpriteRenderer>().flipX = false;
            A_GamePanel.Instance.powBarFollowPlayer.offset = new Vector3(1.1f, 0, 0);
            isRight = true;
        }
    }

    private void Update()
    {
        if (!isPlay) return;
        if(transform.position.y - GetComponent<SpriteRenderer>().bounds.size.y / 2 < Camera.main.transform.position.y - Camera.main.orthographicSize )
        {
            AGameController.Instance.GameOver();    //��Ϸ����
        }
    }
}
