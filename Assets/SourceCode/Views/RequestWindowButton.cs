using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class RequestWindowButton : MonoBehaviour
{
    [SerializeField] private WindowType _type = default;
    [Inject] private IWindowsController _windowsController = default;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _windowsController.WindowRequest(_type);
    }
}
