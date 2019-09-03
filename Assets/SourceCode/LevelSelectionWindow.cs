using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelSelectionWindow : WindowBase
{
    [SerializeField] private Button _closeButton = default;
    [SerializeField] private GameObject _content = default;

    [Inject] private IGameConfig _config = default;
    [Inject] private IWindowsController _windowsController = default;

    public override WindowType Type => WindowType.Levels;

    private void Start()
    {
        _closeButton.onClick.AddListener(CloseWindow);
        CreateLevels();
    }

    //TODO temp simple solution
    private void CreateLevels()
    {
        var levelCell = Resources.Load<LevelCell>(Const.LevelCell);

        const int totalLevels = 20;
        const int activeLevels = 11;

        var configs = _config.LevelConfigs.ToList();

        for (int i = 0; i < totalLevels; i++)
        {
            var index = i % configs.Count;
            var cell = Instantiate(levelCell, _content.transform);
            cell.SetId(configs[index].Id, i < activeLevels);
        }
    }

    private void CloseWindow()
    {
        _windowsController.WindowRequest(Type, false);
    }
}
