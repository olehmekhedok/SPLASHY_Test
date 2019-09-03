using UnityEngine;
using Zenject;

[RequireComponent(typeof(CanvasGroup))]
public class TransitionWindow : WindowBase
{
    [Inject] private IGameController _gameController = default;
    private CanvasGroup _canvas;

    private CanvasGroup Canvas => _canvas ?? (_canvas = GetComponent<CanvasGroup>());

    public override WindowType Type => WindowType.Transition;

    private void Start()
    {
        transform.SetSiblingIndex(int.MaxValue);
    }

    private void OnEnable()
    {
        Canvas.blocksRaycasts = true;
        Canvas.LeanAlpha(1, 0.3f).setOnComplete(FadeOut);
    }

    private void Hide()
    {
        Canvas.blocksRaycasts = false;
        Canvas.alpha = 0f;
        gameObject.SetActive(false);
    }

    private void FadeOut()
    {
        _gameController.ResetMatch();
        Canvas.LeanAlpha(0, 0.3f).setOnComplete(Hide);
    }
}