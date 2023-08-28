using Cysharp.Threading.Tasks;
using Fungus;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class GameLogic : MonoBehaviour
{
    public static GameLogic Instance;
    [SerializeField]
    private InkStoryManager storyManager;
    [SerializeField]
    private TextController textController;

    [SerializeField]
    private AudioClip rotateAudioClip, moveTileAudioClip;
    private AudioSource audioSource;

    private DefaultInputConfig input;
    [HideInInspector]
    public DefaultInputConfig.UIActions uiActions;
    [HideInInspector]
    public DefaultInputConfig.CommonActions commonActions;

    private bool allowMoveTextUpdate = true, allowMoveTileTextUpdate = true, allowRotateTileTextUpdate = true;
    private int intervalMiliseconds = 8000;

    private UniTaskCompletionSource taskCompleter;
    public UniTask StoryDialogCompleted => taskCompleter.Task;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        input = new DefaultInputConfig();
        uiActions = input.UI;
        commonActions = input.Common;
        EnableUIAction();
        DisableCommonAction();
    }

    // Start is called before the first frame update
    void Start()
    {
        taskCompleter = new UniTaskCompletionSource();
        textController.DisplayOneLinePlotText();
    }

    private DefaultInputConfig.CommonActions GetCommonActions()
    {
        return commonActions;
    }


    // Update is called once per frame
    void Update()
    {
        if (uiActions.DialogNext.IsPressed())
        {
            ShowOneLineOfDialog();
        }
    }

    public void OnStoryEnd()
    {
        Application.Quit();
    }
    private void ShowOneLineOfDialog()
    {
        bool canContinue = textController.DisplayOneLinePlotText();
        if (canContinue)
        {
            _ = TemporarilyDisableUIAction(500);
        }
        else
        {
            //已经完成当前剧情对话
            taskCompleter.TrySetResult();
            DisableUIAction();
            EnableCommonAction();
        }
    }

    public void PlayActionTextAndAudio(PlayerAction op)
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
                audioSource.PlayOneShot(rotateAudioClip);
                if (allowRotateTileTextUpdate)
                {
                    _ = textController.DisplayActionText(op);
                    _ = TempDisableRotateText();
                };
                break;
            case PlayerAction.moveTile:
                audioSource.PlayOneShot(moveTileAudioClip);
                if (allowMoveTileTextUpdate)
                {
                    _ = textController.DisplayActionText(op);
                    _ = TempDisableMoveTileText();
                };
                break;
        }
    }

    public void ExitLevel()
    {
        storyManager.ExitLevel();
        ShowOneLineOfDialog();
        taskCompleter = new UniTaskCompletionSource();//重置剧情对话状态
        EnableUIAction();
        DisableCommonAction();
    }

    public void NextLevel()
    {
        storyManager.NextLevel();
        ShowOneLineOfDialog();
        taskCompleter = new UniTaskCompletionSource();//重置剧情对话状态
        EnableUIAction();
        DisableCommonAction();
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

    public void EnableCommonAction()
    {
        commonActions.Enable();
        InputManger.Instance.InputEnable();
    }
    public void DisableCommonAction()
    {
        commonActions.Disable();
        InputManger.Instance.InputDisable();
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
