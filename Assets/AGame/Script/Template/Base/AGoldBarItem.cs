using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AGoldBarItem : MonoBehaviour
{
    public Text mGoldTxt;
    public GameObject goldCoinPrefab; // 金币预制体
    
    private Tween mGoldTween;
    
    private void Start()
    {
        AEventModule.AddEvent<int>(AEventType.ChangeGold, OnGoldChange);
        
        OnGoldChange(AGameManager.Instance.CurrGold);
    }

    private void OnDestroy()
    {
        AEventModule.RemoveEvent<int>(AEventType.ChangeGold, OnGoldChange);
        mGoldTween?.Kill();
        mGoldTween = null;
    }

    private void OnGoldChange(int gold)
    {
        var oldGold = int.Parse(mGoldTxt.text);
        var newGold = AGameManager.Instance.CurrGold;
        // 数字变化动画
        mGoldTween?.Kill();
        mGoldTween = DOTween.To(() => oldGold, x => 
        {
            oldGold = x;
            mGoldTxt.text = oldGold.ToString();
        }, newGold, 0.5f).SetEase(Ease.OutQuart)
        .OnComplete((() => { mGoldTxt.text = AGameManager.Instance.CurrGold.ToString(); }));
        
    }
    
    /// <summary>
    /// 收金币
    /// </summary>
    /// <param name="startPos">开始位置</param>
    /// <param name="finish">结束回调</param>
    public void GoldCollectAnim(Vector3 startPos, System.Action finish = null)
    {
        // 金币移动到最终位置
        AGoldFlayCtrl.GoldFlayAnim(goldCoinPrefab, transform, startPos, transform.position, finish);
    }
}