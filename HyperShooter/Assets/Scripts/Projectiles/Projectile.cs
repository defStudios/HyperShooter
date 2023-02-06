using UnityEngine;
using Components;
using Infection;
using Utils;

namespace Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ProjectileData data;
        [SerializeField] private Transform modelTransform;
        [SerializeField] private ProjectileAppearance appearance;
        [SerializeField] private ProjectileCollision collision;
        
        private ProjectileMovement _movement;
        private ScaleController _scale;
        private Timer _flightTimeout;
        
        public void Init()
        {
            _movement = new ProjectileMovement(transform, data.MoveSpeed);
            _scale = new ScaleController(modelTransform, data.MinScale, data.InitialScale);
            
            collision.OnHitInfectable += OnHitInfectable;
            
            appearance.SetPumpingAppearance();
        }

        public void MakeScaleStep() =>
            _scale.MakeScaleStep(data.ScaleIncreaseStep);

        public void Fly(Vector3 direction)
        {
            _flightTimeout = new Timer(data.FlightTimeout, () => DestroySelf());
            
            appearance.SetFlightAppearance();
            _movement.StartMovement(direction);
        }

        private void OnHitInfectable(Vector3 position)
        {
            float radius = modelTransform.localScale.x * data.InfectionRadiusMultiplier;

            var hitColliders = Physics.OverlapSphere(position, radius);
            
            foreach (Collider collider in hitColliders)
            {
                if (collider.TryGetComponent(out IInfectable infectable))
                    infectable.Infect();
            }
            
            DestroySelf();
        }

        private void DestroySelf()
        {
            _flightTimeout.OnDestroy();
            _movement.OnDestroy();
            
            Destroy(gameObject);
        }
    }
}
