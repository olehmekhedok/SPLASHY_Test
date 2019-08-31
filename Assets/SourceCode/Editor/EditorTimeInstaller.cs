using UnityEditor;
using Zenject;

[InitializeOnLoad]
public class EditorTimeInstaller : EditorStaticInstaller<EditorTimeInstaller>
{
    static EditorTimeInstaller()
    {
        Install();
    }

    public override void InstallBindings()
    {
        Container.Bind<IGameConfig>().To<GameConfig>().FromScriptableObjectResource(typeof(GameConfig).ToString()).AsSingle().Lazy();
    }
}
