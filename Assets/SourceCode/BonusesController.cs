using System.Linq;
using UnityEngine;

public interface IBonusesController
{
    float NextBonusTime { get; }
    void Collect(int hours);
}

//TODO it's a simple implementation
public class BonusesController : IBonusesController
{
    private IBonusesConfig _bonusesConfig;
    private IProgressController _progressController;

    public float NextBonusTime
    {
        get
        {
            var config = _bonusesConfig.GetBonuses.First();
            return (config.Hours * 60f * 60f) - Time.realtimeSinceStartup;
        }
    }

    public BonusesController(IBonusesConfig bonusesConfig, IProgressController progressController)
    {
        _bonusesConfig = bonusesConfig;
        _progressController = progressController;
    }

    public void Collect(int hours)
    {
        var config = _bonusesConfig.GetBonuses.First(b => b.Hours == hours);
        _progressController.AddCrystals(config.Reward);
    }
}

//TODO it's a simple implementation