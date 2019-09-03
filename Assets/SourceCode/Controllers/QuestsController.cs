using System;
using System.Linq;
using Zenject;

public interface IQuestsController
{
    event Action<QuestsSetConfig> OnQuestCompleted;
    event Action OnRewardCollected;
    QuestsSetConfig GetCurrentQuests { get; }
    bool IsCurrentQuestsCompleted { get; }
    void CollectReward();
}

//TODO it's a simple implementation
public class QuestsController : IQuestsController
{
    [Inject] private IQuestsConfig _questsConfig = default;
    [Inject] private IProgressController _progressController = default;

    private int _currentQuestIndex;

    public event Action<QuestsSetConfig> OnQuestCompleted;
    public event Action OnRewardCollected;

    public QuestsSetConfig GetCurrentQuests => _questsConfig.GetQuests.ElementAt(_currentQuestIndex);

    public bool IsCurrentQuestsCompleted
    {
        get
        {
            foreach (var quest in GetCurrentQuests.Quests)
            {
                var isCompleted = false;

                switch (quest.Type)
                {
                    case QuestType.CollectCrystal:
                        isCompleted = _progressController.Crystals >= quest.Amount;
                        break;

                    case QuestType.CollectScore:
                        isCompleted = _progressController.TotalScore >= quest.Amount;
                        break;

                    case QuestType.FinishLevel:
                        isCompleted =_progressController.Levels >= quest.Amount;
                        break;
                }

                if (isCompleted == false)
                    return false;
            }

            return true;
        }
    }

    public QuestsController(IProgressController progressController)
    {
        _progressController = progressController;

        _progressController.OnTotalScore += OnProgressChanged;
        _progressController.OnCrystals += OnProgressChanged;
        _progressController.OnLevels += OnProgressChanged;
    }

    public void CollectReward()
    {
        if (IsCurrentQuestsCompleted)
        {
            _progressController.AddCrystals(GetCurrentQuests.Reward);

            ++_currentQuestIndex;
            var count = _questsConfig.GetQuests.Count();

            if (_currentQuestIndex >= count)
                _currentQuestIndex = count - 1;

            OnRewardCollected?.Invoke();
        }
    }

    private void OnProgressChanged(int value)
    {
        if (IsCurrentQuestsCompleted)
            OnQuestCompleted?.Invoke(GetCurrentQuests);
    }
}
