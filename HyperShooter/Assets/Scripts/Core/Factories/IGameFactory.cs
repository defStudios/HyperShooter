using Core.Services;
using UnityEngine;
using Player;

namespace Core.Factories
{
    public interface IGameFactory : ISingleService
    {
        public PlayerController SpawnPlayer(Vector3 position);
    }
}
