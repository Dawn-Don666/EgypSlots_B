using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���������Spin����
/// </summary>
public class AgeAlarmTrick : FilmUIFetus
{
[UnityEngine.Serialization.FormerlySerializedAs("watchAdBtn")]    public Button BrickHeBeg;   //���������Spin������ť
[UnityEngine.Serialization.FormerlySerializedAs("giveUpBtn")]    public Button NoteOxBeg;    //��������Spin������ť
[UnityEngine.Serialization.FormerlySerializedAs("addSpinCount")]    public int KeyFlowDaddy= 10;   //����Spin�Ĵ���
[UnityEngine.Serialization.FormerlySerializedAs("watchADImg")]
    public Image BrickADLaw;   //����水ťͼƬ
[UnityEngine.Serialization.FormerlySerializedAs("noAdIconSprite")]    public Sprite GoHeCitySteady;   //û�й���ͼƬ
[UnityEngine.Serialization.FormerlySerializedAs("haveAdIconSprite")]    public Sprite FireHeCitySteady;   //�й���ͼƬ

    private bool WearGrassHe= true;   //�Ƿ���Ҫ�����

    void Start()
    {
        BrickHeBeg.onClick.AddListener(GrassHeBegLathe);
        NoteOxBeg.onClick.AddListener(TactOxBegLathe);
    }

    /// <summary>
    /// ��ʼ��
    /// </summary>
    /// <param name="isProactive">�Ƿ�������ҳ��</param>
    public void Rake(bool isProactive = true)
    {
        BrickADLaw.sprite = FireHeCitySteady;
        if (!isProactive)   //����������ҳ�� ����û�д��������򿪵����
        {
            if (!PlayerPrefs.HasKey("IsFirstOpenAdd10") || PlayerPrefs.GetInt("IsFirstOpenAdd10", 0) == 0)
            {
                WearGrassHe = false;
                BrickADLaw.sprite = GoHeCitySteady;
                PlayerPrefs.SetInt("IsFirstOpenAdd10", 1);
            }
        }
    }

    /// <summary>
    /// ���������Spin����
    /// </summary>
    void GrassHeBegLathe()
    {
        if (WearGrassHe)
        {
            ADReelect.Recharge.GlueWeeklyTrain((b) =>
            {
                if (b)
                {
                    //���Ϳ������spin������
                    RomeClockRotate.TieRecharge().TourHeBergClock("1017");
                    FlowSlow.TieRecharge().AgeSpine(KeyFlowDaddy);
                    TowerUIAkin(nameof(AgeAlarmTrick));
                }
            }, "9");
        }
        else
        {
            FlowSlow.TieRecharge().AgeSpine(KeyFlowDaddy);
            TowerUIAkin(nameof(AgeAlarmTrick));
        }
    }

    /// <summary>
    /// ȡ��
    /// </summary>
    void TactOxBegLathe()
    {
        UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).GetComponent<SinkTrick>().DuckNoDebtFlow();
        ADReelect.Recharge.HeNorwayAgeDaddy();
        TowerUIAkin(nameof(AgeAlarmTrick));
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
