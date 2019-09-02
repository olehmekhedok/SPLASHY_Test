using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] _uiObjects = default;
    [SerializeField] private Text _totalScore = default;

    [Inject] private IProgressController _progressController = default;
    [Inject] private IGameController _gameController = default;

    private void Awake()
    {
        _progressController.OnTotalScore += OnTotalScore;
        OnTotalScore(_progressController.TotalScore);

        _gameController.OnStartMatch += OnStartMatch;
        _gameController.OnResetMatch += OnFinishMatch;
    }

    private void OnTotalScore(int score)
    {
        _totalScore.text = "Total Score:" + score;
    }

    private void OnStartMatch()
    {
        SetContentActive(false);
    }

    private void OnFinishMatch()
    {
        SetContentActive(true);
    }

    private void SetContentActive(bool isActive)
    {
        foreach (var uiObject in _uiObjects)
        {
            uiObject.SetActive(isActive);
        }
    }
}
