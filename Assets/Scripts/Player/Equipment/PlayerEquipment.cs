using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEquipment : MonoBehaviour
{
    [SerializeField] private float _pickUpRadius;

    [SerializeField] private SpriteRenderer _head;
    [SerializeField] private SpriteRenderer _body;
    [SerializeField] private Transform _shootingPoint;

    [SerializeField] private List<Weapon> _weapons;
    private int _currentWeaponId;

    private PlayerInputActions _inputActions;

    private int _itemMaskId;

    private void Awake()
    {
        _inputActions = GetComponentInParent<PlayerInputSystem>().InputActions;

        _weapons = GetComponentsInChildren<Weapon>().ToList();
        _currentWeaponId = 0;

        foreach (var weapon in _weapons)
            weapon.Equip();

        _itemMaskId = LayerMask.GetMask("Item");
    }
    private void Start()
    {
        EquipWeapon();
    }

    private void EquipWeapon()
    {
        WeaponBase data = _weapons[_currentWeaponId].WeaponData;
        _head.transform.localPosition = data.HeadPosition;
        _body.sprite = data.ArmedSprite;
        _body.transform.localPosition = data.BodyPosition;
        _shootingPoint.transform.localPosition = data.ShootingPoint;
    }

    private void PickUp(InputAction.CallbackContext context)
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, _pickUpRadius, Vector2.zero, 0f, _itemMaskId);

        if (hit.transform.TryGetComponent<Weapon>(out Weapon weapon))
        {
            weapon.Equip();
            weapon.transform.parent = this.transform;
            weapon.transform.localPosition = Vector3.zero;
            _weapons.Add(weapon);
        }
    }

    private void DropWeapon(InputAction.CallbackContext context)
    {
        if (_weapons[_currentWeaponId] is not IDroppable weapon) return;

        weapon.Drop();
        _weapons.RemoveAt(_currentWeaponId);
        _currentWeaponId--;
        EquipWeapon();
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        _weapons[_currentWeaponId].Shoot(_shootingPoint);
    }

    private void ChangeWeapon(InputAction.CallbackContext context)
    {
        _currentWeaponId = (_currentWeaponId + (context.ReadValue<float>() > 0 ? 1 : _weapons.Count - 1)) % _weapons.Count;
        EquipWeapon();
    }

    private void OnEnable()
    {
        _inputActions.Player.Shoot.started += Shoot;
        _inputActions.Player.ScrollWeapon.started += ChangeWeapon;
        _inputActions.Player.PickUp.started += PickUp;
        _inputActions.Player.Drop.started += DropWeapon;
    }

    private void OnDisable()
    {
        _inputActions.Player.Shoot.started -= Shoot;
        _inputActions.Player.ScrollWeapon.started -= ChangeWeapon;
        _inputActions.Player.PickUp.started -= PickUp;
        _inputActions.Player.Drop.started -= DropWeapon;
    }
}