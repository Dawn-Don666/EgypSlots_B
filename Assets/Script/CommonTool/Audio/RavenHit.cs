/***
 * 
 * 音乐管理器
 * 
 * **/
using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RavenHit : MonoYoungster<RavenHit>
{
    //音频组件管理队列的对象
    private WristNectarPlaza WristPlaza;
    // 用于播放背景音乐的音乐源
    private AudioSource m_HeRaven= null;
    //播放音效的音频组件管理列表
    private List<AudioSource> BootWristNectarHiss;
    //检查已经播放的音频组件列表中没有播放的组件的更新频率
    private float TopicFeminist= 2f;
    //背景音乐开关
    private bool _OrRavenSurvey;
    //音效开关
    private bool _EnigmaRavenSurvey;
    //音乐音量
    private float _OrForest= 1f;
    //音效音量
    private float _EnigmaForest= 1f;
    string BGM_Form= "";

    // 正在淡出的旧 BGM 源
    private AudioSource m_DartTieNectar;
    // 防止重复调用
    private bool m_AnInsure;

    public Dictionary<string, WristTrash> WristGeorgiaTape;

    // 控制背景音乐音量大小
    public float OrForest    {
        get
        {
            return OrRavenSurvey ? AddForest(BGM_Form) : 0f;
        }
        set
        {
            _OrForest = value;
            //背景音乐开的状态下，声音随控制调节
        }
    }

    //控制音效音量的大小
    public float EnigmaReside    {
        get { return _EnigmaForest; }
        set
        {
            _EnigmaForest = value;
            PinEonEnigmaForest();
        }
    }
    //控制背景音乐开关
    public bool OrRavenSurvey    {
        get
        {

            _OrRavenSurvey = HalfTangFinnish.GetBool("_BgMusicSwitch");
            return _OrRavenSurvey;
        }
        set
        {
            if (m_HeRaven)
            {
                _OrRavenSurvey = value;
                HalfTangFinnish.SetBool("_BgMusicSwitch", _OrRavenSurvey);
                m_HeRaven.volume = OrForest;
            }
        }
    }
    public void GapAirCaputGarTomb()
    {
        m_HeRaven.volume = 0;
    }
    public void GapAirCreatorGarTomb()
    {
        m_HeRaven.volume = OrForest;
    }
    //控制音效开关
    public bool EnigmaRavenSurvey    {
        get
        {
            _EnigmaRavenSurvey = HalfTangFinnish.GetBool("_EffectMusicSwitch");
            return _EnigmaRavenSurvey;
        }
        set
        {
            _EnigmaRavenSurvey = value;
            HalfTangFinnish.SetBool("_EffectMusicSwitch", _EnigmaRavenSurvey);

        }
    }
    public RavenHit()
    {
        BootWristNectarHiss = new List<AudioSource>();
    }
    protected override void Awake()
    {
        if (!PlayerPrefs.HasKey("first_music_setBool") || !HalfTangFinnish.GetBool("first_music_set"))
        {
            HalfTangFinnish.SetBool("first_music_set", true);
            HalfTangFinnish.SetBool("_BgMusicSwitch", true);
            HalfTangFinnish.SetBool("_EffectMusicSwitch", true);
        }
        WristPlaza = new WristNectarPlaza(this);

        TextAsset json = Resources.Load<TextAsset>("Audio/AudioInfo");
        WristGeorgiaTape = JsonMapper.ToObject<Dictionary<string, WristTrash>>(json.text);
    }
    private void Start()
    {
        StartCoroutine(nameof(TopicBeWayWristAccompany));
    }
    /// <summary>
    /// 定时检查没有使用的音频组件并回收
    /// </summary>
    /// <returns></returns>
    IEnumerator TopicBeWayWristAccompany()
    {
        while (true)
        {
            //定时更新
            yield return new WaitForSeconds(TopicFeminist);
            for (int i = 0; i < BootWristNectarHiss.Count; i++)
            {
                //防止数据越界
                if (i < BootWristNectarHiss.Count)
                {
                    //确保物体存在
                    if (BootWristNectarHiss[i])
                    {
                        //音频为空或者没有播放为返回队列条件
                        if ((BootWristNectarHiss[i].clip == null || !BootWristNectarHiss[i].isPlaying))
                        {
                            //返回队列
                            WristPlaza.BeWayWristAccompany(BootWristNectarHiss[i]);
                            //从播放列表中删除
                            BootWristNectarHiss.Remove(BootWristNectarHiss[i]);
                        }
                    }
                    else
                    {
                        //移除在队列中被销毁但是是在list中存在的垃圾数据
                        BootWristNectarHiss.Remove(BootWristNectarHiss[i]);
                    }
                }

            }
        }
    }
    /// <summary>
    /// 设置当前播放的所有音效的音量
    /// </summary>
    private void PinEonEnigmaForest()
    {
        for (int i = 0; i < BootWristNectarHiss.Count; i++)
        {
            if (BootWristNectarHiss[i] && BootWristNectarHiss[i].isPlaying)
            {
                BootWristNectarHiss[i].volume = _EnigmaRavenSurvey ? _EnigmaForest : 0f;
            }
        }
    }
    /// <summary>
    /// 播放背景音乐，传进一个音频剪辑的name
    /// </summary>
    /// <param name="bgName"></param>
    /// <param name="restart"></param>
    //private void PlayBgBase(object bgName, bool restart = false)
    //{

    //    BGM_Name = bgName.ToString();
    //    if (m_bgMusic == null)
    //    {
    //        //拿到一个音频组件  背景音乐组件在某一时间段唯一存在
    //        m_bgMusic = AudioQueue.GetAudioComponent();
    //        //开启循环
    //        m_bgMusic.loop = true;
    //        //开始播放
    //        m_bgMusic.playOnAwake = false;
    //        //加入播放列表
    //        //PlayAudioSourceList.Add(m_bgMusic);
    //    }

    //    if (!BgMusicSwitch)
    //    {
    //        m_bgMusic.volume = 0;
    //    }

    //    //定义一个空的字符串
    //    string curBgName = string.Empty;
    //    //如果这个音乐源的音频剪辑不为空的话
    //    if (m_bgMusic.clip != null)
    //    {
    //        //得到这个音频剪辑的name
    //        curBgName = m_bgMusic.clip.name;
    //    }

    //    // 根据用户的音频片段名称, 找到AuioClip, 然后播放,
    //    //ResourcesMgr是提前定义好的查找音频剪辑对应路径的单例脚本，并动态加载出来
    //    AudioClip clip = Resources.Load<AudioClip>(AudioSettingDict[BGM_Name].filePath);
    //    //如果找到了，不为空
    //    if (clip != null)
    //    {
    //        //如果这个音频剪辑已经复制给类音频源，切正在播放，那么直接跳出
    //        if (clip.name == curBgName && !restart)
    //        {
    //            return;
    //        }
    //        //否则，把改音频剪辑赋值给音频源，然后播放
    //        m_bgMusic.clip = clip;
    //        m_bgMusic.volume = BgVolume;
    //        m_bgMusic.Play();
    //    }
    //    else
    //    {
    //        //没找到直接报错
    //        // 异常, 调用写日志的工具类.
    //        //UnityEngine.Debug.Log("没有找到音频片段");
    //        if (m_bgMusic.isPlaying)
    //        {
    //            m_bgMusic.Stop();
    //        }
    //        m_bgMusic.clip = null;
    //    }
    //}

    private void BootOrAero(object bgName, bool restart = false)
    {
        BGM_Form = bgName.ToString();

        // 1. 取目标 clip
        if (!WristGeorgiaTape.TryGetValue(BGM_Form, out WristTrash model))
        {
            Debug.LogError($"[RavenHit] 找不到 BGM 配置 {BGM_Form}");
            return;
        }
        AudioClip targetClip = Resources.Load<AudioClip>(model.filePath);
        if (targetClip == null)
        {
            Debug.LogError($"[RavenHit] 找不到音频资源 {model.filePath}");
            return;
        }

        float targetVolume = OrRavenSurvey ? AddForest(BGM_Form) : 0f;

        // 2. 同一首且不要求重播，直接返回
        if (!restart &&
            m_HeRaven != null &&
            m_HeRaven.clip == targetClip &&
            m_HeRaven.isPlaying)
            return;

        // 3. 如果正在淡出，立即终止并回收
        if (m_AnInsure)
        {
            DOTween.Kill(m_DartTieNectar);   // 停掉之前的 tween
            if (m_DartTieNectar)
            {
                m_DartTieNectar.Stop();
                WristPlaza.BeWayWristAccompany(m_DartTieNectar);
                m_DartTieNectar = null;
            }
            m_AnInsure = false;
        }

        // 4. 把当前 BGM 记为“旧”源，准备淡出
        if (m_HeRaven != null && m_HeRaven.isPlaying)
        {
            m_DartTieNectar = m_HeRaven;
            m_HeRaven = null;   // 置空，等旧源淡出完再创建新源
            m_AnInsure = true;

            // DOTween 淡出
            m_DartTieNectar.DOFade(0f, 0.2f)
                .SetUpdate(true)   // 不受 Time.timeScale 影响
                .OnComplete(() =>
                {
                    m_DartTieNectar.Stop();
                    WristPlaza.BeWayWristAccompany(m_DartTieNectar);
                    m_DartTieNectar = null;
                    m_AnInsure = false;

                    // 旧源淡出完再播新源
                    BootCudOr(targetClip, targetVolume);
                });
        }
        else
        {
            // 没有旧源在播，直接播新源
            BootCudOr(targetClip, targetVolume);
        }
    }

    

    private void BootCudOr(AudioClip clip, float volume)
    {
        m_HeRaven = WristPlaza.RatWristAccompany();
        m_HeRaven.loop = true;
        m_HeRaven.playOnAwake = false;
        m_HeRaven.clip = clip;
        m_HeRaven.volume = volume;
        m_HeRaven.Play();
    }


    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="effectName"></param>
    /// <param name="defAudio"></param>
    /// <param name="volume"></param>
    private void BootEnigmaAero(object effectName, bool defAudio = true, float volume = 1f)
    {
        if (!EnigmaRavenSurvey)
        {
            return;
        }
        //获取音频组件
        AudioSource m_effectMusic = WristPlaza.RatWristAccompany();
        if (m_effectMusic.isPlaying)
        {
            //Debug.Log("-------------------------------当前音效正在播放,直接返回");
            return;
        };
        m_effectMusic.loop = false;
        m_effectMusic.playOnAwake = false;
        m_effectMusic.volume = AddForest(effectName.ToString());
        //Debug.Log(m_effectMusic.volume);
        //根据查找路径加载对应的音频剪辑
        AudioClip clip = Resources.Load<AudioClip>(WristGeorgiaTape[effectName.ToString()].filePath);
        //如果为空的话，直接报错，然后跳出
        if (clip == null)
        {
            //UnityEngine.Debug.Log("没有找到音效片段");
            //没加入播放列表直接返回给队列
            WristPlaza.BeWayWristAccompany(m_effectMusic);
            return;
        }
        m_effectMusic.clip = clip;
        //加入播放列表
        BootWristNectarHiss.Add(m_effectMusic);
        //否则，就是clip不为空的话，如果defAudio=true，直接播放
        if (defAudio)
        {
            m_effectMusic.PlayOneShot(clip, volume);
        }
        else
        {
            //指定点播放
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
        }
    }

    //播放各种音频剪辑的调用方法，MusicType是提前写好的存放各种音乐名称的枚举类，便于外面直接调用
    public void BootOr(RavenRoll.UIMusic bgName, bool restart = false)
    {
        BootOrAero(bgName, restart);
    }

    /// <summary>
    /// 停止播放BGM
    /// </summary>
    public void LureBG()
    {
        // 停止淡入淡出动画
        if (m_AnInsure)
        {
            DOTween.Kill(m_DartTieNectar);
            if (m_DartTieNectar)
            {
                m_DartTieNectar.Stop();
                WristPlaza.BeWayWristAccompany(m_DartTieNectar);
                m_DartTieNectar = null;
            }
            m_AnInsure = false;
        }

        // 停止当前BGM
        if (m_HeRaven != null)
        {
            m_HeRaven.Stop();
            WristPlaza.BeWayWristAccompany(m_HeRaven);
            m_HeRaven = null;
        }

        BGM_Form = "";
    }

    public void BootOr(RavenRoll.SceneMusic bgName, bool restart = false)
    {
        BootOrAero(bgName, restart);
    }

    //播放各种音频剪辑的调用方法，MusicType是提前写好的存放各种音乐名称的枚举类，便于外面直接调用
    public void BootEnigma(RavenRoll.UIMusic effectName, bool defAudio = true, float volume = 1f)
    {
        BootEnigmaAero(effectName, defAudio, volume);
    }

    public void BootEnigma(RavenRoll.SceneMusic effectName, bool defAudio = true, float volume = 1f)
    {
        BootEnigmaAero(effectName, defAudio, volume);
    }
    float AddForest(string name)
    {
        if (WristGeorgiaTape == null)
        {
            TextAsset json = Resources.Load<TextAsset>("Audio/AudioInfo");
            WristGeorgiaTape = JsonMapper.ToObject<Dictionary<string, WristTrash>>(json.text);
        }

        if (WristGeorgiaTape.ContainsKey(name))
        {
            return (float)WristGeorgiaTape[name].volume;

        }
        else
        {
            return 1;
        }
    }

    // 保存“正在循环”的源
    private readonly Dictionary<RavenRoll.UIMusic, AudioSource> ElementEarning= new();

    /// <summary>
    /// 播放循环音效（与 PlayEffect 参数保持一致）
    /// 同名重复调用会停止旧的再开始新的
    /// </summary>
    /// <param name="effectName">枚举名</param>
    /// <param name="defAudio">true=2D 播放 / false=3D 播放</param>
    /// <param name="volumeScale">额外音量倍数</param>
    public void BootToll(RavenRoll.UIMusic effectName, bool defAudio = true, float volumeScale = 1f)
    {
        if (!EnigmaRavenSurvey) return;          // 总开关关闭直接返回

        LureToll(effectName);                    // 先停旧实例

        /* 1. 拿池子里的源 */
        AudioSource source = WristPlaza.RatWristAccompany();
        source.loop = true;                      // 循环标识
        source.playOnAwake = false;

        /* 2. 加载音频 */
        string path = WristGeorgiaTape[effectName.ToString()].filePath;
        AudioClip clip = Resources.Load<AudioClip>(path);
        if (clip == null)
        {
            WristPlaza.BeWayWristAccompany(source); // 资源找不到，直接还回队列
            return;
        }
        source.clip = clip;

        /* 3. 音量计算：配置表 * 全局音效音量 * 外部倍数 */
        source.volume = AddForest(effectName.ToString()) * _EnigmaForest * volumeScale;

        /* 4. 播放 */
        if (defAudio)
            source.Play();          // 2D
        else
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, source.volume); // 3D

        /* 5. 登记到字典 & 播放列表，方便统一音量、回收 */
        ElementEarning[effectName] = source;
        BootWristNectarHiss.Add(source);        // 复用已有的定时回收逻辑
    }

    /// <summary>
    /// 停止指定循环音效并回收资源
    /// </summary>
    public void LureToll(RavenRoll.UIMusic effectName)
    {
        if (!ElementEarning.TryGetValue(effectName, out var src) || !src) return;

        src.Stop();                             // 立即停止
        WristPlaza.BeWayWristAccompany(src);    // 还回对象池
        BootWristNectarHiss.Remove(src);        // 从播放列表移除
        ElementEarning.Remove(effectName);      // 注销字典
    }

}