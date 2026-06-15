using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 矩形遮罩镂空
/// </summary>
public class MainAvoidNail : MonoBehaviour
{
    public static MainAvoidNail instance;
[UnityEngine.Serialization.FormerlySerializedAs("Hand")]    public GameObject Both;

    /// <summary>
    /// 高亮显示目标
    /// </summary>
    private GameObject Potash;
[UnityEngine.Serialization.FormerlySerializedAs("Text")]
    public Text Crew;
    /// <summary>
    /// 区域范围缓存
    /// </summary>
    private Vector3[] Quality= new Vector3[4];
    /// <summary>
    /// 镂空区域中心
    /// </summary>
    private Vector4 Patrol;
    /// <summary>
    /// 最终的偏移x
    /// </summary>
    private float PotashAmountX= 0;
    /// <summary>
    /// 最终的偏移y
    /// </summary>
    private float PotashAmountY= 0;
    /// <summary>
    /// 遮罩材质
    /// </summary>
    private Material Nystatin;
    /// <summary>
    /// 当前的偏移x
    /// </summary>
    private float ArtworkAmountX= 0f;
    /// <summary>
    /// 当前的偏移y
    /// </summary>
    private float ArtworkAmountY= 0f;
    /// <summary>
    /// 高亮区域缩放的动画时间
    /// </summary>
    private float GrowthAnew= 0.1f;
    /// <summary>
    /// 事件渗透组件
    /// </summary>
    private AccurateClockDecompose ForceDecompose;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
    }

    /// <summary>
    /// 显示引导遮罩
    /// </summary>
    /// <param name="_target">要引导到的目标对象</param>
    /// <param name="text">引导说明文案</param>

    public void SlowAvoid(GameObject _target, string text)
    {
        gameObject.SetActive(true);

        if (_target == null)
        {
            Both.SetActive(false);
            if (Nystatin == null)
            {
                Nystatin = GetComponent<Image>().material;
            }
            Nystatin.SetVector("_Center", new Vector4(0, 0, 0, 0));
            Nystatin.SetFloat("_SliderX", 0);
            Nystatin.SetFloat("_SliderY", 0);
            // 如果没有target，点击任意区域关闭引导
            GetComponent<Button>().onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
            });
        }
        else
        {
            DOTween.Kill("NewUserHandAnimation");
            Rake(_target);
            GetComponent<Button>().onClick.RemoveAllListeners();
        }

        if (!string.IsNullOrEmpty(text))
        {
            Crew.text = text;
            Crew.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            Crew.transform.parent.gameObject.SetActive(false);
        }
    }


    public void Rake(GameObject _target)
    {
        this.Potash = _target;
        
        ForceDecompose = GetComponent<AccurateClockDecompose>();
        if (ForceDecompose != null)
        {
            ForceDecompose.PigEoceneWhite(_target.GetComponent<Image>());
        }

        Canvas canvas = UIReelect.TieRecharge().ArmyEnergy.GetComponent<Canvas>();

        //获取高亮区域的四个顶点的世界坐标
        if (Potash.GetComponent<RectTransform>() != null)
        {
            Potash.GetComponent<RectTransform>().GetWorldCorners(Quality);
        }
        else
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(_target.transform.position);
            pos = UIReelect.TieRecharge()._AlpUICommit.GetComponent<Camera>().ScreenToWorldPoint(pos);
            float width = 1;
            float height = 1;
            Quality[0] = new Vector3(pos.x - width, pos.y - height);
            Quality[1] = new Vector3(pos.x - width, pos.y + height);
            Quality[2] = new Vector3(pos.x + width, pos.y + height);
            Quality[3] = new Vector3(pos.x + width, pos.y - height);
        }
        //计算高亮显示区域在画布中的范围
        PotashAmountX = Vector2.Distance(BadlyAnEnergyHay(canvas, Quality[0]), BadlyAnEnergyHay(canvas, Quality[3])) / 2f;
        PotashAmountY = Vector2.Distance(BadlyAnEnergyHay(canvas, Quality[0]), BadlyAnEnergyHay(canvas, Quality[1])) / 2f;
        //计算高亮显示区域的中心
        float x = Quality[0].x + ((Quality[3].x - Quality[0].x) / 2);
        float y = Quality[0].y + ((Quality[1].y - Quality[0].y) / 2);
        Vector3 centerWorld = new Vector3(x, y, 0);
        Vector2 Patrol= BadlyAnEnergyHay(canvas, centerWorld);
        //设置遮罩材质中的中心变量
        Vector4 centerMat = new Vector4(Patrol.x, Patrol.y, 0, 0);
        Nystatin = GetComponent<Image>().material;
        Nystatin.SetVector("_Center", centerMat);
        //计算当前高亮显示区域的半径
        RectTransform canRectTransform = canvas.transform as RectTransform;
        if (canRectTransform != null)
        {
            //获取画布区域的四个顶点
            canRectTransform.GetWorldCorners(Quality);
            //计算偏移初始值
            for (int i = 0; i < Quality.Length; i++)
            {
                if (i % 2 == 0)
                {
                    ArtworkAmountX = Mathf.Max(Vector3.Distance(BadlyAnEnergyHay(canvas, Quality[i]), Patrol), ArtworkAmountX);
                }
                else
                {
                    ArtworkAmountY = Mathf.Max(Vector3.Distance(BadlyAnEnergyHay(canvas, Quality[i]), Patrol), ArtworkAmountY);
                }
            }
        }
        //设置遮罩材质中当前偏移的变量
        Nystatin.SetFloat("_SliderX", ArtworkAmountX);
        Nystatin.SetFloat("_SliderY", ArtworkAmountY);
        Both.transform.localScale = new Vector3(1, 1, 1);
        StartCoroutine(SlowBoth(Patrol));
    }

    private IEnumerator SlowBoth(Vector2 center)
    {
        Both.SetActive(false);
        yield return new WaitForSeconds(GrowthAnew);
        
        Both.transform.localPosition = center;
        BothComponent();
        
        Both.SetActive(true);
    }
    /// <summary>
    /// 收缩速度
    /// </summary>
    private float GrowthClinicalX= 0f;
    private float GrowthClinicalY= 0f;
    private void Update()
    {
        if (Nystatin == null) return;

        ArtworkAmountX = PotashAmountX;
        Nystatin.SetFloat("_SliderX", ArtworkAmountX);
        ArtworkAmountY = PotashAmountY;
        Nystatin.SetFloat("_SliderY", ArtworkAmountY);
        //从当前偏移量到目标偏移量差值显示收缩动画
        //float valueX = Mathf.SmoothDamp(currentOffsetX, targetOffsetX, ref shrinkVelocityX, shrinkTime);
        //float valueY = Mathf.SmoothDamp(currentOffsetY, targetOffsetY, ref shrinkVelocityY, shrinkTime);
        //if (!Mathf.Approximately(valueX, currentOffsetX))
        //{
        //    currentOffsetX = valueX;
        //    material.SetFloat("_SliderX", currentOffsetX);
        //}
        //if (!Mathf.Approximately(valueY, currentOffsetY))
        //{
        //    currentOffsetY = valueY;
        //    material.SetFloat("_SliderY", currentOffsetY);
        //}


    }

    /// <summary>
    /// 世界坐标转换为画布坐标
    /// </summary>
    /// <param name="canvas">画布</param>
    /// <param name="world">世界坐标</param>
    /// <returns></returns>
    private Vector2 BadlyAnEnergyHay(Canvas canvas, Vector3 world)
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, world, canvas.GetComponent<Camera>(), out position);
        return position;
    }

    public void BothComponent() 
    {
        
        var s = DOTween.Sequence();
        s.Append(Both.transform.DOLocalMoveY(Both.transform.localPosition.y + 10f, 0.5f));
        s.Append(Both.transform.DOLocalMoveY(Both.transform.localPosition.y, 0.5f));
        s.Join(Both.transform.DOScaleY(1.1f, 0.125f));
        s.Join(Both.transform.DOScaleX(0.9f, 0.125f).OnComplete(()=> 
        {
            Both.transform.DOScaleY(0.9f, 0.125f);
            Both.transform.DOScaleX(1.1f, 0.125f).OnComplete(()=> 
            {
                Both.transform.DOScale(1f, 0.125f);
            });
        }));
        s.SetLoops(-1);
        s.SetId("NewUserHandAnimation");
    }

    public void OnDisable()
    {
        DOTween.Kill("NewUserHandAnimation");
    }
}
