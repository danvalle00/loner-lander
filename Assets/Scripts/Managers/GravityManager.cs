using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public static GravityManager Instance { get; private set; }
    [Header("Gravity Settings")]
    [SerializeField] private PlanetType currentPlanet = PlanetType.Moon;

    private const float MOON_GRAVITY = 0.166f;
    private const float MARS_GRAVITY = 0.38f;
    private const float PLUTO_GRAVITY = 0.063f;
    private const float URANUS_GRAVITY = 0.887f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
    }
    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
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

    public PlanetType GetCurrentPlanet() => currentPlanet;
    public float GetCurrentGravityScale() => GetGravityScale(currentPlanet);

}
public enum PlanetType
{
    Moon,
    Mars,
    Pluto,
    Uranus
}

