using UnityEngine;

namespace Visualizers
{
    public class Projection : MonoBehaviour
    {
        [SerializeField] private Transform meshTransform;
        
        private Transform _transform;
        private Transform _origin;
        private Transform _target;

        private void Awake()
        {
            _transform = transform;
        }

        public void SetProjection(Transform origin, Transform target)
        {
            _origin = origin;
            _target = target;
            
            UpdateProjection();
        }

        public void UpdateProjection()
        {
            var originPos = new Vector3(_origin.position.x, 0, _origin.position.z);
            var targetPos = new Vector3(_target.position.x, 0, _target.position.z);
            
            var direction = targetPos - originPos;
            float xScale = _origin.localScale.x;
            float zScale = direction.z;

            meshTransform.localScale = new Vector3(xScale, meshTransform.localScale.y, zScale);
            _transform.position = new Vector3(originPos.x, _transform.position.y, originPos.z + direction.z / 2);
        }
    }
}