using UnityEngine;

public class Pistol : Weapon
{
    private string _name;
    private int _damage;

    private void Start()
    {
        _name = WeaponBase.Name;
        _damage = WeaponBase.Damage;
    }

    public override void Shoot()
    {
        Debug.Log("Pistol shoot");
    }
}