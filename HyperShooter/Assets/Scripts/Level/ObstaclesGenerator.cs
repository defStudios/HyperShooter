using System.Collections.Generic;
using Core.Factories;
using Core.Pools;
using Obstacles;
using UnityEngine;

namespace Level
{
    public class ObstaclesGenerator
    {
        private readonly IGameFactory _gameFactory;
        private readonly List<IPooledObject<Obstacle>> _obstacles;
        
        public ObstaclesGenerator(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _obstacles = new List<IPooledObject<Obstacle>>();
        }

        public void GenerateObstacles(Vector3 spawnAreaPoint, float areaRadius, int obstaclesAmount)
        {
            for (int i = 0; i < obstaclesAmount; i++)
            {
                var randomPoint = Random.insideUnitCircle * areaRadius;
                var pos = spawnAreaPoint + new Vector3(randomPoint.x, 0, randomPoint.y);

                var obstacle = _gameFactory.SpawnObstacle(pos);
                _obstacles.Add(obstacle);
            }
        }

        public void DestroyObstacles()
        {
            foreach (var obstacle in _obstacles)
                obstacle.Despawn();

            _obstacles.Clear();
        }
    }
}
