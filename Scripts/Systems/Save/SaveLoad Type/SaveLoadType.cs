using System.Collections.Generic;

namespace Game.Systems.Save
{
    public abstract class SaveLoadType
    {
        public abstract bool IsExist(string path);
        public abstract void SaveFile(string path, object data);

        public abstract Dictionary<string, object> LoadFile(string path);

    }
}