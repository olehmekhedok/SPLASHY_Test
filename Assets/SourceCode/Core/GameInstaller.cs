using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameController>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<InputController>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<WindowsController>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<LevelsController>().AsSingle();
        Container.BindInterfacesAndSelfTo<ProgressController>().AsSingle().Lazy();
        Container.BindInterfacesAndSelfTo<BonusesController>().AsSingle().Lazy();
        Container.BindInterfacesAndSelfTo<QuestsController>().AsSingle().Lazy();

        Container.Bind<IGameConfig>().To<GameConfig>().FromScriptableObjectResource(typeof(GameConfig).ToString()).AsSingle().Lazy();
        Container.Bind<IBonusesConfig>().To<BonusesConfig>().FromScriptableObjectResource(typeof(BonusesConfig).ToString()).AsSingle().Lazy();
        Container.Bind<IQuestsConfig>().To<QuestsConfig>().FromScriptableObjectResource(typeof(QuestsConfig).ToString()).AsSingle().Lazy();

        Container.BindMemoryPool<PlatformView, PlatformView.Pool>()
            .WithInitialSize(30)
            .FromComponentInNewPrefabResource(Const.Platform)
            .UnderTransformGroup("Platforms");
    }
}