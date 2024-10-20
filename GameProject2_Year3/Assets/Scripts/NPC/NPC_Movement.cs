using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPC_Movement : MonoBehaviour , Ipauseable
{
    [Header("Setting")]
    [SerializeField] private float speed;
    [SerializeField] private Transform markLeft , markRight ;
    [SerializeField] private string animatorName;
    [SerializeField] bool paused = false;
    private bool canMove = true;

    NPC_Animation anim;
    SpriteRenderer sprite;
    private bool canGoLeft = false; // check if can go to left Dir

    public void pause(){
        // Debug.Log("Player pause");
        anim.playAnimOnBoolFalse(animatorName);
        if(!paused) paused = !paused;
    }

    public void resume(){
        // Debug.Log("Player resume");
        if(paused) paused = !paused;
    } 

    private void OnEnable() {
        if(!anim) anim = GetComponent<NPC_Animation>();
        if(!sprite) sprite = anim.anim.gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(NPC_WalkRest());
    }

    private void Update() {
        if(paused) return;
        if(!canMove) {
            anim.playAnimOnBoolFalse(animatorName);
            return;
        }

        if(canGoLeft){
            flipX(false);
            moveDir(markLeft);
            if(checkFinish(markLeft)){
                canGoLeft = false;
                return;
            }
            else return;
        }    
        else{
            flipX(true);
            moveDir(markRight);
            if(checkFinish(markRight)){
                canGoLeft = true;
                return;
            }
            else return;
        }
    }

    private void flipX(bool flip){
        if(flip){
            sprite.flipX = true;
        }
        else{
            sprite.flipX = false;
        }
    }

    // rest time
    IEnumerator NPC_WalkRest(){
        float rnd = Random.Range(2f, 5f);
        // Debug.Log(rnd);
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

        // Debug.Log("Random" + rnd);

        if(rnd == 1) canGoLeft = true;
        else if (rnd == 2)canGoLeft = false;
        else yield return null;

        yield return null;
    }

    private void moveDir(Transform dir){  
        float _speed = speed * Time.deltaTime;  
        anim.playAnimOnBoolTrue(animatorName);
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
