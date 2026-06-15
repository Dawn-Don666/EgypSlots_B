using DG.Tweening;
using UnityEngine;

public class AGoldFlayCtrl
{
    /// <summary>
    /// 收金币
    /// </summary>
    /// <param name="goldGo">金币图标</param>
    /// <param name="parent">父物体</param>
    /// <param name="startPos">起始位置</param>
    /// <param name="endPos">最终位置</param>
    /// <param name="finish">结束回调</param>
    public static void GoldFlayAnim(GameObject goldGo, Transform parent, Vector3 startPos, Vector3 endPos, System.Action finish = null)
    {
        var a = 8;
        //如果没有就算了
        if (a == 0)
        {
            finish();
        }
        //数量不超过15个
        else if (a > 15) 
        {
            a =15;
        }
        //每个金币的间隔
        float Delaytime = 0;
        for (int i = 0; i < a; i++) 
        {
            int t = i;
            //每次延迟+1
            Delaytime += 0.06f;
            //复制一个图标
            GameObject goldIcon = Object.Instantiate(goldGo, parent);
            //初始化
            goldIcon.transform.position = startPos;
            goldIcon.transform.localScale = new Vector3(1, 1, 1);
            //金币弹出随机位置
            float offsetX = UnityEngine.Random.Range(-0.8f, 0.8f);
            float offsetY = UnityEngine.Random.Range(-0.8f, 0.8f);
            //创建动画队列
            var s = DOTween.Sequence();
            //金币出现
            s.Append(goldIcon.transform.DOMove(new Vector3(goldIcon.transform.position.x + offsetX, goldIcon.transform.position.y + offsetY, goldIcon.transform.position.z), 0.15f).SetDelay(Delaytime).OnComplete(()=> 
            {
                //限制音效播放数量
                if (Mathf.Sin(t)>0)
                {
                    // MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.Sound_GoldCoin);
                }
            }));
            //金币移动到最终位置
            s.Append(goldIcon.transform.DOMove(endPos, 0.6f).SetDelay(0.2f));
            s.Join(goldIcon.transform.DOScale(0.8f, 0.3f).SetEase(Ease.InBack));
            s.AppendCallback(() =>
            {
                //收尾
                s.Kill();
                Object.Destroy(goldIcon);
                if (t == a - 1)
                {
                    finish?.Invoke();
                }
            });
        }
    }
}