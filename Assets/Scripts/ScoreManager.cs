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
        return count;
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }
}