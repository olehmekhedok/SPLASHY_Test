using UnityEngine;

public class BallAnimation : MonoBehaviour
{
    public Rigidbody rb;

    public float storedVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        rb.velocity = new Vector3(0, storedVelocity, 0);

        var platftom = other.GetComponent<Platform>();

        if (platftom != null)
        {
            GameMamager.instance.CheckColor(platftom);
        }
    }
}