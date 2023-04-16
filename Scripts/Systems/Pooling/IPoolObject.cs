using UnityEngine;
using UnityEngine.Pool;

namespace Game.Systems.Pooling
{
    public interface IPoolObject
    {
        public GameObject GO { get; }
        public Transform Tr { get; }
        public IObjectPool<IPoolObject> Pool { get; }

        public void InitPooledItem(IObjectPool<IPoolObject> initPool);

        public virtual void TakeFromPool()
        {
            GO.SetActive(true);
        }

        public virtual void ReturnedToPool()
        {
            GO.SetActive(false);
        }
    }
}