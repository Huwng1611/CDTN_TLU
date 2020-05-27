using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Button")]
    public Button startButton;

    [Header("Main Menu")]
    public GameObject mainMenu;

    [Header("Panel Level")]
    public GameObject panelSelectLevel;
    public GameObject panelEndLevel;
    public ListLevelItems listLevelItems;

    [Header("Panel Complete Level")]
    public Text levelText;
    public Text movesCount;
    public Text bestScore;

    private void Start()
    {
        this.StartButtonListener();
        this.InitLevel();
    }

    private void InitLevel()
    {
        this.listLevelItems.levelItems = new LevelItem[this.listLevelItems.transform.childCount];
        for (int i = 0; i < this.listLevelItems.levelItems.Length; i++)
        {
            this.listLevelItems.levelItems[i] = this.listLevelItems.transform.GetChild(i).GetComponent<LevelItem>();
        }
    }

    private void StartButtonListener()
    {
        this.startButton.onClick.AddListener(() =>
        {
            int level = GameData.Instance.saveGame.CompletedLevelCount + 1;
            if (level < 1) level = 1;
            GameManager.Instance.OpenLevel(level);
            GameManager.Instance.DisplayGame(GameManager.Instance.gamePlayPrefab);
        });
    }

    public void OpenNextLevel()
    {
        GameManager.Instance.OpenLevel(GameData.Instance.selectedLevel + 1);
        GameManager.Instance.DisplayGame(GameManager.Instance.gamePlayPrefab);
        this.DisablePanelLevel();
    }

    public void ReplayLevel()
    {
        GameManager.Instance.OpenLevel(GameData.Instance.selectedLevel);
        GameManager.Instance.DisplayGame(GameManager.Instance.gamePlayPrefab);
        this.DisablePanelLevel();
    }

    public void BackToMainMenu()
    {
        this.mainMenu.SetActive(true);
        this.DisablePanelLevel();
    }

    private void DisablePanelLevel()
    {
        this.panelEndLevel.SetActive(false);
        this.panelSelectLevel.SetActive(false);
    }
}