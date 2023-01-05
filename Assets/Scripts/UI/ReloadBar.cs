using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ReloadBar : MonoBehaviour
{
    [SerializeField] private Image _progressBar;
    [SerializeField] private Image _background;

    private Coroutine _timerCoroutine;

    private void Start()
    {
        BarSetActive(false);
    }

    public void StartReload(float seconds)
    {
        BarSetActive(true);
        _progressBar.fillAmount = 1f;

        _timerCoroutine = StartCoroutine(StartÑountdown(seconds));
    }

    private IEnumerator StartÑountdown(float seconds)
    {
        float remainingSeconds = seconds;

        while (remainingSeconds > 0f)
        {
            _progressBar.fillAmount = remainingSeconds / seconds;
            yield return null;
            remainingSeconds -= Time.deltaTime;
        }

        BarSetActive(false);
    }

    public void CancelReload()
    {
        StopCoroutine(_timerCoroutine);
        BarSetActive(false);
    }
    
    private void BarSetActive(bool value)
    {
        _progressBar.enabled = value;
        _background.enabled = value;
    }
}