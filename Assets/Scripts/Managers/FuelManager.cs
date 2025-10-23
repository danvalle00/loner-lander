using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FuelManager : MonoBehaviour
{
    public static FuelManager Instance { get; private set; }
    private Slider fuelSlider;
    [SerializeField] private FuelScriptableObject fuelData;


    public System.Action OnFuelDepleted;
    public System.Action<float> OnFuelConfigurationChanged;
    public System.Action<float> OnFuelChanged;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (fuelSlider == null)
        {
            fuelSlider = FindFirstObjectByType<Slider>();
        }
        InitializeFuel();
    }

    void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void InitializeFuel()
    {
        fuelData.remainingFuel = fuelData.maxFuel;
    }

    public void SetStartingFuel()
    {
        if (fuelData == null)
        {
            return;
        }
        float fuelAmount = fuelSlider.value;
        fuelData.maxFuel = fuelAmount;
        fuelData.remainingFuel = fuelAmount;
        OnFuelConfigurationChanged?.Invoke(fuelAmount);
    }
    public void ConsumeFuel(float amount)
    {
        if (fuelData == null)
        {
            return;
        }
        float previousFuel = fuelData.remainingFuel;
        fuelData.remainingFuel -= amount;
        fuelData.remainingFuel = Mathf.Max(0, fuelData.remainingFuel);
        if (previousFuel != fuelData.remainingFuel)
        {
            OnFuelChanged?.Invoke(fuelData.remainingFuel);
        }
        if (fuelData.remainingFuel <= 0 && previousFuel > 0)
        {
            OnFuelDepleted?.Invoke();
        }

    }
    public float GetRemainingFuel() => fuelData != null ? fuelData.remainingFuel : 0f;
    public float GetMaxFuel() => fuelData != null ? fuelData.maxFuel : 0f;
}
