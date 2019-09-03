using System;
using System.Linq;
using Zenject;

public interface IMissionsController
{
    event Action<MissionConfig> OnMissionCompleted;
    MissionsSetConfig GetCurrentMissions { get; }
}

//TODO it's a simple implementation
public class MissionsController : IMissionsController
{
    [Inject] private IMissionsConfig _missionsConfig = default;

    public event Action<MissionConfig> OnMissionCompleted;

    public MissionsSetConfig GetCurrentMissions => _missionsConfig.GetMissions.First();

    public MissionsController(IProgressController progressController)
    {
        progressController.OnTotalScore += OnTotalScore;
        progressController.OnCrystals += OnCrystals;
        progressController.OnLevels += OnLevels;
    }

    private void OnLevels(int levels)
    {
        var mission = GetMissionBy(MissionType.FinishLevel);
        if (mission.Amount <= levels)
        {
            OnMissionCompleted?.Invoke(mission);
        }
    }

    private void OnCrystals(int crystals)
    {
        var mission = GetMissionBy(MissionType.CollectCrystal);
        if (mission.Amount <= crystals)
        {
            OnMissionCompleted?.Invoke(mission);
        }
    }

    private void OnTotalScore(int score)
    {
        var mission = GetMissionBy(MissionType.CollectScore);
        if (mission.Amount <= score)
        {
            OnMissionCompleted?.Invoke(mission);
        }
    }

    private MissionConfig GetMissionBy(MissionType type)
    {
        return GetCurrentMissions.Missions.First(m => m.Type == type);
    }
}