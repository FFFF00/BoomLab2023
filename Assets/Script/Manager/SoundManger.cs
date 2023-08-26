using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : MonoSingletion<SoundManger>
{
    [Space] [Header("BGM")] 
    public AudioSource bgmAudioSource;
    public float bgmVolume;
    public AudioClip bgm;
    
    [Space] [Header("背景")] 
    public AudioSource bgAudioSource;
    public float bgVolume;
    public AudioClip bg;
    
    [Space]
    [Header("人物")]
    public AudioSource dashAudioSource;
    public AudioClip dash;
    
    public AudioSource walkAudioSource;
    public AudioClip walk;
    
    public AudioSource slideAudioSource;
    public AudioClip slide;
    
    public AudioSource blockAudioSource;
    public AudioClip block;
    // Start is called before the first frame update
    void Start()
    {
        MessageManager.Instance.AddListener(MessageManager.MessageId.DashBegin, Dash);
        MessageManager.Instance.AddListener(MessageManager.MessageId.SlideBegin, Slide);
        MessageManager.Instance.AddListener(MessageManager.MessageId.Block, Block);
        MessageManager.Instance.AddListener(MessageManager.MessageId.WalkBegin, WalkBegin);
        MessageManager.Instance.AddListener(MessageManager.MessageId.WalkEnd, WalkEnd);

        bgmAudioSource.clip = bgm;
        bgmAudioSource.volume = bgmVolume;
        bgmAudioSource.Play();
        
        bgAudioSource.clip = bg;
        bgAudioSource.volume = bgVolume;
        bgAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void WalkBegin(Message message)
    {
        if (!walkAudioSource.isPlaying)
        {
            walkAudioSource.clip = walk;
            walkAudioSource.Play();
        }
    }
    
    private void WalkEnd(Message message)
    {
        walkAudioSource.Pause();
    }
    
    private void Dash(Message message)
    {
        dashAudioSource.clip = dash;
        dashAudioSource.Play();
    }
    
    private void Slide(Message message)
    {
        slideAudioSource.clip = slide;
        slideAudioSource.Play();
    }
    
    private void Block(Message message)
    {
        blockAudioSource.clip = block;
        blockAudioSource.Play();
    }
}
