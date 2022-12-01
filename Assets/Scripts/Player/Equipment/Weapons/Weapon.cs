using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public Action<int> Shooted;

    [SerializeField] protected WeaponBase WeaponBaseData;
    public WeaponBase WeaponData => this.WeaponBaseData;

    public abstract void PickUp();
    public abstract void Shoot(Transform shootingPoint);
    public abstract int GetRemainingBullets();
}