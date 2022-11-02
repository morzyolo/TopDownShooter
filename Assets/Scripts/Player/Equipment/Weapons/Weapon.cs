using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected WeaponBase WeaponBase;

    public abstract void Shoot();

    public void SetWeaponBase(WeaponBase weaponBase) => this.WeaponBase = weaponBase;
}