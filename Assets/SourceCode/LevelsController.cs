using System;
using System.Collections.Generic;
using Zenject;

public interface ILevelsController
{
    event Action<int> OnLevelLoad;
    void LoadLevel(int levelId);
}

public class LevelsController : ILevelsController
{
    [Inject] private IGameConfig _config;
    [Inject] Platform.Pool _platformPool;

    private int _currentLevel;
    public event Action<int> OnLevelLoad;

    private readonly List<Platform> _platforms = new List<Platform>();

    public void LoadLevel(int levelId)
    {
        var confg = _config.GetLevelConfigBy(levelId);

        foreach (var platform in confg.Platforms)
        {
            _platforms.Add(_platformPool.Spawn(platform));
        }
    }

    public void UnLoadLevel()
    {
        foreach (var platform in _platforms)
        {
            _platformPool.Despawn(platform);
        }

        _platforms.Clear();
    }
}
