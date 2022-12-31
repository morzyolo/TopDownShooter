using Cysharp.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class Glock26 : Weapon, IDroppable
{
    private int _currentBulletCount;
    private int _magazineCapacity;
    private int _spareBullets;
    private List<Bullet> _bullets;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;

    private Color _invisibleColor;
    private Color _visibleColor;

    private UniTask _reloadTask;
    private UniTask _shootingTask;

    private CancellationTokenSource _reloadCancellation;
    private CancellationTokenSource _shootingCancellation;

    private void Awake()
    {
        _currentBulletCount = WeaponBaseData.MagazineCapacity;
        _magazineCapacity = WeaponBaseData.MagazineCapacity;
        _spareBullets = WeaponBaseData.SpareBullets;
        _bullets = new List<Bullet>();

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();

        _invisibleColor = new Color(1f, 1f, 1f, 0f);
        _visibleColor = new Color(1f, 1f, 1f, 1f);
    }

    public void Drop()
    {
        TryCancelReloadTask();
        _spriteRenderer.color = _visibleColor;
        _collider.enabled = true;
        transform.parent = null;
    }

    public override void PickUp()
    {
        _spriteRenderer.color = _invisibleColor;
        _collider.enabled = false;
    }

    public override void Shoot(Transform shootingPoint)
    {
        if (!_shootingTask.Status.IsCompleted() || !_reloadTask.Status.IsCompleted()) return;

        if (_currentBulletCount == 0)
        {
            return;
        }

        _shootingTask = UniTask.Delay(WeaponBaseData.ShootingDelay);

        int bulletId = FindFreeBulletId();
        if (bulletId == -1)
            _bullets.Add(Instantiate(WeaponBaseData.BulletPrefab, shootingPoint.position, shootingPoint.rotation));
        else
        {
            _bullets[bulletId].transform.SetPositionAndRotation(shootingPoint.position, shootingPoint.rotation);
            _bullets[bulletId].gameObject.SetActive(true);
        }
        _currentBulletCount--;
        Notify();
    }

    public override void TryReload()
    {
        if (_currentBulletCount == _magazineCapacity || _spareBullets == 0) return;

        _reloadTask = Reload();
    }

    private async UniTask Reload()
    {
        _reloadTask = UniTask.Delay(WeaponBaseData.ReloadTime, cancellationToken: _reloadCancellation.Token);
        await _reloadTask;

        int neededCount = _magazineCapacity - _currentBulletCount;
        if (neededCount < _spareBullets)
        {
            _currentBulletCount = _magazineCapacity;
            _spareBullets -= neededCount;
        }
        else
        {
            _currentBulletCount += _spareBullets;
            _spareBullets = 0;
        }
        Notify();
    }

    public override void TryCancelReloadTask()
    {
        if (!_reloadTask.Status.IsCompleted())
        {
            _reloadCancellation.Cancel();
            _reloadCancellation = new CancellationTokenSource();
        } 
    }

    public override void Attach(WeaponObserver observer)
    {
        Observer = observer;
        Observer.SetData(this.WeaponData, _currentBulletCount, _spareBullets);
    }
    public override void Notify() => Observer.ChangeBulletsText(_currentBulletCount, _spareBullets);

    private int FindFreeBulletId()
    {
        for (int i = 0; i < _bullets.Count; i++)
            if (!_bullets[i].isActiveAndEnabled)
                return i;
        return -1;
    }

    private void OnEnable()
    {
        if (_shootingCancellation != null)
            _reloadCancellation.Dispose();

        if (_reloadCancellation != null)
            _shootingCancellation.Dispose();

        _reloadCancellation = new CancellationTokenSource();
        _shootingCancellation = new CancellationTokenSource();
    }

    private void OnDisable()
    {
        _reloadCancellation.Cancel();
        _shootingCancellation.Cancel();
        _reloadCancellation.Dispose();
        _shootingCancellation.Dispose();
    }

    private void OnDestroy()
    {
        _reloadCancellation.Dispose();
        _shootingCancellation.Dispose();
    }
}