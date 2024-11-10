using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindSetActiveObj : MonoBehaviour
{
    FadeOnCam activeObj;
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask mask;

    void Update()
    {
        findAndSetActive();
        // findShader();
    }

    private void findAndSetActive(){
        RaycastHit hit;
        var dir = cam.transform.position - transform.position;
        var ray = new Ray(transform.position,dir.normalized);


        if(Physics.Raycast(ray,out hit,300,mask)){
            activeObj = hit.transform.gameObject.GetComponent<FadeOnCam>();
            activeObj.deactivate();
        }
        else{
            if(activeObj == null) return;
            activeObj.activate();
            activeObj = null;
        }
    }

    #region  old
    // private void OnEnable() {
    //     setFloat(0);
    // }

    // private void OnDisable() {
    //     setFloat(0);
    // }

    // public static int PosID = Shader.PropertyToID("_Position");
    // public static int SizeID = Shader.PropertyToID("_Size");

    // [SerializeField] private Material treeMat , leafMat;
    // [SerializeField] private float size;

    // private void findShader(){
    //     var dir = cam.transform.position - transform.position;
    //     var ray = new Ray(transform.position,dir.normalized);

    //     if(Physics.Raycast(ray,3000,mask))  setFloat(size);
    //     else setFloat(0);

    //     var view = cam.WorldToViewportPoint(transform.position);

    //     setVector(view);
    // }

    // private void setFloat(float value){
    //     treeMat.SetFloat(SizeID,value); 
    //     leafMat.SetFloat(SizeID,value); 
    // }

    // private void setVector(Vector3 vec){
    //     treeMat.SetVector(PosID,vec);
    //     leafMat.SetVector(PosID,vec);
    // }
    #endregion
}
