using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>  </summary>
public class SlotPanel : AUIWindow
{
    public GameObject SlotMachine;
    public Sprite[] SlotSprites;
    public Transform RealItemParent;
    List<List<Transform>> RealItems;

    int FakeLoopMax = 15;
    float ColStartDelay = 0.4f;
    bool IsSloting;
    bool IsSlotWin;
    RewardData Reward;
    public Button AllGetBtn;

    float[][] _colSlotYs;
    Vector3[][] _colInitialPos;
    float[] _colStepY;
    int[] _colLoopCount;
    int[] _colResultIndex;
    int _colFinishedCount;

    const int ColCount = 3;
    const int RowCount = 5;
    const int CenterRowIndex = 2;

    void Start()
    {
        Init();
        AllGetBtn.onClick.AddListener(Slot);
    }

    void Init()
    {
        RealItems = new List<List<Transform>>();
        _colSlotYs = new float[ColCount][];
        _colInitialPos = new Vector3[ColCount][];
        _colStepY = new float[ColCount];
        _colLoopCount = new int[ColCount];
        _colResultIndex = new int[ColCount];

        for (int i = 0; i < ColCount; i++)
        {
            RealItems.Add(new List<Transform>());
            _colInitialPos[i] = new Vector3[RowCount];

            for (int j = 0; j < RowCount; j++)
            {
                Transform item = RealItemParent.GetChild(i * RowCount + j);
                _colInitialPos[i][j] = item.localPosition;
                SetSlotIcon(item, Random.Range(0, SlotSprites.Length));
                item.name = $"第{i + 1}列  第{j + 1}行";
                RealItems[i].Add(item);
            }

            CaptureColLayout(i);
        }
    }

    void CaptureColLayout(int col)
    {
        var ys = RealItems[col]
            .Select(t => t.localPosition.y)
            .OrderByDescending(y => y)
            .ToArray();

        _colSlotYs[col] = ys;
        _colStepY[col] = Mathf.Abs(ys[0] - ys[1]);
    }

    public void Slot()
    {
        if (IsSloting)
            return;

        if (RealItems == null || RealItems.Count == 0)
            Init();

        IsSloting = true;
        _colFinishedCount = 0;
        int[] resultIndices = BuildResultIndices();

        for (int i = 0; i < ColCount; i++)
        {
            _colLoopCount[i] = 0;
            _colResultIndex[i] = resultIndices[i];
            ResetCol(i, refreshRandomIcon: true);

            int col = i;
            float delay = col * ColStartDelay;
            AddTimer(_ => StartColScroll(col), delay);
        }
    }

    int[] BuildResultIndices()
    {
        int[] indices = new int[ColCount];
        IsSlotWin = true;

        if (IsSlotWin)
        {
            var tempRewardList = new List<RewardData>();
            var a = new RewardData
            {
                num = 200,
                weight = 10,
                numMax = 999,
                numMin = 100
            };
            tempRewardList.Add(a);
            Reward = tempRewardList[0];

            int totalValue = Reward.num < 10
                ? Mathf.RoundToInt(Reward.num * 100)
                : (int)Reward.num;

            indices[0] = totalValue / 100;
            indices[1] = (totalValue / 10) % 10;
            indices[2] = totalValue % 10;
            Debug.Log("老虎机中奖：数值： " + Reward.num);
        }
        else
        {
            int[] temp = new int[SlotSprites.Length];
            for (int i = 0; i < SlotSprites.Length; i++)
                temp[i] = i;

            for (int i = 0; i < ColCount; i++)
            {
                int pick = Random.Range(0, SlotSprites.Length - i);
                int value = temp[pick];
                temp[pick] = temp[SlotSprites.Length - 1 - i];
                temp[SlotSprites.Length - 1 - i] = value;
                indices[i] = value;
            }
        }

        return indices;
    }

    void StartColScroll(int col)
    {
        ScrollColStep(col, "开始");
    }

    void ScrollColStep(int col, string phase)
    {
        float animTime;
        Ease ease;
        GetPhaseSettings(phase, out animTime, out ease);

        var items = RealItems[col];
        float step = _colStepY[col];
        int pending = items.Count;

        for (int i = 0; i < items.Count; i++)
        {
            Transform item = items[i];
            item.DOKill();

            float targetY = item.localPosition.y - step;
            item.DOLocalMoveY(targetY, animTime)
                .SetEase(ease)
                .OnComplete(() =>
                {
                    pending--;
                    if (pending > 0)
                        return;

                    OnColStepComplete(col, phase);
                });
        }
    }

    void OnColStepComplete(int col, string phase)
    {
        Transform recycled = SnapColToGrid(col);

        if (phase != "结束")
            SetSlotIcon(recycled, Random.Range(0, SlotSprites.Length));

        if (phase == "开始")
        {
            ScrollColStep(col, "持续");
            return;
        }

        if (phase == "持续")
        {
            _colLoopCount[col]++;
            if (_colLoopCount[col] < FakeLoopMax)
                ScrollColStep(col, "持续");
            else
                ScrollColStep(col, "结束");
            return;
        }

        ApplyResultToCenter(col);
        _colFinishedCount++;
        if (_colFinishedCount >= ColCount)
            SlotFinish();
    }

    Transform SnapColToGrid(int col)
    {
        var items = RealItems[col];
        var sorted = items.OrderByDescending(t => t.localPosition.y).ToList();
        float[] slotYs = _colSlotYs[col];
        int count = sorted.Count;

        for (int i = 0; i < count; i++)
        {
            Transform item = sorted[i];
            int slotIndex = (i + 1) % count;
            Vector3 pos = item.localPosition;
            item.localPosition = new Vector3(pos.x, slotYs[slotIndex], pos.z);
        }

        return sorted[count - 1];
    }

    void ApplyResultToCenter(int col)
    {
        float centerY = _colSlotYs[col][CenterRowIndex];
        int spriteIndex = _colResultIndex[col];

        for (int i = 0; i < RealItems[col].Count; i++)
        {
            Transform item = RealItems[col][i];
            if (Mathf.Abs(item.localPosition.y - centerY) < 0.5f)
            {
                SetSlotIcon(item, spriteIndex);
                return;
            }
        }
    }

    void ResetCol(int col, bool refreshRandomIcon)
    {
        var items = RealItems[col];

        for (int i = 0; i < items.Count; i++)
        {
            Transform item = items[i];
            item.DOKill();
            item.localPosition = _colInitialPos[col][i];

            if (refreshRandomIcon)
                SetSlotIcon(item, Random.Range(0, SlotSprites.Length));
        }
    }

    void GetPhaseSettings(string phase, out float animTime, out Ease ease)
    {
        if (phase == "开始")
        {
            animTime = 0.2f;
            ease = Ease.InSine;
        }
        else if (phase == "持续")
        {
            animTime = 0.05f;
            ease = Ease.Linear;
        }
        else
        {
            animTime = 0.2f;
            ease = Ease.OutCubic;
        }
    }

    void SetSlotIcon(Transform item, int index)
    {
        index = Mathf.Clamp(index, 0, SlotSprites.Length - 1);
        item.Find("Icon").GetComponent<Image>().sprite = SlotSprites[index];
    }

    void SlotFinish()
    {
        IsSloting = false;
    }

    void OnDestroy()
    {
        KillAllTweens();
    }

    void KillAllTweens()
    {
        if (RealItems == null)
            return;

        for (int i = 0; i < RealItems.Count; i++)
        {
            for (int j = 0; j < RealItems[i].Count; j++)
                RealItems[i][j].DOKill();
        }
    }
}

public class RewardData
{
    public int weight { get; set; }
    public float num { get; set; }
    public int numMax { get; set; }
    public int numMin { get; set; }
}
