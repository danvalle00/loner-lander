using UnityEngine;

public class ManagerBootstrap : MonoBehaviour
{
    [Header("Managers References")]
    [SerializeField] private GameObject gameManagerPrefab;
    [SerializeField] private GameObject scoreManagerPrefab;
    [SerializeField] private GameObject gravityManagerPrefab;
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
        if (FindFirstObjectByType<GravityManager>() == null)
        {
            if (gravityManagerPrefab != null)
            {
                Instantiate(gravityManagerPrefab);
            }
        }
        Destroy(this.gameObject);
    }
}
