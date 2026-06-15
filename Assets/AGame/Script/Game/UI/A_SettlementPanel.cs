using UnityEngine.UI;

/// <summary>
/// ����ҳ��
/// </summary>
public class A_SettlementPanel : AUIWindow
{
    public Button playAgainBtn; //����һ�ΰ�ť
    public Button backHomeBtn;  //�ص���ҳ��ť
    public Button closeBtn; //�رհ�ť

    void Start()
    {
        playAgainBtn.onClick.AddListener(OnPlayAgainBtnClick);
        backHomeBtn.onClick.AddListener(OnBackHomeBtnClick);
        closeBtn.onClick.AddListener(OnBackHomeBtnClick);

        ATimeController.Instance.Stop();
    }

    /// <summary>
    /// ����һ�ΰ�ť
    /// </summary>
    void OnPlayAgainBtnClick()
    {
        CloseUI();
        AGameController.Instance.ReplayGame();  //����һ��
    }

    /// <summary>
    /// �ص���ҳ��ť
    /// </summary>
    void OnBackHomeBtnClick()
    {
        CloseUI();
        //CloseUI<A_GamePanel>();
        A_GamePanel.Instance.GoldMain = 0;
        A_GamePanel.Instance.IsSlotGameWin = false;
        AGameController.Instance.currentLayer = 0;
        ShowUI<AMainPanel_A>();
    }
}
