using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(BlackboardGeneric))]
public class BlackBoardInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        BlackboardGeneric bb = target as BlackboardGeneric;

        foreach (var entry in bb.Memories)
        {
            GUILayout.BeginHorizontal();

            GUILayout.Label(entry.Key, GUILayout.Width(100));
            GUILayout.Label(entry.Value.ToString(), GUILayout.Height(100));


            //Logica de conversion//
            //if (entry.Value is float)
            //{
            //    float intVal = (float)entry.Value;
            //    EditorGUILayout.FloatField(intVal);
            //}

            //if (entry.Value is GameObject)
            //{
            //    GameObject go = (GameObject)entry.Value;
            //    EditorGUILayout.ObjectField(go, typeof(GameObject), true);

            //}

            GUILayout.EndHorizontal();
        }
    }
}