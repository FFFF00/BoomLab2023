using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TextController : MonoBehaviour
{
    [SerializeField] private InkStoryManager storyManager;

    [SerializeField] private TMP_Text text;

    public int textSpeedMilisecond = 50;

    [SerializeField] public List<AudioClip> typingClips;

    private CancellationTokenSource cancelToken;
    private AudioSource typingAudioSource;
    private System.Random random;

    private void Awake()
    {
        typingAudioSource = GetComponent<AudioSource>();
        random = new System.Random();
    }

    public bool DisplayOneLinePlotText()
    {
        bool canContinue;
        string str = storyManager.StepStory(out canContinue);

        if (canContinue)
        {
            if (str.Trim().Length == 0)
            {
                return DisplayOneLinePlotText();
            }
            if (str == "END")
            {
                GameLogic.Instance.OnStoryEnd();
            }
            _ = DisplayTextByCharacter(str);
            return true;
        }
        else
        {
            cancelToken.Cancel();
            return false;
        }
    }

    public async UniTask DisplayActionText(PlayerAction op)
    {
        string str = storyManager.SelectAction(op);

        await DisplayTextByCharacter(str);
    }


    public async UniTask DisplayTextByCharacter(string str)
    {
        cancelToken?.Cancel();//取消之前输出操作
        text.text = string.Empty;//置空
        cancelToken = new CancellationTokenSource();//代表本次操作
        try
        {
            foreach (var c in str)
            {
                await UniTask.Delay(textSpeedMilisecond, cancellationToken: cancelToken.Token);
                text.text += c;
                typingAudioSource.PlayOneShot(getRandomAudioClip());
            }

        }
        catch (Exception ex)
        {
            text.text = string.Empty;
        }
    }

    private AudioClip getRandomAudioClip()
    {
        int rand = random.Next(typingClips.Count);
        return typingClips[rand];
    }
}
