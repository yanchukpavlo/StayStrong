using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using Game.Utility;

namespace Game.InEditor
{
    [CustomEditor(typeof(IconCreator))]
    public class IconCreatorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            IconCreator myScript = (IconCreator)target;

            GUILayout.Space(10);
            if (GUILayout.Button("Create image"))
            {
                Type type = typeof(IconCreator);
                MethodInfo methodInfo = type.GetMethod("SaveIcon", BindingFlags.NonPublic | BindingFlags.Instance);
                methodInfo.Invoke(myScript, null);
            }
        }
    }
}