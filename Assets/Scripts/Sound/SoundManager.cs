using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hellmade.Sound;
using System;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] AudioClip[] allSFX;

    [SerializeField] SoundData[] soundMaps;

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
            //EazySoundManager.GlobalSoundsVolume = SaveData.GetVolumeSound();
        }
    }

    public void PlaySound(int sfxIndex)
    {
        EazySoundManager.PlaySound(allSFX[sfxIndex], 1f);
    }

    public void PlaySound(AudioClip audioClip, float volume = 1f)
    {
        EazySoundManager.PlaySound(audioClip, volume);
    }

    public void PlaySound(SoundType soundType, float volume = 1f)
    {
        AudioClip clip = GetAudioByType(soundType);
        if (clip != null)
        {
            EazySoundManager.PlaySound(clip, volume);
        }
        else
        {
            Debug.Log($"Audio clip with key {soundType} not found");
        }
    }

    private AudioClip GetAudioByType(SoundType soundType)
    {
        for (int i = 0; i < soundMaps.Length; i++)
        {
            if (soundMaps[i].SoundType == soundType)
            {
                return soundMaps[i].Clip;
            }    
        }
        return null;
    }
}

[Serializable]
public class SoundData
{
    [SerializeField] SoundType _soundType;
    public SoundType SoundType => _soundType;

    [SerializeField] AudioClip _clip;
    public AudioClip Clip => _clip;
}

[Serializable]
public enum SoundType
{
    Button_Click,
    Popup_Win,
    Level_Done,
}
