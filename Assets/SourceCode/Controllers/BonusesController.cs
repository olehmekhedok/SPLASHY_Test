using System;
using System.Linq;
using UnityEngine;
using Zenject;

public interface IBonusesController
{
    event Action OnRewardCollected;
    float NextBonusTime { get; }
    bool IsBonusReady { get; }
    void Collect(int hours);
}

//TODO it's a simple implementation
public class BonusesController : IBonusesController
{
    [Inject] private IBonusesConfig _bonusesConfig = default;
    [Inject] private IProgressController _progressController = default;

    public event Action OnRewardCollected;

    public bool IsBonusReady { get; private set; } = true;

    public float NextBonusTime
    {
        get
        {
            var config = _bonusesConfig.GetBonuses.First();
            return (config.Hours * 60f * 60f) - Time.realtimeSinceStartup;
        }
    }

    public void Collect(int hours)
    {
        if (IsBonusReady)
        {
            IsBonusReady = false;
            var config = _bonusesConfig.GetBonuses.First(b => b.Hours == hours);
            _progressController.AddCrystals(config.Reward);

            OnRewardCollected?.Invoke();
        }
    }
}
