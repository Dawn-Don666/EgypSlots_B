using UnityEngine;
using System.Collections.Generic;
using Lofelt.NiceVibrations;

public class A_AudioManager : MonoBehaviour
{
    public static A_AudioManager Instance { get; private set; }
    
    public AudioClip DefaultButtonSound;
    public AudioClip DefaultBGM;
    public List<AudioClip> Audios = new List<AudioClip>();
    // 音乐音频源
    private AudioSource musicAudioSource;
    // 音效音频源池
    private List<AudioSource> SoundAudioSources = new List<AudioSource>();
    // 音效音频源数量
    public int SoundPoolSize = 5;

    // 音乐开关
    public bool isMusicOn = true;
    // 音效开关
    public bool isSoundOn = true;

    private void Awake()
    {
        Instance = this;

        isMusicOn = PlayerPrefs.GetInt(AConstant.ArchiveKey.BGMEnabled, 1) == 1;
        isSoundOn = PlayerPrefs.GetInt(AConstant.ArchiveKey.SoundEnabled, 1) == 1;

        // 初始化音乐音频源
        musicAudioSource = gameObject.AddComponent<AudioSource>();
        musicAudioSource.playOnAwake = false;
        musicAudioSource.loop = true;

        // 初始化音效音频源池
        for (int i = 0; i < SoundPoolSize; i++)
        {
            AudioSource SoundSource = gameObject.AddComponent<AudioSource>();
            SoundSource.playOnAwake = false;
            SoundAudioSources.Add(SoundSource);
        }

        PlayBGM();
        
        var isShake = PlayerPrefs.GetInt(AConstant.ArchiveKey.VibrationEnabled, 1) == 1;
        // 切换震动状态
        HapticController.hapticsEnabled = isShake;
    }

    // 切换音乐开关状态
    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        if (isMusicOn)
        {
            if (musicAudioSource.clip == null && DefaultBGM != null)
                musicAudioSource.clip = DefaultBGM;
            if (musicAudioSource.clip != null && !musicAudioSource.isPlaying)
            {
                musicAudioSource.Play();
            }
        }
        else
        {
            musicAudioSource.Pause();
        }
    }

    // 切换音效开关状态
    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
    }

    // 播放音乐
    public void PlayMusic(string name, float volume = 1f)
    {
        if (!isMusicOn) return;

        AudioClip musicClip = GetAudioClip(name);
        if (musicClip != null)
        {
            musicAudioSource.clip = musicClip;
            musicAudioSource.volume = volume;
            musicAudioSource.Play();
        }
    }
    
    public void PlayBGM()
    {
        if (!isMusicOn) return;

        if (DefaultBGM != null)
        {
            musicAudioSource.clip = DefaultBGM;
            musicAudioSource.volume = 1f;
            musicAudioSource.Play();
        }
    }

    public void PlayDefaultButtonSound()
    {
        if (!isSoundOn) return;

        AudioClip SoundClip = DefaultButtonSound;
        if (SoundClip != null)
        {
            foreach (AudioSource SoundSource in SoundAudioSources)
            {
                if (!SoundSource.isPlaying)
                {
                    SoundSource.clip = SoundClip;
                    SoundSource.volume = 1f;
                    SoundSource.Play();
                    return;
                }
            }
            // 如果所有音效源都在使用，可以动态创建新的音频源（这只是简单示例，实际可能需要更好的管理策略）
            AudioSource newSoundSource = gameObject.AddComponent<AudioSource>();
            newSoundSource.playOnAwake = false;
            SoundAudioSources.Add(newSoundSource);
            newSoundSource.clip = SoundClip;
            newSoundSource.Play();
        }
    }

    // 播放音效
    public void PlaySound(string name, float volume = 1f)
    {
        if (!isSoundOn) return;

        AudioClip SoundClip = GetAudioClip(name);
        if (SoundClip != null)
        {
            foreach (AudioSource SoundSource in SoundAudioSources)
            {
                if (!SoundSource.isPlaying)
                {
                    SoundSource.clip = SoundClip;
                    SoundSource.volume = volume;
                    SoundSource.Play();
                    return;
                }
            }
            // 如果所有音效源都在使用，可以动态创建新的音频源（这只是简单示例，实际可能需要更好的管理策略）
            AudioSource newSoundSource = gameObject.AddComponent<AudioSource>();
            newSoundSource.playOnAwake = false;
            SoundAudioSources.Add(newSoundSource);
            newSoundSource.clip = SoundClip;
            newSoundSource.Play();
        }
    }

    private AudioClip GetAudioClip(string name)
    {
        foreach (var clip in Audios)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }
        return null;
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt(AConstant.ArchiveKey.BGMEnabled, isMusicOn ? 1 : 0);
        PlayerPrefs.SetInt(AConstant.ArchiveKey.SoundEnabled, isSoundOn ? 1 : 0);
    }
}