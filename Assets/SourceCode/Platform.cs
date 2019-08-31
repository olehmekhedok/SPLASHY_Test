using UnityEngine;
using Zenject;

public class Platform : MonoBehaviour
{
    public class Pool : MonoMemoryPool<PlatformConfig, Platform>
    {
        protected override void Reinitialize(PlatformConfig velocity, Platform foo)
        {
            foo.Reset(velocity);
        }
    }

    [SerializeField] private PlatformType _type;
    [Inject] private IGameConfig _config;

    private Material _material;
    public Color32 Color;
    public int LineIndex;

    private Material Material
    {
        get
        {
            if (_material == null)
                _material = GetComponent<Renderer>().material;
            return _material;
        }
    }

    public PlatformType Type => _type;

    public void SetType(PlatformType type)
    {
        _type = type;
    }

    public void SetColor(Color color)
    {
        Material.color = color;
    }

    private void Reset(PlatformConfig config)
    {
        transform.position = config.Position;
        var color = _config.GetPlatformColorBy(config.Type);
        SetColor(color);
        SetType(config.Type);
    }
}