using UnityEngine;
using Zenject;

public class PlatformView : MonoBehaviour
{
    public class Pool : MonoMemoryPool<PlatformConfig, PlatformView>
    {
        protected override void Reinitialize(PlatformConfig config, PlatformView foo)
        {
            foo.Reset(config);
        }
    }

    [SerializeField] private PlatformType _type;
    [SerializeField] private ParticleSystem _vfx = default;

    [Inject] private IGameConfig _gameConfig = default;
    private Material _material;

    public PlatformType Type => _type;

    private Material Material => _material ?? (_material = GetComponent<Renderer>().material);

    public void PlayDisappearAnimation()
    {
        transform
            .LeanMoveLocalY(transform.localPosition.y - 0.1f, 0.2f)
            .setEaseShake()
            .setOnComplete(() => { transform.LeanMoveLocalY(transform.localPosition.y - 10f, 2f); });

        SpawnVFX();
    }

    private void SpawnVFX()
    {
        var color = _gameConfig.GetPlatformColorBy(Type);
        _vfx.GetComponent<Renderer>().sharedMaterial.color = color;
        _vfx.Play();
    }

    private void Reset(PlatformConfig config)
    {
        LeanTween.cancel(gameObject);

        transform.position = config.Position;
        var color = _gameConfig.GetPlatformColorBy(config.Type);

        Material.color = color;
        _type = config.Type;
    }
}
