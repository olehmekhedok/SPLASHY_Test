using UnityEngine;
using UnityEngine.SceneManagement;

public class BallAnimation2 : MonoBehaviour
{
    public float Speed;
    private LTDescr ds;
    public GameObject Camera;
    public ParticleSystem FX;

    private void OnTriggerEnter(Collider other)
    {
        if (GameMamager.instance.Pause)
            return;

        if (ds != null)
            LeanTween.descr(ds.id);

        if (other.tag == GameMamager.Platform)
        {
            var platform = other.GetComponent<Platform>();

            if (platform != null)
            {
                GameMamager.instance.CheckColor(platform);
                StartAnimation(platform.LineIndex + 1);

                var fx = Instantiate(FX, platform.transform, true);
                fx.GetComponent<Renderer>().sharedMaterial.color = platform.Color;
                fx.Play();

                platform.transform
                    .LeanMoveLocalY(platform.transform.localPosition.y - 0.1f, 0.2f)
                    .setEaseShake()
                    .setOnComplete(() =>
                    {
                        platform.transform.LeanMoveLocalY(platform.transform.localPosition.y - 10f, 2f);
                    });
            }
        }

        if (other.tag == GameMamager.Abyss)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void StartAnimation(int index)
    {
        var lines = GameMamager.instance.transform.GetComponentsInChildren<PlatfromLine>();
        var line = lines[index];
        transform.LeanMoveLocalZ(line.transform.position.z, Speed * 2f);
        transform.LeanMoveLocalY(3, Speed).setEaseOutQuad().setOnComplete(MoveBack);

        transform.LeanScaleY(0.8f, 0.08f).setLoopPingPong(1);
    }

    private void MoveBack()
    {
       ds =  transform.LeanMoveLocalY(0, Speed).setEaseInQuad();
    }

    private void Update()
    {
        var curpos =  Camera.transform.position;
        curpos.z = transform.position.z;

        Camera.transform.position = curpos;
    }
}