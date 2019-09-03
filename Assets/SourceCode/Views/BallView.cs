using UnityEngine;
using Zenject;

public class BallView : MonoBehaviour
{
    [Inject] private IGameController _gameController = default;
    [Inject] private IGameConfig _config = default;

    private Renderer _renderer;

    private Renderer Renderer => _renderer ?? (_renderer = GetComponentInChildren<Renderer>());

    private void Awake()
    {
        _gameController.OnNextPlatform += OnNextPlatform;
        _gameController.OnStartMatch += OnStartMatch;

        OnNextPlatform(_gameController.NextPlatformType);
    }

    private void OnStartMatch()
    {
        PlayJumpAnimation();
    }

    private void OnNextPlatform(PlatformType type)
    {
        var color = _config.GetPlatformColorBy(type);
        Renderer.material.color = color;
    }

    private void OnTriggerEnter(Collider other)
    {
        LeanTween.cancel(gameObject);

        if (_gameController.IsPause)
            return;

        if (_gameController.CheckTriggeredObject(other.tag))
        {
            var platform = other.GetComponent<PlatformView>();

            if (platform != null && _gameController.CheckColor(platform.Type))
            {
                PlayJumpAnimation();
                platform.PlayDisappearAnimation();
            }
        }
        else
        {
            PlayFallDownAnimation();
        }
    }

    private void PlayJumpAnimation()
    {
        var speed = _config.BallSpeed;

        transform.LeanMoveLocalY(3, speed)
            .setEaseOutQuad()
            .setOnComplete(() => transform.LeanMoveLocalY(-1f, speed).setEaseInQuad());
    }

    private void PlayFallDownAnimation()
    {
        transform.LeanMoveLocalY(-100, 5);
    }
}
