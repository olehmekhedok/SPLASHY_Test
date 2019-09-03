using System;
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
        Container.BindInterfacesAndSelfTo<MissionsController>().AsSingle().Lazy();

        Container.Bind<IGameConfig>().To<GameConfig>().FromScriptableObjectResource(typeof(GameConfig).ToString()).AsSingle().Lazy();
        Container.Bind<IBonusesConfig>().To<BonusesConfig>().FromScriptableObjectResource(typeof(BonusesConfig).ToString()).AsSingle().Lazy();
        Container.Bind<IMissionsConfig>().To<MissionsConfig>().FromScriptableObjectResource(typeof(MissionsConfig).ToString()).AsSingle().Lazy();

        Container.BindMemoryPool<Platform, Platform.Pool>()
            .WithInitialSize(30)
            .FromComponentInNewPrefabResource(Const.Platform)
            .UnderTransformGroup("Platforms");
    }
}