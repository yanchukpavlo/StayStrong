using System.Collections.Generic;
using System;
using UnityEngine;
using Game.Core;
using Game.Systems.Save;

namespace Game.Systems.Building
{
    public class StructureManager : MonoBehaviour, ISaveable
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public List<StructureData> StructureDatas { get; private set; }

        [Header("Events")]
        [SerializeField] GameEvent eventBuildStructure;
        [SerializeField] GameEvent eventDestroyStructure;

        List<Structure> structures = new List<Structure>();

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(Id))
                Id = ISaveable.GetId();
        }

        private void OnEnable()
        {
            ISaveable.Saveables[Id] = this;
            eventBuildStructure.RegisterListener(OnBuildStructure);
            eventDestroyStructure.RegisterListener(OnDestroyStructure);
        }

        private void OnDisable()
        {
            ISaveable.Saveables.Remove(Id);
            eventBuildStructure.UnregisterListener(OnBuildStructure);
            eventDestroyStructure.UnregisterListener(OnDestroyStructure);
        }

        public void Reset() => structures = new List<Structure>();

        void OnBuildStructure(Component sender, object data)
        {
            Structure structure = (Structure)data;
            structures.Add(structure);
        }

        void OnDestroyStructure(Component sender, object data)
        {
            Structure structure = (Structure)data;
            structures.Remove(structure);
            structure.gameObject.SetActive(false);
            Destroy(structure.gameObject, 1f);
        }

        public object SaveState()
        {
            SaveStructure[] saveDatas = new SaveStructure[structures.Count];
            for (var i = 0; i < saveDatas.Length; i++)
            {
                saveDatas[i] = new SaveStructure(structures[i].Data.Id, structures[i].SaveState());
            }

            return saveDatas;
        }

        public void LoadState(object data)
        {
            SaveStructure[] saveDatas = (SaveStructure[])data;
            CreateLoadedStructures(saveDatas);
        }

        void CreateLoadedStructures(SaveStructure[] saveDatas)
        {
            foreach (var item in structures)
                Destroy(item.gameObject);

            structures.Clear();

            for (int i = 0; i < saveDatas.Length; i++)
            {
                if (IsCanGetStructureData(saveDatas[i].StructureId, out StructureData structureData))
                {
                    var structure = Instantiate(structureData.Prefab);
                    structure.LoadState(saveDatas[i].StructureData);
                    structures.Add(structure);
                }
            }
        }

        bool IsCanGetStructureData(string id, out StructureData structureData)
        {
            structureData = null;

            foreach (var item in StructureDatas)
            {
                if (item.Id == id)
                {
                    structureData = item;
                    return item;
                }
            }

            return false;
        }

        [Serializable]
        public class SaveStructure
        {
            public SaveStructure(string Id, object data)
            {
                StructureId = Id;
                StructureData = data;
            }

            public string StructureId;
            public object StructureData;
        }
    }
}