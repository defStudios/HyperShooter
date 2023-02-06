using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Levels/New Level", order = 0)]
public class LevelData : ScriptableObject
{
    public Vector3 PlayerSpawnPosition => playerSpawnPosition;
    public Vector3 DoorsSpawnPosition => doorsSpawnPosition;
    public Vector3 ProjectionSpawnPosition => projectionSpawnPosition;

    public Vector3 CameraOffset => cameraOffset;
    public Vector3 CameraEulerRotation => cameraEulerRotation;

    public float RequiredDistanceToDoors => requiredDistanceToDoors;
    
    [Header("Spawn")]
    [SerializeField] private Vector3 playerSpawnPosition;
    [SerializeField] private Vector3 doorsSpawnPosition;
    [SerializeField] private Vector3 projectionSpawnPosition;
    
    [Header("Camera")]
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private Vector3 cameraEulerRotation;

    [Header("Game Balance")] 
    [SerializeField] private float requiredDistanceToDoors;
}
