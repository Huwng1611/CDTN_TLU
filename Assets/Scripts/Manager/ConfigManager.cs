using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : Singleton<ConfigManager>
{
    public MapConfig mapConfig;

    private void Awake()
    {
        this.LoadAllConfig();
    }

    private void LoadAllConfig()
    {
        this.LoadMapConfig();
    }

    /// <summary>
    /// Map config
    /// </summary>
    private void LoadMapConfig()
    {
        var jsonContent = Resources.Load<TextAsset>("GameConfig/MapConfig");
        this.mapConfig = JsonUtility.FromJson<MapConfig>(jsonContent.text);
    }
}

public class MapConfig
{
    public float TileSize;
    public int MapSize;
    public int LevelAmount;
}
