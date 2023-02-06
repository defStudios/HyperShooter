using Core.Services;
using Core.Workflow;
using Core.Assets;
using Core.States;
using Core.Scenes;

namespace Core
{
    public class Game
    {
        public StateMachine StateMachine { get; private set; }
        
        public Game(ICoroutineRunner coroutineRunner, ITickRunner tickRunner, IFixedTickRunner fixedTickRunner, 
            IAssetsDatabase assetsDatabase)
        {
            var sceneLoader = new SceneLoader(coroutineRunner);
            
            StateMachine = new StateMachine(tickRunner, fixedTickRunner, sceneLoader, assetsDatabase, 
                ServiceManager.Container);
            
            StateMachine.Enter<BootstrapState>();
        }
    }
}
