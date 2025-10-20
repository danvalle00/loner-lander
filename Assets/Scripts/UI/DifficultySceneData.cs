using TMPro;
using UnityEngine;

public class DifficultySceneData : MonoBehaviour
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
}
