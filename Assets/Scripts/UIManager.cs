using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
// using CoinSystem;

public class UIManager : MonoBehaviour
{
    public GameObject Playbutton;
    public TextMeshProUGUI scoreText;

    private CoinManagerPool pool;

    // Called when the Start/Play button is clicked
        void Start()
    {
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
}