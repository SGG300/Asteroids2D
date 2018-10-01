using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour {

    public Image SFXButton;
    public Image MusicButton;
    public Sprite[] SFXStatus;
    public Sprite[] MusicStatus;

    public void OnEnable()
    {
        CheckStatusSFX();
        CheckStatusVolume();

    }

    public void CheckStatusSFX()
    {
        if(AudioManager.instance.SFX)
        {
            SFXButton.sprite = SFXStatus[0];
        }
        else
        {
            SFXButton.sprite = SFXStatus[1];
        }
    }

    public void CheckStatusVolume()
    {
        if (AudioManager.instance.Music)
        {
            MusicButton.sprite = MusicStatus[0];
        }
        else
        {
            MusicButton.sprite = MusicStatus[1];
        }
    }

    public void SwitchSFX()
    {
        AudioManager.instance.SwitchSFX();
        CheckStatusSFX();
    }

    public void SwitchMusic()
    {
        AudioManager.instance.SwitchMusic();
        CheckStatusVolume();
    }

    public void CloseSettingsScene()
    {
        Time.timeScale = 1.0f;
        SceneManager.UnloadSceneAsync("SettingsScene");
    }
}
