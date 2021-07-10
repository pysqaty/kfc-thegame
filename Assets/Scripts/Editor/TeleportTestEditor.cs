using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TeleportTest))]
public class TeleportTestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GUILayout.BeginHorizontal();
        if(GUILayout.Button("Disappear"))
        {
            (target as TeleportTest).Disappear();
        }
        if(GUILayout.Button("Appear"))
        {
            (target as TeleportTest).Appear();
        }
        GUILayout.EndHorizontal();
        base.OnInspectorGUI();
    }
}
