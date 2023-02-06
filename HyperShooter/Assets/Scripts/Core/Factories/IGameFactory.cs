using Core.Services;
using UnityEngine;
using Player;
using Visualizers;

namespace Core.Factories
{
    public interface IGameFactory : ISingleService
    {
        public PlayerController SpawnPlayer(Vector3 position);
        public Doors SpawnDoors(Vector3 position);
        public Projection SpawnProjection(Vector3 position);
    }
}
