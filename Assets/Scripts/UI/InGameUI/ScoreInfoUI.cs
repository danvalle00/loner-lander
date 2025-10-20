using UnityEngine;
using TMPro;

public class ScoreInfoUI : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();

    }
    void Update()
    {
        scoreText.text = $"Current Score: {ScoreManager.Instance.GetCurrentScore()} \nCurrent Multi: {ScoreManager.Instance.GetScoreMultiplier()}x";
    }

}
