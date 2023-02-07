using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "Player Data", menuName = "Player/New Data", order = 0)]
    public class PlayerData : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float JumpPower { get; private set; }
        [field: SerializeField] public float JumpLength { get; private set; }
        
        [field: SerializeField] public float BouncingMinDistanceToTarget { get; private set; }
        [field: SerializeField] public float MovementMinDistanceToTarget { get; private set; }
        
        [field: SerializeField, Space] public float InitialScale { get; private set; }
        [field: SerializeField] public float MinScale { get; private set; }
        [field: SerializeField] public float ScaleDecreaseStep { get; private set; }
        
        [field: SerializeField, Space] public Vector3 ProjectileSpawnOffset { get; private set; }
        [field: SerializeField] public float ObstacleOffset { get; private set; }
    }
}