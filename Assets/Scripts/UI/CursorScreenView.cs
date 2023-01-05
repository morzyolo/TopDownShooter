using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CursorScreenView : MonoBehaviour
{
    [SerializeField] private Image _cursorImage;
    [SerializeField] private ReloadBar _reloadBar;
    [SerializeField] private WeaponObserver _weaponObserver;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        transform.position = Mouse.current.position.ReadValue();
    }

    private void StartShowingReload(float seconds) => _reloadBar.StartReload(seconds);

    private void StopShowingReload() => _reloadBar.CancelReload();

    private void OnEnable()
    {
        _weaponObserver.ReloadStarted += StartShowingReload;
        _weaponObserver.ReloadCanceled += StopShowingReload;
    }

    private void OnDisable() 
    {
        _weaponObserver.ReloadStarted -= StartShowingReload;
        _weaponObserver.ReloadCanceled -= StopShowingReload;
    }
}