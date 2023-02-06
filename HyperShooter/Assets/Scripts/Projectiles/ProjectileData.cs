using UnityEngine;

namespace Projectiles
{
    [CreateAssetMenu(fileName = "Projectile Data", menuName = "Projectiles/New Data", order = 0)]
    public class ProjectileData : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float MinScale { get; private set; }
        [field: SerializeField] public float ScaleIncreaseStep { get; private set; }
        [field: SerializeField] public float InfectionRadiusMultiplier { get; private set; }
    }
}