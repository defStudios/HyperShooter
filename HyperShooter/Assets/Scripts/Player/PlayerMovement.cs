using Core.Services;
using Core.Workflow;
using UnityEngine;
using Visualizers;
using System;

namespace Player
{
    public class PlayerMovement : ITickable
    {
        public Action<bool> OnMovementDone { get; set; }
        
        private readonly Transform _transform;
        private readonly Transform _mesh;
        private readonly Doors _doors;
        
        private readonly float _moveSpeed;
        private readonly float _jumpLength;
        private readonly float _jumpPower;
        private readonly float _targetPositionOffset;
        private readonly float _requiredDistanceToDoors;
        private readonly LayerMask _layerMask;

        private bool _moving;
        private float _jumpsAmount;
        private Vector3 _endPoint;

        public PlayerMovement(Transform transform, Transform mesh, Doors doors, 
            float moveSpeed, float jumpPower, float jumpLength,
            float targetPositionOffset, float requiredDistanceToDoors, LayerMask layerMask)
        {
            _transform = transform;
            _mesh = mesh;
            _doors = doors;
            _moveSpeed = moveSpeed;
            _jumpLength = jumpLength;
            _jumpPower = jumpPower;
            _targetPositionOffset = targetPositionOffset;
            _requiredDistanceToDoors = requiredDistanceToDoors;
            _layerMask = layerMask;

            ServiceManager.Container.Single<ITickRunner>().Subscribe(this);
        }

        public void Tick(float deltaTime)
        {
            if (!_moving)
                return;

            Vector3 direction = _endPoint - _transform.position;
            float distance = direction.magnitude;
            
            if (distance > .1f)
                _transform.position += direction.normalized * (_moveSpeed * Time.deltaTime);
            else
                MovementDone();
            /*
            if (distance > _targetPositionOffset * 2)
            {
                var pos = _transform.position +
                          (Vector3.up * (Mathf.Sin(Time.time * _jumpPower) * _jumpLength * Time.deltaTime));

                pos += _transform.forward * (_moveSpeed * Time.deltaTime);
                _transform.position = pos;
            }
            else
            {
                if (distance > .1f)
                    _transform.position += direction * (_moveSpeed * Time.deltaTime);
                else
                    MovementDone();
            }
            */
        }

        public void OnDestroy()
        {
            ServiceManager.Container.Single<ITickRunner>().Unsubscribe(this);
        }

        public void UpdatePumpingPosition(float scaleStep)
        {
            _transform.position += Vector3.up * ((scaleStep / 2) * Time.deltaTime);
        }

        public void MoveTowardsDoors()
        {
            _endPoint = GetEndPoint();
            _moving = true;
        }

        private void MovementDone()
        {
            _moving = false;
            
            bool reachedDoors = (_doors.transform.position - _transform.position).magnitude < _requiredDistanceToDoors;
            OnMovementDone?.Invoke(reachedDoors);
        }

        private Vector3 GetEndPoint()
        {
            bool hasObstacle = Physics.SphereCast(_transform.position, _mesh.localScale.x / 2, _transform.forward, out var hit, 100, _layerMask);
            
            float targetZ =  hasObstacle ? hit.point.z : _doors.transform.position.z;
            var pos = new Vector3(_transform.position.x, _transform.position.y, targetZ);
            pos += (_transform.position - pos).normalized * _targetPositionOffset;

            return pos;
        }
    }
}
