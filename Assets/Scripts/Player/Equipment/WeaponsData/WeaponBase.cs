using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Base", menuName = "Weapon Data", order = 51)]
public class WeaponBase : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _damage;
    [SerializeField] private int _magazineCapacity;
    [SerializeField] private int _spareBullets;

    [SerializeField] private Sprite _bodySprite;
    [SerializeField] private Sprite _weaponSprite;
    [SerializeField] private Bullet _bulletPrefab;

    [SerializeField] private Vector3 _headPosition;
    [SerializeField] private Vector3 _bodyPosition;
    [SerializeField] private Vector3 _shootingPoint;

    public string Name { get => _name; }
    public int Damage { get => _damage; }
    public int MagazineCapacity { get => _magazineCapacity; }
    public int SpareBullets { get => _spareBullets; }
    public Sprite BodySprite { get => _bodySprite; }
    public Sprite WeaponSprite { get => _weaponSprite; }
    public Bullet BulletPrefab { get => _bulletPrefab; }
    public Vector3 HeadPosition { get => _headPosition; }
    public Vector3 BodyPosition { get => _bodyPosition; }
    public Vector3 ShootingPoint { get => _shootingPoint; }
}