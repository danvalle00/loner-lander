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
    void Awake()
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
        Time.timeScale = 1f;
        if (player == null)
        {
            player = FindFirstObjectByType<Player>();
        }
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
        panel.SetActive(true);
        Time.timeScale = 0f;
    }
    private void HidePanels()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    private void ResumeTime()
    {
        Time.timeScale = 1f;
    }
    public void RestartLevel()
    {
        ResumeTime();
        Scene active = SceneManager.GetActiveScene();
        SceneManager.LoadScene(active.name);
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
