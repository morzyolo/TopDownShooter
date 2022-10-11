using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _smoothTime = 0.1f;

    private Vector3 _velocity;
    private float _distance = -5f;

    private void Update()
    {
        Vector3 targetPosition = new Vector3(_player.position.x, _player.position.y, _distance);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
    }
}