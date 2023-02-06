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
            Moving
        }
        
        public Action<Projectile> OnProjectileLaunched { get; set; }
        public Action OnPlayerOvercameMinimumScale { get; set; }
        public Action<bool> OnMovementDone { get; set; }
        
        [SerializeField] private PlayerData data;
        [SerializeField] private Transform modelTransform;
        [SerializeField] private LayerMask obstaclesLayerMask;

        private PlayerMovement _movement;
        private ScaleController _scale;
        private Projection _projection;

        private Projectile _projectile;

        private Status _status;

        public void Init(Doors doors, float requiredDistanceToDoors, Projection projection)
        {
            _status = Status.Idle;
            
            _movement = new PlayerMovement(modelTransform, doors, data.MoveSpeed,  
                data.JumpPower, data.JumpLength, data.ObstacleOffset, requiredDistanceToDoors, obstaclesLayerMask);
            _movement.OnMovementDone += MovementDone;
            
            _scale = new ScaleController(modelTransform, data.MinScale, data.InitialScale);
            
            _projection = projection;
            _projection.SetProjection(modelTransform, doors.transform);

            ServiceManager.Container.Single<IInput>().OnTapBegun += OnTapBegun;
            ServiceManager.Container.Single<IInput>().OnTapping += OnTapping;
            ServiceManager.Container.Single<IInput>().OnTapEnded += OnTapEnded;
        }

        private void OnDestroy()
        {
            _movement.OnDestroy();
            
            ServiceManager.Container.Single<IInput>().OnTapBegun -= OnTapBegun;
            ServiceManager.Container.Single<IInput>().OnTapping -= OnTapping;
            ServiceManager.Container.Single<IInput>().OnTapEnded -= OnTapEnded;
        }

        public void StartMovementStage()
        {
            if (_scale.OvercameMinimumScale)
            {
                OnPlayerOvercameMinimumScale?.Invoke();
                return;
            }

            _status = Status.Moving;
            _movement.MoveTowardsDoors();
        }
       
        private void MovementDone(bool reachedDoors)
        {
            _status = Status.Idle;
            OnMovementDone?.Invoke(reachedDoors);
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
            _scale.MakeScaleStep(-data.ScaleDecreaseStep);
            _projectile.MakeScaleStep();
            
            _projection.UpdateProjection();
            
            if (_scale.OvercameMinimumScale)
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
