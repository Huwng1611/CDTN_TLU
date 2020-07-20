using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xml2CSharp;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class TestAutoGenerateMap : MonoBehaviour
{
    [Header("XML Data")]
    public Level level;

    [Header("GameObject")]
    public PlayerType player;
    public BoxType[] boxes;
    public WallType[] walls;
    public TargetType[] targets;

    [Header("Content")]
    public Transform content;

    private void Start()
    {
        int halfSize = ConfigManager.Instance.mapConfig.MapSize >> 1;
        float offset = -halfSize * ConfigManager.Instance.mapConfig.TileSize;
        this.content.transform.localPosition = new Vector2(offset, offset);
    }

    public void AddUnit()
    {
        boxes = content.GetComponentsInChildren<BoxType>();
        player = content.GetComponentInChildren<PlayerType>();
        walls = content.GetComponentsInChildren<WallType>();
        targets = content.GetComponentsInChildren<TargetType>();
    }

    public void SaveDataToXMLObjectFirst()
    {
        level.Boxes.Unit = new List<Unit>();
        level.Walls.Unit = new List<Unit>();
        level.Targets.Unit = new List<Unit>();

        for (int i = 0; i < boxes.Length; i++)
        {
            var new_unit = new Unit();
            new_unit.X = (Mathf.CeilToInt(boxes[i].transform.localPosition.x / ConfigManager.Instance.mapConfig.TileSize)).ToString();
            new_unit.Y = (Mathf.CeilToInt(boxes[i].transform.localPosition.y / ConfigManager.Instance.mapConfig.TileSize)).ToString();
            level.Boxes.Unit.Add(new_unit);
        }

        for (int i = 0; i < walls.Length; i++)
        {
            var new_unit = new Unit();
            new_unit.X = (Mathf.CeilToInt(walls[i].transform.localPosition.x / ConfigManager.Instance.mapConfig.TileSize)).ToString();
            new_unit.Y = (Mathf.CeilToInt(walls[i].transform.localPosition.y / ConfigManager.Instance.mapConfig.TileSize)).ToString();
            level.Walls.Unit.Add(new_unit);
        }

        for (int i = 0; i < targets.Length; i++)
        {
            var new_unit = new Unit();
            new_unit.X = (Mathf.CeilToInt(targets[i].transform.localPosition.x / ConfigManager.Instance.mapConfig.TileSize)).ToString();
            new_unit.Y = (Mathf.CeilToInt(targets[i].transform.localPosition.y / ConfigManager.Instance.mapConfig.TileSize)).ToString();
            level.Targets.Unit.Add(new_unit);
        }

        level.Players.Unit.X = (Mathf.CeilToInt(player.transform.localPosition.x / ConfigManager.Instance.mapConfig.TileSize)).ToString();
        level.Players.Unit.Y = (Mathf.CeilToInt(player.transform.localPosition.y / ConfigManager.Instance.mapConfig.TileSize)).ToString();
    }

    public void SaveMap()
    {
        var serializer = new XmlSerializer(typeof(Level));
        var stream = new FileStream(Application.dataPath + level.Name + ".xml", FileMode.Create);
        serializer.Serialize(stream, level);
        stream.Close();
    }
}
