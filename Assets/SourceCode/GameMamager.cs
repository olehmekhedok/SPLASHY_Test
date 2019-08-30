using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameMamager : MonoBehaviour
{
    public static string Abyss = "Abyss";
    public static string Platform = "Platform";

    public static float Speed = 5;

    public Camera Camera;
    public MeshRenderer Ball;

    public Color32[] Colors = default;

    private Color32 curColor32;

    public static GameMamager instance { get; protected set; }

    public bool Pause
    {
        get => _pause;
        set
        {
            if (value == false && _pause)
            {
                FindObjectOfType<BallAnimation2>().StartAnimation(1);
            }

            _pause = value;
        }
    }

    private bool _pause = true;

    [ContextMenu("Do Something")]
    public void Awake()
    {
        PlatfromLine[] lines = transform.GetComponentsInChildren<PlatfromLine>();
        var start = 0f;

        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];

            var cutpos = line.transform.localPosition;
            line.transform.localPosition = new Vector3(cutpos.x, 0, start);
            start += Speed;

            if (i > 5)
                line.transform.LeanSetLocalPosY(5f);

            foreach (Platform platform in line.Platfroms)
            {
                platform.Color = GecRandomColor();
                platform.LineIndex = i;
                var lp = platform.transform.localPosition;
                platform.transform.localPosition = new Vector3(lp.x, 0, 0);

                platform.GetComponent<MeshRenderer>().material.color = platform.Color;
            }
        }

        ChoseColor(1);
        instance = this;
    }

    public void ChoseColor(int index)
    {
        PlatfromLine[] lines = transform.GetComponentsInChildren<PlatfromLine>();
        PlatfromLine line = lines[index];
        int i = Random.Range(0, line.Platfroms.Length);
        curColor32 = line.Platfroms[i].Color;

        if (lines.Length > index + 5)
        {
            lines[index + 5].transform.LeanMoveLocalY(0, 1.5f).setEaseOutBounce();
        }

        LeanTween.value(Camera.gameObject, c => Ball.sharedMaterial.color = curColor32, Camera.backgroundColor, curColor32, 0.15f);
    }

    public Color32 GecRandomColor()
    {
        var index = Random.Range(0, Colors.Length);
        return Colors[index];
    }

    public void CheckColor(Platform platf)
    {
#if UNITY_EDITOR
        ChoseColor(platf.LineIndex + 1);
        return;
#endif
        if (curColor32.a == platf.Color.a && curColor32.b == platf.Color.b && curColor32.g == platf.Color.g && curColor32.r == platf.Color.r)
        {
            ChoseColor(platf.LineIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}