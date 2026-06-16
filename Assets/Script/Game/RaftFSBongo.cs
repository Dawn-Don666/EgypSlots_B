using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 5x5FSģʽ��������ʾ���ֵ����
/// </summary>
public class RaftFSBongo : MonoYoungster<RaftFSBongo>
{
    private int[] Variety= new int[5] { 0, 0, 0, 0, 0 };   //�洢5x5FSģʽ�и���λ�õ�����

    public GameObject Woolen_Too;   //��õ��ƶ���Ч
    public GameObject Woolen_Young;   //���ٵ��ƶ���Ч

    public Font MuscleWoad;
    public Font KarstWoad;
    public Font RubWoad;

    private void Start()
    {
        Bike();
    }

    /// <summary>
    /// ��ʼ��
    /// ��ʼ�������е�����
    /// </summary>
    void Bike()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                RaftFSBongoEnvy grid = transform.GetChild(i * 5 + j).GetComponent<RaftFSBongoEnvy>();
                grid.PinEnvyCrease(PestTangFinnish.RatRuminate().RaftFSBongoUnravel[i, j]);
            }
        }
    }

    /// <summary>
    /// ����Win����Boost�����������
    /// </summary>
    /// <param name="slots">��̨��Slots����</param>
    /// <param name="slotsBoard">��̨</param>
    public void PinBravery(ESlotType[,] slots, ShelfBongo slotsBoard)
    {
        StartCoroutine(PinBraverySack(slots, slotsBoard));
    }

    private IEnumerator PinBraverySack(ESlotType[,] slots, ShelfBongo slotsBoard)
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int indexI = i;

                if (Variety[i] > 4) continue;   //��ֹ�±�Խ��
                if (slots[i, j] == ESlotType.Boost)     //������Boost
                {
                    GameObject boostFx = GameObjectPool.RatRuminate().GetObj("BoostMoveFx", Woolen_Young);    //��ը���ƶ���Ч
                    boostFx.transform.SetParent(UIFinnish.RatRuminate().RatCoastMeForm(nameof(PestCoast)).transform.Find("Fx"), false);
                    Vector2 ViralBit= slotsBoard.DoorKey[i].GetComponent<HollandWeek>().RatPose(j).position;   //������Чλ��
                    Vector2 endPos = RatGrid(i, Variety[i]).transform.position;
                    //���Ŷ���
                    slotsBoard.DoorKey[i].GetComponent<HollandWeek>().RatPose(j).GetComponent<Pose>().BootUndertake(2f);

                    boostFx.transform.position = ViralBit;
                    float fxMoveTime = Vector2.Distance(ViralBit, endPos) / 5;   //������Ч�ƶ�ʱ��
                    boostFx.transform.DOMove(endPos, fxMoveTime).OnComplete(() =>
                    {
                        RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_FireBallD);
                        EmbryonicFinnish.RatRuminate().Endow(ShakeType.Soft);   //ˮ����
                        RatGrid(indexI, Variety[indexI]).PinEnvyQuery(EFiveFSGridStateType.Destroyed);
                        Variety[i]++;
                        StartCoroutine(BergSidewalk(boostFx));    //������β
                    });
                    yield return new WaitForSeconds(fxMoveTime);
                }
                else if (slots[i, j] == ESlotType.Win) //������Win
                {
                    GameObject winFx = GameObjectPool.RatRuminate().GetObj("WinMoveFx", Woolen_Too);    //��õ��ƶ���Ч
                    winFx.transform.SetParent(UIFinnish.RatRuminate().RatCoastMeForm(nameof(PestCoast)).transform.Find("Fx"), false);
                    Vector2 ViralBit= slotsBoard.DoorKey[i].GetComponent<HollandWeek>().RatPose(j).position;   //������Чλ��
                    Vector2 endPos = RatGrid(i, Variety[i]).transform.position;
                    //���Ŷ���
                    slotsBoard.DoorKey[i].GetComponent<HollandWeek>().RatPose(j).GetComponent<Pose>().BootUndertake(2f);

                    winFx.transform.position = ViralBit;
                    float fxMoveTime = Vector2.Distance(ViralBit, endPos) / 5;   //������Ч�ƶ�ʱ��
                    winFx.transform.DOMove(endPos, fxMoveTime).OnComplete(() =>
                    {
                        RavenHit.RatRuminate().BootEnigma(RavenRoll.UIMusic.SFX_FireBallW);
                        EmbryonicFinnish.RatRuminate().Endow(ShakeType.Soft);   //ˮ����
                        RatGrid(indexI, Variety[indexI]).PinEnvyQuery(EFiveFSGridStateType.Selected);
                        Variety[i]++;
                        StartCoroutine(BergSidewalk(winFx));    //������β
                    });    //�ƶ���Ч��������
                    yield return new WaitForSeconds(fxMoveTime);
                }
            }
        }

        DiscontentSackFinnish.RatRuminate().DiscontentAge(ESettlementType.WinAndBoostAnim); //����win��boost�����������
    }

    /// <summary>
    /// ������β
    /// </summary>
    /// <param name="trailing"></param>
    /// <returns></returns>
    IEnumerator BergSidewalk(GameObject trailing)
    {
        yield return new WaitForSeconds(0.3f);
        GameObjectPool.RatRuminate().PushObj(trailing);
    }

    /// <summary>
    /// ���������ȡ����
    /// </summary>
    /// <param name="axis">��</param>
    /// <param name="index">�����ϵĵڼ�������</param>
    public RaftFSBongoEnvy RatGrid(int axis, int index)
    {
        return transform.GetChild(axis * 5 + index).GetComponent<RaftFSBongoEnvy>();
    }

    /// <summary>
    /// �������
    /// </summary>
    public void LegalBongo()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                RaftFSBongoEnvy grid = transform.GetChild(i * 5 + j).GetComponent<RaftFSBongoEnvy>();
                grid.PinEnvyQuery(EFiveFSGridStateType.Normal);
            }
        }
        Variety[0] = 0;
        Variety[1] = 0;
        Variety[2] = 0;
        Variety[3] = 0;
        Variety[4] = 0;
    }
}

/// <summary>
/// RaftFSBongo�ĸ��ӵ�����
/// </summary>
public enum EFiveFSGridStateType
{
    Normal,     //Ĭ��״̬
    Selected,   //��ѡ��
    Destroyed,  //���ݻ�
}
