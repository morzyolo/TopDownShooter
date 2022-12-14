using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;

public class Unarmed : Weapon
{
    [SerializeField] private SpriteRenderer _body;
    [SerializeField] private int _millisecondsPerFrame;
    [SerializeField] private Sprite[] _punchFrames;

    [SerializeField] private Vector2 _punchBoxSize;

    private UniTask _punchTask;

    public override void PickUp() { }

    public override void Shoot(Transform shootingPoint)
    {
        if (!_punchTask.Status.IsCompleted()) return;

        _punchTask = UniTask.Delay(WeaponBaseData.ShootingDelay);

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

    public override void TryReload() { }
    public override void TryCancelReloadTask() { }

    private async void Punch()
    {
        foreach(var frame in _punchFrames)
        {
            _body.sprite = frame;
            await Task.Delay(_millisecondsPerFrame);
        }
        _body.sprite = WeaponBaseData.BodySprite;
    }

    public override void Attach(WeaponObserver observer)
    {
        Observer = observer;
        Observer.SetData(this.WeaponData, WeaponBaseData.MagazineCapacity, WeaponBaseData.SpareBullets);
    }

    public override void Notify() { }
}