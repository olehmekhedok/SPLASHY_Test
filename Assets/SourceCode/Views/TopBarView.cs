using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TopBarView : MonoBehaviour
{
    [SerializeField] private Text _score = default;
    [SerializeField] private Text _crystals = default;
    [SerializeField] private Text _totalScore = default;

    [Inject] private IProgressController _progressController = default;
    [Inject] private IGameController _gameController = default;

    private Vector3 _initPosition;

    private void Awake()
    {
        _progressController.OnCurrentScore += OnCurrentScore;
        _progressController.OnTotalScore += OnTotalScore;
        _progressController.OnCrystals += OnCrystals;

        _gameController.OnStartMatch += OnStartMatch;
        _gameController.OnResetMatch += OnResetMatch;

        _initPosition = _totalScore.transform.position;

        OnResetMatch();
    }

    private void OnStartMatch()
    {
        LeanTween.moveY(_score.gameObject, _initPosition.y, 0.2f);
        LeanTween.moveY(_totalScore.gameObject, _initPosition.y + 300f, 0.2f);
    }

    private void OnResetMatch()
    {
        LeanTween.moveY(_score.gameObject, _initPosition.y + 300f, 0.2f);
        LeanTween.moveY(_totalScore.gameObject, _initPosition.y, 0.2f);
    }

    private void OnCrystals(int crystals)
    {
        var from = int.Parse(_crystals.text);
        LeanTween.value(_crystals.gameObject, c => _crystals.text = ((int) c).ToString(), from, crystals, 0.5f);
        _crystals.transform.LeanScale(new Vector3(1.3f, 1.3f, 1.3f), 0.5f).setEaseShake();
    }

    private void OnTotalScore(int score)
    {
        _totalScore.text = $"Total Score{Environment.NewLine}<color=#978679>{score}</color>";
    }

    private void OnCurrentScore(int score)
    {
        _score.text = $"Score{Environment.NewLine}<color=#978679>{score}</color>";
    }
}
