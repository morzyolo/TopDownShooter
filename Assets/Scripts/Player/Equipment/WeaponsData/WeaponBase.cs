using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Base", menuName = "Weapon Data", order = 51)]
public class WeaponBase : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _damage;
    [SerializeField] private int _magazineSize;

    [SerializeField] private Sprite _armedSprite;
    [SerializeField] private Sprite _weaponSprite;
    [SerializeField] private Bullet _bulletPrefab;

    [SerializeField] private Vector3 _headPosition;
    [SerializeField] private Vector3 _bodyPosition;
    [SerializeField] private Vector3 _shootingPoint;

    public string Name { get => _name; }
    public int Damage { get => _damage; }
    public int MagazineSize { get => _magazineSize; }
    public Sprite ArmedSprite { get => _armedSprite; }
    public Sprite WeaponSprite { get => _weaponSprite; }
    public Bullet BulletPrefab { get => _bulletPrefab; }
    public Vector3 HeadPosition { get => _headPosition; }
    public Vector3 BodyPosition { get => _bodyPosition; }
    public Vector3 ShootingPoint { get => _shootingPoint; }
}