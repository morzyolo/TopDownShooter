using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
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