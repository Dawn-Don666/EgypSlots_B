using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ȴ�С��Ϸ�����ҳ��
/// </summary>
public class FestiveBackGrassHeTrick : FilmUIFetus
{
[UnityEngine.Serialization.FormerlySerializedAs("watchAdBtn")]    public Button BrickHeBeg;   //������ö������λ��ᰴť
[UnityEngine.Serialization.FormerlySerializedAs("giveUpBtn")]    public Button NoteOxBeg;    //������ť
    void Start()
    {
        BrickHeBeg.onClick.AddListener(GrassHeBegLathe);
        NoteOxBeg.onClick.AddListener(TactOxBegLathe);
    }

    /// <summary>
    /// ������ö������λ��ᰴť����¼�
    /// </summary>
    void GrassHeBegLathe()
    {
        //���´��뿴���ִ��
        ADReelect.Recharge.GlueWeeklyTrain((b) =>
        {
            if (b)
            {
                TowerUIAkin(nameof(FestiveBackGrassHeTrick));   //�رմ�ҳ��
                CollectGoldenDaunt.TieRecharge().Tour("CompareSize_WatchAd");
            }
        },"11");
    }

    /// <summary>
    /// ������ť����¼�
    /// </summary>
    void TactOxBegLathe()
    {
        ADReelect.Recharge.HeNorwayAgeDaddy();
        TowerUIAkin(nameof(FestiveBackGrassHeTrick));   //�رմ�ҳ��
        CollectGoldenDaunt.TieRecharge().Tour("CompareSize_GiveUp");
    }
}
