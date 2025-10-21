using TMPro;
using UnityEngine;


public class ScreenInfo : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI planetInfoText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI shipInfoText;
    [SerializeField] private Player player;

    void Update()
    {
        if (player != null)
        {
            UpdateText();
        }
    }
    private void UpdateText()
    {
        planetInfoText.text = $"Current Planet: {GravityManager.Instance.GetCurrentPlanet()} \nGravity Scale: {GravityManager.Instance.GetCurrentGravityScale()}";

        scoreText.text = $"Current Score: {ScoreManager.Instance.GetCurrentScore()} \nCurrent Multi: {ScoreManager.Instance.GetScoreMultiplier()}x";

        shipInfoText.text = $"Horizontal Speed: {player.GetHorizontalSpeed()} \nVertical Speed: {player.GetVerticalSpeed()} \nCurrent Fuel: {player.GetCurrentFuel():F2}";
    }

}
