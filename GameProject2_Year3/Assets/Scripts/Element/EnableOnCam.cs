using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO : use on every obj that need to close when cam doesn't look 
public class EnableOnCam : MonoBehaviour
{
    [Header("CamDetect")]
    Camera cam;
    MeshRenderer _renderer;
    Collider colliders;
    Plane[] cameraFrustum;
    internal bool _isEnable = false;

    private void Start() {
        // get camera component from camera name FrustumCam 
        cam = GameObject.Find("FrustumCam").GetComponent<Camera>();
        _renderer = GetComponent<MeshRenderer>();
        colliders = GetComponent<Collider>();
    }

    private void Update() {
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(cam);
    
        if(GeometryUtility.TestPlanesAABB(cameraFrustum,colliders.bounds)){
            _renderer.enabled = true;            
        }
        else{
            _renderer.enabled = false;
        }
        _isEnable = _renderer.enabled;
    }
}
