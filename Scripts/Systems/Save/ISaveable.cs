using System.Collections.Generic;

namespace Game.Systems.Save
{
    public interface ISaveable : IId
    {
        public static Dictionary<string, ISaveable> Saveables = new Dictionary<string, ISaveable>();

        public object SaveState();

        public void LoadState(object data);
    }
}