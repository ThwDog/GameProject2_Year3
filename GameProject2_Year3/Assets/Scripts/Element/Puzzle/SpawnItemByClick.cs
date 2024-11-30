using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItemByClick : MonoBehaviour , Ipauseable , IRestartable
{
    [Header("Setting")]
    [SerializeField] ItemScript spawnObj;
    ShowUICollision showUI;
    [SerializeField] EventScript _event;
    [SerializeField] string soundName;
    public bool isSpawn = false;
    [SerializeField] bool giveRealItem = true;
    [Header("For fake Spawn")]
    [SerializeField] CollectableItem_Scriptable item;


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
            if(!spawnObj) {
                if(_event) _event._FinishEvent();
                return;
            }
            if(giveRealItem) spawnObj.Collect(FindAnyObjectByType<InventorySystem>());
            else {
                if(item._sprite != null) FindAnyObjectByType<InventorySystem>().player.setShowItemSprite(item._sprite);
                spawnObj.setCollect(FindAnyObjectByType<InventorySystem>());
            }
            playSound();
            if(_event) _event._FinishEvent();
            CloseDescription();
        }
    }

    private void playSound(){
        if(soundName != null) SoundManager.instance.PlaySfx(soundName);
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
