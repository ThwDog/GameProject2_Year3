using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(EnableOnCam))]
public class EnableOnCam_Editor : Editor
{
    SerializedProperty closeType;
    SerializedProperty _target;
    // SerializedProperty range;

    private void OnEnable() {
        closeType = serializedObject.FindProperty("closeType");
        _target = serializedObject.FindProperty("target");
        // range = serializedObject.FindProperty("range");
    }

    public override void OnInspectorGUI() {
        serializedObject.Update ();

        var enableOnCam = target as EnableOnCam;
        EditorGUILayout.LabelField("");

        EditorGUILayout.PropertyField(closeType);
        if(enableOnCam.closeType == EnableOnCam.type.obj){
            EditorGUILayout.PropertyField(_target);
        }
        // EditorGUILayout.PropertyField(range);
        

        serializedObject.ApplyModifiedProperties ();
    }
}
