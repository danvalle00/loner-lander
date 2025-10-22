using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Level Progression")]
    [SerializeField]
    private string[] levelProgression = new string[]
    {
        "MoonScene",
        "MarsScene",
        "PlutoScene",
        "UranusScene"
    };
    [Header("Panel References")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    void Start()
    {
        SetupWinPanelButtons();
        SetupLosePanelButtons();
    }
    private void SetupWinPanelButtons()
    {
        if (winPanel == null)
        {
            return;
        }
        Button nextLevelButton = FindButtons(winPanel.transform, "NextLevel");
        Button mainMenuButton = FindButtons(winPanel.transform, "MainMenu");
        if (nextLevelButton != null)
        {
            nextLevelButton.onClick.RemoveAllListeners();
            nextLevelButton.onClick.AddListener(OnNextLevelButtonClicked);
        }
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.RemoveAllListeners();
            mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        }
    }
    private void SetupLosePanelButtons()
    {
        if (losePanel == null)
        {
            return;
        }
        Button retryButton = FindButtons(losePanel.transform, "Retry");
        Button mainMenuButton = FindButtons(losePanel.transform, "MainMenu");
        if (retryButton != null)
        {
            retryButton.onClick.RemoveAllListeners();
            retryButton.onClick.AddListener(OnRetryButtonClicked);
        }
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.RemoveAllListeners();
            mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        }
    }
    private Button FindButtons(Transform parent, string childName)
    {
        Transform childTransform = parent.Find(childName); // procura no transform do pai (panel) um transform com o nome do botao
        if (childTransform == null)
        {
            return null;
        }
        Button button = childTransform.GetComponent<Button>();
        return button;
    }

    private void OnNextLevelButtonClicked()
    {
        string nextLevel = GetNextLevelName();
        GameManager.Instance.LoadGameLevel(nextLevel);
    }
    private void OnRetryButtonClicked()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        GameManager.Instance.RestartLevel(currentScene);
    }

    private void OnMainMenuButtonClicked()
    {
        GameManager.Instance.LoadMainMenu();
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
}
