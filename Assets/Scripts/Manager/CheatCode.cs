using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCode : MonoBehaviour
{
    LoadScene loadScene;

    private void Awake() {
        loadScene = GetComponent<LoadScene>();
    }

    private void OnGUI() {
        if(GUI.Button(new Rect(10, 10, 100, 100),"Next scene")){
            loadScene._LoadStage();
        }
    }
}
