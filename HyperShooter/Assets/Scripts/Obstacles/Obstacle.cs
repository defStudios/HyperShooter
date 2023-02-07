using System.Threading.Tasks;
using UnityEngine;
using Core.Pools;
using Infection;

namespace Obstacles
{
    public class Obstacle : MonoBehaviour, IPooledObject<Obstacle>, IInfectable
    {
        public IObjectPool<Obstacle> Pool { get; private set; }

        [SerializeField] private ObstacleAppearance appearance;

        public async void Infect(int durationMilliseconds)
        {
            appearance.SetInfectedAppearance();
            await Task.Delay(durationMilliseconds);
            DestroySelf();
        }

        public void SetPool(IObjectPool<Obstacle> pool) => Pool = pool;
        public void OnSpawned() => appearance.SetDefaultAppearance();
        public void Despawn() => Pool.DestroyInstance(this);

        private void DestroySelf()
        {
            Despawn();
        }
    }
}
