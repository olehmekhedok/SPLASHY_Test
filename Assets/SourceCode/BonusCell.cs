using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class BonusCell : MonoBehaviour
{
    [SerializeField] private Text _days = default;
    [SerializeField] private Text _reward = default;
    [SerializeField] private Color _complete = default;

    [Inject] private IBonusesController _bonusesController = default;

    private bool _isReady;

    public int Hours { get; private set; }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(CollectBonus);
    }

    public void Init(BonusConfig bonus)
    {
        Hours = bonus.Hours;
        _days.text = "Day" + TimeSpan.FromHours(Hours).Days;
        _reward.text = bonus.Reward.ToString();
    }

    public void SetReady(bool isReady)
    {
        _isReady = isReady;
    }

    private void CollectBonus()
    {
        if (_isReady)
        {
            _bonusesController.Collect(Hours);
            GetComponent<Image>().color = _complete;
        }
    }
}
