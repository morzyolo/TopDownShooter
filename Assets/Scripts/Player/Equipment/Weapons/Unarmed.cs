using UnityEngine;

public class Unarmed : Weapon
{
    public override void Equip() { }
    public override void Drop() { }

    public override void Shoot(Animator animator)
    {
        Debug.Log("Kick");
    }
}