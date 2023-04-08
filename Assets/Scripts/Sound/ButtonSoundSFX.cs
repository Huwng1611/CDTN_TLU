using Hellmade.Sound;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSoundSFX : MonoBehaviour
{
    private void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(PlayButtonSFX);
        }
    }


    public void PlayButtonSFX()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySound(SoundType.Button_Click);
        }
    }
}
