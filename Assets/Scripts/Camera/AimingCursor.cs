using UnityEngine;

public class AimingCursor : MonoBehaviour
{
    [SerializeField] private Texture2D[] _cursorTextures;
    [SerializeField] private float _frameRate;

    private float _timer;
    private int _frameCount;
    private int _currentFrame;

    private Vector2 _hotspot;

    private void Start()
    {
        _frameCount = _cursorTextures.Length;
        _currentFrame = 0;
        _timer = _frameRate;
        _hotspot = new Vector2(16, 16);
        Cursor.SetCursor(_cursorTextures[0], _hotspot, CursorMode.Auto);
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0f)
        {
            _timer += _frameRate;
            _currentFrame = (_currentFrame + 1) % _frameCount;
            Cursor.SetCursor(_cursorTextures[_currentFrame], _hotspot, CursorMode.Auto);
        }
    }
}