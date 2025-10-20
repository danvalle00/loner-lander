using UnityEngine;

[CreateAssetMenu(fileName = "FuelScriptableObject", menuName = "Scriptable Objects/FuelScriptableObject")]
public class FuelScriptableObject : ScriptableObject
{
    public float maxFuel;
    public float remainingFuel;
    public int fuelConsumptionRate;

}
