using UnityEngine;

public class InputMamager : MonoBehaviour
{
    public float speed;

    public GameObject ball;
    public GameObject camera;

    private Vector3 _delta;

    private bool pressed;

    void Update()
    {

#if UNITY_EDITOR

        if (Input.GetMouseButtonDown(0))
        {
            GameMamager.instance.Pause = false;
            _delta = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            UpdatePos(Input.mousePosition - _delta);
            _delta = Input.mousePosition;
        }
#endif
        if (Input.touchCount == 1)
        {
            var t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began)
            {
                if(pressed == false)
                    GameMamager.instance.Pause = false;

                _delta = t.position;

                pressed = true;
            }
            else if (t.phase == TouchPhase.Moved)
            {
                Vector3 pos = t.position;
                UpdatePos(pos - _delta);
                _delta = t.position;
            }
            else  if (t.phase == TouchPhase.Ended)
            {
                pressed = false;
            }
        }
    }

    private void UpdatePos(Vector3 inputPosition)
    {
        if( GameMamager.instance.Pause)
            return;

        Vector3 bp = ball.transform.localPosition;
        bp.x += (inputPosition.x * speed);
        ball.transform.localPosition = bp;

        var cp = camera.transform.localPosition;
        cp.x += (inputPosition.x * speed);
        camera.transform.localPosition = cp;
    }
}