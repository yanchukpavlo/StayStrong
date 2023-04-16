using System.Collections.Generic;
using AYellowpaper;
using AYellowpaper.SerializedCollections;
using UnityEngine;
using Game.Core;
using Game.Systems.Save;

namespace Game.Systems.Building
{
    [CreateAssetMenu(menuName = "SO/Structure Data", fileName = "StructureData_")]
    public class StructureData : ScriptableObject, IId
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public string Name { get; private set; } = "Structure Name";
        [field: SerializeField] public Structure Prefab { get; private set; } = null;
        [field: SerializeField] public Sprite Icon { get; private set; } = null;
        [field: SerializeField] public SerializedDictionary<CurrencyData, float> PriceDictionary = null;
        [field: SerializeField] public SerializedDictionary<InterfaceReference<IVariable>, float> IncomeDictionary = null;

        [field: SerializeField, Header("Dummy info")] public Vector2 ColliderSize { get; private set; } = new Vector2(2f, 2f);
        [field: SerializeField] public Mesh Mesh { get; private set; } = null;

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(Id))
                Id = IId.GetId();
        }
    }
}