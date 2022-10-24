using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _currentSpeed = 2.5f;
    [SerializeField] private float _normalSpeed = 2.5f;
    [SerializeField] private float _increasedSpeed = 4f;

    [SerializeField] private Transform _legsTransform;
    [SerializeField] private float _legsRotationSpeed;

    private PlayerInputActions _inputActions;
    private Rigidbody2D _rigidbody;
    private Animator _moveAnimator;

    private Vector2 _direction;

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
            _legsTransform.rotation = Quaternion.Lerp(
                _legsTransform.rotation,
                Quaternion.Euler(0f, 0f, Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg - 90f), 
                _legsRotationSpeed * Time.deltaTime);
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