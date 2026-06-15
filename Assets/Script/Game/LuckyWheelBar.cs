using Coffee.UIExtensions;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

/// <summary>
/// 转盘能量条
/// </summary>
public class LuckyWheelBar : MonoSingleton<LuckyWheelBar>
{
    public Image luckyWheelBar;  //幸运转盘能量条
    public int maxLuckyWheelEnergy = 10;  //最大能量值，能量到达这个值时，弹出幸运转盘
    public GameObject trail;    //拖尾预制体
    public UIParticle particle; //粒子特效

    //转盘的能量值
    private int LuckyWheelEnergy
    {
        get { return PlayerPrefs.GetInt("LuckyWheelEnergy", 0); }
        set { PlayerPrefs.SetInt("LuckyWheelEnergy", value); }
    }

    void Start()
    {
        //初始化幸运转盘能量条
        luckyWheelBar.fillAmount = LuckyWheelEnergy / (float)GameDataManager.GetInstance().maxLuckyWheelEnergy;
    }

    /// <summary>
    /// 设置能量条
    /// </summary>
    public void SetBar()
    {
        //luckyWheelBar.fillAmount = LuckyWheelEnergy / (float)GameDataManager.GetInstance().maxLuckyWheelEnergy;
        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_LuckyWheelEnrgy);
        luckyWheelBar.DOFillAmount(LuckyWheelEnergy / (float)GameDataManager.GetInstance().maxLuckyWheelEnergy, 0.3f);
    }

    /// <summary>
    /// 增加能量
    /// </summary>
    /// <returns>是否触发幸运转盘</returns>
    public bool IsTriggerLuckyWheel(Vector2Int slotPos)
    {
        SlotsBoard slotsBoards = UIManager.GetInstance().GetPanelByName(nameof(GamePanel)).GetComponent<GamePanel>().slotsBoard;  //机台
        Vector2 pos = slotsBoards.axisArr[slotPos.x].GetComponent<MachineAxis>().GetSlot(slotPos.y).GetComponent<Slot>().transform.position;
        GameObject trailObj = GameObjectPool.GetInstance().GetObj("LuckWheelTrail", trail);
        trailObj.transform.SetParent(UIManager.GetInstance().GetPanelByName(nameof(GamePanel)).transform);
        trailObj.transform.position = pos;
        LuckyWheelEnergy += 1;  //增加幸运转盘能量
        //拖尾飞行
        trailObj.transform.DOMove(transform.Find("WheelImg").position, 0.5f).OnComplete(() =>
        {
            SetBar(); 
            particle.Play();  //播放粒子特效
            StartCoroutine(ResetTrail(trailObj, slotPos));  //重置拖尾位置
        });

        if (LuckyWheelEnergy >= GameDataManager.GetInstance().maxLuckyWheelEnergy)  //如果转盘能量大于等于最大值，则触发幸运转盘
        {
            LuckyWheelEnergy -= GameDataManager.GetInstance().maxLuckyWheelEnergy;  //能量值显示触发幸运钻盘后剩余能量
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 重置拖尾位置
    /// </summary>
    /// <param name="trailObj"></param>
    /// <param name="pos"></param>
    /// <returns></returns>
    IEnumerator ResetTrail(GameObject trailObj, Vector2 pos)
    {
        yield return new WaitForSeconds(0.3f);
        GameObjectPool.GetInstance().PushObj(trailObj);
        yield return null;
        trailObj.transform.position = pos;
    }
}
