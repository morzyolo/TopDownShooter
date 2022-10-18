using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Base", menuName = "Weapon Data", order = 51)]
public class WeaponBase : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _damage;
    [SerializeField] private Sprite _weaponSprite;
    [SerializeField] private Sprite _bulletSprite;

    public string Name { get => _name; }
    public int Damage { get => _damage; }
    public Sprite WeaponSprite { get => _weaponSprite; }
    public Sprite BulletSprite { get => _bulletSprite; }
}