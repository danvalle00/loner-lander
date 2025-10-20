using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public static GravityManager Instance { get; private set; }
    [Header("Gravity Settings")]
    [SerializeField] private PlanetType currentPlanet = PlanetType.Moon;

    [Header("References")]
    [SerializeField] private Player player;


    private const float MOON_GRAVITY = 0.166f;
    private const float MARS_GRAVITY = 0.38f;
    private const float PLUTO_GRAVITY = 0.063f;
    private const float URANUS_GRAVITY = 0.887f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        if (player == null)
        {
            player = FindFirstObjectByType<Player>();
        }
        ApplyGravity(currentPlanet);
    }

    private void ApplyGravity(PlanetType planet)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        float gravityScale = GetGravityScale(planet);
        rb.gravityScale = gravityScale;
        Debug.Log($"Gravity applied: {planet} with scale {gravityScale}");
    }

    private float GetGravityScale(PlanetType planet)
    {
        return planet switch
        {
            PlanetType.Moon => MOON_GRAVITY,
            PlanetType.Mars => MARS_GRAVITY,
            PlanetType.Pluto => PLUTO_GRAVITY,
            PlanetType.Uranus => URANUS_GRAVITY,
            _ => MOON_GRAVITY,
        };
    }
    public void SetPlanet(PlanetType planet)
    {
        currentPlanet = planet;
        ApplyGravity(currentPlanet);
        Debug.Log($"Gravity set to {planet} with scale {GetGravityScale(planet)}");
    }


    public PlanetType GetCurrentPlanet()
    {
        return currentPlanet;
    }
    public float GetCurrentGravityScale()
    {
        return GetGravityScale(currentPlanet);
    }
    public enum PlanetType
    {
        Moon,
        Mars,
        Pluto,
        Uranus
    }
}
