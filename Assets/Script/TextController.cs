using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    [SerializeField] private InkStoryManager storyManager;

    [SerializeField] private TMP_Text text;


    public bool DisplayOneLinePlotText()
    {
        bool canContinue;
        string str = storyManager.StepStory(out canContinue);
        if (canContinue)
        {
            text.text = "";//Çå¿Õ
            _ = DisplayTextByCharacter(str);
            return true;
        }
        else
        {
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
        foreach (var c in str)
        {
            await UniTask.Delay(c);
            text.text += c;
        }
        return;
    }
}
