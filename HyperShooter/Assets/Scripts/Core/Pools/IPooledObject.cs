using UnityEngine;

namespace Core.Pools
{
    public interface IPooledObject<T> where T: MonoBehaviour
    {
        public IObjectPool<T> Pool { get; }

        public void SetPool(IObjectPool<T> pool);
        public void OnSpawned();
        public void Despawn();
    }
}