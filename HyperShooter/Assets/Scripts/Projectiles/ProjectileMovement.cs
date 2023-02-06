using Core.Services;
using Core.Workflow;
using UnityEngine;

namespace Projectiles
{
    public class ProjectileMovement : ITickable
    {
        private readonly Transform _transform;
        private readonly float _moveSpeed;

        private bool _moving;
        private Vector3 _direction;

        public ProjectileMovement(Transform transform, float moveSpeed)
        {
            _transform = transform;
            _moveSpeed = moveSpeed;
            
            ServiceManager.Container.Single<ITickRunner>().Subscribe(this);
        }

        public void OnDestroy()
        {
            ServiceManager.Container.Single<ITickRunner>().Unsubscribe(this);
        }

        public void Tick(float deltaTime)
        {
            if (!_moving)
                return;
            
            _transform.Translate(_direction * (_moveSpeed * Time.deltaTime));
        }

        public void StartMovement(Vector3 direction)
        {
            _moving = true;
            _direction = direction;
        }
    }
}