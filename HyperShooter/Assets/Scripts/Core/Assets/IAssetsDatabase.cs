using Core.Services;
using Projectiles;
using Visualizers;
using Obstacles;
using Player;

namespace Core.Assets
{
    public interface IAssetsDatabase : ISingleService
    {
        public PlayerController Player { get; }
        public Projectile Projectile { get; }
        public Projection Projection { get; }
        
        public Obstacle Obstacle { get; }

        public Doors Doors { get; }
        
        public string BootstrapSceneName { get; }
        public string LevelSceneName { get; }
        
        public LevelData[] Levels { get; }
    }
}
