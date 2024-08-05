

using System.Collections;
using UnityEngine;

public class WeaponPickupController : ItemPickupController<WeaponInfo>
{

    protected override void Awake()
    {
        base.Awake();
        Type = ItemPickupType.None;
    }

    public override void Collect(EntityController entity)
    {
        Inventory inventory = entity.GetComponent<Inventory>();
        var weaponInfo = inventory?.AddWeapon(Info);

        if (weaponInfo != null)
        {
            Vector3 position = entity.transform.position;
            var weaponPickup = Instantiate(weaponInfo.itemPrefab, position, Quaternion.Euler(0, 0, weaponInfo.rotation)).GetComponent<WeaponPickupController>();
            weaponPickup.SetInfo(weaponInfo);
        }
    }
}
