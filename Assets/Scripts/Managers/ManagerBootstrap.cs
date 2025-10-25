using UnityEngine;

public class ManagerBootstrap : MonoBehaviour
{
    [Header("Managers References")]
    [SerializeField] private GameObject gameManagerPrefab;
    [SerializeField] private GameObject scoreManagerPrefab;
    [SerializeField] private GameObject fuelManagerPrefab;
    void Awake()
    {
        if (FindFirstObjectByType<GameManager>() == null)
        {
            if (gameManagerPrefab != null)
            {
                Instantiate(gameManagerPrefab);
            }

        }
        if (FindFirstObjectByType<ScoreManager>() == null)
        {
            if (scoreManagerPrefab != null)
            {
                Instantiate(scoreManagerPrefab);
            }
        }
        if (FindFirstObjectByType<FuelManager>() == null)
        {
            if (fuelManagerPrefab != null)
            {
                Instantiate(fuelManagerPrefab);
            }
        }
        Destroy(this.gameObject);
    }
}
