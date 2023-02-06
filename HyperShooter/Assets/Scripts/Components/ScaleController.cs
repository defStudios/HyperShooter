using UnityEngine;

namespace Components
{
    public class ScaleController
    {
        public bool OvercameMinimumScale { get; private set; }
        
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

            if (scale.x < _minScale)
            {
                scale = Vector3.one * _minScale;
                OvercameMinimumScale = true;
            }

            _transform.localScale = scale;
        }
    }
}
