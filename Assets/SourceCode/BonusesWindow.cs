using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BonusesWindow : WindowBase
{
    [SerializeField] private Button _closeButton = default;
    [SerializeField] private GameObject _content = default;
    [SerializeField] private Text _nextReward = default;

    [Inject] private IBonusesController _bonusesController = default;
    [Inject] private IBonusesConfig _bonusesConfig = default;
    [Inject] private IWindowsController _windowsController = default;

    private readonly List<BonusCell> _bonuses = new List<BonusCell>();

    public override WindowType Type => WindowType.Bonus;

    private void Start()
    {
        _closeButton.onClick.AddListener(CloseWindow);
        CreateBonusesView();
    }

    private void CloseWindow()
    {
        _windowsController.WindowRequest(Type, false);
    }

    private void CreateBonusesView()
    {
        var template = Resources.Load<BonusCell>(Const.BonusCell);

        foreach (var config in _bonusesConfig.GetBonuses)
        {
            var cell = Instantiate(template, _content.transform);
            cell.Init(config);
            _bonuses.Add(cell);
        }

        _bonuses.First().SetReady(true);
    }

    private void Update()
    {
        var time = TimeSpan.FromSeconds(_bonusesController.NextBonusTime).ToString(@"dd\.hh\:mm\:ss");
        _nextReward.text = $"Next Reward in: <color=#FFFFFF>{time}</color>";
    }
}
