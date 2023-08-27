using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextController : MonoBehaviour
{
    [SerializeField] private InkStoryManager storyManager;

    [SerializeField] private TMP_Text text;


    public void DisplayPlotText()
    {
        while (true)
        {
            bool canContinue = storyManager.StepStory(delegate (string str)
            {
                DisplayTextByCharacter(str);
            });
            if (!canContinue)
            {
                return;
            }
        }
    }

    public void DisplayActionText(PlayerAction op)
    {
        storyManager.StepAction(delegate (string str) { DisplayTextByCharacter(str); }, op);
    }

    public IEnumerator DisplayTextByCharacter(string str)
    {
        foreach(var c in str)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            text.text += c;
        }
    }
}
