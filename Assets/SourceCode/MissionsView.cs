using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MissionsView : MonoBehaviour
{
    [SerializeField] private Button _showContent = default;
    [SerializeField] private Button _hideContent = default;
    [SerializeField] private GameObject _content = default;

    [Inject] private IMissionsController _missionsController = default;
    [Inject] private IProgressController _progressController;

    private readonly List<MissionCell> _missions = new List<MissionCell>();

    private void Awake()
    {
        _showContent.onClick.AddListener(ShowContent);
        _hideContent.onClick.AddListener(HideContent);

        CreateMissionsView();
        _missionsController.OnMissionCompleted += OnMissionCompleted;
    }

    private void ShowContent()
    {
        _content.SetActive(true);

        foreach (var mission in _missions)
        {
            var amount = 0;
            switch (mission.Type)
            {
                case MissionType.CollectCrystal:
                    amount = _progressController.Crystals;
                    break;

                case MissionType.JumpOnPlatform:
                    amount = _progressController.TotalScore;
                    break;

                case MissionType.FinishLevel:
                    amount = _progressController.Levels;
                    break;
            }

            mission.UpdateProgress(amount);
        }
    }

    private void HideContent()
    {
        _content.SetActive(false);
    }

    private void OnMissionCompleted(MissionConfig config)
    {
        var mission = _missions.First(m => m.Type == config.Type);
        mission.InitMission(config);
    }

    private void CreateMissionsView()
    {
        var template = Resources.Load<MissionCell>(Const.MissionCell);
        foreach (var config in _missionsController.GetCurrentMissions.Missions)
        {
            var cell = Instantiate(template, _content.transform);
            cell.InitMission(config);
            _missions.Add(cell);
        }
    }
}
