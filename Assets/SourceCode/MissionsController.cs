using System;
using System.Linq;

public interface IMissionsController
{
    event Action<MissionConfig> OnMissionCompleted;
    MissionsSetConfig GetCurrentMissions { get; }
}

public class MissionsController : IMissionsController
{
    public event Action<MissionConfig> OnMissionCompleted;

    private IProgressController _progressController;
    private IMissionsConfig _missionsConfig;

    public MissionsSetConfig GetCurrentMissions => _missionsConfig.GetMissions.First();

    public MissionsController(IMissionsConfig missionsConfig, IProgressController progressController)
    {
        _progressController = progressController;
        _missionsConfig = missionsConfig;

        _progressController.OnTotalScore += OnTotalScore;
        _progressController.OnCrystals += OnCrystals;
        _progressController.OnLevels += OnLevels;
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