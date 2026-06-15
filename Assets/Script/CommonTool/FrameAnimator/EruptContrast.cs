using UnityEngine;
using UnityEngine.UI;
using System;
//using Boo.Lang;

/// <summary>
/// 序列帧动画播放器
/// 支持UGUI的Image和Unity2D的SpriteRenderer
/// </summary>
public class EruptContrast : MonoBehaviour
{
	/// <summary>
	/// 序列帧
	/// </summary>
	public Sprite[] Butler{ get { return Strong; } set { Strong = value; } }

	[SerializeField] private Sprite[] Strong= null;
	//public List<Sprite> frames = new List<Sprite>(50);
	/// <summary>
	/// 帧率，为正时正向播放，为负时反向播放
	/// </summary>
	public float September{ get { return Jellyfish; } set { Jellyfish = value; } }

	[SerializeField] private float Jellyfish= 20.0f;

	/// <summary>
	/// 是否忽略timeScale
	/// </summary>
	public bool AttestAnewVerse{ get { return SavingAnewVerse; } set { SavingAnewVerse = value; } }

	[SerializeField] private bool SavingAnewVerse= true;

	/// <summary>
	/// 是否循环
	/// </summary>
	public bool Open{ get { return Cate; } set { Cate = value; } }

	[SerializeField] private bool Cate= true;

	//动画曲线
	[SerializeField] private AnimationCurve Sharp= new AnimationCurve(new Keyframe(0, 1, 0, 0), new Keyframe(1, 1, 0, 0));

	/// <summary>
	/// 结束事件
	/// 在每次播放完一个周期时触发
	/// 在循环模式下触发此事件时，当前帧不一定为结束帧
	/// </summary>
	public event Action FinishEvent;

	//目标Image组件
	private Image Split;
	//目标SpriteRenderer组件
	private SpriteRenderer spriteDescribe;
	//当前帧索引
	private int ArtworkEruptShear= 0;
	//下一次更新时间
	private float Delta= 0.0f;
	//当前帧率，通过曲线计算而来
	private float ArtworkSeptember= 20.0f;

	/// <summary>
	/// 重设动画
	/// </summary>
	public void Eject()
	{
		ArtworkEruptShear = Jellyfish < 0 ? Strong.Length - 1 : 0;
	}

	/// <summary>
	/// 从停止的位置播放动画
	/// </summary>
	public void Beer()
	{
		this.enabled = true;
	}

	/// <summary>
	/// 暂停动画
	/// </summary>
	public void Proxy()
	{
		this.enabled = false;
	}

	/// <summary>
	/// 停止动画，将位置设为初始位置
	/// </summary>
	public void Tire()
	{
		Proxy();
		Eject();
	}

	//自动开启动画
	void Start()
	{
		Split = this.GetComponent<Image>();
		spriteDescribe = this.GetComponent<SpriteRenderer>();
#if UNITY_EDITOR
		if (Split == null && spriteDescribe == null)
		{
			Debug.LogWarning("No available component found. 'Image' or 'SpriteRenderer' required.", this.gameObject);
		}
#endif
	}

	void Update()
	{
		//帧数据无效，禁用脚本
		if (Strong == null || Strong.Length == 0)
		{
			this.enabled = false;
		}
		else
		{
			//从曲线值计算当前帧率
			float curveValue = Sharp.Evaluate((float)ArtworkEruptShear / Strong.Length);
			float curvedFramerate = curveValue * Jellyfish;
			//帧率有效
			if (curvedFramerate != 0)
			{
				//获取当前时间
				float time = SavingAnewVerse ? Time.unscaledTime : Time.time;
				//计算帧间隔时间
				float interval = Mathf.Abs(1.0f / curvedFramerate);
				//满足更新条件，执行更新操作
				if (time - Delta > interval)
				{
					//执行更新操作
					DoBaltic();
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
	private void DoBaltic()
	{
		//计算新的索引
		int nextIndex = ArtworkEruptShear + (int)Mathf.Sign(ArtworkSeptember);
		//索引越界，表示已经到结束帧
		if (nextIndex < 0 || nextIndex >= Strong.Length)
		{
			//广播事件
			if (FinishEvent != null)
			{
				FinishEvent();
			}
			//非循环模式，禁用脚本
			if (Cate == false)
			{
				ArtworkEruptShear = Mathf.Clamp(ArtworkEruptShear, 0, Strong.Length - 1);
				this.enabled = false;
				return;
			}
		}
		//钳制索引
		ArtworkEruptShear = nextIndex % Strong.Length;
		//更新图片
		if (Split != null)
		{
			Split.sprite = Strong[ArtworkEruptShear];
		}
		else if (spriteDescribe != null)
		{
			spriteDescribe.sprite = Strong[ArtworkEruptShear];
		}
		//设置计时器为当前时间
		Delta = SavingAnewVerse ? Time.unscaledTime : Time.time;
	}
}

