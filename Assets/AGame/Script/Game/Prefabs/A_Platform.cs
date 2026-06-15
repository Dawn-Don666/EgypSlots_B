using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ƽ̨
/// </summary>
public class A_Platform : MonoBehaviour
{
    public int m_PlatformId; //ƽ̨id
    public GameObject coinsObj; //���ƽ̨�Դ��Ľ�Ҷ���
    public GameObject timeItemObj; //���ƽ̨�Դ���ʱ����߶���

    public float maxPlatformLength = 2; //�����
    public float minPlatformLength = 1.5f; //�������

    /// <summary>
    /// ��ʼ��ƽ̨
    /// </summary>
    public virtual void Init(int platformId)
    {
        m_PlatformId = platformId;
        coinsObj.SetActive(false);  //�������ƽ̨�ϵĽ��
        timeItemObj.SetActive(false);  //�������ƽ̨�ϵ�ʱ�����
        coinsObj.transform.localScale = new Vector3(1, 1, 1);
        timeItemObj.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        //���ɽ��
        float creatGoldProb;    //���ɽ�ҵĸ���
        if(this is A_MovePlatform)
        {
            creatGoldProb = A_Config.movePlatformGold;
        }
        else if(this is A_ConveyorPlatform)
        {
            creatGoldProb = A_Config.conveyorPlatformGold;
        }
        else if(this is A_SpringPlatform)
        {
            creatGoldProb = A_Config.springPlatformGold;
        }
        else if(this is A_DisappearPlatform)
        {
            creatGoldProb = A_Config.disappearPlatformGold;
        }
        else
        {
            creatGoldProb = A_Config.defaultPlatformGold;
        }

        //�Ƿ����ɽ��
        if(Random.value < creatGoldProb)
        {
            coinsObj.SetActive (true);
        }
        //��������ɽ�� �Ϳ��Կ�һ���Ƿ�����ʱ�����
        else if(Random.value < A_Config.timeItemProbability)
        {
            timeItemObj.SetActive(true);
        }

        //���ð��ӳ���
        Vector3 scale = transform.localScale;
        scale.x = Random.Range(minPlatformLength, maxPlatformLength);
        transform.localScale = scale;
        GetComponent<BoxCollider2D>().autoTiling = true;
        
        float parentScaleX = transform.localScale.x;
    
        if (coinsObj != null)
        {
            Vector3 coinScale = coinsObj.transform.localScale;
            coinsObj.transform.localScale = new Vector3(coinScale.x / parentScaleX, coinScale.y, coinScale.z);
        }
    
        if (timeItemObj != null)
        {
            Vector3 itemScale = timeItemObj.transform.localScale;
            timeItemObj.transform.localScale = new Vector3(itemScale.x / parentScaleX, itemScale.y, itemScale.z);
        }
    }

    /// <summary>
    /// �Ӵ�ƽ̨
    /// </summary>
    /// <param name="collision"></param>
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && (collision.GetContact(0).point.y > transform.position.y || m_PlatformId == 0))
        {
            A_VibrationManager.Instance.Shake(A_ShakeType.Medium);
            AGameController.Instance.SetCtrl(true); //����䵽ƽ̨�ϲ��ܼ�������
            if(m_PlatformId != 0) A_DailyTaskManager.Instance.AddTaskItem(1, 1);  //������Ծƽ̨�����񣬵��治��
            AGameController.Instance.SetLayer(m_PlatformId); //����ƽ̨�
            AGameController.Instance.FallPlatformCount++;   //�䵽ƽ̨������
            
            A_GamePanel.Instance?.StartJumpBySlot();
            if(AGameController.Instance.isCtrling) AEventModule.Send("A_StartChargeUp");    //�����ǰ���ڰ��� ��ʼ����
            AEventModule.Send("A_ChangeScore"); //���ͼ�¼����
        }
    }
}
