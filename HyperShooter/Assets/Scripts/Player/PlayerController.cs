using Core.Factories;
using Core.Services;
using Visualizers;
using Projectiles;
using UnityEngine;
using Core.Input;
using Components;
using System;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private enum Status
        {
            Idle,
            Pumping,
        }
        
        public Action<Projectile> OnProjectileLaunched { get; set; }

        public PlayerMovement Movement { get; private set; }
        public ScaleController Scale { get; private set; }
        
        [SerializeField] private PlayerData data;
        [SerializeField] private Transform modelTransform;
        [SerializeField] private LayerMask obstaclesLayerMask;

        private Projection _projection;
        private Projectile _projectile;

        private Status _status;

        public void Init(Doors doors, float requiredDistanceToDoors, Projection projection)
        {
            _status = Status.Idle;
            
            Movement = new PlayerMovement(transform, modelTransform, doors, 
                data.MoveSpeed, data.JumpPower, data.JumpLength, 
                data.ObstacleOffset, requiredDistanceToDoors, obstaclesLayerMask);
            
            Scale = new ScaleController(modelTransform, data.MinScale, data.InitialScale);
            
            _projection = projection;
            _projection.SetProjection(modelTransform, doors.transform);

            ServiceManager.Container.Single<IInput>().OnTapBegun += OnTapBegun;
            ServiceManager.Container.Single<IInput>().OnTapping += OnTapping;
            ServiceManager.Container.Single<IInput>().OnTapEnded += OnTapEnded;
        }

        private void OnDestroy()
        {
            Movement.OnDestroy();
            
            ServiceManager.Container.Single<IInput>().OnTapBegun -= OnTapBegun;
            ServiceManager.Container.Single<IInput>().OnTapping -= OnTapping;
            ServiceManager.Container.Single<IInput>().OnTapEnded -= OnTapEnded;
        }

        private void OnTapBegun()
        {
            var spawnPos = transform.position + data.ProjectileSpawnOffset;
            _projectile = ServiceManager.Container.Single<IGameFactory>().SpawnProjectile(spawnPos);
            _projectile.Init();

            _status = Status.Pumping;
        }

        private void OnTapping()
        {
            Scale.MakeScaleStep(-data.ScaleDecreaseStep);
            _projectile.MakeScaleStep();
            
            _projection.UpdateProjection();
            
            if (Scale.OvercameMinimumScale)
                ThrowProjectile();
        }

        private void OnTapEnded()
        {
            ThrowProjectile();
        }

        private void ThrowProjectile()
        {
            if (_status != Status.Pumping)
                return;

            _projectile.Fly(transform.forward);
            _status = Status.Idle;
            
            OnProjectileLaunched?.Invoke(_projectile);
        }
    }
}
