using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer), typeof(BoxCollider2D))]
public class Glock26 : Weapon, IDroppable
{
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;

    private Color _invisibleColor;
    private Color _visibleColor;

    private Camera _mainCamera;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
        _invisibleColor = new Color(1f, 1f, 1f, 0f);
        _visibleColor = new Color(1f, 1f, 1f, 1f);
        _mainCamera = Camera.main;
    }

    public override void Equip()
    {
        _spriteRenderer.color = _invisibleColor;
        _collider.enabled = false;
    }

    public void Drop()
    {
        _spriteRenderer.color = _visibleColor;
        _collider.enabled = true;
        transform.parent = null;
    }

    public override void Shoot(Transform shootingPoint)
    {
        Vector3 direction = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;

        RaycastHit2D[] hits = Physics2D.RaycastAll(shootingPoint.position, direction, 10f);

        if (hits.Length == 0) return;

        if (hits[0].transform.TryGetComponent<IDamageable>(out var enemy))
        {
            enemy.TakeDamage(WeaponBaseData.Damage);
        }
    }
}