using Player;
using UnityEngine;

namespace Core.Assets
{
    public class AssetsProvider : IAssetsProvider
    {
        private readonly IAssetsDatabase _database;
        
        public AssetsProvider(IAssetsDatabase database)
        {
            _database = database;
        }

        public PlayerController GetPlayer()
        {
            throw new System.NotImplementedException();
        }
    }
}
