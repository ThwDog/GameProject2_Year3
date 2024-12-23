using System.Collections;
using UnityEngine;

//TODO : one per area 

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] GameObject _gameObject;
    [SerializeField] GameObject target;
    [SerializeField] Vector3 offset;
    [SerializeField] bool isSpawn = false;
    [Header("")]
    [SerializeField] bool CanSpawnOnThis = false;
    PlayerController player;
    EventScript _event;

    private void Awake() {
        _event = GetComponent<EventScript>();

        if(!CanSpawnOnThis) return;
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
        if(!CanSpawnOnThis) return;
        player = FindAnyObjectByType<PlayerController>();
        player.canPlayWalkSound = true;
        // player.gameObject.SetActive(false);
        // Debug.Log("ReSpawn");
        StartCoroutine(DeSpawnWait(player.gameObject));
        reSpawn();
        _event._StartEvent();
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
