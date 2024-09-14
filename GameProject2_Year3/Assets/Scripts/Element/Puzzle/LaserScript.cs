using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{

    // TODO : make line static *can only assign length when in editor* and child to point and add box collision in line to check
    // TODO : use Q and E to  rotation this by 45 Deg
    [Header("Laser setting")]
    [Tooltip("If this prism can open check it to true else check to false")][SerializeField] bool canOpen = true; // can open this laser
    public LineRenderer lineR;
    [SerializeField] Transform firePoint;
    [Tooltip("Max length of laser")][SerializeField] float maxLength = 3;
    // [SerializeField] Vector3 direction = new Vector3(0, 0, 1);
    [Header("")]
    LaserScript hitObj; // last obj that been hit
    [Tooltip("If it first to so laser then click in on")]public bool isOpen = false;
    [Header("")]
    [SerializeField][Range(0f,10f)] private float delayButtonTime = 1.5f;
    [SerializeField][Range(0f,100f)] private float rotateSpeed = 10f;
    bool isPressButton = false;
    bool rayHit = false;
    bool isRo = false;
    Quaternion rotateTarget; // target of rotation
    ShowUICollision showUI;

    private void Start() {
        laserUpdate();
        if(isOpen) EnableLaser();
    }

    private void Update() {
        if(!canOpen) return;
        if(!isOpen){
            if(hitObj != null) {
                hitObj.isOpen = false;
                hitObj = null;
            }
            DisableLaser();
            gameObject.GetComponent<SphereCollider>().enabled = false;
            return;
        }
        else gameObject.GetComponent<SphereCollider>().enabled = true;
        rayHitOtherObj();
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
    void ray(){
        RaycastHit hit;
        if(!isOpen) return;

        if(Physics.Raycast(firePoint.position, firePoint.forward,out hit,maxLength)){
            if(hit.transform.gameObject.GetComponent<LaserScript>() && !hit.transform.gameObject.GetComponent<LaserScript>().isOpen){
                // set line renderer end position to obj that hit and add float to it
                lineR.SetPosition(1, new Vector3(0,0,hit.distance + 0.4f));              
                
                hitObj = hit.transform.gameObject.GetComponent<LaserScript>();
                hitObj.isOpen = true;
                rayHit = true;
            }
        }
        else {
            lineR.SetPosition(1,new Vector3(0,0,maxLength));
            rayHit = false;

            if(hitObj != null) {
                hitObj.isOpen = false;
                hitObj = null;
            }
        }
    }

    // TODO : Add more obj if has other obj
    void rayHitOtherObj(){
        RaycastHit hit;
        if(!isOpen) return;

        if(Physics.Raycast(firePoint.position, firePoint.forward,out hit,maxLength)){
            // TODO : Here
            if(hit.transform.gameObject.GetComponent<PlayerController>() && !rayHit){
                lineR.SetPosition(1, new Vector3(0,0,hit.distance + 0.4f));              
            }
        }
        else lineR.SetPosition(1,new Vector3(0,0,maxLength));

    }

    private void OnDrawGizmosSelected()
    {
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
        if(!other.gameObject.GetComponent<PlayerController>()) return;
        if(!showUI) showUI = GetComponent<ShowUICollision>();
        showUI.ShowDescription();
        ray();
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
        if(!other.gameObject.GetComponent<PlayerController>()) return;
        if(!showUI) showUI = GetComponent<ShowUICollision>();
        showUI.CloseDescription();
    }

    IEnumerator resetButton(float sec){
        if(!isPressButton) yield break;
        yield return new WaitForSeconds(sec);
        isPressButton = false;
    }


    // private void colliderUpdate(){
    //     if (_collider == null) lineR.gameObject.AddComponent<MeshCollider>();
    //     _collider = lineR.gameObject.GetComponent<MeshCollider>();
    //     Mesh mesh = new Mesh();
    //     lineR.BakeMesh(mesh,false);
    //     _collider.sharedMesh = mesh;
    //     _collider.convex = true;
    //     _collider.isTrigger = true;
    // }

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
