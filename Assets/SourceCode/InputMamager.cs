using UnityEngine;

public class InputMamager : MonoBehaviour
{
    public float speed;

    public GameObject ball;
    public Camera camera;

    private Vector3 _delta;

    void Update()
    {

#if UNITY_EDITOR

        if (Input.GetMouseButtonDown(0))
        {
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
                _delta = t.position;
            }
            else if (t.phase == TouchPhase.Moved)
            {
                Vector3 pos = t.position;
                UpdatePos(pos - _delta);
                _delta = t.position;
            }
        }
    }

    private void UpdatePos(Vector3 inputPosition)
    {
    //    inputPosition.z = 10.0f;
 //       inputPosition = Camera.main.ScreenToWorldPoint(inputPosition);

        var bp = ball.transform.localPosition;

        bp.x += (inputPosition.x * speed);

        ball.transform.localPosition = bp;
    }
}