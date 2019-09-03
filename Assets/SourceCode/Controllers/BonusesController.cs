using System.Linq;
using UnityEngine;
using Zenject;

public interface IBonusesController
{
    float NextBonusTime { get; }
    void Collect(int hours);
}

//TODO it's a simple implementation
public class BonusesController : IBonusesController
{
    [Inject] private IBonusesConfig _bonusesConfig = default;
    [Inject] private IProgressController _progressController = default;

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
        var config = _bonusesConfig.GetBonuses.First(b => b.Hours == hours);
        _progressController.AddCrystals(config.Reward);
    }
}