using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [Header("UI Panels References")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
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
        FindReferences();
        HidePanels();
    }

    public void Win()
    {
        if (isGameOver) return;
        isGameOver = true;
        ScoreManager.Instance.ScoreAddPoints();
        player.DisableInputs();
        FreezeAndShow(winPanel);

    }

    public void Lose()
    {
        if (isGameOver) return;
        isGameOver = true;
        player.DisableInputs();
        FreezeAndShow(losePanel);
    }
    private void FreezeAndShow(GameObject panel)
    {
        if (panel != null)
        {
            panel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    private void HidePanels()
    {
        if (winPanel) winPanel.SetActive(false);
        if (losePanel) losePanel.SetActive(false);
    }

    private void FindReferences()
    {
        if (winPanel == null) // if not assigned in inspector assign it
        {
            winPanel = GameObject.Find("WinPanel");
        }
        if (losePanel == null)
        {
            losePanel = GameObject.Find("LosePanel");
        }
        if (player == null)
        {
            player = FindFirstObjectByType<Player>();
        }
    }

    private void ResumeTime()
    {
        Time.timeScale = 1f;
    }

    // funcoes que os botoes chamam n UI
    public void RestartLevel(string sceneName)
    {
        ResumeTime();
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

}
