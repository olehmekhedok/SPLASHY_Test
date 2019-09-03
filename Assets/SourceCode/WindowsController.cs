using UnityEngine;

public interface IWindowsController
{
    void WindowRequest(WindowType type, bool active = true);
}

public class WindowsController : MonoBehaviour, IWindowsController
{
    [SerializeField] private WindowBase[] _windows = default;

    public void WindowRequest(WindowType type, bool active = true)
    {
        foreach (var window in _windows)
        {
            if (window.Type == type)
            {
                window.gameObject.SetActive(active);
                break;
            }
        }
    }
}
