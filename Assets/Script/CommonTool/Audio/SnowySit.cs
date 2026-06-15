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

public class SnowySit : RestChristian<SnowySit>
{
    //音频组件管理队列的对象
    private KneadInflowWeird KneadWeird;
    // 用于播放背景音乐的音乐源
    private AudioSource m_bgSnowy= null;
    //播放音效的音频组件管理列表
    private List<AudioSource> BeerKneadInflowFond;
    //检查已经播放的音频组件列表中没有播放的组件的更新频率
    private float OccurHardwood= 2f;
    //背景音乐开关
    private bool _OnSnowyRecess;
    //音效开关
    private bool _MethylSnowyRecess;
    //音乐音量
    private float _OnForget= 1f;
    //音效音量
    private float _MethylForget= 1f;
    string BGM_Lady= "";

    // 正在淡出的旧 BGM 源
    private AudioSource m_WareJarInflow;
    // 防止重复调用
    private bool m_IfGrassy;

    public Dictionary<string, KneadTrove> KneadElectroBind;

    // 控制背景音乐音量大小
    public float OnForget    {
        get
        {
            return OnSnowyRecess ? PegForget(BGM_Lady) : 0f;
        }
        set
        {
            _OnForget = value;
            //背景音乐开的状态下，声音随控制调节
        }
    }

    //控制音效音量的大小
    public float MethylOrient    {
        get { return _MethylForget; }
        set
        {
            _MethylForget = value;
            PigCarMethylForget();
        }
    }
    //控制背景音乐开关
    public bool OnSnowyRecess    {
        get
        {

            _OnSnowyRecess = MileLieuReelect.GetBool("_BgMusicSwitch");
            return _OnSnowyRecess;
        }
        set
        {
            if (m_bgSnowy)
            {
                _OnSnowyRecess = value;
                MileLieuReelect.SetBool("_BgMusicSwitch", _OnSnowyRecess);
                m_bgSnowy.volume = OnForget;
            }
        }
    }
    public void setMadTowerWedAnew()
    {
        m_bgSnowy.volume = 0;
    }
    public void TieMadExpanseWedAnew()
    {
        m_bgSnowy.volume = OnForget;
    }
    //控制音效开关
    public bool MethylSnowyRecess    {
        get
        {
            _MethylSnowyRecess = MileLieuReelect.GetBool("_EffectMusicSwitch");
            return _MethylSnowyRecess;
        }
        set
        {
            _MethylSnowyRecess = value;
            MileLieuReelect.SetBool("_EffectMusicSwitch", _MethylSnowyRecess);

        }
    }
    public SnowySit()
    {
        BeerKneadInflowFond = new List<AudioSource>();
    }
    protected override void Awake()
    {
        if (!PlayerPrefs.HasKey("first_music_setBool") || !MileLieuReelect.GetBool("first_music_set"))
        {
            MileLieuReelect.SetBool("first_music_set", true);
            MileLieuReelect.SetBool("_BgMusicSwitch", true);
            MileLieuReelect.SetBool("_EffectMusicSwitch", true);
        }
        KneadWeird = new KneadInflowWeird(this);

        TextAsset json = Resources.Load<TextAsset>("Audio/AudioInfo");
        KneadElectroBind = JsonMapper.ToObject<Dictionary<string, KneadTrove>>(json.text);
    }
    private void Start()
    {
        StartCoroutine(nameof(OccurNoJawKneadModernize));
    }
    /// <summary>
    /// 定时检查没有使用的音频组件并回收
    /// </summary>
    /// <returns></returns>
    IEnumerator OccurNoJawKneadModernize()
    {
        while (true)
        {
            //定时更新
            yield return new WaitForSeconds(OccurHardwood);
            for (int i = 0; i < BeerKneadInflowFond.Count; i++)
            {
                //防止数据越界
                if (i < BeerKneadInflowFond.Count)
                {
                    //确保物体存在
                    if (BeerKneadInflowFond[i])
                    {
                        //音频为空或者没有播放为返回队列条件
                        if ((BeerKneadInflowFond[i].clip == null || !BeerKneadInflowFond[i].isPlaying))
                        {
                            //返回队列
                            KneadWeird.UnJawKneadModernize(BeerKneadInflowFond[i]);
                            //从播放列表中删除
                            BeerKneadInflowFond.Remove(BeerKneadInflowFond[i]);
                        }
                    }
                    else
                    {
                        //移除在队列中被销毁但是是在list中存在的垃圾数据
                        BeerKneadInflowFond.Remove(BeerKneadInflowFond[i]);
                    }
                }

            }
        }
    }
    /// <summary>
    /// 设置当前播放的所有音效的音量
    /// </summary>
    private void PigCarMethylForget()
    {
        for (int i = 0; i < BeerKneadInflowFond.Count; i++)
        {
            if (BeerKneadInflowFond[i] && BeerKneadInflowFond[i].isPlaying)
            {
                BeerKneadInflowFond[i].volume = _MethylSnowyRecess ? _MethylForget : 0f;
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

    private void BeerOnFilm(object bgName, bool restart = false)
    {
        BGM_Lady = bgName.ToString();

        // 1. 取目标 clip
        if (!KneadElectroBind.TryGetValue(BGM_Lady, out KneadTrove model))
        {
            Debug.LogError($"[SnowySit] 找不到 BGM 配置 {BGM_Lady}");
            return;
        }
        AudioClip targetClip = Resources.Load<AudioClip>(model.filePath);
        if (targetClip == null)
        {
            Debug.LogError($"[SnowySit] 找不到音频资源 {model.filePath}");
            return;
        }

        float targetVolume = OnSnowyRecess ? PegForget(BGM_Lady) : 0f;

        // 2. 同一首且不要求重播，直接返回
        if (!restart &&
            m_bgSnowy != null &&
            m_bgSnowy.clip == targetClip &&
            m_bgSnowy.isPlaying)
            return;

        // 3. 如果正在淡出，立即终止并回收
        if (m_IfGrassy)
        {
            DOTween.Kill(m_WareJarInflow);   // 停掉之前的 tween
            if (m_WareJarInflow)
            {
                m_WareJarInflow.Stop();
                KneadWeird.UnJawKneadModernize(m_WareJarInflow);
                m_WareJarInflow = null;
            }
            m_IfGrassy = false;
        }

        // 4. 把当前 BGM 记为“旧”源，准备淡出
        if (m_bgSnowy != null && m_bgSnowy.isPlaying)
        {
            m_WareJarInflow = m_bgSnowy;
            m_bgSnowy = null;   // 置空，等旧源淡出完再创建新源
            m_IfGrassy = true;

            // DOTween 淡出
            m_WareJarInflow.DOFade(0f, 0.2f)
                .SetUpdate(true)   // 不受 Time.timeScale 影响
                .OnComplete(() =>
                {
                    m_WareJarInflow.Stop();
                    KneadWeird.UnJawKneadModernize(m_WareJarInflow);
                    m_WareJarInflow = null;
                    m_IfGrassy = false;

                    // 旧源淡出完再播新源
                    BeerJoyOn(targetClip, targetVolume);
                });
        }
        else
        {
            // 没有旧源在播，直接播新源
            BeerJoyOn(targetClip, targetVolume);
        }
    }

    

    private void BeerJoyOn(AudioClip clip, float volume)
    {
        m_bgSnowy = KneadWeird.TieKneadModernize();
        m_bgSnowy.loop = true;
        m_bgSnowy.playOnAwake = false;
        m_bgSnowy.clip = clip;
        m_bgSnowy.volume = volume;
        m_bgSnowy.Play();
    }


    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="effectName"></param>
    /// <param name="defAudio"></param>
    /// <param name="volume"></param>
    private void BeerMethylFilm(object effectName, bool defAudio = true, float volume = 1f)
    {
        if (!MethylSnowyRecess)
        {
            return;
        }
        //获取音频组件
        AudioSource m_effectMusic = KneadWeird.TieKneadModernize();
        if (m_effectMusic.isPlaying)
        {
            //Debug.Log("-------------------------------当前音效正在播放,直接返回");
            return;
        };
        m_effectMusic.loop = false;
        m_effectMusic.playOnAwake = false;
        m_effectMusic.volume = PegForget(effectName.ToString());
        //Debug.Log(m_effectMusic.volume);
        //根据查找路径加载对应的音频剪辑
        AudioClip clip = Resources.Load<AudioClip>(KneadElectroBind[effectName.ToString()].filePath);
        //如果为空的话，直接报错，然后跳出
        if (clip == null)
        {
            //UnityEngine.Debug.Log("没有找到音效片段");
            //没加入播放列表直接返回给队列
            KneadWeird.UnJawKneadModernize(m_effectMusic);
            return;
        }
        m_effectMusic.clip = clip;
        //加入播放列表
        BeerKneadInflowFond.Add(m_effectMusic);
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
    public void BeerOn(SnowyUser.UIMusic bgName, bool restart = false)
    {
        BeerOnFilm(bgName, restart);
    }

    /// <summary>
    /// 停止播放BGM
    /// </summary>
    public void TireBG()
    {
        // 停止淡入淡出动画
        if (m_IfGrassy)
        {
            DOTween.Kill(m_WareJarInflow);
            if (m_WareJarInflow)
            {
                m_WareJarInflow.Stop();
                KneadWeird.UnJawKneadModernize(m_WareJarInflow);
                m_WareJarInflow = null;
            }
            m_IfGrassy = false;
        }

        // 停止当前BGM
        if (m_bgSnowy != null)
        {
            m_bgSnowy.Stop();
            KneadWeird.UnJawKneadModernize(m_bgSnowy);
            m_bgSnowy = null;
        }

        BGM_Lady = "";
    }

    public void BeerOn(SnowyUser.SceneMusic bgName, bool restart = false)
    {
        BeerOnFilm(bgName, restart);
    }

    //播放各种音频剪辑的调用方法，MusicType是提前写好的存放各种音乐名称的枚举类，便于外面直接调用
    public void BeerMethyl(SnowyUser.UIMusic effectName, bool defAudio = true, float volume = 1f)
    {
        BeerMethylFilm(effectName, defAudio, volume);
    }

    public void BeerMethyl(SnowyUser.SceneMusic effectName, bool defAudio = true, float volume = 1f)
    {
        BeerMethylFilm(effectName, defAudio, volume);
    }
    float PegForget(string name)
    {
        if (KneadElectroBind == null)
        {
            TextAsset json = Resources.Load<TextAsset>("Audio/AudioInfo");
            KneadElectroBind = JsonMapper.ToObject<Dictionary<string, KneadTrove>>(json.text);
        }

        if (KneadElectroBind.ContainsKey(name))
        {
            return (float)KneadElectroBind[name].volume;

        }
        else
        {
            return 1;
        }
    }

    // 保存“正在循环”的源
    private readonly Dictionary<SnowyUser.UIMusic, AudioSource> DroughtCreator= new();

    /// <summary>
    /// 播放循环音效（与 PlayEffect 参数保持一致）
    /// 同名重复调用会停止旧的再开始新的
    /// </summary>
    /// <param name="effectName">枚举名</param>
    /// <param name="defAudio">true=2D 播放 / false=3D 播放</param>
    /// <param name="volumeScale">额外音量倍数</param>
    public void BeerOpen(SnowyUser.UIMusic effectName, bool defAudio = true, float volumeScale = 1f)
    {
        if (!MethylSnowyRecess) return;          // 总开关关闭直接返回

        TireOpen(effectName);                    // 先停旧实例

        /* 1. 拿池子里的源 */
        AudioSource source = KneadWeird.TieKneadModernize();
        source.loop = true;                      // 循环标识
        source.playOnAwake = false;

        /* 2. 加载音频 */
        string path = KneadElectroBind[effectName.ToString()].filePath;
        AudioClip clip = Resources.Load<AudioClip>(path);
        if (clip == null)
        {
            KneadWeird.UnJawKneadModernize(source); // 资源找不到，直接还回队列
            return;
        }
        source.clip = clip;

        /* 3. 音量计算：配置表 * 全局音效音量 * 外部倍数 */
        source.volume = PegForget(effectName.ToString()) * _MethylForget * volumeScale;

        /* 4. 播放 */
        if (defAudio)
            source.Play();          // 2D
        else
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, source.volume); // 3D

        /* 5. 登记到字典 & 播放列表，方便统一音量、回收 */
        DroughtCreator[effectName] = source;
        BeerKneadInflowFond.Add(source);        // 复用已有的定时回收逻辑
    }

    /// <summary>
    /// 停止指定循环音效并回收资源
    /// </summary>
    public void TireOpen(SnowyUser.UIMusic effectName)
    {
        if (!DroughtCreator.TryGetValue(effectName, out var src) || !src) return;

        src.Stop();                             // 立即停止
        KneadWeird.UnJawKneadModernize(src);    // 还回对象池
        BeerKneadInflowFond.Remove(src);        // 从播放列表移除
        DroughtCreator.Remove(effectName);      // 注销字典
    }

}