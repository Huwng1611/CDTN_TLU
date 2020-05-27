using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelItem : MonoBehaviour
{
    private const string KEY_DATA = "Level ";

    [Header("Data")]
    public ItemLevelData itemLevelData;

    [Header("Level Item")]
    public Image lockIcon;
    public Text levelText;

    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => OnClickThisLevel());
        var level_id = this.transform.GetSiblingIndex() + 1;
        this.levelText.text = level_id.ToString();
        this.itemLevelData.levelID = level_id;
        this.GetLevelData();
    }

    public void GetLevelData()
    {
        var index = this.transform.GetSiblingIndex() + 1;
        if (index == 1)
        {
            this.itemLevelData.levelStatus = EnumLevelStatus.UNLOCK;
            this.UpdateUI();
            return;
        }
        if (PlayerPrefs.HasKey(KEY_DATA + index))
        {
            this.itemLevelData.levelStatus = (EnumLevelStatus)PlayerPrefs.GetInt(KEY_DATA + index);
            this.UpdateUI();
        }
        else
        {
            this.itemLevelData.levelStatus = EnumLevelStatus.LOCK;
            this.UpdateUI();
        }
    }

    public void SaveLevelData(EnumLevelStatus enumLevelStatus)
    {
        this.itemLevelData.levelStatus = enumLevelStatus;
        PlayerPrefs.SetInt(KEY_DATA + (this.transform.GetSiblingIndex() + 1), (int)enumLevelStatus);
        UpdateUI();
    }

    public void OnClickThisLevel()
    {
        if (itemLevelData.levelStatus == EnumLevelStatus.LOCK)
        {
            return;
        }
        else
        {
            GameManager.Instance.OpenLevel(this.transform.GetSiblingIndex() + 1);
            GameManager.Instance.DisplayGame(GameManager.Instance.gamePlayPrefab);
            UIManager.Instance.panelSelectLevel.SetActive(false);
            UIManager.Instance.mainMenu.SetActive(false);
        }
    }

    public void UpdateUI()
    {
        if (this.itemLevelData.levelStatus == EnumLevelStatus.UNLOCK)
        {
            this.lockIcon.enabled = false;
            this.levelText.enabled = true;
            this.GetComponent<Button>().enabled = true;
        }
        else
        {
            this.lockIcon.enabled = true;
            this.levelText.enabled = false;
            this.GetComponent<Button>().enabled = false;
        }
    }
}

[Serializable]
public class ItemLevelData
{
    public int levelID;
    public EnumLevelStatus levelStatus;
}