using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Transform gameplayTransform;
    public GameObject gamePlayPrefab;

    private void Awake()
    {
        this.Init();
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    private void Init()
    {
        GameData.Instance.saveGame = new SaveGame();
    }

    public void OpenLevel(int level)
    {
        if (level > ConfigManager.Instance.mapConfig.LevelAmount)
        {
            level = 1;
        }
        GameData.Instance.selectedLevel = level;
        //this.DisplayGame(gamePlayPrefab);
        UIManager.Instance.panelSelectLevel.SetActive(false);
        UIManager.Instance.panelEndLevel.SetActive(false);
        MusicManager.Instance.PlayMusic(0);
    }

    public void BackToMainMenu()
    {
        UIManager.Instance.mainMenu.SetActive(true);
        this.ClearMap();
        MusicManager.Instance.StopMusic();
    }

    public void DisplayGame(GameObject prefabObject, bool isLoading = true)
    {
        if (isLoading)
        {
            this.ClearMap();
            var go = Instantiate(prefabObject, this.gameplayTransform);
            go.name = prefabObject.name;
        }
        else
        {
            var go = Instantiate(prefabObject, this.gameplayTransform);
            go.name = prefabObject.name;
        }
    }

    public void ClearMap()
    {
        for (int i = 0; i < this.gameplayTransform.childCount; i++)
        {
            Destroy(gameplayTransform.GetChild(i).gameObject);
        }
    }

    [ContextMenu("Reset All Data")]
    /// <summary>
    /// only for testing
    /// </summary>
    public void ResetAllData()
    {
        PlayerPrefs.DeleteAll();
        GameData.Instance.saveGame.ResetAllData();
    }
}
