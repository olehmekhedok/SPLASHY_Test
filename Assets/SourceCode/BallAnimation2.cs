using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class BallAnimation2 : MonoBehaviour
{
    public GameObject Camera;
    [SerializeField] private ParticleSystem _fX;
    [SerializeField] private Renderer _renderer;

    [Inject] public IGameMamager _gameManager;
    [Inject] private IGameConfig _config;
    [Inject] private IInputManager _inputManager;

    private void Awake()
    {
        _gameManager.OnNextPlatform += OnNextPlatform;
        _gameManager.OnPause += OnPause;
        _inputManager.OnDrag += OnDrag;

        OnNextPlatform(_gameManager.NextPlatformType);
    }

    private void OnDrag(float positionX)
    {
        var position = transform.localPosition;
        position.x += (positionX * _config.InputSpeed);
        transform.localPosition = position;
    }

    private void OnNextPlatform(PlatformType type)
    {
        var color = _config.GetPlatformColorBy(type);
        LeanTween.value(gameObject, c => _renderer.material.color = c, _renderer.material.color, color, 0.15f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_gameManager.Pause)
            return;

        LeanTween.cancel(transform.gameObject);

        if (other.tag == Const.PlatformTag)
        {
            var platform = other.GetComponent<Platform>();

            if (platform != null)
            {
                _gameManager.CheckColor(platform.Type);
                PlayJumpAnimation();

                var fx = Instantiate(_fX, platform.transform, true);
                fx.GetComponent<Renderer>().sharedMaterial.color = platform.Color;
                fx.Play();

                platform.transform
                    .LeanMoveLocalY(platform.transform.localPosition.y - 0.1f, 0.2f)
                    .setEaseShake()
                    .setOnComplete(() => { platform.transform.LeanMoveLocalY(platform.transform.localPosition.y - 10f, 2f); });
            }
        }

        if (other.tag == Const.AbyssTag)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnPause(bool pause)
    {
        if (pause == false)
            PlayJumpAnimation();
    }

    public void PlayJumpAnimation()
    {
        var speed = _config.BallSpeed;
        var nextPosition = _gameManager.NextPlatformIndex * _config.PlatformGap;
        transform.LeanMoveLocalZ(nextPosition, _config.BallSpeed * 2f);
        transform.LeanMoveLocalY(3, speed).setEaseOutQuad()
            .setOnComplete(() => transform.LeanMoveLocalY(0, speed).setEaseInQuad());
    }

    private void Update()
    {
        var curpos = Camera.transform.position;
        curpos.z = transform.position.z;

        Camera.transform.position = curpos;
    }
}
