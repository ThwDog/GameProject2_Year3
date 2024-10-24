using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemByClick : MonoBehaviour , Ipauseable , IRestartable
{
    [Header("Setting")]
    [SerializeField] ItemScript spawnObj;
    ShowUICollision showUI;
    public bool isSpawn = false;

    public void ShowDescription(){
        if(GetComponent<ShowUICollision>()){
            showUI = GetComponent<ShowUICollision>();
            showUI.ShowDescription();
        }
    }

    public void CloseDescription(){
        if(GetComponent<ShowUICollision>()){
            showUI = GetComponent<ShowUICollision>();
            showUI.CloseDescription();
        }
    }
    
    public void Spawn(){
        if(!isSpawn){
            Debug.Log("Spawn Item : "  + spawnObj.name);
            isSpawn = true;
            if(!spawnObj) return;
            spawnObj.Collect(FindAnyObjectByType<InventorySystem>());
            CloseDescription();
        }
    }

    public void pause(){
    }

    public void resume(){
    }

    public void _Restart()
    {
        isSpawn = false;
    }
}
