using Core.Services;
using Obstacles;
using Projectiles;
using Visualizers;
using Player;
using UI;

namespace Core.Assets
{
    public interface IAssetsProvider : ISingleService
    {
        public PlayerController GetPlayer();
        public Doors GetDoors();
        public Projection GetProjection();
        public Projectile GetProjectile();
        public Obstacle GetObstacle();

        public Popup GetWinPopup();
        public Popup GetLosePopup();
    }
}
