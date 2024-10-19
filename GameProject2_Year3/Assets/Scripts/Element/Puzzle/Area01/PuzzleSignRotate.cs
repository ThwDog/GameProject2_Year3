using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSignRotate : RotateScript ,checkBool
{
    public enum deg{
        deg1  = 45, deg2 =  90, deg4 = 135 ,deg5 = 180, deg6 = 225,deg7 = 270,deg8 = 315 , deg9 = 360
    }
    [Header("Setting")]
    [SerializeField] bool isWin = false;
    [Tooltip("deg1  = 45, deg2 =  90, deg4 = 135 ,deg5 = 180, deg6 = 225,deg7 = 270,deg8 = 315 , deg9 = 360")]
    [SerializeField] deg degWin;
    [SerializeField] float tolerance = 0.2f;
    ShowUICollision showUI;
    EventScript _event;


    private void Start() {
        _event = GetComponent<EventScript>();
        showUI = GetComponent<ShowUICollision>();
    }

    private void Update() {
        _RotateObj();
    }

    public override void _RotateObj()
    {
        if(isRo){
            transform.rotation = Quaternion.Lerp(transform.rotation, rotateTarget, rotateSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, rotateTarget) < 0.01f)
            {
                checkRote();
                isRo = false;
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        if(!other.gameObject.GetComponent<PlayerController>()) return;

        showUI.ShowDescription();
        //rotate left
        if(Input.GetKey(KeyCode.Q) && !isPressButton && !isRo) {
            _SetRotate(45f);
            isPressButton = true;
            StartCoroutine(resetButton(delayButtonTime));
        }
        //rotate Right
        if(Input.GetKey(KeyCode.E) && !isPressButton && !isRo) {
            _SetRotate(-45f);
            isPressButton = true;
            StartCoroutine(resetButton(delayButtonTime));
        }
    }

    private void OnTriggerExit(Collider other) {
        if(!other.gameObject.GetComponent<PlayerController>()) return;
        showUI.CloseDescription();
    }

    

    private void checkRote(){
        Debug.Log("Check");
        switch(_roteType){
            case roteType.x :
                if( Mathf.Abs(this.transform.eulerAngles.x - (float)degWin) < tolerance ){
                    isWin = true;
                    _event._FinishEvent();
                }
                else{
                    isWin = false;
                    _event._FinishEvent();
                }
                break;
            case roteType.y :
                if( Mathf.Abs(this.transform.eulerAngles.y - (float)degWin) < tolerance ){
                    isWin = true;
                    _event._FinishEvent();
                }
                else{
                    isWin = false;
                    _event._FinishEvent();
                }
                break;
            case roteType.z :
                if( Mathf.Abs(this.transform.eulerAngles.z - (float)degWin) < tolerance ){
                    isWin = true;
                    _event._FinishEvent();
                }
                else{
                    isWin = false;
                    _event._FinishEvent();
                }
                break;
        }
    }

    // Set Rotate deg
    

    public bool _check()
    {
        return isWin;
    }
}
