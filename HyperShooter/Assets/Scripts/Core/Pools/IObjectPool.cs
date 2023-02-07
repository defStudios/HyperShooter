using UnityEngine;

namespace Core.Pools
{
    public interface IObjectPool<TObject>
    {
        public TObject GetInstance();
        public void DestroyInstance(TObject instance);
    }
}