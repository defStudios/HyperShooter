using UnityEngine;

namespace Core.Cameras
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField] private float smoothTime;
        
        private Transform _transform;
        private Transform _target;
        private Vector3 _offset;

        private Vector3 _velocity;

        private void Awake()
        {
            _transform = transform;
        }

        private void LateUpdate()
        {
            if (_target == null)
                return;

            _transform.position = Vector3.SmoothDamp(_transform.position, _target.position + _offset, 
                ref _velocity, smoothTime);
        }

        public void SetTarget(Transform target, Vector3 offset, Vector3 eulerRotation)
        {
            _target = target;
            _offset = offset;
            
            transform.SetPositionAndRotation(target.position + offset, Quaternion.Euler(eulerRotation));
        }
    }
}
