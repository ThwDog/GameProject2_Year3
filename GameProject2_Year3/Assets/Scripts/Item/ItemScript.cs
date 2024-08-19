using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour , Ipauseable
{
    [SerializeField] CollectableItem_Scriptable _Scriptable;
    [SerializeField] GameObject description;
    [SerializeField] bool hasCollect;
    InventorySystem _inventory;

    public void ShowDescription(){
        // Debug.Log("Show Description " + gameObject.name);
        if(!description) return;
        description.SetActive(true);
    }

    public void CloseDescription(){
        // Debug.Log("Close Description " + gameObject.name);
        if(!description) return;
        description.SetActive(false);
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.GetComponent<InventorySystem>()){
            if(hasCollect) return;
            _inventory = other.gameObject.GetComponent<InventorySystem>();
            ShowDescription();
            if(Input.GetKey(KeyCode.E)){
                Collect(_inventory);
            }
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.GetComponent<PlayerController>()){
            CloseDescription();
        }      
    }
    // TODO : Add function
    // add item to inventory
    public void Collect(InventorySystem _inventory){
        if(_inventory.inventory.Contains(_Scriptable.name)) return;
        _inventory.inventory.Add(_Scriptable.name);
        hasCollect = true;
        this.gameObject.SetActive(false);
    }

    public void pause(){
    }

    public void resume(){
    }
}
