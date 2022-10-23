using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _currentSpeed = 2.5f;
    [SerializeField] private float _normalSpeed = 2.5f;
    [SerializeField] private float _increasedSpeed = 4f;

    private PlayerInputActions _inputActions;
    private Animator _moveAnimator;

    private Vector2 _direction;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _moveAnimator = GetComponent<Animator>();
        _inputActions = GetComponent<PlayerInputSystem>().InputActions;
    }

    private void Update()
    {
        _direction = _inputActions.Player.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        if (_direction.sqrMagnitude > 0.9f)
        {
            _moveAnimator.SetBool("IsMove", true);
            Move();
        }
        else
            _moveAnimator.SetBool("IsMove", false);
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