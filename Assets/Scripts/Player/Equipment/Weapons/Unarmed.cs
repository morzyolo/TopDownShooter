using System.Threading.Tasks;
using UnityEngine;

public class Unarmed : Weapon
{
    [SerializeField] private SpriteRenderer _body;
    [SerializeField] private int _millisecondsPerFrame;
    [SerializeField] private Sprite[] _punchFrames;

    private Sprite _originalBodySprite;

    [SerializeField] private Vector2 _punchBoxSize;

    private void Awake()
    {
        _originalBodySprite = _body.sprite;
    }

    public override void Equip() { }

    public override void Shoot(Transform shootingPoint)
    {
        Punch();
        RaycastHit2D[] hits = Physics2D.BoxCastAll(shootingPoint.position, _punchBoxSize, shootingPoint.transform.rotation.z, Vector2.zero);

        foreach (var hit in hits)
        {
            if (hit.transform.TryGetComponent<IDamageable>(out var enemy))
            {
                enemy.TakeDamage(WeaponBaseData.Damage);
            }
        }
    }

    private async void Punch()
    {
        foreach(var frame in _punchFrames)
        {
            _body.sprite = frame;
            await Task.Delay(_millisecondsPerFrame);
        }
        _body.sprite = _originalBodySprite;
    }
}