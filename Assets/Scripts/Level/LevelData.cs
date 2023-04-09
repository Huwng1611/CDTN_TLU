using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public enum EnumLevelStatus
{
    LOCK,
    UNLOCK
}

[System.Serializable]
public class LevelData
{
    public int levelID;
    public EnumLevelStatus enumLevelStatus;
    public UnitData[] walls;
    public UnitData[] players;
    public UnitData[] targets;
    public UnitData[] boxes;

    private XmlDocument xmlDocument;
    private Dictionary<string, UnitData> unitDataDictionary = new Dictionary<string, UnitData>();

    public LevelData(int level_ID)
    {
        //fortesting
        this.enumLevelStatus = EnumLevelStatus.UNLOCK;

        this.levelID = level_ID;
        var data = Resources.Load<TextAsset>("LevelSystem/level" + level_ID);
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(data.text);
        this.xmlDocument = xmlDocument;

        walls = GetUnitDatas("walls", EnumUnitType.WALL);
        players = GetUnitDatas("players", EnumUnitType.PLAYER);
        targets = GetUnitDatas("targets", EnumUnitType.TARGET);
        boxes = GetUnitDatas("boxes", EnumUnitType.BOX);
    }

    private UnitData[] GetUnitDatas(string type, EnumUnitType enumUnitType)
    {
        XmlNode level_node = this.xmlDocument.SelectSingleNode("level");
        XmlNodeList unit_nodes = level_node.SelectSingleNode(type).SelectNodes("unit");
        UnitData[] unit_datas = new UnitData[unit_nodes.Count];
        for (int i = 0; i < unit_datas.Length; i++)
        {
            XmlElement element = unit_nodes[i] as XmlElement;
            var x = element.GetAttribute("x");
            var y = element.GetAttribute("y");

            UnitData unit_data = new UnitData();
            {
                unit_data.x = float.Parse(x);
                unit_data.y = float.Parse(y);
                unit_data.unitType = enumUnitType;
                unit_datas[i] = unit_data;
                this.unitDataDictionary[GetPositionFlag(unit_data.x, unit_data.y, unit_data.unitType)] = unit_data;
            }
        }
        return unit_datas;
    }

    private UnitData GetUnitData(float x, float y, EnumUnitType enumUnitType)
    {
        string flag = GetPositionFlag(x, y, enumUnitType);
        if (this.unitDataDictionary.ContainsKey(flag))
        {
            return this.unitDataDictionary[flag];
        }
        else
        {
            return null;
        }
    }

    public string GetPositionFlag(float x, float y, EnumUnitType enumUnitType)
    {
        return string.Format("{0}_{1}_{2}", x, y, enumUnitType);
    }

    public bool IsWall(float x, float y)
    {
        return this.CheckType(x, y, EnumUnitType.WALL);
    }

    public bool IsTarget(float x, float y)
    {
        return this.CheckType(x, y, EnumUnitType.TARGET);
    }

    public bool IsBox(float x, float y)
    {
        return this.CheckType(x, y, EnumUnitType.BOX);
    }

    public bool CheckType(float x, float y, EnumUnitType enumUnitType)
    {
        var unit_data = this.GetUnitData(x, y, enumUnitType);
        if (unit_data == null || unit_data.unitType != enumUnitType)
        {
            return false;
        }
        return true;
    }
}

public class UnitData
{
    public EnumUnitType unitType;
    public float x;
    public float y;
}

public class RecordUnitData
{
    public Vector2 playerTile;
    public Vector2 boxTile;
    public EnumDirection enumDirection;
    public int boxID;

    public RecordUnitData(Vector2 player_tile, Vector2 box_tile, EnumDirection dir, int box_ID)
    {
        this.playerTile = player_tile;
        this.boxTile = box_tile;
        this.enumDirection = dir;
        this.boxID = box_ID;
    }
}

[System.Serializable]
public class CompleteLevelInfor
{
    public int levelID;
    public int movesCount;

    public CompleteLevelInfor(int levelID, int movesCount)
    {
        this.levelID = levelID;
        this.movesCount = movesCount;
    }
}

[System.Serializable]
public class SavedGameData
{
    public int lastLevel = -1;
    public int unlockLevel = -1;
    public List<CompleteLevelInfor> completeLevelInfors = new List<CompleteLevelInfor>();
}