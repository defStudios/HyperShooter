using Core.Factories;
using Core.Cameras;
using UnityEngine;
using Visualizers;
using Player;

namespace Level
{
    public class LevelController
    {
        private readonly IGameFactory _gameFactory;
        
        public LevelData Data { get; private set; }
        
        public PlayerController Player { get; private set; }
        public Doors Doors { get; private set; }
        public Projection Projection { get; private set; }
        public ObstaclesGenerator ObstaclesGenerator { get; private set; }

        public LevelController(IGameFactory gameFactory, LevelData data)
        {
            _gameFactory = gameFactory;
            Data = data;
        }

        public void LoadLevel()
        {
            Player = _gameFactory.SpawnPlayer(Data.PlayerSpawnPosition);
            Doors = _gameFactory.SpawnDoors(Data.DoorsSpawnPosition);
            Projection = _gameFactory.SpawnProjection(Data.ProjectionSpawnPosition);

            Player.Init(Doors, Data.RequiredDistanceToDoors, Projection);
            
            Camera.main.GetComponent<CameraFollower>()
                .SetTarget(Player.transform, Data.CameraOffset, Data.CameraEulerRotation);

            ObstaclesGenerator = new ObstaclesGenerator(_gameFactory);
            ObstaclesGenerator.GenerateObstacles(Data.SpawnAreaPoint, Data.SpawnAreaRadius, Data.ObstaclesAmount);
        }

        public void CleanUp()
        {
            Object.Destroy(Player.gameObject);
            Object.Destroy(Doors.gameObject);
            Object.Destroy(Projection.gameObject);
            ObstaclesGenerator.DestroyObstacles();
        }
    }
}