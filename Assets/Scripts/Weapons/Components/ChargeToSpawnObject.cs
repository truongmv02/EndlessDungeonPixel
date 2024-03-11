using UnityEngine;

[RequireComponent(typeof(Charge))]
[RequireComponent(typeof(AddObjectHandle))]
public class ChargeToSpawnObject : MonoBehaviour
{
    private Charge charge;
    private AddObjectHandle addObject;

    private void Start()
    {
        charge = GetComponent<Charge>();
        addObject = GetComponent<AddObjectHandle>();
        charge.OnCurrentChargeChange += OnCurrentChargeChange;
    }

    private void OnCurrentChargeChange(int value)
    {
        addObject.Info.turnCount = value;
    }
}