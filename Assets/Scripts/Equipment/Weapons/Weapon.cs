using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponBase WeaponBaseData;
    public WeaponBase WeaponData => this.WeaponBaseData;

    protected WeaponObserver Observer;

    public abstract void PickUp();
    public abstract void Shoot(Transform shootingPoint);
    public abstract void TryReload();
    public abstract void TryCancelReloadTask();

    public abstract void Attach(WeaponObserver observer);
    public void Detach() => Observer = null;
    public abstract void Notify();
}