using System;
using UnityEngine;

namespace Game.Core
{
    public static class EventsManager
    {
        public static event Action OnUpdate;
        public static void InvokeUpdate()
        {
            OnUpdate?.Invoke();
        }

        public static event Action OnTick;
        public static void InvokeTick()
        {
            OnTick?.Invoke();
        }

        public static event Action<Vector2> OnInputMove;
        public static void InvokeInputMove(Vector2 value)
        {
            OnInputMove?.Invoke(value);
        }

        public static event Action<float> OnInputRotate;
        public static void InvokeInputRotate(float value)
        {
            OnInputRotate?.Invoke(value);
        }

        public static event Action<float> OnInputZoom;
        public static void InvokeInputZoom(float value)
        {
            OnInputZoom?.Invoke(value);
        }

        public static event Action OnInputClick;
        public static void InvokeInputClick()
        {
            OnInputClick?.Invoke();
        }
    }
}