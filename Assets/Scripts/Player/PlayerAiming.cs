using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAiming : MonoBehaviour
{
    private void FixedUpdate()
    {
        #if PLATFORM_STANDALONE_WIN
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        LookAt(mousePosition);
        #endif
    }

    private void LookAt(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }
}