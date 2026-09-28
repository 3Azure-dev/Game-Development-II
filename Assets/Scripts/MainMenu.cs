using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    public GameObject mainPanel;
    public GameObject optionsPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (highScoreText != null && ScoreManager.Instance != null)
        highScoreText.text = ScoreManager.Instance.HighScore.ToString();

        if (optionsPanel != null) optionsPanel.SetActive(false);
        if (mainPanel != null) mainPanel.SetActive(true);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenOptions()
    {
        mainPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
    public void OnResetHighScoreClicked()
    {
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.ResetHighScore();

        if (highScoreText != null)
            highScoreText.text = "0";
    }
}
