using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO : use on every obj that need to close when cam doesn't look 
public class EnableOnCam : MonoBehaviour
{
    enum type{
        renderer , obj
    }

    [Header("CamDetect")]
    Camera cam;
    MeshRenderer _renderer;
    [SerializeField] type closeType = type.renderer;
    [SerializeField] GameObject[] target;
    Collider colliders;
    Plane[] cameraFrustum;
    internal bool _isEnable = false;

    private void Awake() {
        // get camera component from camera name FrustumCam 
        cam = GameObject.Find("FrustumCam").GetComponent<Camera>();
        colliders = GetComponent<Collider>();
        if(closeType == type.renderer)_renderer = GetComponent<MeshRenderer>();
    }

    private void Update() {
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(cam);
    
        if(GeometryUtility.TestPlanesAABB(cameraFrustum,colliders.bounds)){
            if(closeType == type.renderer) _renderer.enabled = true;   
            else setTarget(true);
        }
        else{
            if(closeType == type.renderer) _renderer.enabled = false;
            else setTarget(false);
        }
        if(closeType == type.renderer) _isEnable = _renderer.enabled;
    }

    private void setTarget(bool _bool){
        foreach(var _target in target){
            _target.SetActive(_bool);
        }
    }
}
