using UnityEngine;
using Game.Systems.Pooling;

namespace Game.Systems.Audio
{
    public class AudioFromPool : MonoBehaviour
    {
        [SerializeField] PoolCore sfxPool;
        [SerializeField] bool selfPos;

        public void PlayFromPool()
        {
            sfxPool.Pool.Get().Tr.position = selfPos ? transform.position : Vector3.zero;
        }
    }
}