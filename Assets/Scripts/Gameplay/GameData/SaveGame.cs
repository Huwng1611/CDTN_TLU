using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveGame
{
    private SavedGameData savedGameData;
    private string fileData;

    public int LastLevel
    {
        get
        {
            return this.savedGameData.lastLevel;
        }
        set
        {
            this.savedGameData.lastLevel = value;
            this.Save();
        }
    }

    public int UnlockLevel
    {
        get
        {
            return this.savedGameData.unlockLevel;
        }
        set
        {
            this.savedGameData.unlockLevel = value;
            this.Save();
        }
    }

    public int CompletedLevelCount
    {
        get
        {
            return this.savedGameData.completeLevelInfors.Count;
        }
    }

    public SaveGame()
    {
        this.fileData = GetSavedFile();
        if (File.Exists(fileData))
        {
            string content = File.ReadAllText(fileData);
            savedGameData = JsonUtility.FromJson<SavedGameData>(content);
        }
        else
        {
            savedGameData = new SavedGameData();
            this.Save();
        }
    }

    public void SaveCompletedLevelInfor(int level, int movesCount)
    {
        CompleteLevelInfor level_info = null;
        foreach (var lv in savedGameData.completeLevelInfors)
        {
            if (lv.levelID == level)
            {
                level_info = lv;
                break;
            }
        }

        if (level_info != null)
        {
            level_info.movesCount = movesCount;
        }
        else
        {
            var completed_level = new CompleteLevelInfor(level, movesCount);
            this.savedGameData.completeLevelInfors.Add(completed_level);
        }

        this.Save();

        //var completed_level = new CompleteLevelInfor(level, movesCount);
        //this.savedGameData.completeLevelInfors.Add(completed_level);
    }

    public int GetCompletedLevelInfor(int levelID)
    {
        CompleteLevelInfor level_info = null;
        foreach (var lv in savedGameData.completeLevelInfors)
        {
            if (lv.levelID == levelID)
            {
                level_info = lv;
                break;
            }
        }

        if (level_info != null)
        {
            return level_info.movesCount;
        }
        else
        {
            return -1;
        }
    }

    private string GetSavedFile()
    {
        return Application.persistentDataPath + "/save.txt";
    }

    private void Save()
    {
        string content = JsonUtility.ToJson(savedGameData);
        File.WriteAllText(fileData, content);
    }

    /// <summary>
    /// only for testing
    /// </summary>
    public void ResetAllData()
    {
        if (File.Exists(fileData))
        {
            File.Delete(fileData);
        }
        this.savedGameData = new SavedGameData();
    }
}
