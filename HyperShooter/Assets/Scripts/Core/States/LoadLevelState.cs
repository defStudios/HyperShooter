using Core.Factories;
using Core.Services;
using Core.Cameras;
using Core.Scenes;
using Core.Assets;
using Level;
using UnityEngine;
using Player;
using Visualizers;

namespace Core.States
{
    public class LoadLevelState : IPayloadedState<LevelData>
    {
        private readonly StateMachine _stateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly IAssetsDatabase _assets;
        private readonly ServiceManager _services;

        private LevelData _levelData;
        private LevelController _levelController;

        public LoadLevelState(StateMachine stateMachine, ServiceManager services)
        {
            _stateMachine = stateMachine;
            _gameFactory = services.Single<IGameFactory>();
            _assets = services.Single<IAssetsDatabase>();
            _services = services;
        }

        public void Enter(LevelData levelData)
        {
            _levelData = levelData;
            _services.Single<ISceneLoader>().Load(_assets.LevelSceneName, OnSceneLoaded);
        }

        public void Exit() { }

        private void OnSceneLoaded()
        {
            _levelController?.CleanUp();
            
            _levelController = new LevelController(_gameFactory, _levelData);
            _levelController.LoadLevel();
            
            _stateMachine.Enter<GameLoopState, LevelController>(_levelController);
        }
    }
}
