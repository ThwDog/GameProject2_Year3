using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    public enum type{
        output , // laser that can only get laser 
        input , // can get and shoot laser
        broken //laser that can't do anything
    }
    [Header("Laser setting")]
    // [Tooltip("If this prism can open check it to true else check to false")][SerializeField] bool canOpen = true; // can open this laser
    public type _type = type.output;
    public LineRenderer lineR;
    [SerializeField] Transform firePoint;
    [Tooltip("Max length of laser")][SerializeField] float maxLength = 3;
    [Header("")]
    LaserScript hitObj; // last obj that been hit
    [Tooltip("If it first to so laser then click in on")]public bool isOpen = false;
    [Header("")]
    [SerializeField][Range(0f,10f)] private float delayButtonTime = 1.5f;
    [SerializeField][Range(0f,100f)] private float rotateSpeed = 10f;
    [Header("")]
    [SerializeField] LayerMask targetLayer;
    bool isPressButton = false;
    bool isRo = false;
    Quaternion rotateTarget; // target of rotation
    ShowUICollision showUI;
    LaserScriptManager manager;

    private void Start() {
        manager = GetComponentInParent<LaserScriptManager>();
        showUI = GetComponent<ShowUICollision>();
        laserUpdate();
        if(isOpen) EnableLaser();
    }

    private void Update() {
        if(manager.isWin) {
            if(showUI.checkActive()) showUI.CloseDescription();
            
            gameObject.GetComponent<SphereCollider>().enabled = false;
            return;
        }

        if(_type == type.broken) return;

        // if is open is false disable laser and disable sphere collider that can get receive laser hit
        if(!isOpen){
            if(hitObj != null) {
                hitObj.isOpen = false;
                hitObj = null;
            }
            DisableLaser();
            showUI.CloseDescription();
            gameObject.GetComponent<SphereCollider>().enabled = false;
            return;
        }
        if(!gameObject.GetComponent<SphereCollider>().enabled) gameObject.GetComponent<SphereCollider>().enabled = true;


        rayCastChangeLaserLength();
        rayCastGetLaser();

        if(_type != type.output) return;
        EnableLaser();

        // if rotate is true slowly rotate to target
        if(isRo){
            transform.rotation = Quaternion.Lerp(transform.rotation, rotateTarget, rotateSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, rotateTarget) < 0.01f)
            {
                isRo = false;
            }
        }
        
    }
    
    // check ray cast hit if in hit to another laser or not
    void rayCastGetLaser(){
        RaycastHit hit;
        if(!isOpen) return;

        if(Physics.Raycast(firePoint.position, firePoint.forward,out hit,maxLength,targetLayer)){

            if(hit.transform.gameObject.GetComponent<LaserScript>() && !hit.transform.gameObject.GetComponent<LaserScript>().isOpen &&
            hit.transform.gameObject.GetComponent<LaserScript>()._type != type.broken){

                hitObj = hit.transform.gameObject.GetComponent<LaserScript>();
                if(hitObj._type == type.input){
                    StartCoroutine(winDelay(1.5f));
                }
                hitObj.isOpen = true;
            }
            else if(hit.transform.gameObject.layer == 3){
                if(hitObj != null) {
                    hitObj.isOpen = false;
                    hitObj = null;
                }
                return;
            }
        }
        else {
            if(hitObj != null) {
                hitObj.isOpen = false;
                hitObj = null;
            }
        }
    }

    IEnumerator winDelay(float delay){
        if(manager.isWin) yield break;
        yield return new WaitForSeconds(delay);
        manager.Win();
        yield break;
    }

    // TODO : Add more obj if has other obj
    // change length of laser by git position of target layer obj
    void rayCastChangeLaserLength(){
        RaycastHit hit;
        if(!isOpen) return;

        if(Physics.Raycast(firePoint.position, firePoint.forward,out hit,maxLength,targetLayer)){
            // TODO : Here
            if(hit.transform.gameObject.layer == 6 && hit.transform.gameObject.GetComponent<LaserScript>()._type != type.broken){
                lineR.SetPosition(1, new Vector3(0,0,hit.distance * 1.6f));              
            }
            else lineR.SetPosition(1, new Vector3(0,0,hit.distance + 0.4f));              
        }
        else {
            lineR.SetPosition(1,new Vector3(0,0,maxLength));
        }

    }

    private void OnDrawGizmosSelected()
    {
        if(_type != type.output) return;
        Gizmos.matrix = firePoint.transform.localToWorldMatrix;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Vector3.zero,firePoint.forward * maxLength);
    }

    private void rotateLaser(float Deg){
        if(this.gameObject.transform.localRotation == Quaternion.Euler(0,360,0)){
            this.gameObject.transform.localRotation = Quaternion.Euler(0,0,0);
        }
        rotateTarget = transform.rotation * Quaternion.Euler(0, Deg, 0);
        isRo = true;
    }

    private void OnTriggerStay(Collider other) {
        if(manager.isWin) return;

        if(_type != type.output) return;

        if(!other.gameObject.GetComponent<PlayerController>()) return;

        showUI.ShowDescription();
        //rotate left
        if(Input.GetKey(KeyCode.Q) && !isPressButton && !isRo) {
            rotateLaser(-45f);
            isPressButton = true;
            StartCoroutine(resetButton(delayButtonTime));
        }
        //rotate Right
        if(Input.GetKey(KeyCode.E) && !isPressButton && !isRo) {
            rotateLaser(45f);
            isPressButton = true;
            StartCoroutine(resetButton(delayButtonTime));
        }
    }

    private void OnTriggerExit(Collider other) {
        if(manager.isWin) return;

        if(_type != type.output) return;

        if(!other.gameObject.GetComponent<PlayerController>()) return;
        showUI.CloseDescription();
    }

    IEnumerator resetButton(float sec){
        if(!isPressButton) yield break;
        yield return new WaitForSeconds(sec);
        isPressButton = false;
    }

    private void laserUpdate(){
        lineR.positionCount = 2;
        lineR.SetPosition(0,firePoint.localPosition);
        lineR.SetPosition(1,new Vector3(0,0,maxLength));
    }

    public void EnableLaser(){
        lineR.enabled = true;
    } 

    public void DisableLaser(){
        lineR.enabled = false;
    }
}
