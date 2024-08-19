using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSpawnItem : MonoBehaviour , Ipauseable
{
    [Header("Setting")]
    [SerializeField] ItemScript spawnObj;
    [SerializeField] GameObject description;
    public bool isSpawn = false;

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

    public void Spawn(){
        if(!isSpawn){
            Debug.Log("Spawn");
            isSpawn = true;
            if(!spawnObj) return;
            ItemScript obj = Instantiate(spawnObj , transform.position, transform.rotation);
            obj.Collect(FindAnyObjectByType<InventorySystem>());
            CloseDescription();
        }
    }

    public void pause(){
    }

    public void resume(){
    }
}
