using Core.Services;
using UnityEngine;
using Core.Input;
using Components;
using Core.Factories;
using Projectiles;
using Visualizers;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerData data;
        [SerializeField] private Transform modelTransform;

        private PlayerMovement _movement;
        private ScaleController _scale;
        private Projection _projection;

        private Projectile _projectile;

        public void Init(Doors doors, float requiredDistanceToDoors, Projection projection)
        {
            _movement = new PlayerMovement(transform, doors, data.MoveSpeed, requiredDistanceToDoors);
            _scale = new ScaleController(modelTransform, data.MinScale, data.InitialScale);
            _projection = projection;
            
            _projection.SetProjection(modelTransform, doors.transform);

            ServiceManager.Container.Single<IInput>().OnTapBegun += OnTapBegun;
            ServiceManager.Container.Single<IInput>().OnTapping += OnTapping;
            ServiceManager.Container.Single<IInput>().OnTapEnded += OnTapEnded;
        }

        private void OnTapBegun()
        {
            var spawnPos = transform.position + data.ProjectileSpawnOffset;
            _projectile = ServiceManager.Container.Single<IGameFactory>().SpawnProjectile(spawnPos);
            _projectile.Init();
            
            // set state to pumping
            //throw new System.NotImplementedException();
        }

        private void OnTapping()
        {
            _scale.MakeScaleStep(-data.ScaleDecreaseStep);
            _projectile.MakeScaleStep();
            
            _projection.UpdateProjection();
            
            // check if player is alive
            
            //throw new System.NotImplementedException();
        }

        private void OnTapEnded()
        {
            _projectile.Fly(transform.forward);
            // disable inputs
            
            //throw new System.NotImplementedException();
        }
    }
}
