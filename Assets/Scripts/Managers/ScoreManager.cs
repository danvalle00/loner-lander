using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [SerializeField] private ScoreScriptableObjects scoreData;

    [Header("Runtime Score State")]
    [SerializeField] private float currentScorePoints;
    [SerializeField] private int currentScoreMultiplier;
    private bool hasScored = false;

    [Header("Score Settings")]
    [SerializeField] private float baseScorePoints = 1000f;
    [SerializeField] private float scoreDecreaseRate = 3f;
    [SerializeField] private float fuelToScoreRate = 0.5f;

    public System.Action<float> OnScoreChanged;
    public System.Action<float, float> OnFinalScoreCalculated;

    void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        if (scoreData == null)
        {
            return;
        }
        if (FuelManager.Instance != null)
        {
            FuelManager.Instance.OnFuelConfigurationChanged += OnFuelConfigurationChanged;
        }
    }
    void Update()
    {

        if (!hasScored)
        {
            DecreaseScore(Time.deltaTime * scoreDecreaseRate);
        }
    }
    private void OnFuelConfigurationChanged(float fuelAmount)
    {

        float calculatedScore = CalculateInitialScore(fuelAmount);
        currentScorePoints = calculatedScore;
        scoreData.initialScore = (int)calculatedScore;
        OnScoreChanged?.Invoke(currentScorePoints);
    }

    private float CalculateInitialScore(float fuelAmount)
    {
        return baseScorePoints + (fuelAmount * fuelToScoreRate);
    }

    private void DecreaseScore(float amount)
    {
        float previousScore = currentScorePoints;
        currentScorePoints -= amount;
        currentScorePoints = Mathf.Max(0, currentScorePoints);
        if (previousScore != currentScorePoints)
        {
            OnScoreChanged?.Invoke(currentScorePoints);
        }
    }
    public void SetScoreMultiplier(int multiplier) => currentScoreMultiplier = multiplier;

    public int GetScoreMultiplier() => currentScoreMultiplier;

    public float GetCurrentScore() => currentScorePoints;

    public void ScoreAddPoints()
    {
        if (hasScored)
        {
            return;
        }
        hasScored = true;
        float remainingFuelBonus = FuelManager.Instance != null ? FuelManager.Instance.GetRemainingFuel() : 0f;
        float finalScore = (currentScorePoints + remainingFuelBonus) * currentScoreMultiplier;
        currentScorePoints = finalScore;
        if (finalScore > scoreData.highScore)
        {
            scoreData.highScore = finalScore;
        }
        OnFinalScoreCalculated?.Invoke(finalScore, scoreData.highScore);
        OnScoreChanged?.Invoke(currentScorePoints);
    }
    public void ResetForNextLevel()
    {
        hasScored = false;
        currentScoreMultiplier = 1;
        OnScoreChanged?.Invoke(currentScorePoints);
    }
    public void ResetForNewGame()
    {
        hasScored = false;
        currentScoreMultiplier = 1;
    }
}
