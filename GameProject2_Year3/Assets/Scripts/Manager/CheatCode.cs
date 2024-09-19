using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCode : MonoBehaviour
{
    // TODO : Make Cheat code easy to use 
    LoadScene loadScene;

    private void Awake() {
        loadScene = GetComponent<LoadScene>();
    }

    private void OnGUI() {
        if(GUI.Button(new Rect(10, 10, 100, 50),"Next scene")){
            loadScene._LoadStage();
        }
        if(GUI.Button(new Rect(10, 80, 100, 50),"ReSpawn")){
            SpawnPlayer spawn = FindAnyObjectByType<SpawnPlayer>();
            spawn.deSpawn();
        }
    }
}
