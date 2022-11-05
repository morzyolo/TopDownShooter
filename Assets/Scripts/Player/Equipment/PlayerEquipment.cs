using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEquipment : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _head;
    [SerializeField] private SpriteRenderer _body;
    [SerializeField] private Transform _shootingPoint;

    [SerializeField] private List<Weapon> _weapons;
    private int _currentWeaponId;
    private int _weaponCount;

    private Animator _animator;
    private PlayerInputActions _inputActions;

    private void Awake()
    {
        _animator = GetComponentInParent<Animator>();
        _inputActions = GetComponentInParent<PlayerInputSystem>().InputActions;

        _weapons = GetComponentsInChildren<Weapon>().ToList();
        _currentWeaponId = 0;
        _weaponCount = _weapons.Count;
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

    private void Shoot(InputAction.CallbackContext context) => _weapons[_currentWeaponId].Shoot(_animator);

    private void ChangeWeapon(InputAction.CallbackContext context)
    {
        _currentWeaponId = (_currentWeaponId + (context.ReadValue<float>() > 0 ? 1 : _weaponCount - 1)) % _weaponCount;
        EquipWeapon();
    }

    private void OnEnable()
    {
        _inputActions.Player.Shoot.started += Shoot;
        _inputActions.Player.ScrollWeapon.started += ChangeWeapon;
    }

    private void OnDisable()
    {
        _inputActions.Player.Shoot.started -= Shoot;
        _inputActions.Player.ScrollWeapon.started -= ChangeWeapon;
    }
}