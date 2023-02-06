using Core.Services;
using UnityEngine;
using Core.Input;
using Components;
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

        public void Init(Doors doors, float requiredDistanceToDoors, Projection projection)
        {
            _movement = new PlayerMovement(transform, doors, data.MoveSpeed, requiredDistanceToDoors);
            _scale = new ScaleController(modelTransform, data.MinScale);
            _projection = projection;
            
            _projection.SetProjection(modelTransform, doors.transform);

            ServiceManager.Container.Single<IInput>().OnTapBegun += OnTapBegun;
            ServiceManager.Container.Single<IInput>().OnTapping += OnTapping;
            ServiceManager.Container.Single<IInput>().OnTapEnded += OnTapEnded;
        }

        private void OnTapBegun()
        {
            // create projectile
            // set state to pumping
            
            throw new System.NotImplementedException();
        }

        private void OnTapping()
        {
            // update scale
            _projection.UpdateProjection();
            
            throw new System.NotImplementedException();
        }

        private void OnTapEnded()
        {
            // launch projectile
            // disable inputs
            
            throw new System.NotImplementedException();
        }
    }
}
