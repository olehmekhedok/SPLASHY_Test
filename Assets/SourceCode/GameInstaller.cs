using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameManager>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<InputManager>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<LevelsController>().AsSingle();
        Container.Bind<IGameConfig>().To<GameConfig>().FromScriptableObjectResource(typeof(GameConfig).ToString()).AsSingle().Lazy();

        Container.BindMemoryPool<Platform, Platform.Pool>()
            .WithInitialSize(30)
            .FromComponentInNewPrefabResource(Const.Platform)
            .UnderTransformGroup("Platforms");
    }
}
