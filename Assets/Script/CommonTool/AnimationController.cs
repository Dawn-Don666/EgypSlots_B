using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour
{
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    /// <summary>
    /// 弹窗出现效果
    /// </summary>
    /// <param name="PopBarUp"></param>
    public static void PopShow(GameObject PopBarUp,System.Action finish)
    {
        /*-------------------------------------初始化------------------------------------*/
        PopBarUp.GetComponent<CanvasGroup>().alpha = 0;
        PopBarUp.transform.localScale = new Vector3(0, 0, 0);
        /*-------------------------------------动画效果------------------------------------*/
        PopBarUp.GetComponent<CanvasGroup>().DOFade(1, 0.3f).SetUpdate(true);
        PopBarUp.transform.DOScale(1, 0.5f).SetEase(Ease.OutBack).SetUpdate(true).OnComplete(() =>
        {
            finish();
        });
    }


    /// <summary>
    /// 弹窗消失效果
    /// </summary>
    /// <param name="PopBarDisapper"></param>
    public static void PopHide(GameObject PopBarDisapper,System.Action finish)
    {
        /*-------------------------------------初始化------------------------------------*/
        PopBarDisapper.GetComponent<CanvasGroup>().alpha = 1;
        PopBarDisapper.transform.localScale = new Vector3(1, 1, 1);
        /*-------------------------------------动画效果------------------------------------*/
        PopBarDisapper.GetComponent<CanvasGroup>().DOFade(0, 0.5f).SetUpdate(true);
        PopBarDisapper.transform.DOScale(0, 0.5f).SetEase(Ease.InBack).SetUpdate(true).OnComplete(() => 
        {
            finish();
        });
    }
    /// <summary>
    /// 数字变化动画
    /// </summary>
    /// <param name="startNum"></param>
    /// <param name="endNum"></param>
    /// <param name="text"></param>
    /// <param name="finish"></param>
    public static void ChangeNumber(int startNum, int endNum,float delay, Text text,System.Action finish)
    {
        DOTween.To(() => startNum, x => text.text = x.ToString("N0"), endNum, 0.5f).SetDelay(delay).OnComplete(() =>
        {
            finish();
        });
    }

    public static void ChangeNumber(double startNum, double endNum, float delay, Text text, System.Action finish)
    {
        ChangeNumber(startNum, endNum, delay, text, "", finish);
    }
    public static void ChangeNumber(double startNum, double endNum, float delay, Text text, string prefix, System.Action finish)
    {
        DOTween.To(() => startNum, x => text.text = prefix + NumberUtil.DoubleToStr(x), endNum, 0.5f).SetDelay(delay).OnComplete(() =>
        {
            finish();
        });
    }

    /// <summary>
    /// 收金币
    /// </summary>
    /// <param name="GoldImage">金币图标</param>
    /// <param name="a">金币数量</param>
    /// <param name="StartPosition">起始位置</param>
    /// <param name="EndPosition">最终位置</param>
    /// <param name="parent">父物体</param>
    /// <param name="isUI">是否UI</param>
    /// <param name="finish">结束回调</param>
    public static void GoldMoveBest(int num, Vector3 Start, Vector3 target, Transform parent = null, System.Action finish = null, bool isUpdate = false)
    {
        float Delaytime = 0;
        int a = 0;
        for (int i = 0; i < num; i++)
        {
            GameObject GoldItem = Instantiate(Resources.Load<GameObject>("Prefab/Reward"), parent);
            GoldItem.GetComponent<RectTransform>().position = Start;
            //GoldItem.GetComponent<RectTransform>().localScale = new Vector3(0,0,0);
            var T = DOTween.Sequence().SetUpdate(isUpdate);

            //T.Join(GoldItem.transform.DOScale(1, 0.3f));
            T.Append(GoldItem.transform.DOMoveX(target.x, 1f).SetEase(Ease.InOutQuart));
            T.Join(GoldItem.transform.DOMoveY(target.y, 1f).OnComplete(() =>
            {
                a++;
                if (a == 3)
                {
                    if (finish != null)
                    {
                        finish();
                    }
                }
                Destroy(GoldItem);
            }));
            T.SetDelay(Delaytime);
            Delaytime += 0.1f;

        }
    }
    //public static void GoldMoveBest(GameObject GoldImage, int a, Transform StartTF, Transform EndTF, System.Action finish) {
    //    GoldMoveBest(GoldImage, a, StartTF.position, EndTF.position, finish); 
    //}

    /// <summary>
    /// 横向滚动
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="addPosition"></param>
    /// <param name="Finish"></param>
    public static void HorizontalScroll(GameObject obj, float addPosition, System.Action Finish)
    {
        float positionX = obj.transform.localPosition.x;
        float endPostion = positionX + addPosition;
        obj.transform.DOLocalMoveX(endPostion, 2f).SetEase(Ease.InOutQuart).OnComplete(() => {
            Finish?.Invoke();
        });
    }

    /// <summary>
    /// 收取金币特效
    /// </summary>
    /// <param name="target"></param>
    /// <param name="Item"></param>
    /// <param name="Start"></param>
    /// <param name="parent"></param>
    /// <param name="finish"></param>
    public static void CollectAni(Vector3 target, Vector3 Start, Transform parent, System.Action finish)
    {
       
    }
}
