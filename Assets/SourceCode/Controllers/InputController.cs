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
    IPointerDownHandler,
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

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        OnClick?.Invoke();
    }
}
