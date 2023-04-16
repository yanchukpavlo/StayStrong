using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Game.Utility;

namespace Game.Systems.Save
{
    public class SaveLoadSystem : PersistentSingletone<SaveLoadSystem>
    {
        public string FileName = "TestSaveFile";

        SaveLoadType saveLoadType = new BinarySaveLoadType();

        public void Save() => Save(FileName);

        public void Load() => Load(FileName);

        public bool CheckSaveFile() => CheckSaveFile(FileName);

        public void Save(string name)
        {
            //var dataPack = saveLoadType.LoadFile(SavePath(name));
            var dataPack = new Dictionary<string, object>();
            CaptureSaveable(dataPack);
            saveLoadType.SaveFile(SavePath(name), dataPack);
        }

        public void Load(string name)
        {
            var dataPack = saveLoadType.LoadFile(SavePath(name));
            RestoreSaveable(dataPack);
        }

        public bool CheckSaveFile(string name)
        {
            return saveLoadType.IsExist(SavePath(name));
        }

        string SavePath(string name)
        {
            return $"{Application.persistentDataPath}/{name}.txt";
        }

        void CaptureSaveable(Dictionary<string, object> dataPack)
        {
            foreach (var item in ISaveable.Saveables)
            {
                dataPack[item.Key] = item.Value.SaveState();
            }
        }

        void RestoreSaveable(Dictionary<string, object> dataPack)
        {
            foreach (var item in ISaveable.Saveables)
            {
                if (dataPack.TryGetValue(item.Key, out object statesPack))
                    item.Value.LoadState(statesPack);
            }
        }
    }

#if (UNITY_EDITOR)

    [CustomEditor(typeof(SaveLoadSystem))]
    public class EditorSaveLoadSystem : Editor
    {
        public override void OnInspectorGUI()
        {
            var script = (SaveLoadSystem)target;

            DrawDefaultInspector();

            if (GUILayout.Button("Save"))
                script.Save();

            if (GUILayout.Button("Load"))
                script.Load();
        }
    }

#endif

}