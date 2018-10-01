using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

    public Text highScore;

    public void Start()
    {
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        highScore.text = "HighScore: " + HighScoreManager.instance.hScore.ToString("D4");
    }

    //It Loads to the Gameplay Scene
    public void LoadGameScene(){
		SceneManager.LoadScene ("GameScene");
	}

    public void ResetScore()
    {
        HighScoreManager.instance.ResetHighScore();
        UpdateScoreText();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsScene", LoadSceneMode.Additive);
    }
}
