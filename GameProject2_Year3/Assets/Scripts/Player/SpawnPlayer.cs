using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] GameObject _gameObject;
    [SerializeField] GameObject target;
    [SerializeField] Vector3 offset;
    [SerializeField] bool isSpawn = false;

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
            GameObject player = Instantiate(_gameObject,transform.position + offset, transform.rotation);
            isSpawn = true;
        }
    }

    public void deSpawn(){
        PlayerController player = FindAnyObjectByType<PlayerController>();
        player.gameObject.SetActive(false);
        StartCoroutine(DeSpawnWait(player.gameObject));
    }

    IEnumerator DeSpawnWait(GameObject obj){
        yield return null;
        Destroy(obj);
        isSpawn = false;
        spawn();
    }
}
