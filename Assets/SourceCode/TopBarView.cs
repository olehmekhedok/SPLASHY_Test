using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TopBarView : MonoBehaviour
{
    [SerializeField] private Text _score = default;

    [Inject] private IProgressController _progressController = default;
    [Inject] private IGameController _gameController = default;

    private void Awake()
    {
        _progressController.OnCurrentScore += OnCurrentScore;

        _gameController.OnStartMatch += OnStartMatch;
        _gameController.OnFinishMatch += OnFinishMatch;

        _score.gameObject.SetActive(false);
    }

    private void OnStartMatch()
    {
        _score.gameObject.SetActive(true);
    }

    private void OnFinishMatch()
    {
        _score.gameObject.SetActive(false);
    }

    private void OnCurrentScore(int score)
    {
        _score.text = "Score:" + score;
    }
}
