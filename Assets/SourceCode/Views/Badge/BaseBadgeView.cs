using UnityEngine;
using UnityEngine.UI;
using Zenject;

public abstract class BaseBadgeView : MonoBehaviour
{
    [SerializeField] private Image _badge = default;
    [Inject] private IGameController _gameController = default;

    protected abstract bool IsReady { get; }

    protected abstract void OnStart();

    private void Start()
    {
        _gameController.OnResetMatch += CheckBonusReady;
        OnStart();
    }

    protected void CheckBonusReady()
    {
        var isReady = IsReady;

        if (IsReady)
            _badge.transform.LeanScale(new Vector3(1.3f, 1.3f, 1.3f), 0.5f).setEaseShake();

        _badge.gameObject.SetActive(isReady);
    }
}
