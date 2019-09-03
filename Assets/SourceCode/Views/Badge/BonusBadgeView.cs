using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Image))]
public class BonusBadgeView : BaseBadgeView
{
    [Inject] private IBonusesController _bonusesController = default;

    protected override bool IsReady => _bonusesController.IsBonusReady;

    protected override void OnStart()
    {
        _bonusesController.OnRewardCollected += CheckBonusReady;
    }
}
