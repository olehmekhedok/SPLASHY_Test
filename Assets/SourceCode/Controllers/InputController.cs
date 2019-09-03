using System;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IInputController
{
    event Action<float> OnDrag;
    event Action OnClick;
}

public class InputController : MonoBehaviour,
    IInputController,
    IPointerClickHandler,
    IDragHandler
{
    public event Action<float> OnDrag;
    public event Action OnClick;

    private void Awake()
    {
        transform.SetSiblingIndex(0);
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        OnDrag?.Invoke(eventData.delta.x);
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke();
    }
}
