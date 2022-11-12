using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class Glock26 : Weapon, IDroppable
{
    private SpriteRenderer _sprite;

    private Color _invisibleColor;
    private Color _visibleColor;

    private Camera _mainCamera;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _invisibleColor = new Color(1f, 1f, 1f, 0f);
        _visibleColor = new Color(1f, 1f, 1f, 1f);
        _mainCamera = Camera.main;
    }

    public override void Equip()
    {
        _sprite.color = _invisibleColor;
    }

    public void Drop()
    {
        _sprite.color = _visibleColor;
        transform.parent = null;
    }

    public override void Shoot(Transform shootingPoint)
    {
        Vector3 direction = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(shootingPoint.position, direction);

        if (hit.transform.TryGetComponent<IDamageable>(out var enemy))
        {
            enemy.TakeDamage(WeaponBaseData.Damage);
        }
    }
}