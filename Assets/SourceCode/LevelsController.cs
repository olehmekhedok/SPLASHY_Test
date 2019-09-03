using System.Collections.Generic;
using Zenject;

public interface ILevelsController
{
    void SelectLevel(int levelId);
    int CurrentLevel { get; }
}

public class LevelsController : ILevelsController
{
    [Inject] private IGameConfig _config = default;
    [Inject] Platform.Pool _platformPool = default;

    private readonly List<Platform> _platforms = new List<Platform>();

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

        var configs = _config.GetLevelConfigBy(levelId);
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
