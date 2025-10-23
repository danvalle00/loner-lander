using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [Header("Level Progression")]
    [SerializeField]
    private string[] levelProgression = new string[]
    {
        "MoonScene",
        "MarsScene",
        "PlutoScene",
        "UranusScene"
    };
    public event System.Action OnWinStateTriggered;
    public event System.Action OnLoseStateTriggered;
    private bool isGameOver = false;
    private Player player;
    void Awake()// singleton initialization
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

    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isGameOver = false;
        Time.timeScale = 1f;
        if (player == null)
        {
            player = FindFirstObjectByType<Player>();
        }
    }

    public void Win()
    {
        if (isGameOver)
        {
            return;
        }
        isGameOver = true;
        ScoreManager.Instance.ScoreAddPoints();
        player.DisableInputs();
        Time.timeScale = 0f;
        OnWinStateTriggered?.Invoke();
    }

    public void Lose()
    {
        if (isGameOver)
        {
            return;
        }
        isGameOver = true;
        player.DisableInputs();
        Time.timeScale = 0f;
        OnLoseStateTriggered?.Invoke();
    }


    private string GetNextLevelName()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        for (int i = 0; i < levelProgression.Length; i++)
        {
            if (levelProgression[i] == currentScene)
            {
                if (i + 1 < levelProgression.Length)
                {
                    return levelProgression[i + 1];
                }
                else
                {
                    return "MainMenu";
                }
            }
        }
        return "MainMenu";
    }

    private void ResumeTime()
    {
        Time.timeScale = 1f;
    }

    // funcoes que os botoes chamam n UI
    public void RestartLevel()
    {
        ResumeTime();
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
    public void LoadMainMenu()
    {
        ResumeTime();
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadGameLevel(string sceneName)
    {
        ResumeTime();
        SceneManager.LoadScene(sceneName);
    }
    public void LoadNextLevel()
    {
        ResumeTime();
        string nextLevel = GetNextLevelName();
        SceneManager.LoadScene(nextLevel);
    }


}
