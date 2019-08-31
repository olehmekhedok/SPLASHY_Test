using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public interface IGameMamager
{
    void CheckColor(PlatformType type);
    bool Pause { get; set; }
    int NextPlatformIndex { get; }
    PlatformType NextPlatformType{ get; }
    event Action<PlatformType> OnNextPlatform;
    event Action<bool> OnPause;
}

public class GameManager : MonoBehaviour, IGameMamager
{
    [Inject] private IGameConfig _config;
    [Inject] private ILevelsController _levelsController;

    private bool _pause = true;
    private int _currentLevel = 1;

    public PlatformType NextPlatformType { get; private set; }
    public int NextPlatformIndex { get; private set; }

    public event Action<PlatformType> OnNextPlatform;
    public event Action<bool> OnPause;

    public bool Pause
    {
        get => _pause;
        set
        {
            if (value != _pause)
                OnPause?.Invoke(value);

            _pause = value;
        }
    }

    private void Awake()
    {
        _levelsController.LoadLevel(_currentLevel);
        NextPlatform(++NextPlatformIndex);
    }

    public void CheckColor(PlatformType type)
    {
#if UNITY_EDITOR
        NextPlatform(++NextPlatformIndex);
        return;
#endif
        if (type == NextPlatformType)
        {
            NextPlatform(++NextPlatformIndex);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    private void NextPlatform(int platformIndex)
    {
        var config = _config.GetLevelConfigBy(_currentLevel);
        NextPlatformType = config.Path[platformIndex];
        OnNextPlatform?.Invoke(NextPlatformType);
    }
}



//    [ContextMenu("Do Something")]
//    public void Awake()
//    {
//        PlatfromLine[] lines = transform.GetComponentsInChildren<PlatfromLine>();
//        var start = 0f;
//
//        for (var i = 0; i < lines.Length; i++)
//        {
//            var line = lines[i];
//
//            var cutpos = line.transform.localPosition;
//            line.transform.localPosition = new Vector3(cutpos.x, 0, start);
//            start += Speed;
//
//            if (i > 5)
//                line.transform.LeanSetLocalPosY(5f);
//
//            foreach (Platform platform in line.Platfroms)
//            {
//                platform.Color = GecRandomColor();
//                platform.LineIndex = i;
//                var lp = platform.transform.localPosition;
//                platform.transform.localPosition = new Vector3(lp.x, 0, 0);
//
//                platform.GetComponent<MeshRenderer>().material.color = platform.Color;
//            }
//        }
//
//        ChoseColor(1);
//    }



//    public void ChoseColor(int index)
//    {
//        PlatfromLine[] lines = transform.GetComponentsInChildren<PlatfromLine>();
//        PlatfromLine line = lines[index];
//        int i = Random.Range(0, line.Platfroms.Length);
//        curColor32 = line.Platfroms[i].Color;
//
//        if (lines.Length > index + 5)
//        {
//            lines[index + 5].transform.LeanMoveLocalY(0, 1.5f).setEaseOutBounce();
//        }
//
//       // LeanTween.value(Camera.gameObject, c => Ball.sharedMaterial.color = curColor32, Camera.backgroundColor, curColor32, 0.15f);
//    }