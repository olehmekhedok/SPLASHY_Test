using UnityEngine;
using UnityEngine.UI;

public class MissionCell : MonoBehaviour
{
    [SerializeField] private Text _amount = default;
    [SerializeField] private Text _type = default;
    [SerializeField] private Image _progress = default;

    public MissionType Type { get; private set; }
    public int Amount { get; private set; }

    public void InitMission(MissionConfig config)
    {
        Type = config.Type;
        Amount = config.Amount;
        _type.text = config.Title;
    }

    public void UpdateProgress(int current)
    {
        _amount.text = current + "/" + Amount;
        _progress.fillAmount = current / (float) Amount;
    }
}
