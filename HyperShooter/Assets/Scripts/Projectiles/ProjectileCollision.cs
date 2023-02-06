using UnityEngine;
using Infection;
using System;

public class ProjectileCollision : MonoBehaviour
{
    public Action<Vector3> OnHitInfectable { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        bool isInfectable = other.TryGetComponent(out IInfectable infectable);
        if (isInfectable)
            OnHitInfectable?.Invoke(other.transform.position);
    }
}
