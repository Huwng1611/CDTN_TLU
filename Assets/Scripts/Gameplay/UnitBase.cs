using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all units in game
/// </summary>
public class UnitBase : MonoBehaviour
{
    #region FIELDS & PROPERTIES
    [SerializeField] private EnumUnitType unitType;
    /// <summary>
    /// Type of the unit
    /// </summary>
    public EnumUnitType UnitType
    {
        get
        {
            return this.unitType;
        }
    }

    protected Vector3 tile;
    /// <summary>
    /// Coordinate on map
    /// </summary>
    public Vector3 Tile
    {
        get
        {
            return this.tile;
        }
    }

    protected bool isMove;
    /// <summary>
    /// Check if unit is moving
    /// </summary>
    public bool IsMove
    {
        get
        {
            return this.isMove;
        }
    }

    protected float speed;
    public float Speed
    {
        get
        {
            return this.speed;
        }
        set
        {
            this.speed = value;
        }
    }

    /// <summary>
    /// Sprite for unit
    /// </summary>
    private SpriteRenderer spriteRenderer;

    protected UnitState<EnumUnitState> unitState = new UnitState<EnumUnitState>();

    /// <summary>
    /// 
    /// </summary>
    [SerializeField] protected AudioSource audioMovement;

    protected Vector3 targetPos;
    protected Vector3 toTilePos;

    #endregion

    #region UNITY CALLBACK
    protected virtual void Awake()
    {
        this.Init();
        this.unitState.RegisterState(EnumUnitState.IDLE, OnEnterState);
        this.unitState.RegisterState(EnumUnitState.MOVE, OnEnterState, null, OnUpdateState);
        this.unitState.IsChangeState(EnumUnitState.IDLE);
    }

    protected virtual void FixedUpdate()
    {
        if (this.unitType == EnumUnitType.PLAYER || this.unitType == EnumUnitType.BOX)
        {
            this.unitState.UpdateState(Time.fixedDeltaTime);
        }
    }
    #endregion

    #region FUNCTION

    #region VIRTUAL FUNCTION
    public virtual void SetTile(float x, float y)
    {
        this.tile = new Vector3(x, y);
        this.transform.localPosition = this.tile * ConfigManager.Instance.mapConfig.TileSize;
    }

    protected virtual void OnEnterState(EnumUnitState unitState)
    {
        switch (this.unitState.CurrentState)
        {
            case EnumUnitState.IDLE:
                this.isMove = false;
                if (this.audioMovement != null)
                {
                    this.audioMovement.Stop();
                }
                break;
            case EnumUnitState.MOVE:
                this.isMove = true;
                if (this.audioMovement != null)
                {
                    this.audioMovement.Play();
                }
                break;
        }
    }

    protected virtual void OnUpdateState(EnumUnitState unitState, float time)
    {

    }
    #endregion

    private void Init()
    {
        if (this.unitType == EnumUnitType.TARGET)
        {
            this.spriteRenderer = this.GetComponent<SpriteRenderer>();
            //this.spriteRenderer.sortingLayerName = TagsAndLayers.sortingLayerGround;
        }
        this.SetUnitSpeed(this.unitType);
    }

    private void SetUnitSpeed(EnumUnitType unitType)
    {
        if (unitType == EnumUnitType.PLAYER || unitType == EnumUnitType.BOX)
        {
            this.speed = 2f;
        }
        else
        {
            //Unity target and block cannot move
            speed = 0f;
        }
    }

    public float GetSortingValue()
    {
        return this.transform.localPosition.y;
    }

    public void SetSortingVale(int sorting_value)
    {
        this.spriteRenderer.sortingOrder = sorting_value;
    }
    #endregion
}

public enum EnumUnitType
{
    PLAYER,
    BLOCK,
    BOX,
    TARGET
}

public enum EnumDirection
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    NONE
}

public enum EnumUnitState
{
    MOVE,
    IDLE
}
