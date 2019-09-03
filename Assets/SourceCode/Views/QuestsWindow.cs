using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class QuestsWindow : WindowBase
{
    [SerializeField] private Button _closeButton = default;
    [SerializeField] private Button _collectButton = default;
    [SerializeField] private GameObject _content = default;
    [SerializeField] private Text _reward = default;

    [Inject] private IQuestsController _questsController = default;
    [Inject] private IProgressController _progressController = default;
    [Inject] private IWindowsController _windowsController = default;

    private readonly List<QuestCell> _quests = new List<QuestCell>();

    public override WindowType Type => WindowType.Quests;

    private void Start()
    {
        _closeButton.onClick.AddListener(CloseWindow);
        _collectButton.onClick.AddListener(CollectReward);
        _questsController.OnQuestCompleted += OnQuestCompleted;

        CreateQuestsViews();
    }

    private void OnEnable()
    {
        UpdateProgress();
    }

    private void CreateQuestsViews()
    {
        var template = Resources.Load<QuestCell>(Const.QuestCell);
        var quests = _questsController.GetCurrentQuests;
        _reward.text = quests.Reward.ToString();

        foreach (var config in quests.Quests)
        {
            var cell = Instantiate(template, _content.transform);
            cell.InitQuest(config);
            _quests.Add(cell);
        }

        UpdateProgress();
    }

    private void UpdateProgress()
    {
        foreach (var quest in _questsController.GetCurrentQuests.Quests)
        {
            var cell = _quests.FirstOrDefault(q => q.Type == quest.Type);

            if (cell == null)
                continue;

            var progress = 0;
            switch (cell.Type)
            {
                case QuestType.CollectCrystal:
                    progress = _progressController.Crystals;
                    break;

                case QuestType.CollectScore:
                    progress = _progressController.TotalScore;
                    break;

                case QuestType.FinishLevel:
                    progress = _progressController.Levels;
                    break;
            }

            cell.UpdateProgress(quest.Amount, progress);
        }

        _collectButton.interactable = _questsController.IsCurrentQuestsCompleted;
    }

    private void CloseWindow()
    {
        _windowsController.WindowRequest(Type, false);
    }

    private void OnQuestCompleted(QuestsSetConfig config)
    {
        _collectButton.interactable = true;
    }

    private void CollectReward()
    {
        _questsController.CollectReward();
        _collectButton.interactable = false;
    }
}
