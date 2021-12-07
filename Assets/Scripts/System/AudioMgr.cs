using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioMgr : Singleton<AudioMgr>
{
    [Header("BGM")]
    public AudioClip BGM1;
    public AudioClip BGM2;
    public AudioClip SE_WScream;

    [Header("Source")]
    public AudioSource BGMSource;
    public AudioSource SESource_2D;

    [ContextMenu("测试1")]
    public void Test()
    {
        PlayOneShot2DSE(SE_WScream);
    }

    /// <summary>
    /// 停止当前BGM，播放指定BGM（BGM会重复播放）
    /// </summary>
    /// <param name="clip"></param>
    public void PlayBGM(AudioClip clip,bool isFade)
    {
        BGMSource.loop = true;
        if (BGMSource.isPlaying)
        {
            if(isFade)
            {
                CrossFadeClip(BGMSource, clip);
            }
            else
            {
                BGMSource.Stop();
                BGMSource.clip = clip;
                BGMSource.Play();
            }
        }
        else
        {
            BGMSource.clip = clip;
            BGMSource.Play();
        }
    }

    /// <summary>
    /// 渐变启动BGM
    /// </summary>
    public void FadeStartBGM(AudioClip tarClip)
    {
        BGMSource.volume = 0;
        BGMSource.clip = tarClip;
        BGMSource.Play();
        BGMSource.DOFade(1, 1);
    }

    /// <summary>
    /// 渐变停止BGM
    /// </summary>
    public void FadeStopBGM()
    {
        StartCoroutine(FadeStopCo(BGMSource));
    }

    private IEnumerator FadeStopCo(AudioSource source)
    {
        Tween downTween = source.DOFade(0, 1);
        yield return downTween.WaitForCompletion();
        source.Stop();
    }

    /// <summary>
    /// 0-100设置总音量
    /// </summary>
    /// <param name="volume"></param>
    public void SetTotalVolume(int volume)
    {
        if(volume>=0 && volume<=100)
        {
            float targetVol = volume / 100f;
            AudioListener.volume = targetVol;
        }
    }

    /// <summary>
    /// 用于渐变过渡到下一个Clip
    /// </summary>
    public void CrossFadeClip(AudioSource source,AudioClip tarClip)
    {
        StartCoroutine(CrossFadeClipCo(source, tarClip));
    }

    private IEnumerator CrossFadeClipCo(AudioSource source, AudioClip tarClip)
    {
        Tween downTween = source.DOFade(0, 1);
        yield return downTween.WaitForCompletion();
        source.Stop();
        source.clip = tarClip;
        source.Play();
        source.DOFade(1, 1);
    }

    /// <summary>
    /// 播放一次2D音效
    /// </summary>
    public void PlayOneShot2DSE(AudioClip clip)
    {
        SESource_2D.PlayOneShot(clip, 1);
    }

}
