using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Game.Systems.Save
{
    public class BinarySaveLoadType : SaveLoadType
    {
        public override bool IsExist(string path)
        {
            return File.Exists(path);
        }

        public override void SaveFile(string path, object data)
        {
            using (var stream = File.Open(path, FileMode.Create))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, data);
            }
        }

        public override Dictionary<string, object> LoadFile(string path)
        {
            if (!File.Exists(path))
                return new Dictionary<string, object>();

            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                stream.Position = 0;
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }
    }
}