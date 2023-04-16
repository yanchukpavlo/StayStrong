using UnityEngine;
using Cinemachine;
using Game.Core.Player.Input;
using Game.Utility;

namespace Game.Core.Player.Camera
{
    public class CameraMover : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
        [SerializeField] VariableFloat mapSizeHalfVariable;

        [Header("Settings")]
        [SerializeField] bool useEdgeScrolling = false;
        [SerializeField] int edgeScrollSize = 20;
        [Space]
        [SerializeField] Vector2 moveSpeed = new Vector2(5f, 20f);
        [SerializeField] float zoomSpeed = 3f;
        [SerializeField] float rotateSpeed = 30f;
        [Space]
        [SerializeField] Vector2 offsetZoom = new Vector2(5f, 30f);
        [SerializeField, Min(0.001f)] float zoomLerp = 0.1f;
        [SerializeField, Min(1)] float heightToOnCollider = 10f;

        Transform Tr;
        float currentSpeed;
        Vector3 followOffset;
        CinemachineTransposer transposer;
        CinemachineCollider cinemachineCollider;

        private void Awake()
        {
            Tr = transform;
            transposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            cinemachineCollider = cinemachineVirtualCamera.GetComponent<CinemachineCollider>();
            followOffset = transposer.m_FollowOffset;

            UpdateSppedAndCollider(followOffset.magnitude);
        }

        private void OnEnable()
        {
            EventsManager.OnUpdate += OnUpdate;
        }

        private void OnDisable()
        {
            EventsManager.OnUpdate -= OnUpdate;
        }

        private void OnUpdate()
        {
            HandleCameraMovement();

            if (useEdgeScrolling)
                HandleCameraMovementEdgeScrolling();

            HandleCameraRotation();
            HandleCameraZoom();

            transposer.m_FollowOffset = Vector3.Lerp(transposer.m_FollowOffset, followOffset, zoomLerp);
        }

        void HandleCameraMovement()
        {
            if (InputsReceiver.Move == Vector2.zero)
                return;

            Vector3 moveVector = currentSpeed * GameCore.DeltaTime * (Tr.forward * InputsReceiver.Move.y + Tr.right * InputsReceiver.Move.x);
            Tr.position = (Tr.position + moveVector).ClampedTo(mapSizeHalfVariable.Value);
        }

        void HandleCameraMovementEdgeScrolling()
        {
            Vector2 inputDir = new();
            Vector2 mousePosition = Helper.MousePosition;

            if (mousePosition.x < edgeScrollSize)
            {
                inputDir.x = -1f;
            }
            else if (mousePosition.x > Screen.width - edgeScrollSize)
            {
                inputDir.x = +1f;
            }

            if (mousePosition.y < edgeScrollSize)
            {
                inputDir.y = -1f;
            }
            else if (mousePosition.y > Screen.height - edgeScrollSize)
            {
                inputDir.y = +1f;
            }

            if (inputDir == Vector2.zero)
                return;

            inputDir.Normalize();

            Vector3 moveVector = currentSpeed * GameCore.DeltaTime * (Tr.forward * inputDir.y + Tr.right * inputDir.x);
            Tr.position = (Tr.position + moveVector).ClampedTo(mapSizeHalfVariable.Value);
        }

        void HandleCameraRotation()
        {
            if (InputsReceiver.Rotate .IsZero())
                return;

            Tr.eulerAngles += new Vector3(0, InputsReceiver.Rotate * rotateSpeed * GameCore.DeltaTime, 0);
        }

        void HandleCameraZoom()
        {
            if (InputsReceiver.Zoom .IsZero())
                return;

            followOffset += zoomSpeed * (InputsReceiver.Zoom < 0 ? followOffset.normalized : -followOffset.normalized);
            followOffset = followOffset.ClampedTo(offsetZoom.x, offsetZoom.y);

            UpdateSppedAndCollider(followOffset.magnitude);
        }

        void UpdateSppedAndCollider(float followMagnitude)
        {
            cinemachineCollider.enabled = followMagnitude < heightToOnCollider;
            currentSpeed = Mathf.Lerp(moveSpeed.x, moveSpeed.y, Helper.GetPercent(followMagnitude, offsetZoom.x, offsetZoom.y));
        }
    }
}