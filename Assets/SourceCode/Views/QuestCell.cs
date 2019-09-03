using UnityEngine;
using UnityEngine.UI;

public class QuestCell : MonoBehaviour
{
    [SerializeField] private Text _amount = default;
    [SerializeField] private Text _type = default;
    [SerializeField] private Image _progress = default;

    public QuestType Type { get; private set; }

    public void InitQuest(QuestConfig config)
    {
        Type = config.Type;
        _type.text = config.Title;
    }

    public void UpdateProgress(int desired, int progress)
    {
        _amount.text = progress + "/" + desired;
        _progress.fillAmount = progress / (float) desired;
    }
}
