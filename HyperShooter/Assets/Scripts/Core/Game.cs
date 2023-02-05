using Core.Assets;
using Core.Services;
using Core.States;
using Core.Scenes;
using Core.Utils;

namespace Core
{
    public class Game
    {
        public StateMachine StateMachine { get; private set; }
        
        public Game(ICoroutineRunner coroutineRunner, IAssetsDatabase assetsDatabase)
        {
            var sceneLoader = new SceneLoader(coroutineRunner);
            
            StateMachine = new StateMachine(sceneLoader, assetsDatabase, ServiceManager.Container);
            StateMachine.Enter<BootstrapState>();
        }
    }
}
