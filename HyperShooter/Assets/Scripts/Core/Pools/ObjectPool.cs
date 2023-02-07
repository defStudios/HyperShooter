using System.Collections.Generic;
using UnityEngine;

namespace Core.Pools
{
    public class ObjectPool<TObject> : IObjectPool<TObject> where TObject: MonoBehaviour
    {
        private readonly IPooledObject<TObject> _original;
        private readonly Transform _container;
        private readonly Queue<IPooledObject<TObject>> _pool;

        public ObjectPool(IPooledObject<TObject> original, int initialAmount)
        {
            _original = original;
            _pool = new Queue<IPooledObject<TObject>>();
            _container = new GameObject($"Pool [{typeof(TObject)}]").transform;

            Object.DontDestroyOnLoad(_container);
            
            for (int i = 0; i < initialAmount; i++)
                CreateInstance();
        }
        
        public TObject GetInstance()
        {
            if (_pool.Count == 0)
                CreateInstance();

            var instance = _pool.Dequeue() as TObject;
            instance.gameObject.SetActive(true);
            (instance as IPooledObject<TObject>).OnSpawned();

            return instance;
        }
        
        public void DestroyInstance(TObject instance)
        {
            instance.gameObject.SetActive(false);
            _pool.Enqueue(instance as IPooledObject<TObject>);
        }

        private void CreateInstance()
        {
            var instanceObject = Object.Instantiate(_original as TObject, _container);
            instanceObject.gameObject.SetActive(false);

            var instance = instanceObject as IPooledObject<TObject>;
            instance.SetPool(this);
            _pool.Enqueue(instance);
        }
    }
}