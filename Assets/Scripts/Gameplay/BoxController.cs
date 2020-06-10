using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : UnitBase
{
    public delegate bool OnBoxTileHandler(BoxController boxController, Vector3 tile);
    public OnBoxTileHandler onBoxTileHandler;

    public Vector3 ToTilePos
    {
        get
        {
            return this.toTilePos;
        }
    }

    /// <summary>
    /// Contain sprite default & goal sprite
    /// element 0: default
    /// element 1: sprite on target
    /// </summary>
    [SerializeField] Sprite[] boxSprites;
    [SerializeField] SpriteRenderer mainSprite;

    public bool IsOnTarget { get; private set; } = false;

    protected override void Awake()
    {
        base.Awake();
        this.mainSprite = this.GetComponent<SpriteRenderer>();
    }

    public override void SetTile(float x, float y)
    {
        base.SetTile(x, y);
        this.OnTileChange();
    }

    protected override void OnEnterState(EnumUnitState unitState)
    {
        base.OnEnterState(unitState);
    }

    protected override void OnUpdateState(EnumUnitState unitState, float time)
    {
        base.OnUpdateState(unitState, time);
        this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, this.targetPos, this.speed * Time.deltaTime);
        if (this.transform.localPosition == this.targetPos)
        {
            this.tile = this.toTilePos;
            this.OnTileChange();
            this.unitState.IsChangeState(EnumUnitState.IDLE);
        }
    }

    public void Move(EnumDirection enumDirection)
    {
        switch (enumDirection)
        {
            case EnumDirection.UP:
                this.toTilePos = this.tile + Vector3.up;
                break;
            case EnumDirection.DOWN:
                this.toTilePos = this.tile + Vector3.down;
                break;
            case EnumDirection.LEFT:
                this.toTilePos = this.tile + Vector3.left;
                break;
            case EnumDirection.RIGHT:
                this.toTilePos = this.tile + Vector3.right;
                break;
        }

        this.targetPos = this.toTilePos * ConfigManager.Instance.mapConfig.TileSize;
        this.unitState.IsChangeState(EnumUnitState.MOVE);
    }

    public void OnTileChange()
    {
        if (onBoxTileHandler != null)
        {
            this.IsOnTarget = onBoxTileHandler(this, this.tile);
            if (IsOnTarget)
            {
                this.mainSprite.sprite = this.boxSprites[1];
            }
            else
            {
                this.mainSprite.sprite = this.boxSprites[0];
            }
        }
    }
}
