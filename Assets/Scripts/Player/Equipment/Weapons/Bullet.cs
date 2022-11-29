using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _flightTime;

    private Task _waiting;
    private CancellationTokenSource _cancellationTokenSource;

    private void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector2.right);
    }

    private async Task WaitFlightTime()
    {
        await Task.Delay((int)(_flightTime * 1000), _cancellationTokenSource.Token);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent<IDamageable>(out var enemy))
        {
            enemy.TakeDamage(10);
        }
        _cancellationTokenSource.Cancel();
    }

    private void OnEnable()
    {
        _cancellationTokenSource = new CancellationTokenSource();
        _waiting = WaitFlightTime();
    }

    private void OnDisable()
    {
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
    }
}