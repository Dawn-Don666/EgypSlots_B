using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 老虎机面板
/// 流程：点击按钮 → RelaWeight 先算好结果 → FakeTweenMove 播放滚动动画 → 结束时用 _resultIndices 给真实图标赋值
/// </summary>
public class A_SlotPanel : AUIWindow
{
    // ── Inspector 拖拽赋值 ──────────────────────────────────────

    public Sprite[] SlotSprites;        // 所有道具图标，下标就是道具 ID（0~5）
    public Image realItemImage1;          // 第1列真实结果图（已在中间位置，动画结束后显示）
    public Image realItemImage2;          // 第2列真实结果图
    public Image realItemImage3; 
    public Transform FakeTweenParent;     // 假动画父节点，下面放 15 个子物体（3列×5行）
    public Button OnPlayClick;              // 开始转动按钮

    // ── 常量配置 ────────────────────────────────────────────────

    const int ColCount = 3;             // 列数
    const int RowCount = 5;             // 每列行数
    int _loopMax = 15;                  // 快速滚动循环次数
    float _colStartDelay = 0.4f;        // 每列启动间隔（秒），制造依次启动效果


    List<List<Transform>> _fakeCols;  // 假动画元素 [列][行]
    // ── 运行时缓存 ──────────────────────────────────────────────

    float[][] _colSlotYs;             // 每列 5 个格子的标准 y 坐标（从高到低）
    Vector3[][] _colInitialPos;         // 每个元素的初始位置，用于每轮复位
    float[] _colStepY;                // 每列格子间距（通常 200）
    int[] _colLoopCount;              // 每列当前已滚动的快速循环次数
    int _colFinishedCount;            // 已完成结束动画的列数，满3表示 FakeTweenMove 真正结束

    // ── 权重计算结果（RelaWeight 填充，动画开始前就确定）──────

    bool _isWin;                               // true=三连中奖，false=不中奖
    int[] _resultIndices = new int[ColCount]; // 三列最终显示的道具下标，对应 SlotSprites

    void Start()
    {
        InitFakeIcon();
        OnPlayClick.onClick.AddListener(Slot);
    }

    /// <summary>
    /// 初始化：收集假动画元素，记录每列布局数据，随机设置初始图标
    /// FakeTweenParent 子物体顺序：第1列 child 0~4，第2列 5~9，第3列 10~14
    /// </summary>
    void InitFakeIcon()
    {
        realItemImage1.gameObject.SetActive(false);
        realItemImage2.gameObject.SetActive(false);
        realItemImage3.gameObject.SetActive(false);
        _fakeCols = new List<List<Transform>>();
        _colSlotYs = new float[ColCount][];
        _colInitialPos = new Vector3[ColCount][];
        _colStepY = new float[ColCount];
        _colLoopCount = new int[ColCount];

        for (int col = 0; col < ColCount; col++)
        {
            _fakeCols.Add(new List<Transform>());
            _colInitialPos[col] = new Vector3[RowCount];

            for (int row = 0; row < RowCount; row++)
            {
                // col*5+row 取出对应列对应行的子物体
                Transform item = FakeTweenParent.GetChild(col * RowCount + row);
                item.gameObject.SetActive(true);
                _colInitialPos[col][row] = item.localPosition;
                SetSlotIcon(item, Random.Range(0, SlotSprites.Length));
                _fakeCols[col].Add(item);
            }

            // 记录这一列 5 个格子的标准 y 值（从高到低排序）
            float[] ys = _fakeCols[col].Select(t => t.localPosition.y).OrderByDescending(y => y).ToArray();
            _colSlotYs[col] = ys;
            // 相邻两格的距离就是每次滚动的步长
            _colStepY[col] = Mathf.Abs(ys[0] - ys[1]);
        }
    }

    /// <summary> 给某个格子设置道具图标 </summary>
    void SetSlotIcon(Transform item, int index)
    {
        index = Mathf.Clamp(index, 0, SlotSprites.Length - 1);
        item.Find("Icon").GetComponent<Image>().sprite = SlotSprites[index];
    }

    /// <summary> 点击按钮：先算权重结果，再播放滚动动画 </summary>
    void Slot()
    {
        RelaWeight();
        FakeTweenMove();
        OnPlayClick.gameObject.SetActive(false);
    }

    /// <summary>
    /// 启动三列滚动动画
    /// 每列先复位，再错开 0 / 0.4 / 0.8 秒依次开始
    /// </summary>
    void FakeTweenMove()
    {
        _colFinishedCount = 0;

        for (int col = 0; col < ColCount; col++)
        {
            _colLoopCount[col] = 0;
            //ResetCol(col);

            int c = col;
            AddTimer(_ => ScrollCol(c, "开始"), col * _colStartDelay);
        }
    }

    /// <summary>
    /// 单列滚动一步
    /// phase 三个阶段：
    ///   "开始" → 慢速启动（0.2s）
    ///   "持续" → 快速循环（0.05s，重复 _loopMax 次）
    ///   "结束" → 减速停止（0.2s）
    /// </summary>
    void ScrollCol(int col, string phase)
    {
        float time = phase == "持续" ? 0.05f : 0.2f;
        Ease ease = phase == "开始" ? Ease.InSine : phase == "持续" ? Ease.Linear : Ease.OutCubic;
        float step = _colStepY[col];

        // pending 用于等这一列 5 个元素全部滚完再进入下一步
        int pending = RowCount;

        for (int i = 0; i < RowCount; i++)
        {
            Transform item = _fakeCols[col][i];
            item.DOKill();
            // 往下移动一格
            item.DOLocalMoveY(item.localPosition.y - step, time)
                .SetEase(ease)
                .OnComplete(() =>
                {
                    pending--;
                    if (pending > 0) return;
                    OnScrollDone(col, phase);
                });
        }
    }

    /// <summary> 单列一步滚动完成后的回调，决定下一步做什么 </summary>
    void OnScrollDone(int col, string phase)
    {
        Transform recycled = SnapCol(col);

        // 结束阶段不再换假图标，等会儿直接显示真实结果图
        if (phase != "结束")
        {
            SetSlotIcon(recycled, Random.Range(0, SlotSprites.Length));
        }
            

        if (phase == "开始")
        {
            ScrollCol(col, "持续");
            return;
        }

        if (phase == "持续")
        {
            int spriteIndex = Mathf.Clamp(_resultIndices[col], 0, SlotSprites.Length - 1);
            for (int i = 0; i < _fakeCols[col].Count; i++)
            {
                if (i == 0)
                {
                    SetSlotIcon(_fakeCols[col][i].transform, spriteIndex);
                    break;
                }
            }
            _colLoopCount[col]++;
            ScrollCol(col, _colLoopCount[col] < _loopMax ? "持续" : "结束");
            return;
        }

        // 这一列「结束」阶段滚完
        //ApplyRealItemImage(col);

        _colFinishedCount++;
        if (_colFinishedCount >= ColCount)
            OnFakeTweenMoveComplete();
    }
    
    void OnFakeTweenMoveComplete()
    {
        if (_isWin)
        {
            //跳多少米
            if (_resultIndices[0] == 1)
            {
                AEventModule.Send("SlotGameWin", 20);    
            }else if (_resultIndices[0] == 2)
            {
                AEventModule.Send("SlotGameWin", 50);
            }else if (_resultIndices[0] == 3)
            {
                AEventModule.Send("SlotGameWin", 100);
            }
            else
            {
                Debug.Log("没有要中奖的道具");
            }
        }
        CloseUI();
        AGameController.Instance.PlayGame();
        Debug.Log($"FakeTweenMove 全部结束 _isWin={_isWin}，道具ID=[{_resultIndices[0]}, {_resultIndices[1]}, {_resultIndices[2]}]");
    }

    void ApplyRealItemImage(int col)
    {
        Image[] realImages = { realItemImage1, realItemImage2, realItemImage3 };
        int spriteIndex = Mathf.Clamp(_resultIndices[col], 0, SlotSprites.Length - 1);
        for (int i = 0; i < _fakeCols[col].Count; i++)
        {
            if (i == 0)
            {
                _fakeCols[col][i].gameObject.SetActive(false);
                break;
            }
        }
        realImages[col].sprite = SlotSprites[spriteIndex];
        realImages[col].gameObject.SetActive(true);
    }

    /// <summary>
    /// 把一列元素吸附回 5 个标准格子
    /// 往下滑动一格后，y 最低的那个元素绕回最顶部
    /// </summary>
    Transform SnapCol(int col)
    {
        // 按当前 y 从高到低排序
        List<Transform> sorted = _fakeCols[col].OrderByDescending(t => t.localPosition.y).ToList();
        float[] slotYs = _colSlotYs[col];

        for (int i = 0; i < sorted.Count; i++)
        {
            Transform item = sorted[i];
            Vector3 pos = item.localPosition;
            // 每个元素往下移一格：(i+1)%5 实现环形移位
            item.localPosition = new Vector3(pos.x, slotYs[(i + 1) % sorted.Count], pos.z);
        }

        // 返回被绕回顶部的那一格（y 最低的），用于换随机图标
        return sorted[sorted.Count - 1];
    }

    /// <summary> 复位单列：杀掉 Tween，恢复初始位置，随机图标 </summary>
    void ResetCol(int col)
    {
        for (int i = 0; i < RowCount; i++)
        {
            Transform item = _fakeCols[col][i];
            item.DOKill();
            item.localPosition = _colInitialPos[col][i];
            SetSlotIcon(item, Random.Range(0, SlotSprites.Length));
        }
    }

    /// <summary>
    /// 权重计算：在动画开始前就确定本局胜负和三列结果
    ///
    /// 中奖（75%）：三列显示同一个道具
    ///   道具1 权重50（最高）  道具2 权重30（居中）  道具3 权重20（最低）
    ///
    /// 不中奖（25%）：三列显示不同道具（从 1/2/3 中洗牌分配）
    ///
    /// 结果写入 _isWin 和 _resultIndices，动画结束时读取赋值给 realItemImage
    /// </summary>
    void RelaWeight()
    {
        int winPercent = 75;
        // 下标对应 SlotSprites 索引，0 占位不用，1/2/3 是三种中奖道具
        int[] weights = { 0, 50, 30, 20 };

        if (Random.Range(0, 100) < winPercent)
        {
            // ── 中奖：三列相同 ──
            _isWin = true;
            int winIndex = RollWeight(weights, 1, 3);
            _resultIndices[0] = winIndex;
            _resultIndices[1] = winIndex;
            _resultIndices[2] = winIndex;
        }
        else
        {
            // ── 不中奖：三列各不相同 ──
            _isWin = false;
            int[] pool = { 1, 2, 3 };
            for (int i = 0; i < ColCount; i++)
            {
                int pick = Random.Range(0, pool.Length - i);
                _resultIndices[i] = pool[pick];
                pool[pick] = pool[pool.Length - 1 - i];
            }
        }

        Debug.Log($"老虎机结果 _isWin={_isWin}，道具ID=[{_resultIndices[0]}, {_resultIndices[1]}, {_resultIndices[2]}]");
    }

    /// <summary>
    /// 按权重随机抽取一个道具索引
    /// weights 是权重数组，from~to 是参与抽取的索引范围
    /// 例：weights={0,50,30,20}，from=1，to=3 → 在道具1/2/3中按 50:30:20 抽取
    /// </summary>
    int RollWeight(int[] weights, int from, int to)
    {
        int sum = 0;
        for (int i = from; i <= to; i++)
            sum += weights[i];

        int roll = Random.Range(0, sum);
        for (int i = from; i <= to; i++)
        {
            roll -= weights[i];
            if (roll < 0)
                return i;
        }

        return to;
    }
}
