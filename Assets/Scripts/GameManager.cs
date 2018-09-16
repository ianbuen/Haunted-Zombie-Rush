using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour {

    // Singleton
    public static GameManager instance = null;

    [SerializeField] private GameObject uiMainMenu = null;
    [SerializeField] private GameObject uiGameOver = null;

    private bool playerActive = false;
    private bool gameOver = false;
    private bool gameStarted = false;

    [SerializeField] private Text scoreText = null;
    [SerializeField] private Text summaryText = null;
    private int score;

    public bool PlayerActive {
        get { return playerActive; }
    }

    public bool GameOver {
        get { return gameOver; }
    }

    public bool GameStarted {
        get { return gameStarted; }
    }

    void Awake() {
        //Set screen size for Standalone
        #if UNITY_STANDALONE
            Screen.SetResolution(564, 960, false);
            Screen.fullScreen = false;
        #endif

        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        Assert.IsNotNull(uiMainMenu);
        Assert.IsNotNull(uiGameOver);

        Assert.IsNotNull(scoreText);
        Assert.IsNotNull(summaryText);
    }

    // Use this for initialization
    void Start () {
        score = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (gameStarted)
            scoreText.gameObject.SetActive(true);
        else
            scoreText.gameObject.SetActive(false);
    }

    public void PlayerCollided() {
        gameOver = true;
        StartCoroutine(EndGame());
    }

    public void PlayerStartedGame() {
        playerActive = true;
    }

    public void EnterGame() {
        uiMainMenu.SetActive(false);
        gameStarted = true;
    }

    public void BackToMainMenu() {
        ResetGame();
        uiMainMenu.SetActive(true);
    }

    public void Replay() {
        ResetGame();
        gameStarted = true;
    }

    private void ResetGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameOver = false;
        playerActive = false;
        score = 0;
        scoreText.text = "Brains:  0";
        uiMainMenu.SetActive(false);
        uiGameOver.SetActive(false);
    }

    IEnumerator EndGame() {
        gameStarted = false;
        yield return new WaitForSeconds(2f);
        FinalizeScore();
        uiGameOver.SetActive(true);
    }

    public void AddScore() {
        score++;
        scoreText.text = "Brains:  " + score;
    }

    private void FinalizeScore() {
        summaryText.text = "You  ate   " + score;
        summaryText.text += score != 1 ? "   Brains!" : "   Brain!";
    }
}
