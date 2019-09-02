using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class BonusesView : MonoBehaviour
{
    [SerializeField] private Button _showContent = default;
    [SerializeField] private Button _hideContent = default;
    [SerializeField] private GameObject _content = default;
    [SerializeField] private Text _nextReward = default;

    [Inject] private IBonusesController _bonusesController = default;
    [Inject] private IBonusesConfig _bonusesConfig = default;

    private readonly List<BonusCell> _bonuses = new List<BonusCell>();

    private void Awake()
    {
        _showContent.onClick.AddListener(ShowContent);
        _hideContent.onClick.AddListener(HideContent);

        _bonusesController.OnBonusReady += OnBonusReady;
        CreateBonusesView();
    }

    private void ShowContent()
    {
        _showContent.gameObject.SetActive(false);
        _content.SetActive(true);
    }

    private void HideContent()
    {
        _showContent.gameObject.SetActive(true);
        _content.SetActive(false);
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
    }

    private void OnBonusReady(BonusConfig config)
    {
        LeanTween.delayedCall(0.5f, () =>
        {
            ShowContent();
            var bonus = _bonuses.First(b => b.Hours == config.Hours);
            bonus.SetReady(true);
        });
    }

    private void Update()
    {
        _nextReward.text = "Next Reward in:" + TimeSpan.FromSeconds(_bonusesController.NextBonusTime).ToString(@"dd\.hh\:mm\:ss");
    }
}
