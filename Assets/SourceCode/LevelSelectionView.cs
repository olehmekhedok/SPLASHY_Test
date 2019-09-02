using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelSelectionView : MonoBehaviour
{
    [SerializeField] private Button _showContent = default;
    [SerializeField] private Button _hideContent = default;
    [SerializeField] private GameObject _levelSelection = default;

    [Inject] private IGameConfig _config = default;
    [Inject] private IGameController _gameController = default;

    private void Awake()
    {
        _showContent.onClick.AddListener(ShowContent);
        _hideContent.onClick.AddListener(HideContent);

        CreateLevels();
    }

    private void CreateLevels()
    {
        var levelCell = Resources.Load<LevelCell>(Const.LevelCell);

        foreach (var levelConfig in _config.LevelConfigs)
        {
            var cell = Instantiate(levelCell, _levelSelection.transform);
            cell.SetId(levelConfig.Id);
        }
    }

    private void ShowContent()
    {
        _levelSelection.SetActive(true);
    }

    private void HideContent()
    {
        _levelSelection.SetActive(false);
    }
}
