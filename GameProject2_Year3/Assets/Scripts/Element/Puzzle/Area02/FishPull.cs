using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// TODO : use when finish " pool.finishPool(); "
[RequireComponent(typeof(PoolPuzzle))]
public class FishPull : MonoBehaviour
{
    [SerializeField] private float playerPer = 50 , fishPer = 50; // percentPool Default value

    [SerializeField] private Slider playerBar , FishBar ;
    [SerializeField] private GameObject pullingBar;
    [Header("setting")]
    [SerializeField] [Range(0f,100f)] private float fishPullDelay;
    bool IsPlaying;
    bool fishCanPush = true;
    bool playerCanPush = true;
    PoolPuzzle pool;

    private void OnEnable() {
        pool = GetComponent<PoolPuzzle>();
        _Reset();
    }

    private void OnDisable() {
        pullingBar.SetActive(false);
        // SoundManager.instance.StopAllGameSound();
        _Reset();
    }

    private void Update() {
        playerPer = Mathf.Clamp(playerPer, 0, 100);
        fishPer = Mathf.Clamp(fishPer,0,100);
        
        if(!pool.startPuzzle) return;
        playerBar.value = playerPer;
        FishBar.value = fishPer;
        
        if(!IsPlaying) return;
        winCheck();
        
        if(playerCanPush){
            if(Input.GetKeyUp(KeyCode.Space)){
                pushNPull(ref playerPer,ref fishPer,5);
                StartCoroutine(playerPushDelay(0.5f));
            }
        }

        if(fishCanPush){
            pushNPull(ref fishPer,ref playerPer , 5f);
            StartCoroutine(fishPushDelay(fishPullDelay));
        }
    }

    private void winCheck(){
        if(playerPer >= 100 ) {
            // Debug.Log("player Win");
            playerPer = 100;
            IsPlaying = false;
            pool.finishPool();
            GetComponent<FishPull>().enabled = false;
            SoundManager.instance.StopAllGameSound();
        } 
        else if(fishPer >= 100){
            fishPer = 100;
            IsPlaying = false;
            pool.failPool();
            _Reset();
            GetComponent<FishPull>().enabled = false;
            SoundManager.instance.StopAllGameSound();
        }
    }

    IEnumerator playerPushDelay(float delay){
        playerCanPush = false;
        yield return new WaitForSeconds(delay);
        playerCanPush = true; 
    }

    IEnumerator fishPushDelay(float delay){
        fishCanPush = false;
        yield return new WaitForSeconds(delay);
        fishCanPush = true; 
    }

    // push is the side who is press space bar 
    private void pushNPull(ref float push,ref float pull, float value){
        // Debug.Log("Push N Pull");
        push += value;
        pull -= value;
    }

    private void _Reset(){
        playerPer = 50;
        fishPer = 50;
    }

    public void _SetPlaying(bool _bool){
        pullingBar.SetActive(true);
        IsPlaying = _bool;
        SoundManager.instance.PlayGameSound("Reeling");
    }
}
