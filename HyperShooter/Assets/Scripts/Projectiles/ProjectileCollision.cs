using UnityEngine;
using Infection;
using System;
using Visualizers;

public class ProjectileCollision : MonoBehaviour
{
    public Action<Vector3> OnHitInfectable { get; set; }
    public Action<Vector3> OnHitDoors { get; set; }

    private bool _detectionEnabled;
 
    public void EnableDetection() => _detectionEnabled = true;
    public void DisableDetection() => _detectionEnabled = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!_detectionEnabled)
            return;
        
        bool isInfectable = other.TryGetComponent(out IInfectable infectable);
        if (isInfectable)
        {
            OnHitInfectable?.Invoke(other.transform.position);
            return;
        }

        if (other.CompareTag("Doors"))
            OnHitDoors?.Invoke(other.transform.position);
    }
}
