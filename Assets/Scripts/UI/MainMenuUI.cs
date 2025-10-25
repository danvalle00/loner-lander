using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartNewRun();
        }
    }
    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
