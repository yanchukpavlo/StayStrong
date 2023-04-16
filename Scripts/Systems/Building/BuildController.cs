using UnityEngine;
using Cinemachine;
using Game.Utility;
using Game.Core;
using Game.Systems.Pooling;

namespace Game.Systems.Building
{
    [RequireComponent(typeof(CinemachineImpulseSource))]
    public class BuildController : MonoBehaviour
    {
        [SerializeField] BuildDummy buildingDummy;
        [SerializeField] PoolCore poolFxBuild;
        [SerializeField] PoolCore poolFxDestroy;

        [Header("Settings")]
        [SerializeField] float rotateAmount = 45f;
        [SerializeField] LayerMask groundLayer;
        [SerializeField] float fxAddYPos = 0.3f;
        [SerializeField] Vector2 fxScale = new Vector2(2, 3);

        [Header("Events")]
        [SerializeField] GameEvent eventStartBuild;
        [SerializeField] GameEvent eventBuildStructure;
        [SerializeField] GameEvent eventDestroyStructure;
        [SerializeField] GameEvent eventStartMoveBuilding;
        [SerializeField] GameEvent eventStopMoveBuilding;

        RaycastHit hit;
        StructureData structureData;
        Transform targetTr;
        Transform buildingsRoot;
        CinemachineImpulseSource impulseSource;

        private void Awake()
        {
            buildingsRoot = new GameObject("Buildings_Root").transform;
            impulseSource = GetComponent<CinemachineImpulseSource>();
        }

        private void OnEnable()
        {
            eventStartMoveBuilding.RegisterListener(OnStartMoveBuilding);
            eventStartBuild.RegisterListener(OnStartBuild);
            eventDestroyStructure.RegisterListener(OnDestroyStructure);
        }

        private void OnDisable()
        {
            eventStartMoveBuilding.UnregisterListener(OnStartMoveBuilding);
            eventStartBuild.UnregisterListener(OnStartBuild);
            eventDestroyStructure.UnregisterListener(OnDestroyStructure);
        }

        private void Update()
        {
            Ray ray = Helper.MouseToRay();
            if (Physics.Raycast(ray, out hit, 1000, groundLayer))
                buildingDummy.CacheTr.position = hit.point;
        }

        void OnStartMoveBuilding(Component sender, object data)
        {
            Building building = (Building)data;
            StartMove(building.Data, building.transform);
        }

        void OnStartBuild(Component sender, object data)
        {
            StructureData info = (StructureData)data;
            StartBuild(info);
        }

        void OnDestroyStructure(Component sender, object data)
        {
            if (data is Building building)
            {
                impulseSource.GenerateImpulseWithForce(1f);
                poolFxDestroy.Get(building.transform.position + Vector3.up * fxAddYPos,
                    Vector3Extensions.RandomEulerY(360f), Vector3Extensions.RandomOne(fxScale));
            }
        }

        void OnClick()
        {
            if (Helper.IsPointerOverObject() || !buildingDummy.CanPlace)
            {
                impulseSource.GenerateImpulseWithForce(0.05f);
                return;
            }

            if (targetTr)
                BuildingPlace();
            else
                BuildingCreate();
        }

        void OnRotateObject(float value)
        {
            if (value.IsZero())
                return;

            value = Mathf.Clamp(value, -1f, 1f);
            buildingDummy.CacheTr.Rotate(Vector3.up, rotateAmount * value);
        }

        void StartBuild(StructureData data)
        {
            buildingDummy.TurnOn(data);

            structureData = data;
            StartMove(data, null);
        }

        void StartMove(StructureData data, Transform objectToMove)
        {
            if (!targetTr)
            {
                EventsManager.OnInputClick += OnClick;
                EventsManager.OnInputZoom += OnRotateObject;
            }

            targetTr = objectToMove;

            if (targetTr)
            {
                targetTr.GetComponent<Collider>().enabled = false;
            }

            buildingDummy.TurnOn(data);
        }

        void BuildingCreate()
        {
            DataManager.Wallet.CurrencySubtraction(structureData.PriceDictionary);
            Structure newStructure = Instantiate(structureData.Prefab, buildingsRoot);
            targetTr = newStructure.transform;
            eventBuildStructure.Invoke(this, newStructure);
            BuildingPlace();
        }

        void BuildingPlace()
        {
            impulseSource.GenerateImpulseWithForce(0.5f);
            poolFxBuild.Get(buildingDummy.CacheTr.position + Vector3.up * fxAddYPos,
                Vector3Extensions.RandomEulerY(360f), Vector3Extensions.RandomOne(fxScale));

            targetTr.SetPositionAndRotation(buildingDummy.CacheTr.position, buildingDummy.CacheTr.rotation);
            targetTr.GetComponent<Collider>().enabled = true;

            BuildingCancel();
        }

        void BuildingCancel()
        {
            EventsManager.OnInputClick -= OnClick;
            EventsManager.OnInputZoom -= OnRotateObject;

            eventStopMoveBuilding.Invoke(this, null);

            targetTr = null;
            buildingDummy.TurnOff();
        }
    }
}