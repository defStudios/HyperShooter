using System.Threading.Tasks;
using UnityEngine;

namespace Visualizers
{
    public class Doors : MonoBehaviour
    {
        [SerializeField] private SmoothRotation leftDoor;
        [SerializeField] private SmoothRotation rightDoor;
    
        public async Task Open()
        {
            rightDoor.Rotate();
            await leftDoor.Rotate();
        }
    }
}
