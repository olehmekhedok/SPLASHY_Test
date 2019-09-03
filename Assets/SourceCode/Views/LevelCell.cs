using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class LevelCell : MonoBehaviour
{
    [SerializeField] private Text _textId = default;
    [SerializeField] private Color _active = default;
    [SerializeField] private Color _inactive = default;
    [SerializeField] private Image[] _stars = default;

    [Inject] private ILevelsController _levelsController = default;

    private int _levelId;

    private void Awake()
    {
        var button = GetComponent<Button>();
        button?.onClick.AddListener(OnLevelClick);
    }

    public void SetId(int id, bool isActive)
    {
        _levelId = id;
        _textId.text = (id + 1).ToString();

        var image = GetComponent<Image>();
        var button = GetComponent<Button>();

        button.interactable = isActive;

        if (isActive)
        {
            UpdateStars(Random.Range(1, _stars.Length));
            image.color = Color.white;
        }
        else
        {
            UpdateStars(0);
            image.color = _inactive;
        }
    }

    private void UpdateStars(int stars)
    {
        for (var i = 0; i < _stars.Length; i++)
            _stars[i].color = stars > i ? _active : _inactive;
    }

    private void OnLevelClick()
    {
        _levelsController.SelectLevel(_levelId);
    }
}
