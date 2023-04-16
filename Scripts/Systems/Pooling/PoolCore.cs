using UnityEngine;
using UnityEngine.Pool;

namespace Game.Systems.Pooling
{
    [CreateAssetMenu(menuName = "SO/Pool", fileName = "Pool_")]
    public class PoolCore : ScriptableObject
    {
        enum PoolType
        {
            Stack,
            LinkedList
        }

        [SerializeField] GameObject prefab;

        [Header("Settings")]
        [SerializeField] PoolType poolType = PoolType.Stack;
        [SerializeField] bool collectionChecks = true;
        [SerializeField] int maxPoolSize = 10;
        [SerializeField] int startCapacity = 3;

        Transform root;

        IObjectPool<IPoolObject> pool;

        public IObjectPool<IPoolObject> Pool
        {
            get
            {
                if (root == null)
                    CreatePool();

                return pool;
            }
        }

        public IPoolObject Get(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            Pool.Get(out IPoolObject obj);
            obj.Tr.SetPositionAndRotation(position, rotation);
            obj.Tr.localScale = scale;

            return obj;
        }

        public IPoolObject Get(Vector3 position, Quaternion rotation)
        {
            Pool.Get(out IPoolObject obj);
            obj.Tr.SetPositionAndRotation(position, rotation);

            return obj;
        }

        public IPoolObject Get(Vector3 position)
        {
            return Get(position, Quaternion.identity);
        }

        public IPoolObject Get(Vector3 position, float maxRandomY)
        {
            return Get(position, Quaternion.Euler(0, maxRandomY * Random.value, 0));
        }

        void CreatePool()
        {
            root = new GameObject($"Pool_{prefab.name}").transform;

            if (poolType == PoolType.Stack)
                pool = new ObjectPool<IPoolObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, startCapacity, maxPoolSize);
            else
                pool = new LinkedPool<IPoolObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, collectionChecks, maxPoolSize);
        }

        IPoolObject CreatePooledItem()
        {
            IPoolObject newObject = Instantiate(prefab, root).GetComponent<IPoolObject>();
            newObject.InitPooledItem(Pool);

            return newObject;
        }

        void OnTakeFromPool(IPoolObject obj)
        {
            obj.TakeFromPool();
        }

        void OnReturnedToPool(IPoolObject obj)
        {
            obj.ReturnedToPool();
        }        

        void OnDestroyPoolObject(IPoolObject obj)
        {
            Destroy(obj.GO);
        }
    }
}