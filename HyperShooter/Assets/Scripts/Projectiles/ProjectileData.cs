using UnityEngine;

namespace Projectiles
{
    [CreateAssetMenu(fileName = "Projectile Data", menuName = "Projectiles/New Data", order = 0)]
    public class ProjectileData : ScriptableObject
    {
        public int InfectionDurationMilliseconds => (int)(infectionDuration * 1000);
        public int InfectionPostDurationMilliseconds => (int)(infectionPostDuration * 1000);
        public int ProjectileMissTimeoutMilliseconds => (int)(projectileMissTimeout * 1000);
        
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float FlightTimeout { get; private set; }
        
        [field: SerializeField, Space] public float InitialScale { get; private set; }
        [field: SerializeField] public float MinScale { get; private set; }
        [field: SerializeField] public float ScaleIncreaseStep { get; private set; }
        
        [field: SerializeField, Space] public float InfectionRadiusMultiplier { get; private set; }

        [SerializeField] private float infectionDuration;
        [SerializeField] private float infectionPostDuration;
        [SerializeField] private float projectileMissTimeout;
    }
}