using System;
using Zenject;

public interface IProgressController
{
    int TotalScore { get; }
    int CurrentScore { get; }
    int Crystals { get; }

    event Action<int> OnTotalScore;
    event Action<int> OnCurrentScore;
    event Action<int> OnCrystals;
}

public class ProgressController : IProgressController
{
    private IGameController _gameController;

    public int TotalScore { get; private set; }
    public int CurrentScore { get; private set; }
    public int Crystals { get; private set; }

    public event Action<int> OnTotalScore;
    public event Action<int> OnCurrentScore;
    public event Action<int> OnCrystals;

    [Inject]
    public ProgressController(IGameController gameController)
    {
        _gameController = gameController;
        _gameController.OnNextPlatform += OnNextPlatform;

        _gameController.OnStartMatch += OnStartMatch;
        _gameController.OnFinishMatch += OnFinishMatch;
    }

    private void OnStartMatch()
    {
        CurrentScore = 0;
        OnCurrentScore?.Invoke(CurrentScore);
    }

    private void OnFinishMatch()
    {
        TotalScore += CurrentScore;
        OnTotalScore?.Invoke(TotalScore);
    }

    private void OnNextPlatform(PlatformType type)
    {
        UpdateScore();
    }

    private void UpdateScore()
    {
        CurrentScore = _gameController.NextPlatformIndex - 1;
        OnCurrentScore?.Invoke(CurrentScore);
    }
}
