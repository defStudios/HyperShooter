using Core.Services;
using Visualizers;
using Player;

namespace Core.Assets
{
    public interface IAssetsProvider : ISingleService
    {
        public PlayerController GetPlayer();
        public Doors GetDoors();
        public Projection GetProjection();
    }
}
