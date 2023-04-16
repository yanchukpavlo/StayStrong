using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Game.Utility;

namespace Game.Systems.Pooling
{
    public class PoolObject : MonoBehaviour, IPoolObject
    {
        [SerializeField] float returnTime = 3f;

        public GameObject GO => gameObject;
        public Transform Tr { get; private set; }
        public IObjectPool<IPoolObject> Pool { get; private set; }

        private void OnEnable()
        {
            StartCoroutine(WaitCoroutine());
        }

        public virtual void InitPooledItem(IObjectPool<IPoolObject> initPool)
        {
            Pool = initPool;
            Tr = transform;
        }

        IEnumerator WaitCoroutine()
        {
            yield return Helper.GetWait(returnTime);
            Pool.Release(this);
        }
    }
}