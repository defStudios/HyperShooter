using UnityEngine;
using Components;
using Infection;
using System;
using Timer = Utils.Timer;

namespace Projectiles
{
    public class Projectile : MonoBehaviour
    {
        public Action OnProjectileMissed { get; set; }
        public Action OnObstaclesInfected { get; set; }

        public ProjectileData Data => data;

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
            collision.OnHitDoors += (position) => ProjectileMissed();
            
            appearance.SetPumpingAppearance();
        }

        public void MakeScaleStep() =>
            _scale.MakeScaleStep(data.ScaleIncreaseStep);

        public void Fly(Vector3 direction)
        {
            _flightTimeout = new Timer(data.FlightTimeout, ProjectileMissed);
            
            appearance.SetFlightAppearance();
            _movement.StartMovement(direction);
        }

        private void ProjectileMissed()
        {
            OnProjectileMissed?.Invoke();
            DestroySelf();
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
            
            OnObstaclesInfected?.Invoke();
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
