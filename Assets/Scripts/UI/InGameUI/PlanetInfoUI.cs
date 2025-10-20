using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]

public class PlanetInfoUI : MonoBehaviour
{
    private TextMeshProUGUI planetInfoText;


    void Awake()
    {
        planetInfoText = GetComponent<TextMeshProUGUI>();

    }

    void Start()
    {
        UpdateText();
    }
    private void UpdateText()
    {
        planetInfoText.text = $"Current Planet: {GravityManager.Instance.GetCurrentPlanet()} \nGravity Scale: {GravityManager.Instance.GetCurrentGravityScale()}";
    }
}
