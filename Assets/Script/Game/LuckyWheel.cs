using Coffee.UIExtensions;
using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 幸运转盘
/// </summary>
public class LuckyWheel : MonoBehaviour
{
    public Transform wheel;     //转盘
    public AnimationCurve showCurve;   //转盘出现动画曲线
    public AnimationCurve rotitionCurve;   //转盘转动动画曲线

    public UIParticle idleParticle;   //转盘旋转粒子
    public UIParticle winParticle;     //转盘停止旋转粒子

    public Image wheelPan;  //转盘
    public Sprite cashPanSpr;  //钞票转盘
    public Sprite diamondPanSpr;   //钻石转盘

    private string prize;       //中奖奖项
    private int cashNum;     //如果中的是钞票，钞票数量

    private void Start()
    {
        //注册事件：隐藏转盘
        MessageCenterLogic.GetInstance().Register("LuckyWheel_Hide", (d) => Hide());

        if (CommonUtil.IsApple() && GameManager.GetInstance().platform == E_Platform.IOS) wheelPan.sprite = diamondPanSpr;
        else wheelPan.sprite = cashPanSpr;
    }

    /// <summary>
    /// 触发转盘
    /// </summary>
    public void TriggerLuckWheel()
    {
        //发送触发大转盘打点
        PostEventScript.GetInstance().SendEvent("1010", SaveData.SpinTimes.ToString());

        //云播放动画
        UIManager.GetInstance().GetPanelByName(nameof(GamePanel)).GetComponent<GamePanel>().SetCloudAnim(CloudAnimType.LuckyWheel,false);
        UIManager.GetInstance().GetPanelByName(nameof(GamePanel)).GetComponent<GamePanel>().moneyRain.gameObject.SetActive(false);  //下钱雨隐藏
        ResetWheel();  //重置转盘
        
        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_LuckyWheelSwitch);
        VibrationManager.GetInstance().Shake(ShakeType.Medium);   //蜂鸣震动
        (transform as RectTransform).DOAnchorPosY(1010 - 40, 1.2f).SetEase(showCurve).OnComplete(() =>
        {
            (transform as RectTransform).DOAnchorPosY(1010, 0.2f).SetEase(Ease.Linear);
            StartCoroutine(Rotate());  //开始旋转转盘
        });
    }

    /// <summary>
    /// 重置转盘
    /// </summary>
    public void ResetWheel()
    {
        wheel.localRotation = Quaternion.identity;  //重置转盘
    }

    /// <summary>
    /// 转动转盘
    /// </summary>
    public IEnumerator Rotate()
    {
        yield return new WaitForSeconds(1.1f);
        idleParticle.Play();  //播放转盘旋转粒子
        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_LuckyWheelRolling);
        //计算中奖
        LuckyWheelData data = GameDataManager.GetInstance().luckyWheelData;
        int sum = data.grandJackpotWeight + data.majorJackpotWeight + data.minorJackpotWeight + data.miniJackpotWeight + data.diamondWeight;
        int randomNum = UnityEngine.Random.Range(0, sum);
        if (randomNum < data.grandJackpotWeight)
        {
            prize = "GrandJackpot";
            wheel.DOLocalRotate(new Vector3(0, 0, 1080 - 36), 3.0f, RotateMode.FastBeyond360).SetEase(rotitionCurve);
        }
        else if (randomNum < data.grandJackpotWeight + data.majorJackpotWeight)
        {
            prize = "MajorJackpot";
            wheel.DOLocalRotate(new Vector3(0, 0, 1080 + 4 * 36), 3.0f, RotateMode.FastBeyond360).SetEase(rotitionCurve);
        }
        else if (randomNum < data.grandJackpotWeight + data.majorJackpotWeight + data.minorJackpotWeight)
        {
            prize = "MinorJackpot";
            if (UnityEngine.Random.Range(0, 2) == 0)
                wheel.DOLocalRotate(new Vector3(0, 0, 1080), 3.0f, RotateMode.FastBeyond360).SetEase(rotitionCurve);
            else
                wheel.DOLocalRotate(new Vector3(0, 0, 1080 + 5 * 36), 3.0f, RotateMode.FastBeyond360).SetEase(rotitionCurve);
        }
        else if (randomNum < data.grandJackpotWeight + data.majorJackpotWeight + data.minorJackpotWeight + data.miniJackpotWeight)
        {
            prize = "MiniJackpot";
            if (UnityEngine.Random.Range(0, 3) == 0)
                wheel.DOLocalRotate(new Vector3(0, 0, 1080 + 2 * 36), 3.0f, RotateMode.FastBeyond360).SetEase(rotitionCurve);
            else if (UnityEngine.Random.Range(0, 3) == 1)
                wheel.DOLocalRotate(new Vector3(0, 0, 1080 - 4 * 36), 3.0f, RotateMode.FastBeyond360).SetEase(rotitionCurve);
            else
                wheel.DOLocalRotate(new Vector3(0, 0, 1080 - 2 * 36), 3.0f, RotateMode.FastBeyond360).SetEase(rotitionCurve);
        }
        else
        {
            prize = "Cash";
            if(UnityEngine.Random.Range(0, 3) == 0)
                wheel.DOLocalRotate(new Vector3(0, 0, 1080 + 36), 3.0f, RotateMode.FastBeyond360);
            else if(UnityEngine.Random.Range(0, 3) == 1)
                wheel.DOLocalRotate(new Vector3(0, 0, 1080 + 3 * 36), 3.0f, RotateMode.FastBeyond360);
            else
                wheel.DOLocalRotate(new Vector3(0, 0, 1080 - 3 * 36), 3.0f, RotateMode.FastBeyond360);
            cashNum = UnityEngine.Random.Range(data.minDiamondNumber, data.maxDiamondNumber + 1);
        }
        StartCoroutine(Reward());
    }

    /// <summary>
    /// 奖励
    /// </summary>
    /// <returns></returns>
    IEnumerator Reward()
    {
        yield return new WaitForSeconds(3f);
        winParticle.Play();  //播放转盘停止旋转粒子
        MusicMgr.GetInstance().PlayEffect(MusicType.UIMusic.SFX_LuckyWheelGet);
        VibrationManager.GetInstance().Shake(ShakeType.Soft);   //水滴震动
        yield return new WaitForSeconds(1f);

        //奖励钻石
        if (prize == "Cash")
        {
            UIManager.GetInstance().ShowUIForms(nameof(GeneralRewardPanel)).GetComponent<GeneralRewardPanel>().Init(cashNum);     //打开奖励页面
        }
        //奖励头奖
        else
        {
            JackpotManager.JackpotType type;
            if(Enum.TryParse(prize, out type))
            {
                UIManager.GetInstance().ShowUIForms(nameof(JackPotPanel)).GetComponent<JackPotPanel>().Init(type, "LuckyWheel");
            }
            else
            {
                Debug.LogError("奖项类型错误：" + prize);
            }
        }
    }

    /// <summary>
    /// 隐藏幸运转盘
    /// </summary>
    void Hide()
    {
        (transform as RectTransform).DOAnchorPosY(1930, 0.6f);
        UIManager.GetInstance().GetPanelByName(nameof(GamePanel)).GetComponent<GamePanel>().cloudSpin.gameObject.SetActive(true);
        UIManager.GetInstance().GetPanelByName(nameof(GamePanel)).GetComponent<GamePanel>().moneyRain.gameObject.SetActive(true);  //下钱雨显示
        UIManager.GetInstance().GetPanelByName(nameof(GamePanel)).GetComponent<GamePanel>().SetCloudAnim(CloudAnimType.Idle,true);
    }
}
