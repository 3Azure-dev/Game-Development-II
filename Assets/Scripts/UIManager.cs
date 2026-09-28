using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
// using CoinSystem;

public class UIManager : MonoBehaviour
{
    public GameObject Playbutton;
    public TextMeshProUGUI scoreText;

    private CoinManagerPool pool;

    [Header("Game Over")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverScoreText;

    // Called when the Start/Play button is clicked
        void Start()
    {

        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        // Freeze the entire game at the very beginning
        Time.timeScale = 0f;

        pool = FindAnyObjectByType<CoinManagerPool>();
    }

    void Update()
    {
        if (pool != null && scoreText != null)
        {
            scoreText.text = "Score: " + ScoreManager.Instance.Count;
        }
    }

    public void OnStartButtonClicked()
    {
        StartCoroutine(CountdownRoutine());
        Playbutton.SetActive(false);
    }

    IEnumerator CountdownRoutine()
    {
        // Countdown from 3 to 1
        for (int i = 3; i > 0; i--)
        {
            Debug.Log(i);
            yield return new WaitForSecondsRealtime(1f);
        }

        Debug.Log("Go!");
        
        Time.timeScale = 1f;
        // Game starts here
    }

    void OnEnable()
    {
        Player.OnPlayerDied += ShowGameOver;
    }

    void OnDisable()
    {
        Player.OnPlayerDied -= ShowGameOver;
    }

    void ShowGameOver()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (gameOverScoreText != null)
            gameOverScoreText.text = "Game Over!\nYour Current Score was: " + ScoreManager.Instance.Count;

        Time.timeScale = 0f;
    }

    public void OnReplayClicked()
    {
        Time.timeScale = 1f;

        if (GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject);
            GameManager.Instance = null;
        }

        if (ScoreManager.Instance != null)
        {
            Destroy(ScoreManager.Instance.gameObject);
            ScoreManager.Instance = null;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnQuitToMenuClicked()
    {
        Time.timeScale = 1f;
        ScoreManager.Instance.ResetScore();
        SceneManager.LoadScene("MainMenu");
    }
}