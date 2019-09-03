using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IGameConfig
{
    int PlatformGap { get; }
    float BallSpeed { get; }
    float InputSpeed { get; }
    IEnumerable<LevelConfig> LevelConfigs { get; }
    Color GetPlatformColorBy(PlatformType type);
    LevelConfig GetLevelConfigBy(int levelId);
}

[Serializable]
public struct PlatformColorConfig
{
    public PlatformType Type;
    public Color Color;
}

public class GameConfig : SingletonScriptableObject<GameConfig>, IGameConfig
{
    [SerializeField] private float _ballSpeed = default;
    [SerializeField] private float _inputSpeed = default;
    [SerializeField] private int _platformGap = default;
    [SerializeField] private PlatformColorConfig[] _platformColors = default;
    private readonly Dictionary<int, LevelConfig> _levels = new Dictionary<int, LevelConfig>();

    public int PlatformGap => _platformGap;
    public float BallSpeed => _ballSpeed;
    public float InputSpeed => _inputSpeed;

    public IEnumerable<LevelConfig> LevelConfigs => _levels.Values;

    private void OnEnable()
    {
        LoadLevels();
    }

    public Color GetPlatformColorBy(PlatformType type)
    {
        var config = _platformColors.FirstOrDefault(p => p.Type == type);
        return config.Color;
    }

    public LevelConfig GetLevelConfigBy(int levelId)
    {
        var index = levelId % _levels.Count;
        _levels.TryGetValue(index + 1, out var config);
        return config;
    }

    private void LoadLevels()
    {
        for (int i = 1; i < int.MaxValue; i++)
        {
            var textAsset = Resources.Load<TextAsset>(Const.ToLevelConfigName(i));
            if (textAsset == null)
                break;

            var config = JsonUtility.FromJson<LevelConfig>(textAsset.text);
            _levels.Add(config.Id, config);
        }
    }
}