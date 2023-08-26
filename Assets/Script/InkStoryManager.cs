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
    public Story mainStory;


    public static InkStoryManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public bool StepStory(Action<string> action)
    {
        if (mainStory.canContinue)
        {
            action.Invoke(mainStory.Continue());
            return true;
        }
        else return false;
    }

    public void StepAction(Action<string> callback, PlayerAction op)
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
        callback(mainStory.Continue());
    }

    public void NextLevel()
    {
        ChooseChoiceWithTag("next-level");
    }

    public void ChooseChoiceWithTag(string tag)
    {
        var choices = mainStory.currentChoices;
        foreach (var choice in choices)
        {
            if (choice.tags.Contains(tag))
            { mainStory.ChooseChoiceIndex(choice.index); return; }
        }
        throw new Exception($"Tag {tag} not found in current choices!");
    }
}


public enum PlayerAction
{
    rotate, move, moveTile, exitLevel
}