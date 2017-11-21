using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class AudioMng : MngBase
{
    private const string AUDIODIR = "Audios/";
    private Dictionary<string,AudioClip> audios = new Dictionary<string, AudioClip>();

    [SerializeField]
    private AudioSource bgAudioSource;

    [SerializeField]
    private GameObject playOnlyGO;
    public override void Init()
    {
        EventTool.Add<string>(EventName.Audio.PlayBG,playBG);
        EventTool.Add<string>(EventName.Audio.PlayOnly,playBG);
    }

    private void playBG(string audioName)
    {
        if(!audios.ContainsKey(audioName))
        {
            AudioClip ac = LocalDataTool.ResLoad(AUDIODIR+audioName) as AudioClip;
            audios.Add(audioName,ac);
        }
        bgAudioSource.clip = audios[audioName];
        bgAudioSource.Play();
    }

    private void playOnly(string audioName)
    {
        if(!audios.ContainsKey(audioName))
        {
            AudioClip ac = LocalDataTool.ResLoad(AUDIODIR+audioName) as AudioClip;
            audios.Add(audioName,ac);
        }
        AudioSource audioSource = playOnlyGO.AddComponent<AudioSource>();
        audioSource.clip = audios[audioName];
        audioSource.Play();
        Destroy(audios[audioName],audios[audioName].length);
    }
}