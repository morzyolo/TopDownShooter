using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Glock26 : Weapon, IDroppable
{
    private SpriteRenderer _sprite;

    private Color _invisibleColor;
    private Color _visibleColor;

    private void Awake()
    {
        _invisibleColor = new Color(1f, 1f, 1f, 0f);
        _visibleColor = new Color(1f, 1f, 1f, 1f);
        _sprite = GetComponent<SpriteRenderer>();
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

    public override void Shoot(Animator animator)
    {
        Debug.Log("Pistol shoot");
    }
}