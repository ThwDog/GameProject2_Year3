using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCode : MonoBehaviour
{
    // TODO : Make Cheat code easy to use 
    LoadScene loadScene;
    PlayerController player; 
    public bool cheatEnable = true;

    private void Awake() {
        loadScene = GetComponent<LoadScene>();
    }

    private void OnGUI() {
        areaFindCanCheat();

        if(!cheatEnable) return;
        if(GUI.Button(new Rect(10, 10, 100, 50),"Next scene")){
            loadScene._LoadScene();
        }
        if(player != null)
        {
            if(GUI.Button(new Rect(10, 80, 100, 50),"ReSpawn")){
                SpawnPlayer spawn = FindObjectOfType<SpawnPlayer>();
                spawn.deSpawn();
            }
            if(GUI.Button(new Rect(10, 160, 100, 50),"Increase Speed")){
                player.speed += 10;
            }
            if(player.speed > 5){
                if(GUI.Button(new Rect(10, 240, 100, 50),"decrease Speed")){
                    player.speed -= 10;
                }
            }
        }
    }

    private void areaFindCanCheat(){
        if(!player){
            try{
                if(loadScene.CheckNextStage() - 1 != 0){
                    player = FindObjectOfType<PlayerController>();
                }
            }
            catch{
                return;
            } 
        }
    }
}
