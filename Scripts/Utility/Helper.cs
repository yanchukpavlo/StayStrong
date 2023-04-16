using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Game.Utility
{
    public static class Helper
    {
        public static Vector2 MousePosition => Mouse.current.position.ReadValue();

        static Camera _camera;
        public static Camera Camera
        {
            get
            {
                if (!_camera) _camera = Camera.main;
                return _camera;
            }
        }

        public static Ray MouseToRay()
        {
            return Camera.ScreenPointToRay(MousePosition);
        }

        static public IEnumerator WaitAdnDo(float waitTime, Action doAfter)
        {
            yield return GetWait(waitTime);
            doAfter?.Invoke();
        }

        static readonly Dictionary<float, WaitForSeconds> WaitDictionaty = new Dictionary<float, WaitForSeconds>();
        public static WaitForSeconds GetWait(float time)
        {
            if (WaitDictionaty.TryGetValue(time, out var wait)) return wait;

            WaitDictionaty[time] = new WaitForSeconds(time);
            return WaitDictionaty[time];
        }

        static List<RaycastResult> _resultsObject = new List<RaycastResult>();
        public static bool IsPointerOverObject(bool debug = false)
        {
            PointerEventData _eventDataObject = new PointerEventData(EventSystem.current) { position = Mouse.current.position.ReadValue() };
            _resultsObject.Clear();
            EventSystem.current.RaycastAll(_eventDataObject, _resultsObject);

            if (debug)
                foreach (var item in _resultsObject)
                    Debug.Log(item.gameObject.name);

            return _resultsObject.Count > 0;
        }

        static List<RaycastResult> _resultsUI = new List<RaycastResult>();
        public static bool IsPointerOverUI(bool debug = false)
        {
            PointerEventData _eventDataUI = new PointerEventData(EventSystem.current) { position = Mouse.current.position.ReadValue() };
            _resultsUI.Clear();
            EventSystem.current.RaycastAll(_eventDataUI, _resultsUI);

            EventSystem.current.RaycastAll(_eventDataUI, _resultsUI);
            foreach (RaycastResult r in _resultsUI)
                if (r.gameObject.GetComponent<RectTransform>() != null)
                    return true;

            if (debug)
                foreach (var item in _resultsUI)
                    Debug.Log(item.gameObject.name);

            return false;
        }

        public static void DeleteChildren(this Transform t)
        {
            foreach (Transform child in t) UnityEngine.Object.Destroy(child.gameObject);
        }

        public static void DeleteDisabledChildren(this Transform t)
        {
            foreach (Transform child in t)
            {
                if (!child.gameObject.activeSelf) UnityEngine.Object.Destroy(child.gameObject);
                else DeleteDisabledChildren(child);
            }
        }

        public static float GetPercent(float currentValue, float minValue, float maxValue)
        {
            return (currentValue - minValue) / (maxValue - minValue);
        }

        /// <summary>
        /// Class to cast to type <see cref="T"/>
        /// </summary>
        /// <typeparam name="T">Target type</typeparam>
        public static class CastTo<T>
        {
            /// <summary>
            /// Casts <see cref="S"/> to <see cref="T"/>.
            /// This does not cause boxing for value types.
            /// Useful in generic methods.
            /// </summary>
            /// <typeparam name="S">Source type to cast from. Usually a generic type.</typeparam>
            public static T From<S>(S s)
            {
                return Cache<S>.caster(s);
            }

            private static class Cache<S>
            {
                public static readonly Func<S, T> caster = Get();

                private static Func<S, T> Get()
                {
                    var p = Expression.Parameter(typeof(S));
                    var c = Expression.ConvertChecked(p, typeof(T));
                    return Expression.Lambda<Func<S, T>>(c, p).Compile();
                }
            }
        }
    }
}