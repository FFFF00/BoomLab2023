using Cysharp.Threading.Tasks;
using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameLogic : MonoBehaviour
{
    [SerializeField]
    private InkStoryManager storyManager;
    [SerializeField]
    private TextController textController;
    [SerializeField]
    private PlayerInput input;

    private PlayerInput.UIActions uiActions;

    private bool allowMoveTextUpdate, allowMoveTileTextUpdate, allowRotateTileTextUpdate = true;
    private int intervalMiliseconds = 8000;
    // Start is called before the first frame update
    void Start()
    {
        uiActions = input.UI;
        EnableUIAction();
    }

    // Update is called once per frame
    void Update()
    {
        if (uiActions.DialogNext.IsPressed())
        {
            bool canContinue = textController.DisplayOneLinePlotText();
            if (canContinue)
            {
                _ = TemporarilyDisableUIAction(500);
            }
            else
            {
                DisableUIAction();
            }

        }
    }

    public void ShowActionText(PlayerAction op)
    {
        switch (op)
        {
            case PlayerAction.move:
                if (allowMoveTextUpdate)
                {
                    _ = textController.DisplayActionText(op);
                    _ = TempDisableMoveText();
                };
                break;
            case PlayerAction.rotate:
                if (allowRotateTileTextUpdate)
                {
                    _ = textController.DisplayActionText(op);
                    _ = TempDisableRotateText();
                };
                break;
            case PlayerAction.moveTile:
                if (allowMoveTileTextUpdate)
                {
                    _ = textController.DisplayActionText(op);
                    _ = TempDisableMoveTileText();
                };
                break;
        }
    }
    public void NextLevel()
    {
        storyManager.NextLevel();
    }
    public void ExitLevel()
    {
        storyManager.ExitLevel();
    }

    public void DisableUIAction()
    {
        uiActions.Disable();
    }

    //当那边进入关卡完结/进入状态就重新启用UI逻辑
    public void EnableUIAction()
    {
        uiActions.Enable();
    }
    private async UniTask TemporarilyDisableUIAction(int duration)
    {
        uiActions.Disable();
        await UniTask.Delay(duration);
        uiActions.Enable();
    }

    private async UniTask TempDisableMoveText()
    {
        allowMoveTextUpdate = false;
        await UniTask.Delay(intervalMiliseconds);
        allowMoveTextUpdate = true;
    }


    private async UniTask TempDisableMoveTileText()
    {
        allowMoveTileTextUpdate = false;
        await UniTask.Delay(intervalMiliseconds);
        allowMoveTileTextUpdate = true;
    }
    private async UniTask TempDisableRotateText()
    {
        allowRotateTileTextUpdate = false;
        await UniTask.Delay(intervalMiliseconds);
        allowRotateTileTextUpdate = true;
    }

}
