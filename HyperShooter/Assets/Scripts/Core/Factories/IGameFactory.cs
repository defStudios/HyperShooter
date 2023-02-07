using Core.Services;
using Obstacles;
using UnityEngine;
using Player;
using Projectiles;
using Visualizers;

namespace Core.Factories
{
    public interface IGameFactory : ISingleService
    {
        public PlayerController SpawnPlayer(Vector3 position);
        public Doors SpawnDoors(Vector3 position);
        public Projection SpawnProjection(Vector3 position);
        public Projectile SpawnProjectile(Vector3 position);
        public Obstacle SpawnObstacle(Vector3 position);
    }
}
