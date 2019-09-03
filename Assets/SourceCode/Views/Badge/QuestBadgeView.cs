using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Image))]
public class QuestBadgeView : BaseBadgeView
{
    [Inject] private IQuestsController _questsController = default;

    protected override bool IsReady => _questsController.IsCurrentQuestsCompleted;

    protected override void OnStart()
    {
        _questsController.OnRewardCollected += CheckBonusReady;
        _questsController.OnQuestCompleted += c => CheckBonusReady();
    }
}