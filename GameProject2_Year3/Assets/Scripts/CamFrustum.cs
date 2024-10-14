using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFrustum : MonoBehaviour
{
    CamControlAndSetting camControl;
    internal Plane[] cameraFrustum;

    private void Awake() {
        camControl = GetComponent<CamControlAndSetting>();
    }

    private void Update() {
        cameraFrustum = GeometryUtility.CalculateFrustumPlanes(camControl.cameraFrustum);
        
    }
}
