using System;
using System.Linq;
using UnityEngine;

public interface IBonusesController
{
    float NextBonusTime { get; }
    event Action<BonusConfig> OnBonusReady;
    void Collect(int hours);
}

//TODO it's a simple implementation
public class BonusesController : IBonusesController
{
    public event Action<BonusConfig> OnBonusReady;
    private IBonusesConfig _bonusesConfig;
    private IProgressController _progressController;

    private bool _isCollected;

    public float NextBonusTime
    {
        get
        {
            var config = _bonusesConfig.GetBonuses.First();
            return (config.Hours * 60f * 60f) - Time.realtimeSinceStartup;
        }
    }

    public BonusesController(IGameController gameController, IBonusesConfig bonusesConfig, IProgressController progressController)
    {
        _bonusesConfig = bonusesConfig;
        _progressController = progressController;
        gameController.OnResetMatch += OnResetMatch;
    }

    public void Collect(int hours)
    {
        _isCollected = true;
        var config = _bonusesConfig.GetBonuses.First(b => b.Hours == hours);
        _progressController.AddCrystals(config.Reward);
    }

    private void OnResetMatch()
    {
        if(_isCollected)
            return;

        var bonus = _bonusesConfig.GetBonuses.First();
        OnBonusReady?.Invoke(bonus);
    }
}


//TODO it's a simple implementation