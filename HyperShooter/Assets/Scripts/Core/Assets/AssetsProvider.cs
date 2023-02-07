using Core.Pools;
using UnityEngine;
using Visualizers;
using Projectiles;
using Obstacles;
using Player;

namespace Core.Assets
{
    public class AssetsProvider : IAssetsProvider
    {
        private readonly IAssetsDatabase _database;
        private readonly ObjectPool<Obstacle> _obstaclesPool;

        public AssetsProvider(IAssetsDatabase database)
        {
            _database = database;
            _obstaclesPool = new ObjectPool<Obstacle>(database.Obstacle, 100);
        }

        public PlayerController GetPlayer()
        {
            return Object.Instantiate(_database.Player);
        }

        public Doors GetDoors()
        {
            return Object.Instantiate(_database.Doors);
        }

        public Projection GetProjection()
        {
            return Object.Instantiate(_database.Projection);
        }

        public Projectile GetProjectile()
        {
            return Object.Instantiate(_database.Projectile);
        }

        public Obstacle GetObstacle()
        {
            return _obstaclesPool.GetInstance();
        }
    }
}
