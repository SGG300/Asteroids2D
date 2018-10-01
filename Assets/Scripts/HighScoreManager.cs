using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using System.IO;

public class HighScoreManager : MonoBehaviour
{

    public static HighScoreManager instance; //instance of HighScoreManager

    [HideInInspector]
    public int hScore; //The High Score
    private readonly string playerPrefKeyString = "HighScore";

    //We instance the class
    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    //We always load and update at the start
    void Start()
    {
        Load();
    }
    //We save the data of ScoreData(AKA: The High Score)
    public void Save()
    {
        PlayerPrefs.SetInt(playerPrefKeyString, hScore);
    }

    //We load the data of ScoreData(AKA: The High Score)
    public void Load()
    {
        hScore = PlayerPrefs.GetInt(playerPrefKeyString, 0);
    }

    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey(playerPrefKeyString);
        Load();
    }


}


