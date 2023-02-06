using UnityEngine;

namespace Components
{
    public class ScaleController
    {
        private readonly Transform _transform;
        private readonly float _minScale;

        public ScaleController(Transform transform, float minScale, float initialScale)
        {
            _transform = transform;
            _minScale = minScale;
            _transform.localScale = Vector3.one * initialScale;
        }

        public void MakeScaleStep(float value)
        {
            var scale = _transform.localScale;
            scale += Vector3.one * value * Time.deltaTime;

            _transform.localScale = scale;
            
            // check for minScale ?
        }
    }
}
