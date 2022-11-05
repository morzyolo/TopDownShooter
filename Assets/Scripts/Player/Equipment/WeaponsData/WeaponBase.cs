using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Base", menuName = "Weapon Data", order = 51)]
public class WeaponBase : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _damage;

    [SerializeField] private Sprite _armedSprite;
    [SerializeField] private Sprite _bulletSprite;

    [SerializeField] private Vector3 _headPosition;
    [SerializeField] private Vector3 _bodyPosition;
    [SerializeField] private Vector3 _shootingPoint;

    public string Name { get => _name; }
    public int Damage { get => _damage; }
    public Sprite ArmedSprite { get => _armedSprite; }
    public Sprite BulletSprite { get => _bulletSprite; }
    public Vector3 HeadPosition { get => _headPosition; }
    public Vector3 BodyPosition { get => _bodyPosition; }
    public Vector3 ShootingPoint { get => _shootingPoint; }
}