using Core.Services;
using Player;

namespace Core.Assets
{
    public interface IAssetsProvider : ISingleService
    {
        public PlayerController GetPlayer();
    }
}
