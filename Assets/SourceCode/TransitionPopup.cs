using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(CanvasGroup))]
[RequireComponent(typeof(Image))]
public class TransitionPopup : MonoBehaviour
{
    [Inject] private IGameController _gameController = default;
    private CanvasGroup _canvas;

    private CanvasGroup Canvas => _canvas ?? (_canvas = GetComponent<CanvasGroup>());

    private void Awake()
    {
        _gameController.OnFinishMatch += OnFinishMatch;
        transform.SetSiblingIndex(int.MaxValue);

        Canvas.blocksRaycasts = false;
        Canvas.alpha = 0f;
    }

    private void OnFinishMatch(bool succeed)
    {
        FadeIn();
    }

    private void FadeIn()
    {
        Canvas.blocksRaycasts = true;
        Canvas.LeanAlpha(1, 0.3f).setOnComplete(FadeOut);
    }

    private void FadeOut()
    {
        _gameController.ResetMatch();
        Canvas.LeanAlpha(0, 0.3f).setOnComplete(() => { Canvas.blocksRaycasts = false; });
    }
}
