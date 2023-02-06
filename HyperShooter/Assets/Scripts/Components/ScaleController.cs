using UnityEngine;

namespace Components
{
    public class ScaleController
    {
        private readonly Transform _transform;
        private readonly float _minScale;

        public ScaleController(Transform transform, float minScale)
        {
            _transform = transform;
            _minScale = minScale;
        }

        public void MakeScaleStep(float value)
        {
            var scale = _transform.localScale;
            scale += Vector3.one * value;

            _transform.localScale = scale;
            
            // check for minScale ?
        }
    }
}
