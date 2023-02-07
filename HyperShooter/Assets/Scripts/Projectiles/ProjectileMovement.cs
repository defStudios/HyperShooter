using Core.Services;
using Core.Workflow;
using UnityEngine;

namespace Projectiles
{
    public class ProjectileMovement : IFixedTickable
    {
        private readonly Rigidbody _rigidbody;
        private readonly float _moveSpeed;

        private bool _moving;
        private Vector3 _direction;

        public ProjectileMovement(Rigidbody rigidbody, float moveSpeed)
        {
            _rigidbody = rigidbody;
            _moveSpeed = moveSpeed;
            
            ServiceManager.Container.Single<IFixedTickRunner>().Subscribe(this);
        }

        public void FixedTick(float deltaTime)
        {
            if (!_moving)
                return;

            _rigidbody.velocity = _direction * _moveSpeed;
        }

        public void OnDestroy()
        {
            ServiceManager.Container.Single<IFixedTickRunner>().Unsubscribe(this);
        }

        public void StartMovement(Vector3 direction)
        {
            _moving = true;
            _direction = direction;
        }
    }
}