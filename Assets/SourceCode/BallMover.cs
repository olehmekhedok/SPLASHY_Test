using UnityEngine;

public class BallMover : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

}