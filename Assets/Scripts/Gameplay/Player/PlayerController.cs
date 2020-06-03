using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : UnitBase
{
    /// <summary>
    /// Animator of player
    /// </summary>
    [SerializeField] private Animator animator;

    /// <summary>
    /// Player moving EnumDirection
    /// </summary>
    [SerializeField] private EnumDirection EnumDirection = EnumDirection.NONE;

    /// <summary>
    /// Delegate check player movement
    /// </summary>
    /// <param name="startTile"></param>
    /// <param name="endTile"></param>
    /// <param name="dir"></param>
    /// <returns></returns>
    public delegate bool CheckPlayerMoving(Vector3 startTile, Vector3 endTile, EnumDirection dir);
    public CheckPlayerMoving checkPlayerMoving;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        this.EnumDirection = EnumDirection.NONE;
    }

    protected override void OnEnterState(EnumUnitState EnumUnitState)
    {
        base.OnEnterState(EnumUnitState);
        this.SetPlayerDirection(this.EnumDirection);
    }

    protected override void OnUpdateState(EnumUnitState EnumUnitState, float time)
    {
        base.OnUpdateState(EnumUnitState, time);
        this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, this.targetPos, time * this.speed);
        if (this.targetPos == this.transform.localPosition)
        {
            this.tile = this.toTilePos;
            this.CheckMove();
        }
    }

    public void SetPlayerDirection(EnumDirection EnumDirection)
    {
        this.EnumDirection = EnumDirection;
        if (this.EnumDirection != EnumDirection.NONE)
        {
            this.animator.SetInteger("Dir", (int)this.EnumDirection);
        }
    }

    public void Move(EnumDirection enumDirection)
    {
        this.EnumDirection = enumDirection;
        if (this.unitState.CurrentState == EnumUnitState.MOVE)
        {
            return;
        }
        this.CheckMove();
    }

    private void CheckMove()
    {
        if (this.EnumDirection == EnumDirection.NONE)
        {
            this.unitState.IsChangeState(EnumUnitState.IDLE);
            return;
        }

        Vector3? toTilePos = null;
        switch (this.EnumDirection)
        {
            case EnumDirection.UP:
                toTilePos = this.tile + Vector3.up;
                break;
            case EnumDirection.DOWN:
                toTilePos = this.tile + Vector3.down;
                break;
            case EnumDirection.LEFT:
                toTilePos = this.tile + Vector3.left;
                break;
            case EnumDirection.RIGHT:
                toTilePos = this.tile + Vector3.right;
                break;
        }

        var box = GameplayController.Instance.GetBox(toTilePos.Value.x, toTilePos.Value.y);

        if (this.checkPlayerMoving(this.tile, toTilePos.Value, this.EnumDirection))
        {
            this.toTilePos = toTilePos.Value;
            this.targetPos = this.toTilePos * ConfigManager.Instance.mapConfig.TileSize;
            this.unitState.IsChangeState(EnumUnitState.MOVE);
            //GameData.Instance.recordStack.Push(new RecordUnitData(this.tile, toTilePos.Value, this.EnumDirection, GameplayController.Instance.dynamicUnitList.IndexOf(box)));
        }
        else
        {
            this.unitState.IsChangeState(EnumUnitState.IDLE);
            //GameData.Instance.recordStack.Push(new RecordUnitData(default, default, default, default));
        }
        GameData.Instance.recordStack.Push(new RecordUnitData(this.tile, toTilePos.Value, this.EnumDirection, GameplayController.Instance.dynamicUnitList.IndexOf(box)));
    }
}
