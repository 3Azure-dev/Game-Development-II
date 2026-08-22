using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Audio")]
    public AudioSource Clip;

    [Header("Scene Names")]
    public string gameSceneName = "Game";
    public string mainMenuSceneName = "MainMenu";

    [Header("Main Menu UI")]
    public TextMeshProUGUI mainMenuCoinText;  // Drag your "Coins Saved" TMPro here in MainMenu scene

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        Clip = GetComponent<AudioSource>();

        // When a scene loads, update the main menu coin text (if we're in main menu)
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        Clip.Play();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // If we just loaded the MainMenu, update the coin text
        if (scene.name == mainMenuSceneName)
        {
            UpdateMainMenuCoinText();
        }
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    private void UpdateMainMenuCoinText()
    {
        if (mainMenuCoinText == null)
        {
            // Try to find it in the scene
            GameObject coinTextObj = GameObject.Find("CoinSavedText");
            if (coinTextObj != null)
                mainMenuCoinText = coinTextObj.GetComponent<TextMeshProUGUI>();
        }

        if (mainMenuCoinText != null && ScoreManager.Instance != null)
        {
            mainMenuCoinText.text = "Coins Saved: " + ScoreManager.Instance.Count;
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
