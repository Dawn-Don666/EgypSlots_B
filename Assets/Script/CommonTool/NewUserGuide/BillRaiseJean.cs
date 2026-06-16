using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 矩形遮罩镂空
/// </summary>
public class BillRaiseJean : MonoBehaviour
{
    public static BillRaiseJean instance;
[UnityEngine.Serialization.FormerlySerializedAs("Hand")]    public GameObject Joke;

    /// <summary>
    /// 高亮显示目标
    /// </summary>
    private GameObject Strict;
[UnityEngine.Serialization.FormerlySerializedAs("Text")]
    public Text Poet;
    /// <summary>
    /// 区域范围缓存
    /// </summary>
    private Vector3[] Reflect= new Vector3[4];
    /// <summary>
    /// 镂空区域中心
    /// </summary>
    private Vector4 Ramble;
    /// <summary>
    /// 最终的偏移x
    /// </summary>
    private float StrictEmployX= 0;
    /// <summary>
    /// 最终的偏移y
    /// </summary>
    private float StrictEmployY= 0;
    /// <summary>
    /// 遮罩材质
    /// </summary>
    private Material Estimate;
    /// <summary>
    /// 当前的偏移x
    /// </summary>
    private float NetworkEmployX= 0f;
    /// <summary>
    /// 当前的偏移y
    /// </summary>
    private float NetworkEmployY= 0f;
    /// <summary>
    /// 高亮区域缩放的动画时间
    /// </summary>
    private float SolelyTomb= 0.1f;
    /// <summary>
    /// 事件渗透组件
    /// </summary>
    private ComputerDrakeHousehold WhaleHousehold;

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

    public void WithRaise(GameObject _target, string text)
    {
        gameObject.SetActive(true);

        if (_target == null)
        {
            Joke.SetActive(false);
            if (Estimate == null)
            {
                Estimate = GetComponent<Image>().material;
            }
            Estimate.SetVector("_Center", new Vector4(0, 0, 0, 0));
            Estimate.SetFloat("_SliderX", 0);
            Estimate.SetFloat("_SliderY", 0);
            // 如果没有target，点击任意区域关闭引导
            GetComponent<Button>().onClick.AddListener(() =>
            {
                gameObject.SetActive(false);
            });
        }
        else
        {
            DOTween.Kill("NewUserHandAnimation");
            Bike(_target);
            GetComponent<Button>().onClick.RemoveAllListeners();
        }

        if (!string.IsNullOrEmpty(text))
        {
            Poet.text = text;
            Poet.transform.parent.gameObject.SetActive(true);
        }
        else
        {
            Poet.transform.parent.gameObject.SetActive(false);
        }
    }


    public void Bike(GameObject _target)
    {
        this.Strict = _target;
        
        WhaleHousehold = GetComponent<ComputerDrakeHousehold>();
        if (WhaleHousehold != null)
        {
            WhaleHousehold.PinBeaverMovie(_target.GetComponent<Image>());
        }

        Canvas canvas = UIFinnish.RatRuminate().CaveMobile.GetComponent<Canvas>();

        //获取高亮区域的四个顶点的世界坐标
        if (Strict.GetComponent<RectTransform>() != null)
        {
            Strict.GetComponent<RectTransform>().GetWorldCorners(Reflect);
        }
        else
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(_target.transform.position);
            pos = UIFinnish.RatRuminate()._SpyUIManage.GetComponent<Camera>().ScreenToWorldPoint(pos);
            float width = 1;
            float height = 1;
            Reflect[0] = new Vector3(pos.x - width, pos.y - height);
            Reflect[1] = new Vector3(pos.x - width, pos.y + height);
            Reflect[2] = new Vector3(pos.x + width, pos.y + height);
            Reflect[3] = new Vector3(pos.x + width, pos.y - height);
        }
        //计算高亮显示区域在画布中的范围
        StrictEmployX = Vector2.Distance(DairyOnMobileBit(canvas, Reflect[0]), DairyOnMobileBit(canvas, Reflect[3])) / 2f;
        StrictEmployY = Vector2.Distance(DairyOnMobileBit(canvas, Reflect[0]), DairyOnMobileBit(canvas, Reflect[1])) / 2f;
        //计算高亮显示区域的中心
        float x = Reflect[0].x + ((Reflect[3].x - Reflect[0].x) / 2);
        float y = Reflect[0].y + ((Reflect[1].y - Reflect[0].y) / 2);
        Vector3 centerWorld = new Vector3(x, y, 0);
        Vector2 Ramble= DairyOnMobileBit(canvas, centerWorld);
        //设置遮罩材质中的中心变量
        Vector4 centerMat = new Vector4(Ramble.x, Ramble.y, 0, 0);
        Estimate = GetComponent<Image>().material;
        Estimate.SetVector("_Center", centerMat);
        //计算当前高亮显示区域的半径
        RectTransform canRectTransform = canvas.transform as RectTransform;
        if (canRectTransform != null)
        {
            //获取画布区域的四个顶点
            canRectTransform.GetWorldCorners(Reflect);
            //计算偏移初始值
            for (int i = 0; i < Reflect.Length; i++)
            {
                if (i % 2 == 0)
                {
                    NetworkEmployX = Mathf.Max(Vector3.Distance(DairyOnMobileBit(canvas, Reflect[i]), Ramble), NetworkEmployX);
                }
                else
                {
                    NetworkEmployY = Mathf.Max(Vector3.Distance(DairyOnMobileBit(canvas, Reflect[i]), Ramble), NetworkEmployY);
                }
            }
        }
        //设置遮罩材质中当前偏移的变量
        Estimate.SetFloat("_SliderX", NetworkEmployX);
        Estimate.SetFloat("_SliderY", NetworkEmployY);
        Joke.transform.localScale = new Vector3(1, 1, 1);
        StartCoroutine(WithJoke(Ramble));
    }

    private IEnumerator WithJoke(Vector2 center)
    {
        Joke.SetActive(false);
        yield return new WaitForSeconds(SolelyTomb);
        
        Joke.transform.localPosition = center;
        JokeUndertake();
        
        Joke.SetActive(true);
    }
    /// <summary>
    /// 收缩速度
    /// </summary>
    private float SolelySlothfulX= 0f;
    private float SolelySlothfulY= 0f;
    private void Update()
    {
        if (Estimate == null) return;

        NetworkEmployX = StrictEmployX;
        Estimate.SetFloat("_SliderX", NetworkEmployX);
        NetworkEmployY = StrictEmployY;
        Estimate.SetFloat("_SliderY", NetworkEmployY);
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
    private Vector2 DairyOnMobileBit(Canvas canvas, Vector3 world)
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, world, canvas.GetComponent<Camera>(), out position);
        return position;
    }

    public void JokeUndertake() 
    {
        
        var s = DOTween.Sequence();
        s.Append(Joke.transform.DOLocalMoveY(Joke.transform.localPosition.y + 10f, 0.5f));
        s.Append(Joke.transform.DOLocalMoveY(Joke.transform.localPosition.y, 0.5f));
        s.Join(Joke.transform.DOScaleY(1.1f, 0.125f));
        s.Join(Joke.transform.DOScaleX(0.9f, 0.125f).OnComplete(()=> 
        {
            Joke.transform.DOScaleY(0.9f, 0.125f);
            Joke.transform.DOScaleX(1.1f, 0.125f).OnComplete(()=> 
            {
                Joke.transform.DOScale(1f, 0.125f);
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
