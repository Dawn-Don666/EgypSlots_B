using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ϸ������
/// </summary>
public class AGameController : ASingletonBehaviour<AGameController>
{
    [HideInInspector]
    public bool isCtrling = false;   //�Ƿ����ڿ���
    public GameObject goldPrefab;   //���Ԥ����

    private int fallPlatformCount = 0;   //��ǰ�䵽���ٸ�ƽ̨
    public int FallPlatformCount { get { return fallPlatformCount; } set { fallPlatformCount = value; } }

    public int currentLayer = 0;   //������Ϸ��߶��ٲ�
    public int CurrentLayer { get { return currentLayer; } }

    /// <summary>
    /// ��ʼ��Ϸ
    /// </summary>
    public void PlayGame()
    {
        A_Player.Instance.Play();
        ATimeController.Instance.Resume();  //��������ʱ
    }

    /// <summary>
    /// ��ͣ��Ϸ
    /// </summary>
    public void PauseGame()
    {
        A_Player.Instance.Pause();
        ATimeController.Instance.Pause();  //��ͣ����ʱ
    }

    /// <summary>
    /// ������Ϸ
    /// </summary>
    public void ReplayGame()
    {
        A_DailyTaskManager.Instance.AddTaskItem(0, 1);  //���������������
        A_CameraFollow.Instance.ResetPos(); //��������ͷ
        A_Walls.Instance.Init();    //����ǽ�ںͱ���
        A_Player.Instance.Init();  //�������
        AEventModule.Send("A_ResetPanel");  //����ҳ��
        ATimeController.Instance.Stop();    //ֹͣ����ʱ
        PlayGame();
        ATimeController.Instance.StartCountDown();  //��ʼ����ʱ
        fallPlatformCount = 0;  //�䵽ƽ̨��������
        
        A_GamePanel.Instance.GoldMain = 0;
        A_GamePanel.Instance.IsSlotGameWin = false;
        currentLayer = 0;
    }

    /// <summary>
    /// ��Ϸ����
    /// </summary>
    public void GameOver()
    {
        PauseGame();
        AGame.UI.ShowUI<A_SettlementPanel>();
    }

    /// <summary>
    /// ��Ϸ�Ƿ���Կ���
    /// </summary>
    /// <param name="canCtrl">�Ƿ�ɿ�</param>
    public void SetCtrl(bool canCtrl)
    {
        AEventModule.Send<bool>("A_SetCtrlable", canCtrl);
    }

    /// <summary>
    /// ���õ�ǰ�������ٸ�ƽ̨
    /// </summary>
    /// <param name="layer">�������ٸ�ƽ̨</param>
    public void SetLayer(int layer)
    {
        if (layer / A_Walls.Instance.layerHasPlatform > currentLayer)
        {
            currentLayer = layer / A_Walls.Instance.layerHasPlatform;
            AEventModule.Send<int>("A_SetLayer", currentLayer);
        }
    }

}
