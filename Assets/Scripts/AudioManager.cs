using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;
    public AudioMixer audioMixer;

    private bool music;
    public bool Music
    {
        get
        {
            return music;
        }

        set
        {
            if (value == true)
            {
                audioMixer.SetFloat("VolumeMusic", 0.0f);
            }
            else
            {
                audioMixer.SetFloat("VolumeMusic", -80.0f);
            }
            music = value;
        }
    }

    private bool sFX;
    public bool SFX
    {
        get
        {
            return sFX;
        }

        set
        {
            if (value == true)
            {
                audioMixer.SetFloat("VolumeSFX", 0.0f);
            }
            else
            {
                audioMixer.SetFloat("VolumeSFX", -80.0f);
            }
            sFX = value;
        }
    }

    public void Awake()
    {
        Music = true;
        SFX = true;

        if(instance == null)
        {
            instance = this;
        }
        else if(instance !=this)
        {
            Destroy(this);
        }
    }

    public void SwitchMusic()
    {
        Music = !Music;
    }

    public void SwitchSFX()
    {
        SFX = !SFX;
    }
}
