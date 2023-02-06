using UnityEngine;
using Infection;
using System;
using Visualizers;

public class ProjectileCollision : MonoBehaviour
{
    public Action<Vector3> OnHitInfectable { get; set; }
    public Action<Vector3> OnHitDoors { get; set; }

    private void OnTriggerEnter(Collider other)
    {
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
