using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] private Transform leftDoor;
    [SerializeField] private Transform rightDoor;

    [SerializeField] private float leftDoorOpeningAngle;
    [SerializeField] private float rightDoorOpeningAngle;
    
    [SerializeField] private float openingDuration;

    private bool _openingDoors;
    
    private void Update()
    {
        if (!_openingDoors)
            return;
        
        leftDoor.rotation = Quaternion.Lerp(leftDoor.rotation, Quaternion.Euler(Vector3.up * leftDoorOpeningAngle), Time.deltaTime / openingDuration);
    }
    
    public void Open()
    {
        _openingDoors = true;
    }
}
