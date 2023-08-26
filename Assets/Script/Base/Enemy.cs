using System.Collections;
using DG.Tweening;
using UnityEngine;

public abstract class Enemy : Unit
{
    public virtual void StateMachineInit()
    {
        stateMachine = new StateMachine();
        stateMachine.SetCallbacks(STMove, MoveStart, MoveEnd, null, MoveUpdate, null);
        stateMachine.SetCallbacks(STAim, AimStart, AimEnd, null, AimUpdate, AimCoroutine);
        stateMachine.SetCallbacks(STAttack, AttackStart, AttackEnd, null, AttackUpdate, AttackCoroutine);
        stateMachine.SetCallbacks(STBroken, BrokenStart, BrokenEnd, null, BrokenUpdate, BrokenCoroutine);
        stateMachine.setCurrStat(STMove);
    }

    #region 移动
    public virtual void MoveStart(){}

    public virtual void MoveEnd(){}

    public virtual void MoveUpdate(){}
    #endregion
    
    #region 瞄准
    [Space]
    [Header("瞄准")]
    public float aimTime;
    public virtual void RayAim(Vector2 dir) {}
    public virtual void Aim(Collider2D targetColl) { }

    public virtual void AimStart()
    {
        Message message = new Message();
        message.gameObject = gameObject;
        MessageManager.Instance.SendMessage(MessageManager.MessageId.InterAim, message);
    }
    public virtual void AimEnd() { }
    public abstract void AimUpdate();
    public virtual IEnumerator AimCoroutine()
    {
        yield return 0;
    }
    #endregion

    #region 攻击
    [Space]
    [Header("攻击")]
    public float attackTime;
    protected bool attackEffect;

    public float attackEffectDelay;
    public float attackEffectTime;

    public int attackCharge;

    public float reloadTime;
    protected bool isReloading;
    public abstract void AttackUpdate();
    public virtual void AttackStart()
    {
        Message message = new Message();
        message.gameObject = gameObject;
        MessageManager.Instance.SendMessage(MessageManager.MessageId.InterAttack, message);
    }
    public virtual void AttackEnd()
    {
        isReloading = true;
        Invoke("ReloadFinish", reloadTime);
    }
    public virtual IEnumerator AttackCoroutine()
    {
        yield return 0;
    }
    public virtual void ReloadFinish()
    {
        isReloading = false;
    }
    public virtual bool IsReloading()
    {
        return isReloading;
    }
    #endregion
    
    #region 受击
    [Space]
    [Header("受击")]
    public float shakeTime = 0.3f;
    public float shakeLevel = 0.8f;
    public int shakeFrame = 120;
    
    public virtual void BeAttacked(Player player)
    {
        Debug.Log("啊我死了");
        Message message = new Message();
        message.gameObject = gameObject;
        MessageManager.Instance.SendMessage(MessageManager.MessageId.UnitDistory, message);
        gameObject.SetActive(false);
    }

    public void Shake()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            Vector2 orgPos = renderers[i].transform.localPosition;
            animator.enabled = false;
            Tweener tweener = renderers[i].transform.DOShakePosition(shakeTime, new Vector2(shakeLevel, shakeLevel), shakeFrame);
            tweener.SetUpdate(true);
            CoroutineManager.Instance.StartCoroutine(ShakeReset(renderers[i], orgPos));
        }
    }
    
    public IEnumerator ShakeReset(Renderer renderer , Vector2 pos)
    {
        yield return new WaitForSecondsRealtime(shakeTime);
        yield return 0;
        animator.enabled = true;
        renderer.transform.localPosition = pos;
    }
    #endregion
    
    #region 损坏
    [Space]
    [Header("损坏")]
    public bool isBroken;
    public float brokenTime;
    protected float brokenTimer;
    protected const int brokenThingLayer = 14;
    protected const string brokenThingTag = "BrokenThing";
    protected int originLayer;
    protected string originTag;
    protected int brokenNextState = STBroken;
    public virtual void BrokenUpdate()
    {
        rb.velocity = Vector2.zero;
        gameObject.tag = brokenThingTag;
        gameObject.layer = brokenThingLayer;
    }
    public virtual void BrokenStart()
    {
        isBroken = true;
        coll.enabled = false;

        brokenTimer = Time.time;
        originLayer = gameObject.layer;
        originTag = gameObject.tag;

        StopAllCoroutines();
        //transform.DOKill();
        
        rb.velocity = Vector2.zero;
        
        Message message = new Message();
        message.gameObject = gameObject;
        MessageManager.Instance.SendMessage(MessageManager.MessageId.EnemyBreak, message);
    }
    public virtual void BrokenEnd() 
    {
        isBroken = false;
        gameObject.layer = originLayer;
        gameObject.tag = originTag;
    }
    public virtual IEnumerator BrokenCoroutine()
    {
        yield return 0;
    }
    #endregion
    
    #region 重力
    [Space]
    [Header("重力")]
    public bool mass;
    public float gravity = 1;
    public float maxGravity = 40;
    public float G = 2;
    public LayerMask wallLayer;
    public Vector2 groundCheckSize = new Vector2(0.95f,0.05f);
    public Vector2 groundCheckOffset = new Vector2(0,-1);

    public void Mass()
    {
        if (mass)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            
            if (GroundCheck())
            {
                gravity = 0;
            }
            else
            {         
                gravity += G;
                if (gravity > maxGravity)
                {
                    gravity = maxGravity;
                }
                rb.velocity += Vector2.down * gravity;
            }
        }
    }
    private bool GroundCheck()
    {
        Collider2D[] walls = Physics2D.OverlapBoxAll((Vector2)transform.position + groundCheckOffset, groundCheckSize, 0f, wallLayer);
        if (walls != null && walls.Length > 0)
        {
            return true;
        }
        return false;
    }
    #endregion
    
    #region 工具
    public float switchWait = 0.5f;
    public float switchTimer;
    public Vector2 switchVector2 = new Vector2(0, 40);
    public void SwitchTP(bool up)
    {
        if (Time.realtimeSinceStartup - switchTimer < switchWait)
        {
            return;
        }
        if (up)
        {
            transform.position += (Vector3)switchVector2;
        }
        else
        {
            transform.position -= (Vector3)switchVector2;
        }

        mass = !mass;
        switchTimer = Time.realtimeSinceStartup;
        rb.velocity = Vector2.zero;
    }
    
    public bool isMoving()
    {
        return currState == STMove;
    }

    public Vector2 NormalVector(Vector2 dir)
    {
        if(Mathf.Abs(dir.x) >= Mathf.Abs(dir.y))
        {
            return dir.x > 0 ? Vector2.right : Vector2.left;
        }
        else
        {
            return dir.y > 0 ? Vector2.up : Vector2.down;
        }
    }
    #endregion

    #region 销毁
    public virtual void OnDestroy()
    {
        //Debug.Log("被销毁");
        //EZReplayManager.get.remove4Recording(gameObject);
    }
    #endregion
}
