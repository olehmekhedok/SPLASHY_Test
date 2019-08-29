using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameMamager : MonoBehaviour
{
    public Camera Camera;
    public MeshRenderer Ball;

    public Color32[] Colors = default;


    private Color32 curColor32;

    public static GameMamager instance { get; protected set; }

    public void Awake()
    {
        foreach (Platfromarr platfromarr in FindObjectsOfType<Platfromarr>())
        {
            for (var i = 0; i < platfromarr.Platfroms.Length; i++)
            {
                var platform = platfromarr.Platfroms[i];

                platform.Color = GecRandomColor();
                platform.Index = i;

                platform.GetComponent<MeshRenderer>().material.color =  platform.Color;
            }
        }

        ChoseColor(0);
        instance = this;
    }

    private void ChoseColor(int index)
    {
        var pla = FindObjectsOfType<Platfromarr>();
        var i = Random.Range(0, pla.Length);
        var platfromarr =  pla[i];
        var platf = platfromarr.Platfroms[index + 1];

        curColor32 = platf.Color;

        Camera.backgroundColor = platf.Color;
        Ball.material.color =  platf.Color;
    }

    public Color32 GecRandomColor()
    {
        var imdex = Random.Range(0, Colors.Length);
        return Colors[imdex];
    }

    public void CheckColor(Platform platf)
    {
        if (curColor32.a == platf.Color.a && curColor32.b == platf.Color.b && curColor32.g == platf.Color.g && curColor32.r == platf.Color.r)
        {
            ChoseColor(platf.Index);
        }
    }
}