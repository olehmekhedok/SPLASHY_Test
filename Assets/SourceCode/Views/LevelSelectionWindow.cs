using UnityEngine;
using UnityEngine.UI;
using Zenject;

//TODO temp simple solution
public class LevelSelectionWindow : WindowBase
{
    [SerializeField] private Button _closeButton = default;
    [SerializeField] private GameObject _content = default;

    [Inject] private IWindowsController _windowsController = default;

    public override WindowType Type => WindowType.Levels;

    private void Start()
    {
        _closeButton.onClick.AddListener(CloseWindow);
        CreateLevels();
    }

    private void CreateLevels()
    {
        var levelCell = Resources.Load<LevelCell>(Const.LevelCell);

        const int totalLevels = 20;
        const int activeLevels = 11;

        for (int i = 0; i < totalLevels; i++)
        {
            var cell = Instantiate(levelCell, _content.transform);
            cell.SetId(i, i < activeLevels);
        }
    }

    private void CloseWindow()
    {
        _windowsController.WindowRequest(Type, false);
    }
}
