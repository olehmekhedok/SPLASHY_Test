using UnityEngine;
using UnityEngine.SceneManagement;

public class BallAnimation2 : MonoBehaviour
{
    public float Speed;
    private LTDescr ds;
    public GameObject Camera;

    private void OnTriggerEnter(Collider other)
    {
        if(GameMamager.instance.Pause)
            return;

        if(ds != null)
            LeanTween.descr(ds.id);

        if (other.tag == GameMamager.Platform)
        {
            var platform = other.GetComponent<Platform>();
            
            if (platform != null)
            {
                GameMamager.instance.CheckColor(platform);
                StartAnimation(platform.LineIndex + 1);
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
    }

    private void MoveBack()
    {
       ds =  transform.LeanMoveLocalY(0, Speed).setEaseInQuad();
    }

    void Update()
    {
        var curpos =  Camera.transform.position;
        curpos.z = transform.position.z;

        Camera.transform.position = curpos;
    }
}