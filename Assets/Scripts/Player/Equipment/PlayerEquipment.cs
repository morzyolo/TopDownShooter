using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEquipment : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _head;
    [SerializeField] private SpriteRenderer _body;
    [SerializeField] private Transform _shootingPoint;

    [SerializeField] private WeaponBase[] _weapons;

    private int _currentWeaponId;
    private int _weaponCount;
    private Weapon _weapon;

    private PlayerInputActions _inputActions;

    private void Awake()
    {
        _currentWeaponId = 0;
        _weaponCount = _weapons.Length;
        _inputActions = GetComponent<PlayerInputSystem>().InputActions;
        _weapon = new Pistol();
    }

    private void EquipWeapon()
    {
        _head.transform.localPosition = _weapons[_currentWeaponId].HeadPosition;
        _body.transform.localPosition = _weapons[_currentWeaponId].BodyPosition;
        _body.sprite = _weapons[_currentWeaponId].ArmedSprite;
        _shootingPoint.transform.localPosition = _weapons[_currentWeaponId].ShootingPoint;
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        _weapon.Shoot();
    }

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