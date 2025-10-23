using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Panel References")]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    void Start()
    {
        if (GameManager.Instance == null)
        {
            return;
        }

        HidePanels();

        // Subscribe to game state events
        GameManager.Instance.OnWinStateTriggered += ShowWinPanel;
        GameManager.Instance.OnLoseStateTriggered += ShowLosePanel;

        // Set up button listeners
        SetupWinPanelButtons();
        SetupLosePanelButtons();
    }

    void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnWinStateTriggered -= ShowWinPanel;
            GameManager.Instance.OnLoseStateTriggered -= ShowLosePanel;
        }
    }

    private void ShowWinPanel()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
        }
    }

    private void ShowLosePanel()
    {
        if (losePanel != null)
        {
            losePanel.SetActive(true);
        }
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

        Button tryAgainButton = FindButtons(losePanel.transform, "TryAgain");
        Button mainMenuButton = FindButtons(losePanel.transform, "MainMenu");

        if (tryAgainButton != null)
        {
            tryAgainButton.onClick.RemoveAllListeners();
            tryAgainButton.onClick.AddListener(OnTryAgainButtonClicked);
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.RemoveAllListeners();
            mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        }
    }

    private Button FindButtons(Transform parent, string childName)
    {
        Transform childTransform = parent.Find(childName);

        if (childTransform == null)
        {
            return null;
        }

        Button button = childTransform.GetComponent<Button>();

        return button;
    }

    private void OnNextLevelButtonClicked()
    {
        GameManager.Instance.LoadNextLevel();
    }

    private void OnTryAgainButtonClicked()
    {
        GameManager.Instance.RestartLevel();
    }

    private void OnMainMenuButtonClicked()
    {
        GameManager.Instance.LoadMainMenu();
    }

    private void HidePanels()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }
        if (losePanel != null)
        {
            losePanel.SetActive(false);
        }
    }
}