using UnityEngine;
using Visualizers;
using Projectiles;
using Obstacles;
using Player;

namespace Core.Assets
{
    [CreateAssetMenu(fileName = "Assets Database", menuName = "Assets/New Database", order = 0)]
    public class AssetsDatabase : ScriptableObject, IAssetsDatabase
    {
        [field: SerializeField] public PlayerController Player { get; private set; }
        [field: SerializeField] public Projectile Projectile { get; private set; }
        [field: SerializeField] public Projection Projection { get; private set; }
        
        [field: SerializeField, Space] public Obstacle Obstacle { get; private set; }

        [field: SerializeField, Space] public Doors Doors { get; private set; }
        
        [field: SerializeField, Space] public string BootstrapSceneName { get; private set;}
        [field: SerializeField] public string LevelSceneName { get; private set;}
    }
}
