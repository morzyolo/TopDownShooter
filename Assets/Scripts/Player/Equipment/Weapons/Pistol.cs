using UnityEngine;

public class Pistol : MonoBehaviour, IWeapon
{
    public void Shoot()
    {
        Debug.Log("Pistol shoot");
    }
}