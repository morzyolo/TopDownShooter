using System.Threading.Tasks;
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

        ShootingTask = Task.Delay(0);
    }

    public void Drop()
    {
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
        if (!ShootingTask.IsCompleted) return;

        ShootingTask = Task.Delay(WeaponBaseData.ShootingDelay);

        if (_currentBulletCount == 0)
        {
            return;
        }

        int bulletId = FindFreeBulletId();
        if (bulletId == -1)
            _bullets.Add(Instantiate(WeaponBaseData.BulletPrefab, shootingPoint.position, shootingPoint.rotation));
        else
        {
            _bullets[bulletId].transform.SetPositionAndRotation(shootingPoint.position, shootingPoint.rotation);
            _bullets[bulletId].gameObject.SetActive(true);
        }
        _currentBulletCount--;
        Observer?.ChangeBulletsText(_currentBulletCount, _spareBullets);
    }

    public override void Reload()
    {
        if (_currentBulletCount == _magazineCapacity && _spareBullets == 0) return;

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
        Observer?.ChangeBulletsText(_currentBulletCount, _spareBullets);
    }

    public override void Attach(WeaponObserver observer)
    {
        Observer = observer;
        Observer.SetData(this.WeaponData, _currentBulletCount, _spareBullets);
    }
    public override void Notify() => Observer?.ChangeBulletsText(_currentBulletCount, _spareBullets);

    private int FindFreeBulletId()
    {
        for (int i = 0; i < _bullets.Count; i++)
            if (!_bullets[i].isActiveAndEnabled)
                return i;
        return -1;
    }
}