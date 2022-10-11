using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerEquipment))]
public class PlayerInputSystem : MonoBehaviour
{
    public PlayerInputActions InputActions { get; private set; }

    private void Awake()
    {
        InputActions = new PlayerInputActions();
        InputActions.Enable();
    }

    private void OnDisable()
    {
        InputActions.Disable();
    }
}