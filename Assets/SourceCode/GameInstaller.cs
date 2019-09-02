using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameController>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<InputController>().FromComponentInHierarchy().AsSingle();
        Container.BindInterfacesAndSelfTo<LevelsController>().AsSingle();
        Container.BindInterfacesAndSelfTo<ProgressController>().AsSingle().Lazy();
        Container.Bind<IGameConfig>().To<GameConfig>().FromScriptableObjectResource(typeof(GameConfig).ToString()).AsSingle().Lazy();

        Container.BindMemoryPool<Platform, Platform.Pool>()
            .WithInitialSize(30)
            .FromComponentInNewPrefabResource(Const.Platform)
            .UnderTransformGroup("Platforms");
    }
}


public class BonusController : IBonusController
{
    [Inject] private IGameController _gameController = default;

    public BonusController()
    {
        _gameController.OnResetMatch += OnResetMatch;
    }

    private void OnResetMatch()
    {
        throw new System.NotImplementedException();
    }
}

public interface IBonusController
{
}
