using UnityEngine;
using Zenject;

public class BottomBarView : MonoBehaviour
{
    [Inject] private IGameController _gameController = default;

    private void Awake()
    {
        _gameController.OnStartMatch += OnStartMatch;
        _gameController.OnResetMatch += OnResetMatch;
    }

    private void OnStartMatch()
    {
        LeanTween.moveY(gameObject, transform.position.y - 300f, 0.2f);
    }

    private void OnResetMatch()
    {
        LeanTween.moveY(gameObject, transform.position.y + 300f, 0.2f);
    }
}
