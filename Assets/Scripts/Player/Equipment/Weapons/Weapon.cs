public abstract class Weapon
{
    protected WeaponBase WeaponBase;
    public abstract void Shoot();
    public void SetWeaponBase(WeaponBase weaponBase) => this.WeaponBase = weaponBase;
}