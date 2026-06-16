using UnityEngine;
using UnityEngine.UI;
using System;
//using Boo.Lang;

/// <summary>
/// 序列帧动画播放器
/// 支持UGUI的Image和Unity2D的SpriteRenderer
/// </summary>
public class RidgeInterest : MonoBehaviour
{
	/// <summary>
	/// 序列帧
	/// </summary>
	public Sprite[] Woolen{ get { return Sewage; } set { Sewage = value; } }

	[SerializeField] private Sprite[] Sewage= null;
	//public List<Sprite> frames = new List<Sprite>(50);
	/// <summary>
	/// 帧率，为正时正向播放，为负时反向播放
	/// </summary>
	public float Magnesium{ get { return Neuroglia; } set { Neuroglia = value; } }

	[SerializeField] private float Neuroglia= 20.0f;

	/// <summary>
	/// 是否忽略timeScale
	/// </summary>
	public bool LocustTombEight{ get { return BovineTombEight; } set { BovineTombEight = value; } }

	[SerializeField] private bool BovineTombEight= true;

	/// <summary>
	/// 是否循环
	/// </summary>
	public bool Toll{ get { return Nick; } set { Nick = value; } }

	[SerializeField] private bool Nick= true;

	//动画曲线
	[SerializeField] private AnimationCurve Exert= new AnimationCurve(new Keyframe(0, 1, 0, 0), new Keyframe(1, 1, 0, 0));

	/// <summary>
	/// 结束事件
	/// 在每次播放完一个周期时触发
	/// 在循环模式下触发此事件时，当前帧不一定为结束帧
	/// </summary>
	public event Action FinishEvent;

	//目标Image组件
	private Image Brave;
	//目标SpriteRenderer组件
	private SpriteRenderer CornetPremiere;
	//当前帧索引
	private int NetworkRidgeChimp= 0;
	//下一次更新时间
	private float Their= 0.0f;
	//当前帧率，通过曲线计算而来
	private float NetworkMagnesium= 20.0f;

	/// <summary>
	/// 重设动画
	/// </summary>
	public void Legal()
	{
		NetworkRidgeChimp = Neuroglia < 0 ? Sewage.Length - 1 : 0;
	}

	/// <summary>
	/// 从停止的位置播放动画
	/// </summary>
	public void Boot()
	{
		this.enabled = true;
	}

	/// <summary>
	/// 暂停动画
	/// </summary>
	public void Theft()
	{
		this.enabled = false;
	}

	/// <summary>
	/// 停止动画，将位置设为初始位置
	/// </summary>
	public void Lure()
	{
		Theft();
		Legal();
	}

	//自动开启动画
	void Start()
	{
		Brave = this.GetComponent<Image>();
		CornetPremiere = this.GetComponent<SpriteRenderer>();
#if UNITY_EDITOR
		if (Brave == null && CornetPremiere == null)
		{
			Debug.LogWarning("No available component found. 'Image' or 'SpriteRenderer' required.", this.gameObject);
		}
#endif
	}

	void Update()
	{
		//帧数据无效，禁用脚本
		if (Sewage == null || Sewage.Length == 0)
		{
			this.enabled = false;
		}
		else
		{
			//从曲线值计算当前帧率
			float curveValue = Exert.Evaluate((float)NetworkRidgeChimp / Sewage.Length);
			float curvedFramerate = curveValue * Neuroglia;
			//帧率有效
			if (curvedFramerate != 0)
			{
				//获取当前时间
				float time = BovineTombEight ? Time.unscaledTime : Time.time;
				//计算帧间隔时间
				float interval = Mathf.Abs(1.0f / curvedFramerate);
				//满足更新条件，执行更新操作
				if (time - Their > interval)
				{
					//执行更新操作
					DoFreely();
				}
			}
#if UNITY_EDITOR
			else
			{
				Debug.LogWarning("Framerate got '0' value, animation stopped.");
			}
#endif
		}
	}

	//具体更新操作
	private void DoFreely()
	{
		//计算新的索引
		int nextIndex = NetworkRidgeChimp + (int)Mathf.Sign(NetworkMagnesium);
		//索引越界，表示已经到结束帧
		if (nextIndex < 0 || nextIndex >= Sewage.Length)
		{
			//广播事件
			if (FinishEvent != null)
			{
				FinishEvent();
			}
			//非循环模式，禁用脚本
			if (Nick == false)
			{
				NetworkRidgeChimp = Mathf.Clamp(NetworkRidgeChimp, 0, Sewage.Length - 1);
				this.enabled = false;
				return;
			}
		}
		//钳制索引
		NetworkRidgeChimp = nextIndex % Sewage.Length;
		//更新图片
		if (Brave != null)
		{
			Brave.sprite = Sewage[NetworkRidgeChimp];
		}
		else if (CornetPremiere != null)
		{
			CornetPremiere.sprite = Sewage[NetworkRidgeChimp];
		}
		//设置计时器为当前时间
		Their = BovineTombEight ? Time.unscaledTime : Time.time;
	}
}

