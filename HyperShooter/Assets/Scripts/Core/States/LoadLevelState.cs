using Core.Factories;
using Core.Services;
using Core.Scenes;
using Core.Assets;
using UnityEngine;

namespace Core.States
{
    public class LoadLevelState : IState
    {
        private readonly StateMachine _stateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly IAssetsDatabase _assets;
        private readonly ServiceManager _services;

        public LoadLevelState(StateMachine stateMachine, IGameFactory gameFactory, IAssetsDatabase assets, ServiceManager services)
        {
            _stateMachine = stateMachine;
            _gameFactory = gameFactory;
            _assets = assets;
            _services = services;
        }

        public void Enter()
        {
            // show loading screen
            _services.Single<ISceneLoader>().Load(_assets.LevelSceneName, OnSceneLoaded);
        }

        public void Exit()
        {
            // hide loading screen
        }

        private void OnSceneLoaded()
        {
            // spawn player, doors
            // set camera
            // generate obstacles
            
            _stateMachine.Enter<GameLoopState>();
        }
    }
}
