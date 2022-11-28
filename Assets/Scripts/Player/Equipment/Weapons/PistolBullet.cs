using System.Threading.Tasks;
using UnityEngine;

public class PistolBullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _flightTime;

    private void Update()
    {
        transform.Translate(_speed * Time.deltaTime * Vector2.right);
    }

    private async void WaitFlightTime()
    {
        await Task.Delay((int)(_flightTime * 1000));
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.TryGetComponent<IDamageable>(out var enemy))
        {
            enemy.TakeDamage(10);
        }
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        WaitFlightTime();
    }
}