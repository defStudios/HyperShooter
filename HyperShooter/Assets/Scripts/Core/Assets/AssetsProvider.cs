using Core.Pools;
using UnityEngine;
using Visualizers;
using Projectiles;
using Obstacles;
using Player;
using UI;

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

        public PlayerController GetPlayer() => Object.Instantiate(_database.Player);
        public Doors GetDoors() => Object.Instantiate(_database.Doors);
        public Projection GetProjection() => Object.Instantiate(_database.Projection);
        public Projectile GetProjectile() => Object.Instantiate(_database.Projectile);

        public Obstacle GetObstacle() => _obstaclesPool.GetInstance();
        
        public Popup GetWinPopup() => Object.Instantiate(_database.WinPopup);
        public Popup GetLosePopup() => Object.Instantiate(_database.LosePopup);
    }
}
