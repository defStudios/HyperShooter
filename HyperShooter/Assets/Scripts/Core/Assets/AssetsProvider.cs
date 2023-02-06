using Visualizers;
using UnityEngine;
using Player;

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
            return Object.Instantiate(_database.Player);
        }

        public Doors GetDoors()
        {
            return Object.Instantiate(_database.Doors);
        }

        public Projection GetProjection()
        {
            return Object.Instantiate(_database.Projection);
        }
    }
}
