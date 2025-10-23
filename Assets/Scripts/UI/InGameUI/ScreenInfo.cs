using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


public class ScreenInfo : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI planetInfoText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI shipInfoText;
    [SerializeField] private Player player;

    private float currentScore;
    private float currentMultiplier;
    private float currentFuel;
    private float maxFuel;


    void OnEnable()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged += HandleScoreChanged;
            currentScore = ScoreManager.Instance.GetCurrentScore();
            currentMultiplier = ScoreManager.Instance.GetScoreMultiplier();
            UpdateScoreText();
        }
        if (FuelManager.Instance != null)
        {
            FuelManager.Instance.OnFuelChanged += HandleFuelChanged;
            currentFuel = FuelManager.Instance.GetRemainingFuel();
            maxFuel = FuelManager.Instance.GetMaxFuel();
        }
        if (player == null)
        {
            player = FindFirstObjectByType<Player>();
        }
        UpdatePlanetInfoText();
    }
    void OnDisable()
    {
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.OnScoreChanged -= HandleScoreChanged;
        }
        if (FuelManager.Instance != null)
        {
            FuelManager.Instance.OnFuelChanged -= HandleFuelChanged;
        }
    }
    private void HandleScoreChanged(float newScore)
    {
        currentScore = newScore;
        currentMultiplier = ScoreManager.Instance.GetScoreMultiplier();
        UpdateScoreText();
    }
    private void HandleFuelChanged(float newFuel)
    {
        currentFuel = newFuel;
        maxFuel = FuelManager.Instance.GetMaxFuel();
        UpdateShipInfoText();
    }
    private void UpdatePlanetInfoText()
    {
        if (GravityManager.Instance == null)
        {
            return;
        }
        planetInfoText.text = $"Current Planet: {GravityManager.Instance.GetCurrentPlanet()} \nGravity Scale: {GravityManager.Instance.GetCurrentGravityScale()}";
    }
    private void UpdateShipInfoText()
    {
        shipInfoText.text = $"Horizontal Speed: {player.GetHorizontalSpeed()} \nVertical Speed: {player.GetVerticalSpeed()} \nCurrent Fuel: {currentFuel:F0}/{maxFuel}";
    }
    private void UpdateScoreText()
    {
        scoreText.text = $"Current Score: {currentScore:F0} \nCurrent Multi: {currentMultiplier}x";
    }
    void Update()
    {
        if (player != null)
        {
            UpdateShipInfoText();
        }
    }


}
