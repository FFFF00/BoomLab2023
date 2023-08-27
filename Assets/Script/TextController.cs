using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    [SerializeField] private InkStoryManager storyManager;

    [SerializeField] private TMP_Text text;

    public int textSpeedMilisecond = 50; 

    private CancellationTokenSource cancelToken;

    public bool DisplayOneLinePlotText()
    {
        bool canContinue;
        cancelToken = new CancellationTokenSource();
        string str = storyManager.StepStory(out canContinue);
         if (canContinue)
        {
            cancelToken.Cancel();
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
        string str = storyManager.StepAction(op);
        await DisplayTextByCharacter(str);
    }


    public async UniTask DisplayTextByCharacter(string str)
    {
        try
        {
            foreach (var c in str)
            {
                await UniTask.Delay(textSpeedMilisecond, cancellationToken: cancelToken.Token);
                text.text += c;
            }
        }catch (Exception ex)
        {
            text.text = string.Empty;
        }
    }
}
