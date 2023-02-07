using UnityEngine;

namespace Obstacles
{
    public class ObstacleAppearance : MonoBehaviour
    {
        [SerializeField] private MeshRenderer renderer;
        [SerializeField] private Material defaultMaterial;
        [SerializeField] private Material infectedMaterial;

        public void SetDefaultAppearance() => renderer.material = defaultMaterial;
        public void SetInfectedAppearance() => renderer.material = infectedMaterial;
    }
}