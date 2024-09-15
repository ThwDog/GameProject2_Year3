using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] GameObject _gameObject;
    [SerializeField] GameObject target;
    [SerializeField] Vector3 offset;
    [SerializeField] bool isSpawn = false;
    PlayerController player;

    private void Awake() {
        if(!_gameObject) return;
        if(_gameObject.GetComponent<PlayerController>()){
            if(!FindAnyObjectByType<PlayerController>()){
                spawn();
            }
            else{
                deSpawn();
            }
        }
    }

    void spawn(){
        if(!_gameObject) return;
        if(!target){
            target = this.gameObject;
        }
        if(!isSpawn)
        {
            GameObject players = Instantiate(_gameObject,transform.position + offset, transform.rotation);  
            player = players.GetComponent<PlayerController>();
            isSpawn = true;
        }
    }

    // deSpawn player and Spawn it
    public void deSpawn(){
        player = FindAnyObjectByType<PlayerController>();
        // player.gameObject.SetActive(false);
        StartCoroutine(DeSpawnWait(player.gameObject));
        reSpawn();
    }

    void reSpawn(){
        if(!_gameObject) return;
        if(!target){
            target = this.gameObject;
        }
        if(!isSpawn)
        {
            // GameObject player = Instantiate(_gameObject,transform.position + offset, transform.rotation);
            player.transform.position = transform.position + offset;
            
            isSpawn = true;
        }
    }
    

    IEnumerator DeSpawnWait(GameObject obj){
        player.gameObject.SetActive(false);
        yield return null; // can set to wait
        player.gameObject.SetActive(true);
        // Destroy(obj);
        isSpawn = false;
    }
}
