using Core.Services;
using Core.Workflow;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : ITickable
    {
        private readonly Transform _transform;
        private readonly Doors _doors;
        private readonly float _moveSpeed;
        private readonly float _requiredDistanceToDoors;

        public PlayerMovement(Transform transform, Doors doors, float moveSpeed, float requiredDistanceToDoors)
        {
            _transform = transform;
            _doors = doors;
            _moveSpeed = moveSpeed;
            _requiredDistanceToDoors = requiredDistanceToDoors;

            ServiceManager.Container.Single<ITickRunner>().Subscribe(this);
        }

        public void Tick(float deltaTime)
        {
            // throw new System.NotImplementedException();
        }

        public void OnDestroy()
        {
            ServiceManager.Container.Single<ITickRunner>().Unsubscribe(this);
        }

        public void MoveTowardsDoors()
        {
            // do sphereCast
        }
    }
}
