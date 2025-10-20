using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private Slider fuelSlider;
    public ScoreScriptableObjects scoreData;
    public FuelScriptableObject fuelData;

    [Header("Runtime Score State")]
    [SerializeField] private float currentScorePoints;
    [SerializeField] private int currentScoreMultiplier;

    [Header("Score Settings")]

    [SerializeField] private float scoreDecreaseRate = 3f;
    [SerializeField] private float fuelToScoreRate = 0.5f;

    void Awake()
    {
        fuelSlider = FindFirstObjectByType<Slider>();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        currentScorePoints = scoreData.initialScore;
        currentScoreMultiplier = 1;
    }

    void Update()
    {
        // pegar o score inicial e diminuir ao decorrer do tempo 
        currentScorePoints -= Time.deltaTime * scoreDecreaseRate;
        currentScorePoints = Mathf.Max(0, currentScorePoints);
    }
    public void SetScoreMultiplier(int multiplier)
    {
        currentScoreMultiplier = multiplier;

    }

    public int GetScoreMultiplier()
    {
        return currentScoreMultiplier;
    }

    public string GetCurrentScore()
    {
        return string.Format("{0:F0}", currentScorePoints);
    }

    public void FuelToScore() // qntidade de fuel convertida em pontos qnt mais fuel menos pontos iniciais
    {
        float fuelAmount = fuelSlider.value;
        int pointsFromFuel = 1000 + Mathf.FloorToInt(fuelAmount * fuelToScoreRate);
        fuelData.maxFuel = fuelAmount;
        scoreData.initialScore = pointsFromFuel;
    }
}
