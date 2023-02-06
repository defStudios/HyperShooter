using Core.Factories;
using Core.Services;
using Core.Cameras;
using Core.Scenes;
using Core.Assets;
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

        private PlayerController _player;
        private Doors _doors;
        private Projection _projection;

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
            if (_player != null)
                Object.Destroy(_player.gameObject);
            if (_doors != null)
                Object.Destroy(_doors.gameObject);
            if (_projection != null)
                Object.Destroy(_projection.gameObject);
            
            _player = _gameFactory.SpawnPlayer(_levelData.PlayerSpawnPosition);
            _doors = _gameFactory.SpawnDoors(_levelData.DoorsSpawnPosition);
            _projection = _gameFactory.SpawnProjection(_levelData.ProjectionSpawnPosition);
            
            _player.Init(_doors, _levelData.RequiredDistanceToDoors, _projection);

            Camera.main.GetComponent<CameraFollower>()
                .SetTarget(_player.transform, _levelData.CameraOffset, _levelData.CameraEulerRotation);
            
            // generate obstacles
            
            _stateMachine.Enter<GameLoopState, PlayerController>(_player);
        }
    }
}
