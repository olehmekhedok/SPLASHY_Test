using UnityEngine;
using Zenject;

public class Mover : MonoBehaviour
{
    [Inject] private IGameConfig _config = default;
    [Inject] private IInputController _inputController = default;
    [Inject] private IGameController _gameController = default;

    private Vector3 _dragPosition;

    private void Awake()
    {
        _gameController.OnStartMatch += OnStartMatch;
        _gameController.OnNextPlatform += OnNextPlatform;
        _gameController.OnResetMatch += OnResetMatch;
        _inputController.OnDrag += OnDrag;
    }

    private void OnStartMatch()
    {
        UpdateZ();
    }

    private void OnNextPlatform(PlatformType obj)
    {
        UpdateZ();
    }

    private void OnResetMatch()
    {
        LeanTween.cancel(gameObject);
        transform.position = Vector3.zero;
        _dragPosition = Vector3.zero;
    }

    private void UpdateZ()
    {
        if (_gameController.IsPause)
            return;

        var nextPosition = _gameController.NextPlatformIndex * _config.PlatformGap;
        var from = transform.localPosition.z;
        LeanTween.value(gameObject, c => _dragPosition.z = c, from, nextPosition, _config.BallSpeed * 2f);
    }

    private void OnDrag(float positionX)
    {
        var position = transform.localPosition;
        position.x += (positionX * _config.InputSpeed);
        _dragPosition.x = position.x;
    }

    private void Update()
    {
        if (_gameController.IsPause)
            return;

        var position = transform.localPosition;
        position.x = _dragPosition.x;
        position.z = _dragPosition.z;

        transform.localPosition = position;
    }
}
