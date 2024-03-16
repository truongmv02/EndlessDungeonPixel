

using System.Collections;
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
        BoxCollider2D.size = Info.sprite.bounds.size;
    }

    public void Move(Vector2 direction)
    {
        Rigidbody2D.velocity = direction * 10;
        StartCoroutine(StopMovement());
    }

    public override void Collect(EntityController entity)
    {
        Inventory inventory = entity.GetComponent<Inventory>();
        var weaponInfo = inventory?.AddWeapon(Info as WeaponInfo);

        Destroy(gameObject);

        if (weaponInfo != null)
        {
            Vector3 position = entity.transform.position;
            var weaponPickup = Instantiate(weaponInfo.itemPrefab, position, Quaternion.Euler(0, 0, weaponInfo.rotation)).GetComponent<WeaponPickupController>();
            Vector2 direction;
            if (transform.position.y < position.y)
            {
                direction = Vector2.up * -1;
            }
            else
            {
                direction = Vector2.up;
            }
            weaponPickup.Move(direction);
            weaponPickup.SetInfo(weaponInfo);
            weaponPickup.crated = true;
        }
    }


    IEnumerator StopMovement()
    {
        yield return new WaitForSeconds(0.1f);
        Rigidbody2D.velocity = Vector2.zero;
    }
}
