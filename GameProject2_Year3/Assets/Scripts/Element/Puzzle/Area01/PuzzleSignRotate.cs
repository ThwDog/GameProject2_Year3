using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSignRotate : RotateScript ,checkBool
{
    public enum deg{
        deg45  = 45, deg90 =  90, deg135 = 135 ,deg180 = 180, deg225 = 225,deg270 = 270,deg315 = 315 , deg360 = 360
    }
    [Header("Setting")]
    [SerializeField] bool isWin = false;
    [Tooltip("deg1  = 45, deg2 =  90, deg4 = 135 ,deg5 = 180, deg6 = 225,deg7 = 270,deg8 = 315 , deg9 = 360")]
    [SerializeField] roteType typeWin;
    [SerializeField] roteType _roteType2;
    [SerializeField] deg degWin;
    [SerializeField] float tolerance = 0.2f;
    [SerializeField] private bool canRote = false;
    ShowUICollision showUI;
    EventScript _event;


    private void OnEnable() {
        _event = GetComponent<EventScript>();
        // showUI = GetComponent<ShowUICollision>();
    }

    private void Update() {
        _RotateObj();
    }

    public override void _RotateObj()
    {
        if(!canRote) return;

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
        if(!canRote) return;

        if(!other.gameObject.GetComponent<PlayerController>()) return;

        // showUI.ShowDescription();
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
        if(Input.GetKey(KeyCode.X)){
            switchType();
            isRo = false;
        }
    }

    private void switchType(){
        if(_roteType != _roteType2) _roteType = _roteType2;
        else if(_roteType == _roteType2) _roteType = roteType.z;
    } 

    private void OnTriggerExit(Collider other) {
        if(!canRote) {
            // showUI.CloseDescription();
            return;
        }

        if(!other.gameObject.GetComponent<PlayerController>()) return;
        // showUI.CloseDescription();
    }

    

    private void checkRote(){
        float rotate;
        switch(_roteType){
            case roteType.x :
                rotate = this.transform.eulerAngles.x;
                if(Mathf.Abs(rotate - 0) < tolerance) rotate = 360; // set id rotate is close to 0 then set rote to 360

                if( Mathf.Abs(rotate - (float)degWin) < tolerance ){
                    isWin = true;
                    canRote = !canRote;
                    // showUI.CloseDescription();
                    _event._FinishEvent();
                }
                else{
                    isWin = false;
                }
                break;
            case roteType.y :
                if(_roteType != typeWin) break; 
                rotate = this.transform.eulerAngles.y;

                if(rotate < 0) rotate = Mathf.Abs(rotate);
                // if(Mathf.Abs(this.transform.eulerAngles.z) > tolerance){
                //     isWin = false;
                //     break;
                // }

                if(Mathf.Abs(rotate - 0) < tolerance) rotate = 360;

                if( Mathf.Abs(rotate - (float)degWin) < tolerance ){
                    isWin = true;
                    canRote = !canRote;
                    // showUI.CloseDescription();
                    _event._FinishEvent();
                }
                else{
                    isWin = false;
                }
                break;
            case roteType.z :
                if(_roteType != typeWin) break; 

                rotate = this.transform.eulerAngles.z;
                if(rotate < 0) rotate = Mathf.Abs(rotate);
                // if(Mathf.Abs(this.transform.eulerAngles.y) > tolerance ){
                //     isWin = false;
                //     break;
                // }
                if(Mathf.Abs(rotate - 0) < tolerance) rotate = 360;

                if( Mathf.Abs(rotate - (float)degWin) < tolerance ){
                    isWin = true;
                    canRote = !canRote;
                    // showUI.CloseDescription();
                    _event._FinishEvent();
                }
                else{
                    isWin = false;
                }
                break;
        }
    }

    // Set Rotate deg
    public bool _check()
    {
        return isWin;
    }

    public void setCanRo(bool _bool){
        canRote = _bool;
    }
}
