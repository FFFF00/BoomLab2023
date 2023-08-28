using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class Jigsaw : MonoBehaviour
{

    public Vector2 centerPos;

    public bool fixedPos = false;
    public bool rolling = false;

    [SerializeField]
    private Vector3Int CoordInGrid;
    [SerializeField] private Vector3 LocalPosInGrid;

    private SpriteRenderer jigsawSprite;
    private PlayerInput input;


    private Grid grid;

    //prefab的pivot被设置成了center中间，这和grid这套系统从左下角开始算格子的逻辑不符，必须加offset
    public Vector3 pivotOffset => grid.cellSize / 2;

    private void Awake()
    {
        input = new PlayerInput();
        grid = GetComponentInParent<Grid>();

        //align to grid
        Vector3Int coord = grid.WorldToCell(transform.position);
        Vector3 snappedLocalPos = grid.CellToLocal(coord) + pivotOffset;
        transform.localPosition = snappedLocalPos;

        GameManager.Instance.RegisterObject(this);

        var list = GetComponentsInChildren<SpriteRenderer>(includeInactive: false);
        foreach (var sprite in list)
        {
            if (sprite.enabled)
                jigsawSprite = sprite;
        }
        jigsawSprite.transform.parent = null;
        jigsawSprite.transform.position = transform.position;
    }


    void OnMouseDrag()
    {
        if (fixedPos || !GameLogic.Instance.commonActions.enabled)
            return;

        //float x = Mathf.Clamp(Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).x - centerPos.x), 0, 1);
        //float y = Mathf.Clamp(Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).y - centerPos.y), 0, 1);
        Vector3Int coord = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        
        //Vector2 targetPos = new Vector2(gridSize * x, gridSize * y);
        if (!GameManager.Instance.CheckLegalTargetPos(coord))
            return;
        MoveTo(coord);

        GameManager.Instance.UpdateObject(this);


        //jigsawSprite.transform.DORotate(jigsawSprite.transform.rotation.eulerAngles + Vector3.one, 0.5f);
        //Debug.Log(name + " 被抓了! "); //+ Time.frameCount);
    }

    private void OnMouseOver()
    {
        if (fixedPos || rolling || !GameLogic.Instance.commonActions.enabled)
            return;

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            //transform.transform.DORotate(jigsawSprite.transform.rotation.eulerAngles + new Vector3(0,0,90), 0.5f).OnComplete(() => rolling = false);

            jigsawSprite.transform.DORotate(jigsawSprite.transform.rotation.eulerAngles + new Vector3(0, 0, 90), 0.5f).OnComplete(delegate ()
            {
                transform.transform.Rotate(new Vector3(0, 0, 90));
                rolling = false;
            });

            rolling = true;
            GameLogic.Instance.PlayActionTextAndAudio(PlayerAction.rotate);
        }
    }

    public Vector3Int CellCoord
    {
        get
        {
            CoordInGrid = grid.WorldToCell(transform.position);
            LocalPosInGrid = grid.WorldToLocal(transform.position);
            return CoordInGrid;
        }
    }

    public void MoveTo(Vector3Int coord)
    {
        Vector3 targetPos = grid.CellToLocal(coord) + pivotOffset;
        transform.localPosition = targetPos;
        jigsawSprite.transform.DOMove(transform.position, 0.5f);
        //Debug.Log(name + " 动了 " + Time.frameCount);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        Player player = col.gameObject.GetComponent<Player>();
        if (player)
            fixedPos = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Player player = col.gameObject.GetComponent<Player>();
        if (player)
            fixedPos = false;
    }
}
