using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 5x5FS模式中用来显示数字的面板
/// </summary>
public class FiveFSBoard : MonoSingleton<FiveFSBoard>
{
    private int[] indexes = new int[5] { 0, 0, 0, 0, 0 };   //存储5x5FS模式中各个位置的索引

    public GameObject movefx_Win;   //获得的移动特效
    public GameObject movefx_Boost;   //销毁的移动特效

    public Font normalFont;
    public Font boostFont;
    public Font winFont;

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// 初始化
    /// 初始化格子中的数字
    /// </summary>
    void Init()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                FiveFSBoardGrid grid = transform.GetChild(i * 5 + j).GetComponent<FiveFSBoardGrid>();
                grid.SetGridNumber(GameDataManager.GetInstance().FiveFSBoardNumbers[i, j]);
            }
        }
    }

    /// <summary>
    /// 根据Win还是Boost类型设置面板
    /// </summary>
    /// <param name="slots">机台的Slots类型</param>
    /// <param name="slotsBoard">机台</param>
    public void SetResults(ESlotType[,] slots, SlotsBoard slotsBoard)
    {
        StartCoroutine(SetResultsAnim(slots, slotsBoard));
    }

    private IEnumerator SetResultsAnim(ESlotType[,] slots, SlotsBoard slotsBoard)
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int indexI = i;

                if (indexes[i] > 4) continue;   //防止下标越界
                if (slots[i, j] == ESlotType.Boost)     //格子是Boost
                {
                    GameObject boostFx = GameObjectPool.GetInstance().GetObj("BoostMoveFx", movefx_Boost);    //爆炸的移动特效
                    boostFx.transform.SetParent(UIManager.GetInstance().GetPanelByName(nameof(GamePanel)).transform.Find("Fx"), false);
                    Vector2 startPos = slotsBoard.axisArr[i].GetComponent<MachineAxis>().GetSlot(j).position;   //设置特效位置
                    Vector2 endPos = GetGrid(i, indexes[i]).transform.position;
                    //播放动画
                    slotsBoard.axisArr[i].GetComponent<MachineAxis>().GetSlot(j).GetComponent<Slot>().PlayAnimation(2f);

                    boostFx.transform.position = startPos;
                    float fxMoveTime = Vector2.Distance(startPos, endPos) / 5;   //设置特效移动时间
                    boostFx.transform.DOMove(endPos, fxMoveTime).OnComplete(() =>
                    {
                        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_FireBallD);
                        VibrationManager.GetInstance().Shake(ShakeType.Soft);   //水滴震动
                        GetGrid(indexI, indexes[indexI]).SetGridState(EFiveFSGridStateType.Destroyed);
                        indexes[i]++;
                        StartCoroutine(HideTrailing(boostFx));    //隐藏拖尾
                    });
                    yield return new WaitForSeconds(fxMoveTime);
                }
                else if (slots[i, j] == ESlotType.Win) //格子是Win
                {
                    GameObject winFx = GameObjectPool.GetInstance().GetObj("WinMoveFx", movefx_Win);    //获得的移动特效
                    winFx.transform.SetParent(UIManager.GetInstance().GetPanelByName(nameof(GamePanel)).transform.Find("Fx"), false);
                    Vector2 startPos = slotsBoard.axisArr[i].GetComponent<MachineAxis>().GetSlot(j).position;   //设置特效位置
                    Vector2 endPos = GetGrid(i, indexes[i]).transform.position;
                    //播放动画
                    slotsBoard.axisArr[i].GetComponent<MachineAxis>().GetSlot(j).GetComponent<Slot>().PlayAnimation(2f);

                    winFx.transform.position = startPos;
                    float fxMoveTime = Vector2.Distance(startPos, endPos) / 5;   //设置特效移动时间
                    winFx.transform.DOMove(endPos, fxMoveTime).OnComplete(() =>
                    {
                        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_FireBallW);
                        VibrationManager.GetInstance().Shake(ShakeType.Soft);   //水滴震动
                        GetGrid(indexI, indexes[indexI]).SetGridState(EFiveFSGridStateType.Selected);
                        indexes[i]++;
                        StartCoroutine(HideTrailing(winFx));    //隐藏拖尾
                    });    //移动特效到格子上
                    yield return new WaitForSeconds(fxMoveTime);
                }
            }
        }

        SettlementAnimManager.GetInstance().SettlementEnd(ESettlementType.WinAndBoostAnim); //本次win和boost动画播放完毕
    }

    /// <summary>
    /// 隐藏拖尾
    /// </summary>
    /// <param name="trailing"></param>
    /// <returns></returns>
    IEnumerator HideTrailing(GameObject trailing)
    {
        yield return new WaitForSeconds(0.3f);
        GameObjectPool.GetInstance().PushObj(trailing);
    }

    /// <summary>
    /// 根据坐标获取格子
    /// </summary>
    /// <param name="axis">轴</param>
    /// <param name="index">该轴上的第几个格子</param>
    public FiveFSBoardGrid GetGrid(int axis, int index)
    {
        return transform.GetChild(axis * 5 + index).GetComponent<FiveFSBoardGrid>();
    }

    /// <summary>
    /// 重置面板
    /// </summary>
    public void ResetBoard()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                FiveFSBoardGrid grid = transform.GetChild(i * 5 + j).GetComponent<FiveFSBoardGrid>();
                grid.SetGridState(EFiveFSGridStateType.Normal);
            }
        }
        indexes[0] = 0;
        indexes[1] = 0;
        indexes[2] = 0;
        indexes[3] = 0;
        indexes[4] = 0;
    }
}

/// <summary>
/// FiveFSBoard的格子的类型
/// </summary>
public enum EFiveFSGridStateType
{
    Normal,     //默认状态
    Selected,   //被选中
    Destroyed,  //被摧毁
}
