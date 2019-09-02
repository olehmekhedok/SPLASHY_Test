using UnityEngine;
using UnityEngine.UI;

public class MissionCell : MonoBehaviour
{
    [SerializeField] private Text _amount;
    [SerializeField] private Text _type;

    public MissionType Type { get; private set; }
    public int Amount { get; private set; }

    public void InitMission(MissionConfig config)
    {
        Type = config.Type;
        Amount = config.Amount;
        _type.text = Type.ToString();
    }

    public void UpdateProgress(int current)
    {
        _amount.text = current + "/" + Amount;
    }
}
