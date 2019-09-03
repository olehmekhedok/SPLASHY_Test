using UnityEditor;
using UnityEngine;
using Zenject;

[CustomEditor(typeof(PlatformView))]
public class PlatformEditor : Editor
{
    private IGameConfig _config;
    private MaterialPropertyBlock _propBlock;

    public IGameConfig Config => _config ?? (_config = StaticContext.Container.Resolve<IGameConfig>());

    private void OnEnable()
    {
        _propBlock = new MaterialPropertyBlock();
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var platformType = (PlatformType) serializedObject.FindProperty("_type").enumValueIndex;
        var color = Config.GetPlatformColorBy(platformType);

        var platform = (PlatformView) target;
        var renderer = platform.GetComponent<Renderer>();
        _propBlock.SetColor("_Color", color);
        renderer.SetPropertyBlock(_propBlock);
    }
}
