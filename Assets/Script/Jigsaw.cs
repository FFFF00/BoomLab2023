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
    public float gridSize = 4;
    public Vector2 centerPos;

    public bool fixedPos = false;
    public bool rolling = false;

    [SerializeField]
    private Vector3Int CoordInGrid;

    private SpriteRenderer jigsawSprite;
    private PlayerInput input;


    private Grid grid;
    private void Awake()
    {
        input = new PlayerInput();
        grid = GetComponentInParent<Grid>();

        //align to grid
        Vector3Int coord = grid.WorldToCell(transform.position);
        Vector3 snappedLocalPos = grid.CellToLocal(coord);
        transform.localPosition = snappedLocalPos;

        centerPos = new Vector2(gridSize / 2, gridSize / 2);
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
        var realCoord = new Vector3Int(coord.x, coord.y, 0);
        Vector3 targetPos = grid.CellToLocal(coord);
        //Vector2 targetPos = new Vector2(gridSize * x, gridSize * y);
        if (!GameManager.Instance.CheckLegalTargetPos(coord))
            return;
        transform.localPosition = targetPos;
        GameManager.Instance.UpdateObject(this);

        jigsawSprite.transform.DOMove(transform.position, 0.5f);

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
            return CoordInGrid;
        }
    }

    public void MoveTo(Vector3Int coord)
    {
        var targetPos = grid.CellToWorld(coord);
        transform.position = targetPos;
        jigsawSprite.transform.DOMove(targetPos, 0.5f);
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
