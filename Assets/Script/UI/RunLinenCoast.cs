using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���������Spin����
/// </summary>
public class RunLinenCoast : AeroUIOnset
{
[UnityEngine.Serialization.FormerlySerializedAs("watchAdBtn")]    public Button BreadNoPul;   //���������Spin������ť
[UnityEngine.Serialization.FormerlySerializedAs("giveUpBtn")]    public Button WavyWePul;    //��������Spin������ť
[UnityEngine.Serialization.FormerlySerializedAs("addSpinCount")]    public int GelBaskBland= 10;   //����Spin�Ĵ���
[UnityEngine.Serialization.FormerlySerializedAs("watchADImg")]
    public Image BreadADMix;   //����水ťͼƬ
[UnityEngine.Serialization.FormerlySerializedAs("noAdIconSprite")]    public Sprite AtNoMuteImport;   //û�й���ͼƬ
[UnityEngine.Serialization.FormerlySerializedAs("haveAdIconSprite")]    public Sprite MissNoMuteImport;   //�й���ͼƬ

    private bool WashThickNo= true;   //�Ƿ���Ҫ�����

    void Start()
    {
        BreadNoPul.onClick.AddListener(ThickNoPulFaith);
        WavyWePul.onClick.AddListener(ModeWePulFaith);
    }

    /// <summary>
    /// ��ʼ��
    /// </summary>
    /// <param name="isProactive">�Ƿ�������ҳ��</param>
    public void Bike(bool isProactive = true)
    {
        BreadADMix.sprite = MissNoMuteImport;
        if (!isProactive)   //����������ҳ�� ����û�д��������򿪵����
        {
            if (!PlayerPrefs.HasKey("IsFirstOpenAdd10") || PlayerPrefs.GetInt("IsFirstOpenAdd10", 0) == 0)
            {
                WashThickNo = false;
                BreadADMix.sprite = AtNoMuteImport;
                PlayerPrefs.SetInt("IsFirstOpenAdd10", 1);
            }
        }
    }

    /// <summary>
    /// ���������Spin����
    /// </summary>
    void ThickNoPulFaith()
    {
        if (WashThickNo)
        {
            ADFinnish.Ruminate.WhigLeaderMoral((b) =>
            {
                if (b)
                {
                    //���Ϳ������spin������
                    CashDrakeSeaman.RatRuminate().TakeAtJustDrake("1017");
                    BaskWith.RatRuminate().RunAlien(GelBaskBland);
                    CaputUIEach(nameof(RunLinenCoast));
                }
            }, "9");
        }
        else
        {
            BaskWith.RatRuminate().RunAlien(GelBaskBland);
            CaputUIEach(nameof(RunLinenCoast));
        }
    }

    /// <summary>
    /// ȡ��
    /// </summary>
    void ModeWePulFaith()
    {
        UIFinnish.RatRuminate().RatCoastMeForm(nameof(PestCoast)).GetComponent<PestCoast>().PineNoWoodBask();
        ADFinnish.Ruminate.AtFactorRunBland();
        CaputUIEach(nameof(RunLinenCoast));
    }

    public override void Display(object uiFormParams)
    {
        base.Display(uiFormParams);
        Time.timeScale = 0;
    }

    public override void Hidding()
    {
        base.Hidding();
        Time.timeScale = 1;
    }
}
