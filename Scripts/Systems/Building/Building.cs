using UnityEngine;
using UnityEngine.EventSystems;
using Game.Core;

namespace Game.Systems.Building
{
    public class Building : Structure
    {
        [Header("Events")]
        [SerializeField] GameEvent eventSelectBuilding;

        public override string Id => string.Empty;

        public override void OnPointerClick(PointerEventData eventData)
        {
            eventSelectBuilding.Invoke(this, null);
        }

        private void OnEnable()
        {
            foreach (var item in Data.IncomeDictionary)
            {
                if (item.Key.Value is VariableFloat variableFloat)
                    variableFloat.AddingValue(item.Value);
                else if (item.Key.Value is VariableInt variableInt)
                    variableInt.AddingValue((int)item.Value);
            }
        }

        private void OnDisable()
        {
            foreach (var item in Data.IncomeDictionary)
            {
                if (item.Key.Value is VariableFloat variableFloat)
                    variableFloat.AddingValue(-item.Value);
                else if (item.Key.Value is VariableInt variableInt)
                    variableInt.AddingValue(-(int)item.Value);
            }
        }

        public override void LoadState(object data)
        {
            var saveData = (SaveData)data;
            transform.position = saveData.Position;
            transform.rotation = Quaternion.Euler(0, saveData.rotationY, 0);
        }

        public override object SaveState()
        {
            return new SaveData(transform.position, transform.eulerAngles.y);
        }

        [System.Serializable]
        public class SaveData
        {
            public SaveData(Vector3 pos, float elerY)
            {
                positionX = pos.x;
                positionY = pos.y;
                positionZ = pos.z;

                rotationY = elerY;
            }

            public float positionX;
            public float positionY;
            public float positionZ;

            public float rotationY;

            public Vector3 Position => new Vector3(positionX, positionY, positionZ);
        }
    }
}