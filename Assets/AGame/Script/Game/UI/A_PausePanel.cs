
using UnityEngine.UI;

/// <summary>
/// ��ͣҳ��
/// </summary>
public class A_PausePanel : AUIWindow
{
    public Button giveUpBtn; //���ذ�ť
    public Button closeBtn; //�رհ�ť
    public Button cancelBtn;    //ȡ����ť
    void Start()
    {
        giveUpBtn.onClick.AddListener(BackHome);
        closeBtn.onClick.AddListener(ClosePanel);
        cancelBtn.onClick.AddListener(ClosePanel);
    }

    /// <summary>
    /// ������ҳ
    /// </summary>
    void BackHome()
    {
        CloseUI();
        //CloseUI<A_GamePanel>();
        ATimeController.Instance.Stop();    //ֹͣ��ʱ
        ShowUI<AMainPanel_A>();
        A_GamePanel.Instance.GoldMain = 0;
        A_GamePanel.Instance.IsSlotGameWin = false;
        AGameController.Instance.currentLayer = 0;
    }

    /// <summary>
    /// ������Ϸ
    /// </summary>
    void ClosePanel()
    {
        CloseUI();
        AGameController.Instance.PlayGame();
    }
}
