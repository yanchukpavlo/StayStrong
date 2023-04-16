using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Core.Player.Input
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputsReceiver : MonoBehaviour
    {
        public static Vector2 Move { get; private set; }
        public static float Rotate { get; private set; }
        public static float Zoom { get; private set; }

        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 value = context.ReadValue<Vector2>();
            Move = value;
            EventsManager.InvokeInputMove(value);
        }

        public void OnRotate(InputAction.CallbackContext context)
        {
            float value = context.ReadValue<float>();
            Rotate = value;
            EventsManager.InvokeInputRotate(value);
        }

        public void OnZoom(InputAction.CallbackContext context)
        {
            float value = context.ReadValue<float>();
            Zoom = value;
            EventsManager.InvokeInputZoom(value);
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            if (context.canceled)
                EventsManager.InvokeInputClick();
        }
    }
}