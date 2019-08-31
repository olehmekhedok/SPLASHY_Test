using System;
using UnityEngine;
using Zenject;

public interface IInputManager
{
    event Action<float> OnDrag;
}

public class InputManager : MonoBehaviour, IInputManager
{
    public event Action<float> OnDrag;

    [Inject] private IGameMamager _gameManager;

    private Vector3 _delta;
    private bool _pressed;

    private void Update()
    {
#if UNITY_EDITOR

        if (Input.GetMouseButtonDown(0))
        {
            _gameManager.Pause = false;
            _delta = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            if( _gameManager.Pause == false)
                 OnDrag?.Invoke(Input.mousePosition.x - _delta.x);

            _delta = Input.mousePosition;
        }
#endif
        if (Input.touchCount == 1)
        {
            var t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                if(_pressed == false)
                    _gameManager.Pause = false;

                _delta = t.position;

                _pressed = true;
            }
            else if (t.phase == TouchPhase.Moved)
            {
                Vector3 pos = t.position;

                if( _gameManager.Pause == false)
                    OnDrag?.Invoke(pos.x - _delta.x);

                _delta = t.position;
            }
            else  if (t.phase == TouchPhase.Ended)
            {
                _pressed = false;
            }
        }
    }

    private void UpdatePos(Vector3 inputPosition)
    {
//        if( GameMamager.Pause)
//            return;
//
//        Vector3 bp = ball.transform.localPosition;
//        bp.x += (inputPosition.x * _config.InputSpeed);
//        ball.transform.localPosition = bp;
//
//        var cp = camera.transform.localPosition;
//        cp.x += (inputPosition.x * _config.InputSpeed);
//        camera.transform.localPosition = cp;
    }
}