using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��Ϸ�㼶
/// </summary>
public class A_GamePanel : AUIWindow
{
    public static A_GamePanel Instance;
    public override void OnCreate()
    {
        base.OnCreate();
        Instance = this;
    }

    public Text scoreTxt;  //�����ı�
    public Text layerTxt;  //�㼶�ı�
    public Text goldTxt;  //����ı�
    public Text currentCDTxt;   //��ǰ����ʱ
    public Text maxlayerTxt;    //��ʷ��߲�
    public Image powerBar;  //��Ծ������
    public Image powerBarFollowPlayer;  //������ҵ�������

    public A_ChargeUpBtn chargeUpBtn;  //������ť
    public Button pauseBtn;  //��ͣ��ť
    public Button settingBtn;  //���ð�ť
    public A_UIFollowTarget powBarFollowPlayer; //������ҵ�������

    public float powerBarFillTime;  //������������Ҫ�೤ʱ��

    private bool isChargeUping = false;  //�Ƿ���������
    private bool isChargeAgain = false;  //�Ƿ���������

    private bool isAddCharge = true;  //�Ƿ���������

    [HideInInspector] public int GoldMain; //当前进行的金币
    [HideInInspector] public bool IsSlotGameWin = false;
    private int slotGameWinValue = 0;
    [Header("触发Slot的最大金币数量 默认是5 测试完毕记得改回去")]  public int GoldMainMax = 5; 
    private void Start()
    {
        AEventModule.AddEvent("A_StartChargeUp", StartChargeUp);    //��ʼ�����¼�
        AEventModule.AddEvent("A_EndChargeUp", EndChargeUp);        //���������¼�

        AEventModule.AddEvent<int>("SlotGameWin", (v) =>
        {
            IsSlotGameWin = true;
            slotGameWinValue = v;
        });
        
        AEventModule.AddEvent("A_ChangeGold", () =>
        {
            goldTxt.text = A_SaveData.Instance.A_Gold.ToString();
            // 判断是否是任务触发的增加金币

            if (A_DailyTaskManager.Instance.AddGoldByTask)
            {
                A_DailyTaskManager.Instance.AddGoldByTask = false;
                return;
            }
            
            GoldMain++;
            if (GoldMain > 0 && GoldMain >= GoldMainMax)
            {
                GoldMain = 0;
                AGameController.Instance.PauseGame();
                ShowUI<A_SlotPanel>();
            }
            
        });       //�޸Ľ��
        AEventModule.AddEvent("A_ChangeScore", () => {
            int score = ATimeController.Instance.GetSurvivalTime() * 10 +
                AGameController.Instance.FallPlatformCount * 5 +
                AGameController.Instance.CurrentLayer * 50 * 1;  //1��ϵ��
            //if (score > A_SaveData.Instance.A_BestScore) { A_SaveData.Instance.A_BestScore = score; }
            scoreTxt.text = score.ToString();
        });     //�޸Ļ���
        AEventModule.AddEvent<string>("A_ChangeCD", (str) => currentCDTxt.text = str);     //�޸ĵ���ʱ

        AEventModule.AddEvent<bool>("A_SetCtrlable", SetCtrlable);  //���ÿɿ����¼�
        AEventModule.AddEvent<int>("A_SetLayer", SetLayer);        //���õ�ǰ�������ٲ㼶�¼�
        AEventModule.AddEvent("A_ResetPanel", ResetPanel);        //����ҳ��

        pauseBtn.onClick.AddListener(() => 
        {
            AGameController.Instance.PauseGame();
            ShowUI<A_PausePanel>();
        });
        settingBtn.onClick.AddListener(() => {
            AGameController.Instance.PauseGame();
            ShowUI<A_SettingPanel>();
        });

        ResetPanel();

        //��������
        if(PlayerPrefs.GetInt("A_IsNewPlayer", 1) == 1)
        {
            PlayerPrefs.SetInt("A_IsNewPlayer", 0);
            AGameController.Instance.PauseGame();
            ShowUI<A_HowToPlayPanel>();
        }
    }

    /// <summary>
    /// ����ҳ��
    /// </summary>
    public void ResetPanel()
    {
        layerTxt.text = "0";
        maxlayerTxt.text = A_SaveData.Instance.A_MaxLayer.ToString();
        scoreTxt.text = "0";
        goldTxt.text = A_SaveData.Instance.A_Gold.ToString();
        currentCDTxt.text = ATimeController.Instance.GetStartTime();

        powerBar.fillAmount = 0;    //������Ϊ0
        powerBarFollowPlayer.fillAmount = 0;    //������ҵ�������Ϊ0

        SetCtrlable(true);
    }

    /// <summary>
    /// ��ǰ�������ٲ㼶
    /// </summary>
    /// <param name="layer">�㼶</param>
    public void SetLayer(int layer)
    {
        layerTxt.text = layer.ToString();
        if(layer > A_SaveData.Instance.A_MaxLayer)
        {
            A_SaveData.Instance.A_MaxLayer = layer;
            maxlayerTxt.text = layer.ToString();
        }
    }

    /// <summary>
    /// ���ÿɿ�����
    /// </summary>
    /// <param name="ctrlable">�Ƿ�ɿ�</param>
    public void SetCtrlable(bool ctrlable)
    {
        chargeUpBtn.SetCtrlable(ctrlable);
    }

    public override void OnOpenAnim(Action callback = null)
    {
        Interactable = true;
        callback?.Invoke();
    }

    /// <summary>
    /// ��ʼ����
    /// </summary>
    public void StartChargeUp()
    {
        isChargeAgain = true;
        isChargeUping = true;
    }

    public void StartJumpBySlot()
    {
        if (!IsSlotGameWin) return;
        
        if (slotGameWinValue == 20)
        {
            A_Player.Instance.JumpByAuto(1.3f);    
        }else if (slotGameWinValue == 50)
        {
            A_Player.Instance.JumpByAuto(1.6f);
        }else
        {
            A_Player.Instance.JumpByAuto(2f);
        }
        
        powerBar.fillAmount = 1;
        IsSlotGameWin = false;
    }

    /// <summary>
    /// ��������
    /// </summary>
    public void EndChargeUp()
    {
        isChargeUping = false;
        A_Player.Instance.Jump(powerBar.fillAmount);
    }

    private void Update()
    {
        if (isChargeAgain)
        {
            powerBar.fillAmount = 0;
            powerBarFollowPlayer.fillAmount = 0;
            isAddCharge = true;
            isChargeAgain = false;
        }
        else if(isChargeUping)
        {
            if(isAddCharge)
            {
                powerBar.fillAmount += Time.deltaTime / powerBarFillTime;
                powerBarFollowPlayer.fillAmount = powerBar.fillAmount;
                if(powerBar.fillAmount >= 1) isAddCharge = false;
            }
            else
            {
                powerBar.fillAmount -= Time.deltaTime / powerBarFillTime;
                powerBarFollowPlayer.fillAmount = powerBar.fillAmount;
                if (powerBar.fillAmount <= 0) isAddCharge = true;
            }
        }
    }
}
