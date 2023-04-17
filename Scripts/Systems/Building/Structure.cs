using UnityEngine;
using Game.Core;
using Game.Systems.Save;

namespace Game.Systems.Building
{
    public abstract class Structure : BaseMapObject, ISaveable
    {
        [field: SerializeField] public StructureData Data { get; private set; } = null;
        [SerializeField] protected GameEvent eventDestroyStructure;

        public abstract string Id { get; }

        public abstract void LoadState(object data);

        public abstract object SaveState();
    }
}