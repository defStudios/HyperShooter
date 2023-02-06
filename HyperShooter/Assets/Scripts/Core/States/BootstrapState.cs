using Core.Factories;
using Core.Workflow;
using Core.Services;
using Core.Scenes;
using Core.Assets;
using Core.Input;

namespace Core.States
{
    public class BootstrapState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly ITickRunner _tickRunner;
        private readonly IFixedTickRunner _fixedTickRunner;
        private readonly ISceneLoader _sceneLoader;
        private readonly IAssetsDatabase _assetsDatabase;
        private readonly ServiceManager _services;

        public BootstrapState(StateMachine stateMachine, ITickRunner tickRunner, IFixedTickRunner fixedTickRunner, 
            ISceneLoader sceneLoader, IAssetsDatabase assetsDatabase, ServiceManager services)
        {
            _stateMachine = stateMachine;
            _tickRunner = tickRunner;
            _fixedTickRunner = fixedTickRunner;
            _sceneLoader = sceneLoader;
            _assetsDatabase = assetsDatabase;
            _services = services;
            
            RegisterServices();
        }

        public void Enter()
        {
            _services.Single<ISceneLoader>().Load(_services.Single<IAssetsDatabase>().BootstrapSceneName, EnterLoadLevel);
        }
        
        public void Exit() { }

        private void RegisterServices()
        {
            ServiceManager.Container.RegisterSingle<ITickRunner>(_tickRunner);
            ServiceManager.Container.RegisterSingle<IFixedTickRunner>(_fixedTickRunner);
            
            ServiceManager.Container.RegisterSingle<IInput>(new InputController());
            ServiceManager.Container.RegisterSingle<ISceneLoader>(_sceneLoader);
            ServiceManager.Container.RegisterSingle<IAssetsDatabase>(_assetsDatabase);
            ServiceManager.Container.RegisterSingle<IAssetsProvider>(new AssetsProvider(ServiceManager.Container.Single<IAssetsDatabase>()));
            ServiceManager.Container.RegisterSingle<IGameFactory>(new GameFactory(ServiceManager.Container.Single<IAssetsProvider>()));
        }
        
        private void EnterLoadLevel()
        {
            _stateMachine.Enter<LoadLevelState, LevelData>(_services.Single<IAssetsDatabase>().Levels[0]);
        }
    }
}
