using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPC_Movement : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] private float speed;
    [SerializeField] private Transform markLeft , markRight ;
    public bool canMove = false;

    NPC_Animation anim;
    private bool canGoLeft = true; // check if can go to left Dir

    private void Start() {
        StartCoroutine(NPC_WalkRest());
    }

    private void Update() {
        if(!canMove) return; 

        if(canGoLeft){
            moveDir(markLeft);
            if(checkFinish(markLeft)){
                canGoLeft = false;
                return;
            }
            else return;
        }    
        else{
            moveDir(markRight);
            if(checkFinish(markRight)){
                canGoLeft = true;
                return;
            }
            else return;
        }
    }

    // rest time
    IEnumerator NPC_WalkRest(){
        float rnd = Random.Range(2f, 5f);
        Debug.Log(rnd);
        yield return new WaitForSeconds(rnd);
        canMove = !canMove;
        yield return new WaitForSeconds(rnd);
        StartCoroutine(NPC_RndWalk());
        StartCoroutine(NPC_WalkRest());
    }

    // Random if NPC should change Direction or not
    IEnumerator NPC_RndWalk(){
        int rnd = Random.Range(0,100);
        rnd = Mathf.Clamp(rnd / 50, 0, 2);

        Debug.Log("Random" + rnd);

        if(rnd == 1) canGoLeft = true;
        else if (rnd == 2)canGoLeft = false;
        else yield return null;

        yield return null;
    }

    private void moveDir(Transform dir){  
        float _speed = speed * Time.deltaTime;  
        this.transform.position = Vector3.MoveTowards(transform.position,dir.transform.position,_speed);
    }
    // check if NPC move to mark?
    private bool checkFinish(Transform dir){
        if(Vector3.Angle(transform.position,dir.transform.position) < 0.1f){
            return true;
        }
        return false;
    }
}   
