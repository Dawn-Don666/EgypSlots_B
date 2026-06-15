using DG.Tweening;
using UnityEngine;

/// <summary>
/// 会移动的平台
/// </summary>
public class A_MovePlatform : A_Platform
{
    public float moveRoundX; //横向移动范围
    public float moveRoundY; //纵向移动范围
    public float moveTime = 2;  //移动时间

    private int moveDirection;
    private Rigidbody2D rigid;
    private Vector2 currentStartPos;

    public override void Init(int platformId)
    {
        base.Init(platformId);
        rigid = GetComponent<Rigidbody2D>();
        rigid.isKinematic = true;
        rigid.freezeRotation = true;

        currentStartPos = transform.position; // 每次Init取当前正确位置

        transform.DOKill();
        rigid.DOKill();

        moveDirection = Random.Range(0, 4);
        Move(moveDirection);
    }

    void Move(int direction)
    {
        // 先回到起点（防止漂移）
        rigid.position = currentStartPos;

        switch (direction)
        {
            case 0:
                MoveDown();
                break;
            case 1:
                MoveUp();
                break;
            case 2:
                MoveLeft();
                break;
            case 3:
                MoveRight();
                break;
        }
    }

    // 👇 用「去+回」的动画，永远不漂移
    void MoveDown()
    {
        rigid.DOMoveY(currentStartPos.y - moveRoundY, moveTime)
             .SetEase(Ease.InOutQuad)
             .SetUpdate(UpdateType.Fixed)
             .OnComplete(() =>
             {
                 rigid.DOMoveY(currentStartPos.y, moveTime)
                      .SetEase(Ease.InOutQuad)
                      .SetUpdate(UpdateType.Fixed)
                      .OnComplete(MoveDown); // 循环
             });
    }

    void MoveUp()
    {
        rigid.DOMoveY(currentStartPos.y + moveRoundY, moveTime)
             .SetEase(Ease.InOutQuad)
             .SetUpdate(UpdateType.Fixed)
             .OnComplete(() =>
             {
                 rigid.DOMoveY(currentStartPos.y, moveTime)
                      .SetEase(Ease.InOutQuad)
                      .SetUpdate(UpdateType.Fixed)
                      .OnComplete(MoveUp);
             });
    }

    void MoveLeft()
    {
        rigid.DOMoveX(currentStartPos.x - moveRoundX, moveTime)
             .SetEase(Ease.InOutQuad)
             .SetUpdate(UpdateType.Fixed)
             .OnComplete(() =>
             {
                 rigid.DOMoveX(currentStartPos.x, moveTime)
                      .SetEase(Ease.InOutQuad)
                      .SetUpdate(UpdateType.Fixed)
                      .OnComplete(MoveLeft);
             });
    }

    void MoveRight()
    {
        rigid.DOMoveX(currentStartPos.x + moveRoundX, moveTime)
             .SetEase(Ease.InOutQuad)
             .SetUpdate(UpdateType.Fixed)
             .OnComplete(() =>
             {
                 rigid.DOMoveX(currentStartPos.x, moveTime)
                      .SetEase(Ease.InOutQuad)
                      .SetUpdate(UpdateType.Fixed)
                      .OnComplete(MoveRight);
             });
    }
}