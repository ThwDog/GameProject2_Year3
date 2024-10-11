using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

[CustomEditor(typeof(GroundRandomMaterial))]
public class GroundRandomMat_Editor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        var rnd = target as GroundRandomMaterial;
        
        if(GUILayout.Button("Random")) rnd.random();
    }
}


