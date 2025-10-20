using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public static GravityManager Instance { get; private set; }
    [Header("Gravity Settings")]
    [SerializeField] private PlanetType currentPlanet = PlanetType.Moon;
    [SerializeField] private float customGravity = 1.0f;

    [Header("References")]
    [SerializeField] private Player player;

    // gravity data
    private const float EARTH_GRAVITY = 1.0f;
    private const float MOON_GRAVITY = 0.166f;
    private const float MARS_GRAVITY = 0.38f;
    private const float PLUTO_GRAVITY = 0.063f;

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
            PlanetType.Earth => EARTH_GRAVITY,
            PlanetType.Moon => MOON_GRAVITY,
            PlanetType.Mars => MARS_GRAVITY,
            PlanetType.Pluto => PLUTO_GRAVITY,
            PlanetType.Custom => customGravity,
            _ => MOON_GRAVITY,
        };
    }
    public void SetPlanet(PlanetType planet)
    {
        currentPlanet = planet;
        ApplyGravity(currentPlanet);
        Debug.Log($"Gravity set to {planet} with scale {GetGravityScale(planet)}");
    }
    public void SetCustomGravity(float gravityScale)
    {
        customGravity = gravityScale;
        if (currentPlanet == PlanetType.Custom)
        {
            ApplyGravity(currentPlanet);

        }
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
        Earth,
        Moon,
        Mars,
        Pluto,
        Custom // planeta com gravidade custom
    }
}
