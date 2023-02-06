using Core.Factories;
using Core.Services;
using Core.Scenes;
using Core.Assets;
using Core.Cameras;
using UnityEngine;

namespace Core.States
{
    public class LoadLevelState : IPayloadedState<LevelData>
    {
        private readonly StateMachine _stateMachine;
        private readonly IGameFactory _gameFactory;
        private readonly IAssetsDatabase _assets;
        private readonly ServiceManager _services;

        private LevelData _levelData;

        public LoadLevelState(StateMachine stateMachine, ServiceManager services)
        {
            _stateMachine = stateMachine;
            _gameFactory = services.Single<IGameFactory>();
            _assets = services.Single<IAssetsDatabase>();
            _services = services;
        }

        public void Enter(LevelData levelData)
        {
            // show loading screen
            
            _levelData = levelData;
            _services.Single<ISceneLoader>().Load(_assets.LevelSceneName, OnSceneLoaded);
        }

        public void Exit()
        {
            // hide loading screen
        }

        private void OnSceneLoaded()
        {
            var player = _gameFactory.SpawnPlayer(_levelData.PlayerSpawnPosition);
            var doors = _gameFactory.SpawnDoors(_levelData.DoorsSpawnPosition);
            var projection = _gameFactory.SpawnProjection(_levelData.ProjectionSpawnPosition);
            
            player.Init(doors, _levelData.RequiredDistanceToDoors, projection);

            Camera.main.GetComponent<CameraFollower>()
                .SetTarget(player.transform, _levelData.CameraOffset, _levelData.CameraEulerRotation);
            
            // generate obstacles
            
            _stateMachine.Enter<GameLoopState>();
        }
    }
}
