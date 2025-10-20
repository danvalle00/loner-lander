using UnityEngine;

public class PlatformCollisionCamera : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform changedPositions;
    private float cameraOriginalSize;

    void Awake()
    {
        mainCamera = FindFirstObjectByType<Camera>();
        changedPositions = GetComponent<Transform>();
        cameraOriginalSize = mainCamera.orthographicSize;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered platform trigger area. Adjusting camera.");
            mainCamera.transform.position = new Vector3(changedPositions.position.x, changedPositions.position.y, mainCamera.transform.position.z);
            mainCamera.orthographicSize = 7.5f;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player exited platform trigger area. Resetting camera.");
            mainCamera.orthographicSize = cameraOriginalSize;
            mainCamera.transform.position = new Vector3(0, 0, mainCamera.transform.position.z);
        }
    }
}
