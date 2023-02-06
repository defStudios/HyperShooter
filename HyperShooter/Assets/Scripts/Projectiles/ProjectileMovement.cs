using Core.Services;
using Core.Workflow;
using UnityEngine;

namespace Projectiles
{
    public class ProjectileMovement : ITickable
    {
        private readonly Transform _transform;
        private readonly float _moveSpeed;

        public ProjectileMovement(Transform transform, float moveSpeed)
        {
            _transform = transform;
            _moveSpeed = moveSpeed;
            
            ServiceManager.Container.Single<ITickRunner>().Subscribe(this);
        }

        public void Tick(float deltaTime)
        {
            throw new System.NotImplementedException();
        }

        public void StartMovement(Vector3 direction)
        {
            
        }
    }
}