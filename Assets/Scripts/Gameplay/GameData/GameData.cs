using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public static GameData Instance = new GameData();
    private GameData() { }

    public SaveGame saveGame;
    public Camera mainCamera;
    public Vector2 cameraSize;
    public int selectedLevel = 1;
    public Stack<RecordUnitData> recordStack = new Stack<RecordUnitData>();
    public bool isAllLevel = false;
}
