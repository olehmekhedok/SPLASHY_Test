using UnityEngine;

[ExecuteInEditMode]
public class PlatfromLine : MonoBehaviour
{
    public Platform[] Platfroms
    {
        get
        {
            return GetComponentsInChildren<Platform>();
        }
    }
}