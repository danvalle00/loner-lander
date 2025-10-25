using TMPro;
using UnityEngine;

public class DifficultySceneUI : MonoBehaviour
{
    public FuelScriptableObject fuelData;
    public ScoreScriptableObjects scoreData;

    [SerializeField] private TextMeshProUGUI fuelText;
    [SerializeField] private TextMeshProUGUI scoreText;

    void Update()
    {
        fuelText.text = $"Initial Fuel: {fuelData.maxFuel}";
        scoreText.text = $"Initial Score: {scoreData.initialScore}";
    }

    public void OnStartGameButtonClicked()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartGame();
        }

    }
}
