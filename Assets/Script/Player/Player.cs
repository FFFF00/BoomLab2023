using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Unit
{
    [Space]
    [Header("输入")]
    public Vector2 dir;
    private Vector2 facing;
    
    [Space]
    [Header("Layer")]
    public LayerMask wallLayer;
    [HideInInspector]
    public Vector2 collPos;

    private const int STWalk = 1;
    public int currStat;
    
    void Start()
    {
        Message message = new Message();
        message.gameObject = gameObject;
        MessageManager.Instance.SendMessage(MessageManager.MessageId.PlayerCreate, message);

        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        StateMachineInit();
    }
    private void StateMachineInit()
    {
        stateMachine = new StateMachine();
        stateMachine.SetCallbacks(STWalk, WalkStart, WalkEnd, WalkInput, WalkUpdate, null);
        stateMachine.setCurrStat(STWalk);
    }
    
    // Update is called once per frame
    void Update()
    {
        //获取方向
        dir = InputManger.Instance.GetDir();

        // if (dir.x > 0.1)
        // {
        //     mainRenderer.transform.localScale = new Vector3(-1 , 1, 1);
        // }
        // else if(dir.x < -0.1)
        // {
        //     mainRenderer.transform.localScale = new Vector3(1, 1, 1);
        // }
    }

    private void FixedUpdate()
    {
        collPos = (Vector2)coll.transform.position;
        Action();
    }

    #region 移动
    [Space]
    [Header("移动")]
    public float speed = 4.0f;
    private Vector2 lastFramePos;
    private Vector2 currFramePos;
    private int WalkInput()
    {
        return STWalk;
    }
    
    private void WalkStart()
    {
        currFramePos = transform.position;
        lastFramePos = currFramePos;
    }
    private void WalkUpdate()
    {
        GameLogic.Instance.ShowActionText(PlayerAction.move);
        rb.velocity = dir.normalized * speed;
        lastFramePos = currFramePos;
        currFramePos = transform.position;
    }
    private void WalkEnd()
    {
        MessageManager.Instance.SendMessage(MessageManager.MessageId.WalkEnd);
    }
    #endregion
    
    #region 工具
    [Space]
    [Header("工具")]
    public float shakeTime = 0.05f;
    public float shakeLevel = 0.5f;
    public int shakeFrame = 60;
    public Vector2 initPosition;

    public Vector2 SimpleDir(Vector2 dir)
    {
        float lengthx = Mathf.Abs(dir.normalized.x);
        float lengthy = Mathf.Abs(dir.normalized.y);
        if (lengthx > lengthy)
        {
            dir.y = 0;
        }
        if (lengthy > lengthx)
        {
            dir.x = 0;
        }
        // if (dir.x * dir.y != 0)
        // {
        //     if (dir.x > 0)
        //     {
        //         dir.x = 1;
        //     }
        //     if (dir.x < 0)
        //     {
        //         dir.x = -1;
        //     }
        //     if (dir.y > 0)
        //     {
        //         dir.y = 1;
        //     }
        //     if (dir.y < 0)
        //     {
        //         dir.y = -1;
        //     }
        // }
        dir = dir.normalized;
        return dir;
    }
    public void Shake()
    {
        //mainRenderer.transform.DOKill();
        Vector2 orgPos = mainRenderer.transform.localPosition;
        animator.enabled = false;
        //Tweener tweener = mainRenderer.transform.DOShakePosition(shakeTime, new Vector2(shakeLevel, shakeLevel), shakeFrame);
        //tweener.SetUpdate(true);
        //CoroutineManager.Instance.StartCoroutine(ShakeReset(mainRenderer, orgPos));
    }
    
    public IEnumerator ShakeReset(Renderer renderer , Vector2 pos)
    {
        yield return new WaitForSecondsRealtime(shakeTime);
        yield return 0;
        animator.enabled = true;
        renderer.transform.localPosition = pos;
    }
    
    public void Death(int sourceType, GameObject obj = null)
    {
        //CoroutineManager.Instance.StopAllCoroutine();
        dir = Vector2.zero;
        rb.velocity = Vector2.zero;
        stateMachine.setCurrStat(STWalk);
        transform.position = Vector2.zero;

        Message message = new Message();
        message.num = sourceType;
        MessageManager.Instance.SendMessage(MessageManager.MessageId.PlayerDead, message);
        //CoroutineManager.Instance.StartCoroutine(DeathCoroutine(sourceType));
    }
    
    #endregion
}
