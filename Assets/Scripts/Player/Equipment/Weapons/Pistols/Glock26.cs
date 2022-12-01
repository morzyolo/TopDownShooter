using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class Glock26 : Weapon, IDroppable
{
    private int _remainingBullets;
    private List<Bullet> _bullets;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;

    private Color _invisibleColor;
    private Color _visibleColor;

    private void Awake()
    {
        _remainingBullets = WeaponBaseData.MagazineSize;
        _bullets = new List<Bullet>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        _invisibleColor = new Color(1f, 1f, 1f, 0f);
        _visibleColor = new Color(1f, 1f, 1f, 1f);
    }

    public override void PickUp()
    {
        _spriteRenderer.color = _invisibleColor;
        _collider.enabled = false;
    }

    public override void Shoot(Transform shootingPoint)
    {
        if (_remainingBullets == 0)
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
        _remainingBullets--;
        Shooted?.Invoke(_remainingBullets);
    }

    public override int GetRemainingBullets() => _remainingBullets;

    public void Drop()
    {
        _spriteRenderer.color = _visibleColor;
        _collider.enabled = true;
        transform.parent = null;
    }

    private int FindFreeBulletId()
    {
        for (int i =0; i < _bullets.Count; i++) 
            if (!_bullets[i].isActiveAndEnabled)
                return i;
        return -1;
    }
}