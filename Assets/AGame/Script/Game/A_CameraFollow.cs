using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 摄像机跟随
/// </summary>
public class A_CameraFollow : ASingletonBehaviour<A_CameraFollow>
{
    private float offsetY = 3f;     //玩家高度大于摄像机多少时摄像机启动跟随

    void LateUpdate()
    {
        if(A_Player.Instance.transform.position.y > transform.position.y + offsetY)
        {
            transform.position = new Vector3(transform.position.x, A_Player.Instance.transform.position.y - offsetY, transform.position.z);
            A_Walls.Instance.leftWall.position = new Vector3(A_Walls.Instance.leftWall.position.x, A_Player.Instance.transform.position.y - offsetY, A_Walls.Instance.leftWall.position.z);
            A_Walls.Instance.rightWall.position = new Vector3(A_Walls.Instance.rightWall.position.x, A_Player.Instance.transform.position.y - offsetY, A_Walls.Instance.rightWall.position.z);
            A_Walls.Instance.background.position = new Vector3(A_Walls.Instance.background.position.x, A_Player.Instance.transform.position.y - offsetY, A_Walls.Instance.background.position.z);
        }
    }

    /// <summary>
    /// 重置摄像机位置
    /// </summary>
    public void ResetPos()
    {
        transform.position = new Vector3(0, 0, -10);
    }
}
