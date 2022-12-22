using UnityEngine;
using System.Threading.Tasks;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponBase WeaponBaseData;
    public WeaponBase WeaponData => this.WeaponBaseData;

    protected Task ShootingTask;
    protected WeaponObserver Observer;

    public abstract void PickUp();
    public abstract void Shoot(Transform shootingPoint);
    public abstract void Reload();

    public abstract void Attach(WeaponObserver observer);
    public virtual void Detach() => Observer = null;
    public abstract void Notify();
}