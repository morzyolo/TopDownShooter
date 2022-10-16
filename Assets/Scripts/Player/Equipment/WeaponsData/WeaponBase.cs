using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Base", menuName = "Weapon Data", order = 51)]
public class WeaponBase : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private int _damage;
    [SerializeField] private SpriteRenderer _sprite;

    public string Name { get => _name; }
    public int Damage { get => _damage; }
    public SpriteRenderer Sprite { get => _sprite; }
}