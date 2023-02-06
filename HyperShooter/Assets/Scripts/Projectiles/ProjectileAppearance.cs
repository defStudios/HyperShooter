using UnityEngine;

namespace Projectiles
{
    public class ProjectileAppearance : MonoBehaviour
    {
        [SerializeField] private MeshRenderer renderer;
        [SerializeField] private Material pumpingMaterial;
        [SerializeField] private Material flightMaterial;

        public void SetPumpingAppearance() => renderer.material = pumpingMaterial;
        public void SetFlightAppearance() => renderer.material = flightMaterial;
    }
}