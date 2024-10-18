using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRotate : MonoBehaviour ,checkBool
{
    enum roteType{
        x ,y ,z
    }
    enum deg{
        deg1  = 45, deg2 =  90, deg4 = 135 ,deg5 = 180, deg6 = 225,deg7 = 270,deg8 = 315 , deg9 = 360
    }
    [Header("Setting")]
    [SerializeField] bool isWin = false;
    [SerializeField] roteType _roteType;
    [SerializeField] float rotateSpeed;
    [SerializeField] float delayButtonTime;
    [Tooltip("deg1  = 45, deg2 =  90, deg4 = 135 ,deg5 = 180, deg6 = 225,deg7 = 270,deg8 = 315 , deg9 = 360")]
    [SerializeField] deg degWin;
    [SerializeField] float tolerance = 0.2f;
    ShowUICollision showUI;
    EventScript _event;

    Quaternion rotateTarget;
    bool isRo = false;
    bool isPressButton = false;

    private void Start() {
        _event = GetComponent<EventScript>();
        showUI = GetComponent<ShowUICollision>();
    }

    private void Update() {
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
            rotate(45f);
            isPressButton = true;
            StartCoroutine(resetButton(delayButtonTime));
        }
        //rotate Right
        if(Input.GetKey(KeyCode.E) && !isPressButton && !isRo) {
            rotate(-45f);
            isPressButton = true;
            StartCoroutine(resetButton(delayButtonTime));
        }
    }

    private void OnTriggerExit(Collider other) {
        if(!other.gameObject.GetComponent<PlayerController>()) return;
        showUI.CloseDescription();
    }

    IEnumerator resetButton(float sec){
        if(!isPressButton) yield break;
        yield return new WaitForSeconds(sec);
        isPressButton = false;
    }

    private void checkRote(){
        Debug.Log("Check");
        switch(_roteType){
            case roteType.x :
                if( Mathf.Abs(rotateTarget.x - (float)degWin) < tolerance ){
                    isWin = true;
                    _event._FinishEvent();
                }
                else{
                    isWin = false;
                    _event._FinishEvent();
                }
                break;
            case roteType.y :
                if( Mathf.Abs(rotateTarget.y - (float)degWin) < tolerance ){
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
    private void rotate(float Deg){
        switch(_roteType){
            case roteType.x :
                if(this.gameObject.transform.localRotation == Quaternion.Euler(360,0,0)){
                    this.gameObject.transform.localRotation = Quaternion.Euler(0,0,0);
                }
                rotateTarget = transform.rotation * Quaternion.Euler(Deg, 0, 0);
                isRo = true;
                break;
            case roteType.y :
                if(this.gameObject.transform.localRotation == Quaternion.Euler(0,360,0)){
                    this.gameObject.transform.localRotation = Quaternion.Euler(0,0,0);
                }
                rotateTarget = transform.rotation * Quaternion.Euler(0, Deg, 0);
                isRo = true;
                break;
            case roteType.z :
                if(this.gameObject.transform.localRotation == Quaternion.Euler(0,0,360)){
                    this.gameObject.transform.localRotation = Quaternion.Euler(0,0,0);
                }
                rotateTarget = transform.rotation * Quaternion.Euler(0, 0, Deg);
                isRo = true;
                break;
        }
    }

    public bool _check()
    {
        return isWin;
    }
}
