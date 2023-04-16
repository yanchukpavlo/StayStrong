using UnityEngine;

namespace Game.Systems.Building
{
    [RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
    public class BuildDummy : MonoBehaviour
    {
        [SerializeField] Material canPlaceMaterial;
        [SerializeField] Material blockedPlaceMaterial;

        public bool CanPlace => collidedAmount == 0;
        public Transform CacheTr { get; private set; }

        Renderer dummyRenderer;
        MeshFilter dummyMeshFilter;
        BoxCollider dummyCollider;
        int collidedAmount;

        private void Awake()
        {
            CacheTr = transform;

            dummyMeshFilter = GetComponentInChildren<MeshFilter>();
            dummyRenderer = GetComponentInChildren<MeshRenderer>();

            dummyCollider = GetComponent<BoxCollider>();
            dummyCollider.isTrigger = true;

            GetComponent<Rigidbody>().isKinematic = true;

            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (CanPlace)
                SetPlaceMaterial(false);

            collidedAmount++;
        }

        private void OnTriggerExit(Collider other)
        {
            collidedAmount--;

            if (CanPlace)
                SetPlaceMaterial(true);
        }

        public void TurnOn(StructureData data)
        {
            dummyCollider.size = new Vector3 (data.ColliderSize.x, 1, data.ColliderSize.y);
            dummyMeshFilter.mesh = data.Mesh;
            dummyRenderer.materials = new Material[data.Mesh.subMeshCount];
            SetPlaceMaterial(true);
            gameObject.SetActive(true);
        }

        public void TurnOff()
        {
            gameObject.SetActive(false);
            collidedAmount = 0;
        }

        void SetPlaceMaterial(bool isCanPlaceMaterial)
        {
            Material[] placeMaterial = new Material[dummyRenderer.materials.Length];

            for (int i = 0; i < placeMaterial.Length; i++)
            {
                placeMaterial[i] = isCanPlaceMaterial ? canPlaceMaterial : blockedPlaceMaterial;
            }

            dummyRenderer.materials = placeMaterial;
        }
    }
}