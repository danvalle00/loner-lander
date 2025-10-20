using TMPro;
using UnityEngine;
[RequireComponent(typeof(TextMeshProUGUI))]

public class ShipInfoUI : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    [SerializeField] private Player player;

    void Awake()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        player = FindFirstObjectByType<Player>();
    }

    void Update()
    {
        if (player != null)
        {
            UpdateText();
        }
    }
    private void UpdateText()
    {
        textMesh.text = $"Horizontal Speed: {player.GetHorizontalSpeed()} \nVertical Speed: {player.GetVerticalSpeed()} \nCurrent Fuel: {player.GetCurrentFuel()}";
    }
}
