using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hellmade.Sound;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    [SerializeField] AudioClip[] allMusic;

    #region all music region
    public static int MENU_MUSIC_INDEX = 0;
    public static int COMBAT_MUSIC_INDEX = 1;
    public static int MENU_MUSIC_INDEX_RUBBLERS = 2;
    public static int COMBAT_MUSIC_INDEX_RUBBLERS = 3;
    public static int MENU_MUSIC_INDEX_LEAFIES = 4;
    public static int COMBAT_MUSIC_INDEX_LEAFIES = 5;
    #endregion

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //EazySoundManager.GlobalMusicVolume = SaveData.GetVolumeMusic()*0.4f;
        }
    }

    private void Start()
    {
        PlayMusic(0);
    }

    int currentMusicID;
    public int currentTrackIndex;
    public void PlayMusic(int musicIndex)
    {
        currentTrackIndex = musicIndex;
        currentMusicID = EazySoundManager.PlayMusic(allMusic[musicIndex], 0.5f, true, true, 0.5f, 0.5f, 1.5f, null);
    }

    public void PauseMusic()
    {
        EazySoundManager.GetAudio(currentMusicID).Pause();
    }

    public void ResumeMusic()
    {
        EazySoundManager.GetAudio(currentMusicID).Resume();
    }

    public void StopMusic()
    {
        EazySoundManager.GetAudio(currentMusicID).Stop();
    }
}
