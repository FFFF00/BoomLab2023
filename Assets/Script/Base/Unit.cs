using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb;
    [HideInInspector]
    public Collider2D coll;
    [HideInInspector]
    public Renderer[] renderers;

    public GameObject mainRenderer;

    public Animator animator;

    protected const int STMove = 1;
    protected const int STAim = 2;
    protected const int STAttack = 3;
    protected const int STBroken = 4;

    public StateMachine stateMachine;
    public bool freeze;
        

    //[HideInInspector]
    public int currState;

    public void Action()
    {
        if (stateMachine != null) {
            stateMachine.FixedRun();
            currState = stateMachine.getCurrStat();
        }
    }

    public virtual void CommonInit()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        // if (mainRenderer != null)
        // {
        //     renderers = mainRenderer.GetComponentsInChildren<SpriteRenderer>();
        // }

        Message message = new Message();
        message.gameObject = gameObject;
        MessageManager.Instance.SendMessage(MessageManager.MessageId.UnitCreate, message);
    }

    public virtual void AddForce(Vector2 force) {}
    public virtual void StateCheckEnter() {}
    public virtual void StateCheckExit() {}
}
