using Core.Assets;
using Player;
using UnityEngine;

namespace Core.Factories
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetsProvider _assetsProvider;
        
        public GameFactory(IAssetsProvider assetsProvider)
        {
            _assetsProvider = assetsProvider;
        }

        public PlayerController SpawnPlayer(Vector3 position)
        {
            throw new System.NotImplementedException();
        }
    }
}
