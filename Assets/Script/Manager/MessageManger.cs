using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageManager 
{
    private static MessageManager _instance = null;
    public static MessageManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new MessageManager();
            }
            return _instance;
        }
    }

    public delegate void MessageDelegate(Message message = null);

    public Dictionary<MessageId, MessageDelegate> messageTable = new Dictionary<MessageId, MessageDelegate>();

    public void AddListener(MessageId messageId, MessageDelegate handler)
    {
        //Debug.Log("注册Listener:" + messageId);
        if (messageTable.ContainsKey(messageId))
        {
            messageTable[messageId] += handler;
        }
        else
        {
            messageTable.Add(messageId, handler);
        }
    }

    public void RemoveListener(MessageId messageId, MessageDelegate handler)
    {
        if (messageTable.ContainsKey(messageId))
        {
            messageTable[messageId] -= handler;
        }      
    }

    public void SendMessage(MessageId messageId, Message message = null)
    {
        MessageDelegate handlers;
        if (messageTable.TryGetValue(messageId, out handlers))
        {
            //Debug.Log("发送消息" + messageId);
            handlers?.Invoke(message);
        }
    }

    public enum MessageId{
        UnitCreate,
        UnitDistory,

        LittleExplodeBegin,
        MiddleExplodeBegin,
        
        SwitchCreate,

        PlayerCreate,
        PlayerDeadPre,
        PlayerDead,
        PlayerDestory,
        PlayerReset,

        NextStage,
        CompleteLoadStage,

        RecordBegin,
        RecordEnd,
        ReplayBegin,
        ReplayEnd,

        HackSearchBegin,
        HackSearchEnd,

        ObraDinnBegin,
        ObraDinnEnd,

        HackBegin,
        HackEnd,

        DashBegin,
        DashEnd,
        DashCoroutineEnd,

        Block,
        
        Shake,
        
        BeAttack,

        SlideBegin,
        //SlideEnd,

        AttackBegin,
        AttackEnd,

        Charge,

        EnemyBreak,
        EnemyCollisionBegin,

        IconCollisionBegin,

        AcceptTask,
        FinishTask,

        FindMeme,

        AutoSave,
        SaveModified,

        MenuOpen,
        MenuClose,

        SaveCompleted,

        WallCreate,

        ResetEffectBegin,
        ResetEffectEnd,

        ShutDownBegin,
        ShutDownEnd,

       //ClockBegin,
        ShieldCollSound,
        EnemyCollSound,
        
        GlitchDigitalEffect,
        
        InterAttack,
        InterAim,
        
        WalkBegin,
        WalkEnd,
        
        AimRay,
    }
}
