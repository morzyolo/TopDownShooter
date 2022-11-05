using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected WeaponBase WeaponBaseData;
    public WeaponBase WeaponData => this.WeaponBaseData;

    public abstract void Shoot(Animator animator);
}