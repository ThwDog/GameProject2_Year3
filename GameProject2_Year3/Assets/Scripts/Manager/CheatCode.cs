using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCode : MonoBehaviour
{
    // TODO : Make Cheat code easy to use 
    LoadScene loadScene;
    public bool cheatEnable = true;

    private void Awake() {
        loadScene = GetComponent<LoadScene>();
    }

    private void OnGUI() {
        if(!cheatEnable) return;
        if(GUI.Button(new Rect(10, 10, 100, 50),"Next scene")){
            loadScene._LoadScene();
        }
        if(FindAnyObjectByType<PlayerController>())
        {
            if(GUI.Button(new Rect(10, 80, 100, 50),"ReSpawn")){
                SpawnPlayer spawn = FindAnyObjectByType<SpawnPlayer>();
                spawn.deSpawn();
            }
            if(GUI.Button(new Rect(10, 160, 100, 50),"Increase Speed")){
                PlayerController player = FindAnyObjectByType<PlayerController>();
                player.speed += 10;
            }
            if(FindAnyObjectByType<PlayerController>().speed > 5){
                if(GUI.Button(new Rect(10, 240, 100, 50),"decrease Speed")){
                    PlayerController player = FindAnyObjectByType<PlayerController>();
                    player.speed -= 10;
                }
            }
        }
    }
}
