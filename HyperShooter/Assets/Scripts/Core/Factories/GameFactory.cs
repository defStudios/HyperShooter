using UnityEngine;
using Core.Assets;
using Visualizers;
using Player;
using Projectiles;

namespace Core.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetsProvider _assetsProvider;
        
        public GameFactory(IAssetsProvider assetsProvider)
        {
            _assetsProvider = assetsProvider;
        }

        public PlayerController SpawnPlayer(Vector3 position)
        {
            var player = _assetsProvider.GetPlayer();
            player.transform.position = position;

            return player;
        }

        public Doors SpawnDoors(Vector3 position)
        {
            var doors = _assetsProvider.GetDoors();
            doors.transform.position = position;

            return doors;
        }

        public Projection SpawnProjection(Vector3 position)
        {
            var projection = _assetsProvider.GetProjection();
            projection.transform.position = position;

            return projection;
        }

        public Projectile SpawnProjectile(Vector3 position)
        {
            var projectile = _assetsProvider.GetProjectile();
            projectile.transform.position = position;

            return projectile;
        }
    }
}
