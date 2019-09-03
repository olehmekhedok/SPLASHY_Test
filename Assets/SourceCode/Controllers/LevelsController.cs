using System.Collections.Generic;
using Zenject;

public interface ILevelsController
{
    int CurrentLevel { get; }
    void SelectLevel(int levelId);
}

public class LevelsController : ILevelsController
{
    [Inject] private IGameConfig _gameConfig = default;
    [Inject] PlatformView.Pool _platformPool = default;

    private readonly List<PlatformView> _platforms = new List<PlatformView>();

    public int CurrentLevel { get; private set; }

    public LevelsController(IGameController gameController)
    {
        gameController.OnResetMatch += OnResetMatch;
        gameController.OnFinishMatch += OnFinishMatch;
    }

    private void OnFinishMatch(bool succeed)
    {
        if (succeed)
            ++CurrentLevel;
    }

    public void SelectLevel(int levelId)
    {
        CurrentLevel = levelId;
        UnLoadLevel();

        var configs = _gameConfig.GetLevelConfigBy(levelId);
        foreach (var config in configs.Platforms)
        {
            _platforms.Add(_platformPool.Spawn(config));
        }
    }

    private void UnLoadLevel()
    {
        foreach (var platform in _platforms)
        {
            _platformPool.Despawn(platform);
        }

        _platforms.Clear();
    }

    private void OnResetMatch()
    {
        SelectLevel(CurrentLevel);
    }
}
