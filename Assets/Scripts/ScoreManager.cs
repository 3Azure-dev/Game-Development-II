using UnityEngine;
using UnityEngine.SceneManagement;

//creating scoremanager singelton class for passing score across scenes
public class ScoreManager : MonoBehaviour
{
    [HideInInspector]
    public static ScoreManager Instance;
    private int count;
    private int highScore;
    public int Count => count;
    public int HighScore => highScore;

    private int enemyCount;
    public int EnemyCount => enemyCount;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            highScore = PlayerPrefs.GetInt("HighScore", 0);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int CoinCount()
    {
        count++;
        if (count > highScore)
        {
            highScore = count;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        if (count % 5 == 0)
            {
                Player p = FindAnyObjectByType<Player>();
                if (p != null)
                    p.IncreasestartHealth(20);
            }   
        return count;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    public void EnemySpawned()
    {
        enemyCount++;
    }
    public void EnemyDefeated()
    {
        enemyCount--;
    }

    public void ResetScore()
    {
        count = 0;
        enemyCount = 0;
    }

    public void ResetHighScore()
    {
        highScore = 0;
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.Save();
    }
}