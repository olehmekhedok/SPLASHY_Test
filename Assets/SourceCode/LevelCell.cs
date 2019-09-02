using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelCell : MonoBehaviour
{
    [Inject] private ILevelsController _levelsController = default;
    [SerializeField] private Text _id;
    private int _levelId;

    private void Awake()
    {
        var button = GetComponent<Button>();
        button?.onClick.AddListener(OnLevelClick);
    }

    public void SetId(int id)
    {
        _levelId = id;
        _id.text = id.ToString("D3");
    }

    private void OnLevelClick()
    {
        _levelsController.SelectLevel(_levelId);
    }
}
