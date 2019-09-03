using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class BonusCell : MonoBehaviour
{
    [SerializeField] private Text _days = default;
    [SerializeField] private Text _reward = default;
    [SerializeField] private GameObject _check = default;
    [SerializeField] private Image _bg = default;
    [SerializeField] private Color _complete = default;

    [Inject] private IBonusesController _bonusesController = default;

    private bool _isReady;

    public int Hours { get; private set; }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(CollectBonus);
        _check.SetActive(false);
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
        LeanTween.value(gameObject, c => _bg.color = c, Color.white, _complete, 1f).setLoopPingPong(-1);
    }

    private void CollectBonus()
    {
        if (_isReady)
        {
            _check.SetActive(true);
            _bonusesController.Collect(Hours);
            _isReady = false;
            GetComponent<Image>().color = Color.white;
            LeanTween.cancel(gameObject);
        }
    }
}
    