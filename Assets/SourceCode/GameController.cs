using System;
using UnityEngine;
using Zenject;

public interface IGameController
{
    bool CheckColor(PlatformType type);
    bool CheckTriggeredObject(string otherTag);
    void ResetMatch();
    int NextPlatformIndex { get; }
    bool IsPause { get; }
    PlatformType NextPlatformType { get; }
    event Action<PlatformType> OnNextPlatform;
    event Action OnStartMatch;
    event Action<bool> OnFinishMatch;
    event Action OnResetMatch;
}

public class GameController : MonoBehaviour, IGameController
{
    public event Action<PlatformType> OnNextPlatform;
    public event Action OnStartMatch;
    public event Action<bool> OnFinishMatch;
    public event Action OnResetMatch;

    [Inject] private IGameConfig _config = default;
    [Inject] private ILevelsController _levelsController = default;
    [Inject] private IInputController _inputController = default;
    [Inject] private IWindowsController _windowsController = default;

    public PlatformType NextPlatformType { get; private set; }
    public int NextPlatformIndex { get; private set; }
    public bool IsPause { get; private set; } = true;

    private void Awake()
    {
        _inputController.OnClick += OnClick;
        _levelsController.SelectLevel(0);
        NextPlatform(++NextPlatformIndex);
    }

    public void ResetMatch()
    {
        NextPlatformIndex = 1;
        NextPlatform(NextPlatformIndex);
        OnResetMatch?.Invoke();
    }

    public bool CheckTriggeredObject(string otherTag)
    {
        if (otherTag == Const.PlatformTag)
        {
            return true;
        }

        if (otherTag == Const.AbyssTag)
        {

            Debug.LogError("Abyss.");
            FinishMatch(false);
        }

        return false;
    }

    public bool CheckColor(PlatformType type)
    {
        if (type == NextPlatformType)
        {
            NextPlatform(++NextPlatformIndex);
            return true;
        }

        FinishMatch(false);
        return false;
    }

    private void NextPlatform(int platformIndex)
    {
        var config = _config.GetLevelConfigBy(_levelsController.CurrentLevel);
        if (config.Path.Count > platformIndex)
        {
            NextPlatformType = config.Path[platformIndex];
            OnNextPlatform?.Invoke(NextPlatformType);
        }
        else
        {
            Debug.LogError("Won.");
            FinishMatch(true);
        }
    }

    private void FinishMatch(bool succeed)
    {
        IsPause = true;
        _windowsController.WindowRequest(WindowType.Transition);
        OnFinishMatch?.Invoke(succeed);
    }

    private void OnClick()
    {
        if (IsPause)
        {
            IsPause = false;
            OnStartMatch?.Invoke();
        }
    }
}
