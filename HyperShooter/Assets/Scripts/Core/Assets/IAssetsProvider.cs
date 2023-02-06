using Core.Services;
using Projectiles;
using Visualizers;
using Player;

namespace Core.Assets
{
    public interface IAssetsProvider : ISingleService
    {
        public PlayerController GetPlayer();
        public Doors GetDoors();
        public Projection GetProjection();
        public Projectile GetProjectile();
    }
}
