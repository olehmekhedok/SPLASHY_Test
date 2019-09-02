using System;
using UnityEngine;
using Zenject;

public interface IGameController
{
    bool CheckColor(PlatformType type);
    bool CheckTriggeredObject(string otherTag);
    void ResetMatch();
    int NextPlatformIndex { get; }
    bool IsPause { get; }
    PlatformType NextPlatformType { get; }
    event Action<PlatformType> OnNextPlatform;
    event Action OnStartMatch;
    event Action OnFinishMatch;
    event Action OnResetMatch;
}

public class GameController : MonoBehaviour, IGameController
{
    public event Action<PlatformType> OnNextPlatform;
    public event Action OnStartMatch;
    public event Action OnFinishMatch;
    public event Action OnResetMatch;

    [Inject] private IGameConfig _config = default;
    [Inject] private ILevelsController _levelsController = default;
    [Inject] private IInputController _inputController = default;

    public PlatformType NextPlatformType { get; private set; }
    public int NextPlatformIndex { get; private set; }
    public bool IsPause { get; private set; } = true;

    private void Awake()
    {
        _inputController.OnClick += OnClick;
        _levelsController.SelectLevel(1);
        NextPlatform(++NextPlatformIndex);
    }

    public void ResetMatch()
    {
        NextPlatformIndex = 1;
        NextPlatform(NextPlatformIndex);
        OnResetMatch?.Invoke();
    }

    public bool CheckTriggeredObject(string otherTag)
    {
        if (otherTag == Const.PlatformTag)
        {
            return true;
        }

        if (otherTag == Const.AbyssTag)
        {

            Debug.LogError("Abyss.");
            FinishMatch();
        }

        return false;
    }

    public bool CheckColor(PlatformType type)
    {
//#if UNITY_EDITOR
//        NextPlatform(++NextPlatformIndex);
//        return true;
//#endif
        if (type == NextPlatformType)
        {
            NextPlatform(++NextPlatformIndex);
            return true;
        }
        else
        {
            FinishMatch();
            return false;
        }
    }

    private void NextPlatform(int platformIndex)
    {
        var config = _config.GetLevelConfigBy(_levelsController.CurrentLevel);
        if (config.Path.Count > platformIndex)
        {
            NextPlatformType = config.Path[platformIndex];
            OnNextPlatform?.Invoke(NextPlatformType);
        }
        else
        {
            Debug.LogError("Won.");
            FinishMatch();
        }
    }

    private void FinishMatch()
    {
        IsPause = true;
        OnFinishMatch?.Invoke();
    }

    private void OnClick()
    {
        if (IsPause)
        {
            IsPause = false;
            OnStartMatch?.Invoke();
        }
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