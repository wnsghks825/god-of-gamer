using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GodOfGamer.Event
{
    /// <summary>
    /// EventDefine 에디터 클래스
    /// </summary>
    [CustomEditor(typeof(DefineEventTable))]
    public class EventDefineEditor : Editor
    {
        DefineEventTable eventDefine;

        SerializedProperty csvFileProp;
        SerializedProperty eventsProp;

        List<DefineEvent> evList = new List<DefineEvent>();

        private void Initialize(List<Dictionary<string, object>> csvReadData)
        {
            bool creating = false;
            
            if(eventDefine.tempEventsList.Count != csvReadData.Count)
            {
                eventDefine.tempEventsList = new List<BindingEvent>(evList.Count);
                creating = true;
            }

            foreach ( var line in csvReadData)
            {
                var temp = new DefineEvent(line["Key"].ToString(), line["Descript"].ToString());

                evList.Add(temp);

                if (creating)
                    eventDefine.tempEventsList.Add(new BindingEvent());     // 잘 작동되는지 파악을 자세히
            }
        }

        private void OnEnable()
        {
            eventDefine = (DefineEventTable)target;
            csvFileProp = serializedObject.FindProperty("csvFile");
            eventsProp = serializedObject.FindProperty("tempEventsList");

            evList = new List<DefineEvent>(0);
        }

        public override void OnInspectorGUI()
        {
            // csvFile이 할당되어있지 않다면 오브젝트 필드로 할당할 수 있게 설정
            if (eventDefine.csvFile == null)
            {
                eventDefine.csvFile = EditorGUILayout.ObjectField("CSVFile", eventDefine.csvFile, typeof(TextAsset), true) as TextAsset;
            }
            else
            {
                if (evList.Count == 0)
                    Initialize(Core.CSVReader.Read(eventDefine.csvFile));

                foreach (var eventDefine in evList)
                {
                    EditorGUILayout.LabelField(eventDefine.key, eventDefine.descript.ToString());
                }

                EditorGUILayout.PropertyField(eventsProp, true);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}