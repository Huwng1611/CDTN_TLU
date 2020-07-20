using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static GameplayController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    [Header("Elements")]
    public Transform content;

    [Header("Player")]
    public PlayerController playerController;
    public GameObject playerPrefab;

    [Header("Boxes")]
    public GameObject boxPrefab;

    [Header("walls")]
    public GameObject blockPrefab;

    [Header("Targets")]
    public GameObject targetPrefab;

    [Header("Level Data")]
    public LevelData levelData;

    [Header("Unit List")]
    public List<UnitBase> unitList;
    public List<UnitBase> dynamicUnitList;

    #region Unity Callback
    private void Start()
    {
        this.Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            this.PlayerMovement(Vector2.left);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            this.PlayerMovement(Vector2.down);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            this.PlayerMovement(Vector2.up);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            this.PlayerMovement(Vector2.right);
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.D))
        {
            this.PlayerMovement(Vector2.zero);
        }
    }
    #endregion

    #region Init data
    private void Init()
    {
        //for level testing
        //this.levelData = new LevelData(5);
        //--------------------------------------
        GameData.Instance.recordStack.Clear();
        this.levelData = new LevelData(GameData.Instance.selectedLevel);
        this.unitList = new List<UnitBase>();
        this.dynamicUnitList = new List<UnitBase>();

        this.InitElementsPos();

        this.CreateMap();
        this.playerController.checkPlayerMoving += CheckPlayerMoveHandler;
        GameData.Instance.saveGame.LastLevel = this.levelData.levelID;
    }

    private void InitElementsPos()
    {
        int halfSize = ConfigManager.Instance.mapConfig.MapSize >> 1;
        float offset = -halfSize * ConfigManager.Instance.mapConfig.TileSize;
        this.content.transform.localPosition = new Vector2(offset, offset);
    }
    #endregion

    #region Create level
    private void CreateMap()
    {
        this.CreatePlayerUnit();
        this.CreateBoxUnits();
        this.CreateTargets();
        this.Createwalls();
    }

    private void CreatePlayerUnit()
    {
        for (int i = 0; i < this.levelData.players.Length; i++)
        {
            var tileX = levelData.players[i].x;
            var tileY = levelData.players[i].y;
            this.playerController = this.CreateUnit(this.playerPrefab, tileX, tileY).GetComponent<PlayerController>();
            this.playerController.name = "Player(" + tileX + "," + tileY + ")";
        }
    }

    private void CreateBoxUnits()
    {
        for (int i = 0; i < this.levelData.boxes.Length; i++)
        {
            var tileX = levelData.boxes[i].x;
            var tileY = levelData.boxes[i].y;
            var box = this.CreateUnit(this.boxPrefab, tileX, tileY).GetComponent<BoxController>();
            box.name = "Box(" + tileX + "," + tileY + ")";
            box.onBoxTileHandler += IsBoxOnTarget;
            box.OnTileChange();
        }
    }

    private void CreateTargets()
    {
        var st = new SortTool<UnitBase>();
        foreach (var target in this.levelData.targets)
        {
            var unit_target = CreateUnit(this.targetPrefab, target.x, target.y);
            st.AddItem((int)(unit_target.GetSortingValue() * 100), unit_target);
            unit_target.name = "Target(" + target.x + "," + target.y + ")";
        }

        var sortList = st.Sort(true);
        for (int i = 0; i < sortList.Length; i++)
        {
            //sortList[i].SetSortingVale(i + 1);
            sortList[i].SetSortingVale(-1);
        }
    }

    private void Createwalls()
    {
        for (int i = 0; i < levelData.walls.Length; i++)
        {
            var tileX = levelData.walls[i].x;
            var tileY = levelData.walls[i].y;
            var block = this.CreateUnit(this.blockPrefab, tileX, tileY);
            block.name = "Block(" + tileX + "," + tileY + ")";
        }
    }

    private UnitBase CreateUnit(GameObject prefab, float tileX, float tileY)
    {
        var unit = Instantiate(prefab);
        unit.transform.SetParent(this.content);
        UnitBase unitBase = unit.GetComponent<UnitBase>();
        unitBase.SetTile(tileX, tileY);
        if (unitBase.UnitType != EnumUnitType.TARGET)
        {
            this.unitList.Add(unitBase);
            if (unitBase.UnitType == EnumUnitType.PLAYER || unitBase.UnitType == EnumUnitType.BOX)
            {
                this.dynamicUnitList.Add(unitBase);
            }
        }
        return unitBase;
    }
    #endregion

    private bool IsBoxOnTarget(BoxController box, Vector3 tile)
    {
        if (this.levelData.IsTarget(tile.x, tile.y))
        {
            this.CheckPassLevel();
            return true;
        }
        return false;
    }

    private bool CheckPlayerMoveHandler(Vector3 startTile, Vector3 endTile, EnumDirection enumDirection)
    {
        var x = endTile.x;
        var y = endTile.y;
        if (this.levelData.IsWall(x, y))
        {
            return false;
        }

        var box = GetBox(x, y) as BoxController;
        if (box != null)
        {
            if (box.IsMove)
            {
                return false;
            }
            switch (enumDirection)
            {
                case EnumDirection.UP:
                    y += 1;
                    break;
                case EnumDirection.DOWN:
                    y -= 1;
                    break;
                case EnumDirection.LEFT:
                    x -= 1;
                    break;
                case EnumDirection.RIGHT:
                    x += 1;
                    break;
            }

            if (this.levelData.IsWall(x, y) || this.GetBox(x, y) != null)
            {
                return false;
            }
            //GameData.Instance.recordStack.Push(new RecordUnitData(startTile, endTile, enumDirection, dynamicUnitList.IndexOf(box)));
            box.Move(enumDirection);
        }
        return true;
    }

    public void PlayerMovement(Vector2 direction)
    {
        EnumDirection enumDirection;
        if (direction == Vector2.zero)
        {
            enumDirection = EnumDirection.NONE;
        }
        else
        {
            direction = direction.normalized;
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x < 0)
                {
                    enumDirection = EnumDirection.LEFT;
                }
                else
                {
                    enumDirection = EnumDirection.RIGHT;
                }
            }
            else
            {
                if (direction.y < 0)
                {
                    enumDirection = EnumDirection.DOWN;
                }
                else
                {
                    enumDirection = EnumDirection.UP;
                }
            }
        }
        this.playerController.Move(enumDirection);
    }

    private void CheckPassLevel()
    {
        var number_target = this.levelData.targets.Length;
        int number_boxes = 0;
        for (int i = 0; i < dynamicUnitList.Count; i++)
        {
            if (dynamicUnitList[i].UnitType == EnumUnitType.BOX)
            {
                if (levelData.IsTarget(dynamicUnitList[i].Tile.x, dynamicUnitList[i].Tile.y) == true)
                {
                    number_boxes++;
                }
                if (number_boxes != number_target)
                {
                    if (i < dynamicUnitList.Count - 1)
                    {
                        continue;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        int moves_count = GameData.Instance.recordStack.Count;
        Debug.Log("moves_count == " + moves_count);
        int best_moves_count = GameData.Instance.saveGame.GetCompletedLevelInfor(this.levelData.levelID);
        UIManager.Instance.panelEndLevel.SetActive(true);
        UIManager.Instance.movesCount.text = moves_count.ToString();
        UIManager.Instance.levelText.text = this.levelData.levelID.ToString();
        GameManager.Instance.audioBGMusic.Stop();
        GameManager.Instance.audioFinishLevel.Play();
        if (best_moves_count != -1)
        {
            if (moves_count < best_moves_count)
            {
                UIManager.Instance.bestScore.text = moves_count.ToString();
            }
            else
            {
                UIManager.Instance.bestScore.text = best_moves_count.ToString();
            }
        }
        else
        {
            UIManager.Instance.bestScore.text = moves_count.ToString();
        }


        GameData.Instance.saveGame.SaveCompletedLevelInfor(this.levelData.levelID, moves_count);
        if (this.levelData.levelID < UIManager.Instance.listLevelItems.levelItems.Length)
        {
            UIManager.Instance.listLevelItems.levelItems[this.levelData.levelID].SaveLevelData(EnumLevelStatus.UNLOCK);
            Debug.Log("Level ID = " + this.levelData.levelID + " Unlocked!");
        }
        GameManager.Instance.ClearMap();
    }

    private void DepthSort()
    {
        var sortTool = new SortTool<UnitBase>();
        foreach (var unit in unitList)
        {
            sortTool.AddItem((int)(unit.GetSortingValue() * 100), unit);
        }
        var sortList = sortTool.Sort(true);
        for (int i = 0; i < sortList.Length; i++)
        {
            sortList[i].SetSortingVale(i);
        }
    }

    public UnitBase GetBox(float tileX, float tileY)
    {
        for (int i = 0; i < this.dynamicUnitList.Count; i++)
        {
            if (dynamicUnitList[i].UnitType == EnumUnitType.BOX)
            {
                var box = dynamicUnitList[i] as BoxController;
                if (box.Tile.x == tileX && box.Tile.y == tileY)
                {
                    return dynamicUnitList[i];
                }
                else if (box.IsMove)
                {
                    if (box.ToTilePos.x == tileX && box.ToTilePos.y == tileY)
                    {
                        return dynamicUnitList[i];
                    }
                }
            }
        }
        return null;
    }

    public void Rewind()
    {
        if (GameData.Instance.recordStack.Count > 0)
        {
            var stack = GameData.Instance.recordStack.Pop();
            this.playerController.SetTile(stack.playerTile.x, stack.playerTile.y);
            this.playerController.SetPlayerDirection(stack.enumDirection);
            if (stack.boxID != -1)
            {
                this.dynamicUnitList[stack.boxID].SetTile(stack.boxTile.x, stack.boxTile.y);
            }
        }
    }

    public void BackToMainMenu()
    {
        GameManager.Instance.BackToMainMenu();
    }
}
