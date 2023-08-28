using Cysharp.Threading.Tasks;
using Ink.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InkStoryManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset inkJSONAsset;
    public Story mainStory;

    public static InkStoryManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        mainStory = new Story(inkJSONAsset.text);
    }

    public string StepStory(out bool canContinue)
    {
        if (mainStory.canContinue)
        {
            canContinue = true;
            return mainStory.Continue();
        }
        else canContinue = false;
        return "";
    }

    public string SelectAction(PlayerAction op)
    {
        if(mainStory.canContinue) { throw new Exception("Wrong state! story can still proceed but you are asking for choice."); }
        //mainStory.ChooseChoiceIndex(((int)op));//有点tricky，不要轻易更改
        string str = op switch
        {
            PlayerAction.rotate => "rotate",
            PlayerAction.move => "move",
            PlayerAction.moveTile => "move-tile",
            PlayerAction.exitLevel => "exit-level",
            _ => throw new NotImplementedException(),
        };
        ChooseChoiceWithTag(str);
        
        var res = mainStory.ContinueMaximally();
        return res;
    }
    public void ExitLevel()
    {
        ChooseChoiceWithTag("exit-level");
        //mainStory.Continue();//跳过换行符空行
    }
    public void NextLevel()
    {
        ChooseChoiceWithTag("next-level");
        //mainStory.Continue();//跳过换行符空行
    }

    public void ChooseChoiceWithTag(string tag)
    {
        var choices = mainStory.currentChoices;
        foreach (var choice in choices)
        {
            if (choice.tags.Contains(tag))
            { mainStory.ChooseChoiceIndex(choice.index); return; }
        }
        Debug.Log($"Tag {tag} not found in current choices!");
    }
}


[Serializable]
public enum PlayerAction
{
    rotate, move, moveTile, exitLevel
}