using UnityEngine;

public class ResetQndSair : MonoBehaviour
{
    [SerializeField] private Player player;

    void Awake()
    {
        if (player == null)
        {
            player = FindFirstObjectByType<Player>();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.RestartLevel();
        }
    }
}
