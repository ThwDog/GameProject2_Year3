using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO : use on every obj that need to close when cam doesn't look 
public class EnableOnCam : MonoBehaviour
{
    public enum type{
        renderer , obj
    }

    [Header("CamDetect")]
    MeshRenderer _renderer;
    public type closeType = type.renderer;
    public GameObject[] target;
    // [SerializeField] float range;
    internal Collider colliders;
    Plane[] cameraFrustum;
    internal bool _isEnable = false;
    CamFrustum cam;

    private void Awake() {
        // get camera component from camera name FrustumCam 
        // cam = GameObject.Find("FrustumCam").GetComponent<Camera>();
        colliders = GetComponent<Collider>();
    }

    private void Start() {
        cam = FindObjectOfType<CamFrustum>();
        if(closeType == type.renderer)_renderer = GetComponent<MeshRenderer>();
    }

    private void Update() {
        // cameraFrustum = GeometryUtility.CalculateFrustumPlanes(cam);
    
        if(GeometryUtility.TestPlanesAABB(cam.cameraFrustum,colliders.bounds)){
            setTarget(true);
        }
        else{
            setTarget(false);
        }
        if(closeType == type.renderer) _isEnable = _renderer.enabled;
    }

    public void setTarget(bool _bool){
        if(closeType == type.renderer) _renderer.enabled = _bool;   
        else setTarget_obj(_bool);
    }

    private void setTarget_Ren(bool _bool){
        _renderer.enabled = true;  
    }

    private void setTarget_obj(bool _bool){
        foreach(var _target in target){
            _target.SetActive(_bool);
        }
    }
}
