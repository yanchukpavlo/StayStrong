using UnityEngine;
using UnityEngine.EventSystems;
using Game.Systems.Pooling;
using Game.Core;

namespace Game.Systems.Building
{
    public class Resource : Structure
    {
        [Space]
        [SerializeField, Min(0)] int resourceCount;
        [SerializeField] CurrencyData currency;
        [SerializeField] PoolCore fxImpact;

        int resourceRemained;

        public override string Id => string.Empty;

        private void Awake()
        {
            resourceRemained = resourceCount;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            fxImpact.Get(eventData.pointerPressRaycast.worldPosition,
                Quaternion.FromToRotation(transform.up, eventData.pointerPressRaycast.worldNormal) *
                Quaternion.Euler(0f, Random.value * 360f, 0f));

            ResourceSubtraction();
        }

        public override void LoadState(object data)
        {
            var saveData = (SaveData)data;

            transform.position = saveData.Position;
            transform.rotation = Quaternion.Euler(0, saveData.rotationY, 0);
            resourceRemained = saveData.resourceRemained;
        }

        public override object SaveState()
        {
            return new SaveData(transform.position, transform.eulerAngles.y, resourceRemained);
        }

        void ResourceSubtraction()
        {
            int amount = resourceRemained >= DataManager.Instance.ClickInfluenceVariable.Value ?
                DataManager.Instance.ClickInfluenceVariable.Value : DataManager.Instance.ClickInfluenceVariable.Value - resourceRemained;

            DataManager.Wallet.CurrencyAdd(currency, amount);

            if ((resourceRemained -= amount) == 0)
            {
                eventDestroyStructure.Invoke(this, this);
                Destroy(gameObject);
            }
        }

        [System.Serializable]
        public class SaveData
        {
            public SaveData(Vector3 pos, float elerY, int remained)
            {
                positionX = pos.x;
                positionY = pos.y;
                positionZ = pos.z;

                rotationY = elerY;

                resourceRemained = remained;
            }

            public float positionX;
            public float positionY;
            public float positionZ;

            public float rotationY;

            public int resourceRemained;

            public Vector3 Position => new Vector3(positionX, positionY, positionZ);
        }
    }
}