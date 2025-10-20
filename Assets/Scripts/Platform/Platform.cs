using TMPro;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class Platform : MonoBehaviour
{
    private TextMeshPro childText;

    private Transform platformTransform;


    private Player player;
    [SerializeField] private float minVerticalSpeed = 100f;
    [Header("Platform Settings")]
    [SerializeField, Range(1, 10)] private int scoreMultiplier = 1;
    [SerializeField, Range(1, 10)] private int basePlatformLength = 1;
    private float minimumScale = 0.2f; // previnir que o collider exploda


    void Awake()
    {
        childText = GetComponentInChildren<TextMeshPro>();

        platformTransform = GetComponent<Transform>();
        childText.text = $"{scoreMultiplier}x";
        if (player == null)
        {
            player = FindFirstObjectByType<Player>();
        }
    }

    private void OnValidate()
    {
        platformTransform = GetComponent<Transform>();
        childText = GetComponentInChildren<TextMeshPro>();
        PlatformScaling();
        childText.text = $"{scoreMultiplier}x";
    }

    private void OnCollisionEnter2D(Collision2D collision) // implementar as condicoes de pouso adequado == velocidade baixa ou explode
    {
        float verticalSpeed = float.Parse(player.GetVerticalSpeed());
        if (collision.gameObject == player.gameObject && verticalSpeed < minVerticalSpeed)
        {
            Debug.Log($"Landed on platform with {scoreMultiplier}x multiplier.");
            ScoreManager.Instance.SetScoreMultiplier(scoreMultiplier);
        }
        else
        {
            Debug.Log("Crash! Landing too fast.");
        }
    }

    private void PlatformScaling()
    {
        float scaleFactor = (float)basePlatformLength / scoreMultiplier; // o X da platform é influenciado pelo scoreMultiplier
        scaleFactor = Mathf.Max(scaleFactor, minimumScale);
        transform.localScale = new Vector3(scaleFactor, platformTransform.localScale.y, platformTransform.localScale.z);
        childText.transform.localScale = new Vector3(1f / scaleFactor, 1f / platformTransform.localScale.y, 1f); // garantir que o texto n exploda

    }
}