using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MissionsWindow : WindowBase
{
    [SerializeField] private Button _closeButton = default;
    [SerializeField] private GameObject _content = default;
    [SerializeField] private Text _reward = default;

    [Inject] private IMissionsController _missionsController = default;
    [Inject] private IProgressController _progressController = default;
    [Inject] private IWindowsController _windowsController = default;

    private readonly List<MissionCell> _missions = new List<MissionCell>();

    public override WindowType Type => WindowType.Missions;

    private void Start()
    {
        _closeButton.onClick.AddListener(CloseWindow);
        _missionsController.OnMissionCompleted += OnMissionCompleted;

        CreateMission();
    }

    private void OnEnable()
    {
        UpdateProgress();
    }

    private void UpdateProgress()
    {
        foreach (var mission in _missions)
        {
            var amount = 0;
            switch (mission.Type)
            {
                case MissionType.CollectCrystal:
                    amount = _progressController.Crystals;
                    break;

                case MissionType.CollectScore:
                    amount = _progressController.TotalScore;
                    break;

                case MissionType.FinishLevel:
                    amount = _progressController.Levels;
                    break;
            }

            mission.UpdateProgress(amount);
        }
    }

    private void CloseWindow()
    {
        _windowsController.WindowRequest(Type, false);
    }

    private void OnMissionCompleted(MissionConfig config)
    {
        var mission = _missions.First(m => m.Type == config.Type);
        mission.InitMission(config);
    }

    private void CreateMission()
    {
        var template = Resources.Load<MissionCell>(Const.MissionCell);
        var missions =_missionsController.GetCurrentMissions;
        _reward.text = missions.Reward.ToString();

        foreach (var config in missions.Missions)
        {
            var cell = Instantiate(template, _content.transform);
            cell.InitMission(config);
            _missions.Add(cell);
        }

        UpdateProgress();
    }
}
