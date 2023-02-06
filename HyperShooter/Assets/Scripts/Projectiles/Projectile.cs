using UnityEngine;
using Components;

namespace Projectiles
{
    public class Projectile : MonoBehaviour
    {
        public ScaleController Scale { get; private set; }
        
        [SerializeField] private ProjectileData data;
        [SerializeField] private Transform modelTransform;
        [SerializeField] private ProjectileAppearance appearance;

        private ProjectileMovement _movement;
        
        public void Init()
        {
            _movement = new ProjectileMovement(transform, data.MoveSpeed);
            Scale = new ScaleController(modelTransform, data.MinScale);
            
            appearance.SetPumpingAppearance();
        }

        public void Fly(Vector3 direction)
        {
            appearance.SetFlightAppearance();
            _movement.StartMovement(direction);
        }
    }
}
