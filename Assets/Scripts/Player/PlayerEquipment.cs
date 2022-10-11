using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEquipment : MonoBehaviour
{
    private IWeapon _weapon;

    private PlayerInputActions _inputActions;

    private void Awake()
    {
        _inputActions = GetComponent<PlayerInputSystem>().InputActions;
        _weapon = new Pistol();
    }

    private void Shoot(InputAction.CallbackContext context)
    {
        _weapon.Shoot();
    }

    private void OnEnable()
    {
        _inputActions.Player.Shoot.started += Shoot;
    }

    private void OnDisable()
    {
        _inputActions.Player.Shoot.started -= Shoot;
    }
}