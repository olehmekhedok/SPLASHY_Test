using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class TopBarView : MonoBehaviour
{
    [SerializeField] private Text _score = default;
    [SerializeField] private Text _crystals = default;

    [Inject] private IProgressController _progressController = default;
    [Inject] private IGameController _gameController = default;

    private void Awake()
    {
        _progressController.OnCurrentScore += OnCurrentScore;
        _progressController.OnCrystals += OnCrystals;

        _gameController.OnStartMatch += OnStartMatch;
        _gameController.OnFinishMatch += OnFinishMatch;

        _score.gameObject.SetActive(false);
    }

    private void OnCrystals(int crystals)
    {
        var from = int.Parse(_crystals.text);
        LeanTween.value(_crystals.gameObject, c => _crystals.text = ((int) c).ToString(), from, crystals, 0.5f);
        _crystals.transform.LeanScale(new Vector3(1.3f, 1.3f, 1.3f), 0.5f).setEaseShake();
    }

    private void OnStartMatch()
    {
        _score.gameObject.SetActive(true);
    }

    private void OnFinishMatch(bool succeed)
    {
        _score.gameObject.SetActive(false);
    }

    private void OnCurrentScore(int score)
    {
        _score.text = "Score:" + score;
    }
}
