using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "Player Data", menuName = "Player/New Data", order = 0)]
    public class PlayerData : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float MinScale { get; private set; }
        [field: SerializeField] public float ScaleDecreaseStep { get; private set; }
    }
}