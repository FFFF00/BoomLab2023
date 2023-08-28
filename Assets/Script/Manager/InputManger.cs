using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManger : MonoSingletion<InputManger>
{
    public Vector2 dir;
    public Vector2 dirRaw;
    public PlayerInput playerInput;
   
    private float autoDashTimer = -2f;
    private float autoAttackTimer = -2f;
    private float autoDashReleaseTimer = -2f;
    private float autoAttackReleaseTimer = -2f;

    public float autoDashTime = .15f;
    public float autoAttackTime = .15f;

    public float sentive = 3f;
    public float DeadZone = 0.1f;
    protected override void Awake()
    {
        base.Awake();
        playerInput = GetComponent<PlayerInput>();
    }
    void Start()
    {
        AddListener();
    }

    private void Update()
    {
        if ((dir - dirRaw).magnitude < DeadZone)
        {
            dir = dirRaw;
        }
        else
        {
            dir += (dirRaw - dir).normalized * sentive * Time.deltaTime;

            if (dir.x * dirRaw.x < 0)
            {
                dir.x = 0;
            }
            if (dir.y * dirRaw.y < 0)
            {
                dir.y = 0;
            }
        }
    }
    
    private void AddListener()
    {
        //MessageManager.Instance.AddListener(MessageManager.MessageId.PlayerDeadPre, InputDisable);
        //MessageManager.Instance.AddListener(MessageManager.MessageId.NextStage, InputDisable);
        //MessageManager.Instance.AddListener(MessageManager.MessageId.ResetEffectBegin, InputEnable);
    }

    public void InputDisable(Message message = null)
    {
        playerInput.enabled = false;
        dir = Vector2.zero;
        dirRaw = Vector2.zero;
        
        autoDashTimer = -2f;
        autoAttackTimer = -2f;
        var dpress = Time.time - autoDashTimer <= autoDashTime;

        
    }
    
    public void InputEnable(Message message = null)
    {
        playerInput.enabled = true;
    }

    void OnMove(InputValue value)
    {
        dirRaw = value.Get<Vector2>();
    }
    void OnDash(InputValue value)
    {
        autoDashTimer = Time.time;
    }
    void OnAttack(InputValue value)
    {
        autoAttackTimer = Time.time;
    }
    void OnDashRelease(InputValue value)
    {
        autoDashReleaseTimer = Time.time;
    }
    void OnAttackRelease(InputValue value)
    {
        autoAttackReleaseTimer = Time.time;
    }

    public Vector2 GetDir()
    {
        return dir;
    }

    public void DashReset()
    {
        autoDashTimer = -2f;
    }

    public bool GetDashDown()
    {
        return Time.time - autoDashTimer <= autoDashTime;
    }
    
    public bool GetAttackDown()
    {
        return Time.time - autoAttackTimer <= autoAttackTime;
    }
    
    public bool GetDashRelease()
    {
        return Time.time - autoDashReleaseTimer <= autoDashTime;
    }
    
    public bool GetAttackRelease()
    {
        return Time.time - autoAttackReleaseTimer <= autoAttackTime;
    }

}
