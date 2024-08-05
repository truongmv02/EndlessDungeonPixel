using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public EntityController Owner { set; get; }
    WeaponController weapon;
    public WeaponController Weapon
    {
        get
        {
            return weapon ?? this.GetComponentInChildrenRef(ref weapon);
        }
        set
        {
            weapon = value;
        }
    }

    List<WeaponInfo> weaponList = new List<WeaponInfo>();
    public int MaxWeapon { set; get; } = 100;
    public int CurrentWeaponIndex { get; set; }


    public WeaponInfo AddWeapon(WeaponInfo info)
    {
        WeaponInfo weaponThrow = null;
        if (weaponList.Count < MaxWeapon)
        {
            weaponList.Add(info);
            SwitchWeapon();
        }
        else
        {
            weaponThrow = weaponList[0];
            weaponList[0] = info;
            ChangeWeapon();
        }
        return weaponThrow;
    }

    public void AddSlots(int slots = 1)
    {
        MaxWeapon += slots;
    }

    public void SwitchWeapon()
    {
        if (weaponList.Count == 0 || weaponList.Count == 1) return;

        WeaponInfo currentWeapon = weaponList[0];

        for (int i = 0; i < weaponList.Count - 1; i++)
        {
            weaponList[i] = weaponList[i + 1];
        }
        weaponList[weaponList.Count - 1] = currentWeapon;
        ChangeWeapon();
    }

    private void ChangeWeapon()
    {
        Weapon.SetInfo(weaponList[0]);
        Owner.TargetDetector.Info.detectRange = Weapon.Stats["Range"].Value;
    }

}
