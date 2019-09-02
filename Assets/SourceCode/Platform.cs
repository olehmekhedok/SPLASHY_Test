using UnityEngine;
using Zenject;

public class Platform : MonoBehaviour
{
    public class Pool : MonoMemoryPool<PlatformConfig, Platform>
    {
        protected override void Reinitialize(PlatformConfig config, Platform foo)
        {
            foo.Reset(config);
        }
    }

    [SerializeField] private PlatformType _type;
    [SerializeField] private ParticleSystem _vfx = default;

    [Inject] private IGameConfig _config = default;
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
        var color = _config.GetPlatformColorBy(Type);
        _vfx.GetComponent<Renderer>().sharedMaterial.color = color;
        _vfx.Play();
    }

    private void Reset(PlatformConfig config)
    {
        LeanTween.cancel(gameObject);

        transform.position = config.Position;
        var color = _config.GetPlatformColorBy(config.Type);

        Material.color = color;
        _type = config.Type;
    }
}
