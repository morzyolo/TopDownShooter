using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _currentSpeed = 2.5f;
    [SerializeField] private float _normalSpeed = 2.5f;
    [SerializeField] private float _increasedSpeed = 4f;

    private PlayerInputActions _inputActions;

    private Vector2 _direction;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _inputActions = GetComponent<PlayerInputSystem>().InputActions;
    }

    private void Update()
    {
        _direction = _inputActions.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rigidbody.MovePosition(_rigidbody.position + _direction * _currentSpeed * Time.fixedDeltaTime);
    }

    private void SetIncreasedSpeed(InputAction.CallbackContext context) => _currentSpeed = _increasedSpeed;
    private void SetNormalSpeed(InputAction.CallbackContext context) => _currentSpeed = _normalSpeed;

    private void OnEnable()
    {
        _inputActions.Player.SpeedUp.started += SetIncreasedSpeed;
        _inputActions.Player.SpeedUp.canceled += SetNormalSpeed;
    }

    private void OnDisable()
    {
        _inputActions.Player.SpeedUp.started -= SetIncreasedSpeed;
        _inputActions.Player.SpeedUp.canceled -= SetNormalSpeed;
    }
}