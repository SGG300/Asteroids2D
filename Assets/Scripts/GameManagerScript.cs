using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{

    public static GameManagerScript instance; //The instance of the class
    public int initLife = 3; //The initial life of the spaceship
    public int[] upgradeWeaponTreshold;
    public Text scoreText;//The UI text of the score
    public GameObject[] heartArray; //An array that collects the images of the hearts
    public GameObject gameOver;//The UI text of the end of the game
    public GameObject spaceShip;//Reference to the spaceShip
    public GameObject explosion;//prefab of explosion
    public AudioSource CrashAndBurn;//sound we make at the end of the game

    private int score; //The Actual Score
    private int maxScore = 9999;
    private int actualLife; //The actual life points
    private SpaceshipControlScript spaceShipScript;//script of the spaceship


    //we isntance the class
    void Awake()
    {
        instance = this;
    }

    //we initialize the variables
    void Start()
    {
        actualLife = initLife;
        score = 0;
        spaceShipScript = spaceShip.GetComponent<SpaceshipControlScript>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && Time.timeScale != 0.0f)
        {
            Time.timeScale = 0.0f;
            SceneManager.LoadScene("SettingsScene", LoadSceneMode.Additive);
        }
    }

    //we add to the score points
    public void AddScore(int scorePlus)
    {
        score += scorePlus;
        score = Mathf.Min(score, maxScore);
        if (instance)
        {
            instance.CheckScore();
            instance.CheckShotMode();
        }

    }

    //if the score is bigger than 5, we change to mode 2 of shot
    //if the score is bigger than 10, we change to mode 3 of shot
    public void CheckShotMode()
    {
        if (score > upgradeWeaponTreshold[0])
            spaceShipScript.modeShot = 2;
        if (score > upgradeWeaponTreshold[1])
            spaceShipScript.modeShot = 3;
    }

    //we check the score and modify the text
    public void CheckScore()
    {
        scoreText.text = "Score: " + score.ToString("D4");
    }

    //We remove life points
    public void MinusLife(int damage)
    {
        actualLife -= damage;
        CheckDamage();
    }

    //We check the damage, and remove hearts and desroy the spaceship, the later only it lifepoints are at 0
    public void CheckDamage()
    {

        for (int i = 0; i < heartArray.Length; i++)
        {
            if (actualLife <= i)
            {
                heartArray[i].SetActive(false);
            }
            else
            {
                heartArray[i].SetActive(true);
            }

        }

        if (actualLife <= 0)
        {
            heartArray[0].SetActive(false);
            gameOver.SetActive(true);
            Destroy(spaceShip);
            CrashAndBurn.Play();
            Instantiate(explosion, spaceShip.transform.position, spaceShip.transform.rotation);
            if (HighScoreManager.instance.hScore < score)
            {
                HighScoreManager.instance.hScore = score;
                HighScoreManager.instance.Save();
            }
            Invoke("EndGame", 5.0f);
        }
    }

    //When we end the game, we load the menu
    void EndGame()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
