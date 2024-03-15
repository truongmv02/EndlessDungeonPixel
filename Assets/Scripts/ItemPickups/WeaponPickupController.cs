

using UnityEngine;

public class WeaponPickupController : ItemPickupController
{
    public string Name;
    public bool crated;
    protected override void Awake()
    {
        base.Awake();
        if (!crated)
            Info = DataManager.Instance.WeaponDatas.GetInfo(Name);
    }

    public override void Collect(EntityController entity)
    {
        Inventory inventory = entity.GetComponent<Inventory>();
        var weaponInfo = inventory?.AddWeapon(Info as WeaponInfo);

        Destroy(gameObject);

        if (weaponInfo != null)
        {
            Vector3 position = entity.transform.position + new Vector3(0f, 2f);
            var weaponPickup = Instantiate(weaponInfo.itemPrefab, position, Quaternion.identity).GetComponent<WeaponPickupController>();
            weaponPickup.SetInfo(weaponInfo);
            weaponPickup.crated = true;
        }

    }
}
