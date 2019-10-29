using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GodOfGamer.Event
{
    [CustomEditor(typeof(BasicEventTable))]
    public class BasicEventTableEditor : Editor
    {
        BasicEventTable basicEventDefine;

        SerializedProperty basicListProp;

        private void OnEnable()
        {
            basicEventDefine = (BasicEventTable)target;

            basicListProp = serializedObject.FindProperty("tempBasicList");
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.PropertyField(basicListProp, true);

            serializedObject.ApplyModifiedProperties();
        }
    }
}