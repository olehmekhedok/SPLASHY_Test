using UnityEngine;

[ExecuteInEditMode]
public class Platfromarr : MonoBehaviour
{
    public float speed;

    public Platform[] Platfroms;

    void OnEnable()
    {
        var start = 0f;
        foreach (Platform child in Platfroms)
        {
            child.transform.localPosition = new Vector3(0, 0, start);
            start += speed;
        }
    }
}