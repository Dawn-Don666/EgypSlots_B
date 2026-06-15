using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 5x5FSģʽ��������ʾ���ֵ����
/// </summary>
public class WordFSTrial : RestChristian<WordFSTrial>
{
    private int[] Visitor= new int[5] { 0, 0, 0, 0, 0 };   //�洢5x5FSģʽ�и���λ�õ�����

    public GameObject Resume_Pry;   //��õ��ƶ���Ч
    public GameObject Resume_Weary;   //���ٵ��ƶ���Ч

    public Font AroundWavy;
    public Font RulerWavy;
    public Font BogWavy;

    private void Start()
    {
        Rake();
    }

    /// <summary>
    /// ��ʼ��
    /// ��ʼ�������е�����
    /// </summary>
    void Rake()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                WordFSTrialSeed grid = transform.GetChild(i * 5 + j).GetComponent<WordFSTrialSeed>();
                grid.PigSeedJewett(SinkLieuReelect.TieRecharge().WordFSTrialActress[i, j]);
            }
        }
    }

    /// <summary>
    /// ����Win����Boost�����������
    /// </summary>
    /// <param name="slots">��̨��Slots����</param>
    /// <param name="slotsBoard">��̨</param>
    public void PigIceland(ESlotType[,] slots, AuralTrial slotsBoard)
    {
        StartCoroutine(PigIcelandChew(slots, slotsBoard));
    }

    private IEnumerator PigIcelandChew(ESlotType[,] slots, AuralTrial slotsBoard)
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int indexI = i;

                if (Visitor[i] > 4) continue;   //��ֹ�±�Խ��
                if (slots[i, j] == ESlotType.Boost)     //������Boost
                {
                    GameObject boostFx = GameObjectPool.TieRecharge().GetObj("BoostMoveFx", Resume_Weary);    //��ը���ƶ���Ч
                    boostFx.transform.SetParent(UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).transform.Find("Fx"), false);
                    Vector2 SpillHay= slotsBoard.OralOak[i].GetComponent<UpdraftChop>().TieBare(j).position;   //������Чλ��
                    Vector2 endPos = TieSeed(i, Visitor[i]).transform.position;
                    //���Ŷ���
                    slotsBoard.OralOak[i].GetComponent<UpdraftChop>().TieBare(j).GetComponent<Bare>().BeerComponent(2f);

                    boostFx.transform.position = SpillHay;
                    float fxMoveTime = Vector2.Distance(SpillHay, endPos) / 5;   //������Ч�ƶ�ʱ��
                    boostFx.transform.DOMove(endPos, fxMoveTime).OnComplete(() =>
                    {
                        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_FireBallD);
                        HibernateReelect.TieRecharge().Snake(ShakeType.Soft);   //ˮ����
                        TieSeed(indexI, Visitor[indexI]).PigSeedWaste(EFiveFSGridStateType.Destroyed);
                        Visitor[i]++;
                        StartCoroutine(FoulCollapse(boostFx));    //������β
                    });
                    yield return new WaitForSeconds(fxMoveTime);
                }
                else if (slots[i, j] == ESlotType.Win) //������Win
                {
                    GameObject winFx = GameObjectPool.TieRecharge().GetObj("WinMoveFx", Resume_Pry);    //��õ��ƶ���Ч
                    winFx.transform.SetParent(UIReelect.TieRecharge().TieTrickOfLady(nameof(SinkTrick)).transform.Find("Fx"), false);
                    Vector2 SpillHay= slotsBoard.OralOak[i].GetComponent<UpdraftChop>().TieBare(j).position;   //������Чλ��
                    Vector2 endPos = TieSeed(i, Visitor[i]).transform.position;
                    //���Ŷ���
                    slotsBoard.OralOak[i].GetComponent<UpdraftChop>().TieBare(j).GetComponent<Bare>().BeerComponent(2f);

                    winFx.transform.position = SpillHay;
                    float fxMoveTime = Vector2.Distance(SpillHay, endPos) / 5;   //������Ч�ƶ�ʱ��
                    winFx.transform.DOMove(endPos, fxMoveTime).OnComplete(() =>
                    {
                        SnowySit.TieRecharge().BeerMethyl(SnowyUser.UIMusic.SFX_FireBallW);
                        HibernateReelect.TieRecharge().Snake(ShakeType.Soft);   //ˮ����
                        TieSeed(indexI, Visitor[indexI]).PigSeedWaste(EFiveFSGridStateType.Selected);
                        Visitor[i]++;
                        StartCoroutine(FoulCollapse(winFx));    //������β
                    });    //�ƶ���Ч��������
                    yield return new WaitForSeconds(fxMoveTime);
                }
            }
        }

        EverythingChewReelect.TieRecharge().EverythingShy(ESettlementType.WinAndBoostAnim); //����win��boost�����������
    }

    /// <summary>
    /// ������β
    /// </summary>
    /// <param name="trailing"></param>
    /// <returns></returns>
    IEnumerator FoulCollapse(GameObject trailing)
    {
        yield return new WaitForSeconds(0.3f);
        GameObjectPool.TieRecharge().PushObj(trailing);
    }

    /// <summary>
    /// ���������ȡ����
    /// </summary>
    /// <param name="axis">��</param>
    /// <param name="index">�����ϵĵڼ�������</param>
    public WordFSTrialSeed TieSeed(int axis, int index)
    {
        return transform.GetChild(axis * 5 + index).GetComponent<WordFSTrialSeed>();
    }

    /// <summary>
    /// �������
    /// </summary>
    public void EjectTrial()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                WordFSTrialSeed grid = transform.GetChild(i * 5 + j).GetComponent<WordFSTrialSeed>();
                grid.PigSeedWaste(EFiveFSGridStateType.Normal);
            }
        }
        Visitor[0] = 0;
        Visitor[1] = 0;
        Visitor[2] = 0;
        Visitor[3] = 0;
        Visitor[4] = 0;
    }
}

/// <summary>
/// WordFSTrial�ĸ��ӵ�����
/// </summary>
public enum EFiveFSGridStateType
{
    Normal,     //Ĭ��״̬
    Selected,   //��ѡ��
    Destroyed,  //���ݻ�
}
