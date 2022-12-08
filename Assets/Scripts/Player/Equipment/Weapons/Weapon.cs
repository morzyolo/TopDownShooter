using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public Action<int> Shooted;

    [SerializeField] protected WeaponBase WeaponBaseData;
    public WeaponBase WeaponData => this.WeaponBaseData;

    protected WeaponObserver Observer;

    public abstract void PickUp();
    public abstract void Shoot(Transform shootingPoint);
    public abstract void Reload();

    public abstract void Attach(WeaponObserver observer);
    public virtual void Detach() => Observer = null;
    public abstract void Notify();
}