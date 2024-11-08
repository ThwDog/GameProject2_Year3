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
    // public static int PosID = Shader.PropertyToID("_Position");
    // public static int SizeID = Shader.PropertyToID("_Size");

    // [SerializeField] private Material WallMat;

    // private void findShader(){
    //     var dir = cam.transform.position - transform.position;
    //     var ray = new Ray(transform.position,dir.normalized);

    //     if(Physics.Raycast(ray,3000,mask))WallMat.SetFloat(SizeID,1);    
    //     else WallMat.SetFloat(SizeID,0); 

    //     var view = cam.WorldToViewportPoint(transform.position);

    //     WallMat.SetVector(PosID,view);
    // }
    #endregion
}
