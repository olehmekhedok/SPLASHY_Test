using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IGameConfig
{
    int PlatformGap { get; }
    float BallSpeed { get; }
    float InputSpeed { get; }
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
    [SerializeField] private float _ballSpeed;
    [SerializeField] private float _inputSpeed;
    [SerializeField] private int _platformGap;
    [SerializeField] private PlatformColorConfig[] _platformColors;
    private readonly Dictionary<int, LevelConfig> _levels = new Dictionary<int, LevelConfig>();

    public int PlatformGap => _platformGap;
    public float BallSpeed => _ballSpeed;
    public float InputSpeed => _inputSpeed;

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
        _levels.TryGetValue(levelId, out var config);
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