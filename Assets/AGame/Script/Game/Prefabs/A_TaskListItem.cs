using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ÿ�������б���
/// </summary>
public class A_TaskListItem : MonoBehaviour
{
    public Text informationTxt; //�����ı�
    public Text progressTxt;    //�����ı�
    public Text rewardNumberTxt;      //���������ı�
    public Button getRewardBtn; //��ȡ������ť
    public Image itemBG; //Ԥ���屳��
    public Image taskPopFinish;
    public Image taskFinishMask;

    // public Sprite completeSprite; //��ɱ���ͼƬ
    // public Sprite notCompleteSprite; //û����ɱ���ͼƬ
    //public Text rewardBtnTxt;

    private A_DailyTask task; //����

    private void Start()
    {
        getRewardBtn.onClick.AddListener(GetReward);    //�󶨵���¼�
    }

    /// <summary>
    /// ������ʾ
    /// </summary>
    /// <param name="task">����</param>
    public void UpdateItem(A_DailyTask task)
    {
        this.task = task;
        informationTxt.text = A_DailyTaskManager.Instance.GetTaskStr(task); //��ʾ�������
        progressTxt.text = task.currentCount + "/" + task.needCount;    //��ʾ�������
        rewardNumberTxt.text = "��" + task.rewardCount;  //��ʾ��������
        
        //ˢ�°�ť��ʾ
        UpdateBtn();
    }

    /// <summary>
    /// ��ȡ��ť���
    /// </summary>
    void GetReward()
    {
        if(A_DailyTaskManager.Instance.Claim(task.type))    //��ȡ����
        {
            UpdateBtn();
            AEventModule.Send("A_UpdateMainPanel"); //������ʾ��ҳ
        }
    }

    /// <summary>
    /// ˢ�°�ť��ʾ
    /// </summary>
    void UpdateBtn()
    {
        //������ȡ������ʾ��ť
        if (task.currentCount >= task.needCount)
        {
            getRewardBtn.gameObject.SetActive(true);
            getRewardBtn.interactable = true;
        }
        //��������ȡ
        else
        {
            getRewardBtn.gameObject.SetActive(false);
            getRewardBtn.interactable = true;
        }

        //��������Ѿ���ɣ�����ʾ�������
        if (task.isFinish)
        {
            //itemBG.sprite = completeSprite;
            //rewardBtnTxt.color = new Color32(178, 235, 254, 255);
            //rewardBtnTxt.text = "Completed";
            taskFinishMask.gameObject.SetActive(true);
            taskPopFinish.gameObject.SetActive(true);
            getRewardBtn.gameObject.SetActive(false);
            getRewardBtn.interactable = false;
        }
        else
        {
            //itemBG.sprite = notCompleteSprite;
            //rewardBtnTxt.color = new Color32(42, 135, 196, 255);
            //rewardBtnTxt.text = "Get Reward";
            taskFinishMask.gameObject.SetActive(false);
            taskPopFinish.gameObject.SetActive(false);
            // getRewardBtn.gameObject.SetActive(true);
            // getRewardBtn.interactable = true;
        }
    }
}
