using System.Collections;
using System.Collections.Generic;
using HeneGames.DialogueSystem;
using UnityEngine;

public class ItemScript : MonoBehaviour , Ipauseable , IRestartable
{
    [SerializeField] CollectableItem_Scriptable _Scriptable;
    [SerializeField] bool hasCollect;
    DialogueUI dialogueUI;
    InventorySystem _inventory;
    ShowUICollision showUI;

    private void OnTriggerStay(Collider other) {
        if(hasCollect) return;
        if(other.gameObject.GetComponent<InventorySystem>()){
            _inventory = other.gameObject.GetComponent<InventorySystem>();
            
            if(!showUI) showUI = GetComponent<ShowUICollision>();
            showUI.ShowDescription();
            if(Input.GetKey(KeyCode.E)){
                Collect(_inventory);
            }
        }
    }

    void OnTriggerExit(Collider other){
        if(hasCollect) return;
        if(other.gameObject.GetComponent<PlayerController>()){
            showUI.CloseDescription();
        }      
    }
    // TODO : Add function
    // add item to inventory
    public void Collect(InventorySystem _inventory){
        if(_inventory.inventory.Contains(_Scriptable)) return;
        _inventory.player.anim.SetTrigger("Collect");
        _inventory.inventory.Add(_Scriptable);
        hasCollect = true;
        if(!dialogueUI) dialogueUI = FindAnyObjectByType<DialogueUI>();{
            dialogueUI.ClearText();
            dialogueUI.ShowInteractionUI(false);
        }
        // this.gameObject.SetActive(false);
        disableObj();
    }

    void disableObj(){
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        if(showUI) showUI.CloseDescription();
    }

    void enableObj(){
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
    }

    public void pause(){
    }

    public void resume(){
    }

    public void _Restart()
    {
        enableObj();
        hasCollect = false;
    }
}
