using Core.Services;
using Projectiles;
using Visualizers;
using Obstacles;
using Player;
using UI;

namespace Core.Assets
{
    public interface IAssetsDatabase : ISingleService
    {
        public PlayerController Player { get; }
        public Projectile Projectile { get; }
        public Projection Projection { get; }
        
        public Obstacle Obstacle { get; }

        public Doors Doors { get; }
        
        public Popup WinPopup { get; }
        public Popup LosePopup { get; }
        
        public string BootstrapSceneName { get; }
        public string LevelSceneName { get; }
        
        public LevelData[] Levels { get; }
    }
}
