using UnityEngine;

public class ChargeToSpawnObject : MonoBehaviour
{
    private Charge charge;
    private AddObjectHandle addObjectHandle;

    private void Start()
    {
        charge = GetComponent<Charge>();
        addObjectHandle = GetComponent<AddObjectHandle>();

        BaseUtils.ValidateCheckNullValue(charge, nameof(charge), nameof(ChargeToSpawnObject), name);
        BaseUtils.ValidateCheckNullValue(addObjectHandle, nameof(addObjectHandle), nameof(ChargeToSpawnObject), name);

        charge.OnCurrentChargeChange += OnCurrentChargeChange;
    }

    private void OnCurrentChargeChange(int value)
    {
        addObjectHandle.Info.turnCount = value;
    }

    private void OnDestroy()
    {
        if (charge != null)
            charge.OnCurrentChargeChange -= OnCurrentChargeChange;

    }
}