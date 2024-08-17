using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{

    // TODO : make line static *can only assign length when in editor* and child to point and add box collision in line to check
    [Header("Laser setting")]
    public LineRenderer lineR;
    [SerializeField] Transform firePoint;
    [SerializeField] float maxLength = 3;
    [SerializeField] Vector3 direction = new Vector3(0, 0, 1);
    [Header("")]
    [SerializeField] MeshCollider _collider;
    public bool isOpen;

    private void Start() {
        if(isOpen) EnableLaser();
    }

    private void Update() {
        if(isOpen)  EnableLaser();
        else DisableLaser();
        laserUpdate();
        colliderUpdate();

    }

    private void colliderUpdate(){
        if (_collider == null) lineR.gameObject.AddComponent<MeshCollider>();
        _collider = lineR.gameObject.GetComponent<MeshCollider>();
        Mesh mesh = new Mesh();
        lineR.BakeMesh(mesh,false);
        _collider.sharedMesh = mesh;
        _collider.convex = true;
        _collider.isTrigger = true;
    }

    private void laserUpdate(){
        lineR.positionCount = 2;
        lineR.SetPosition(0,firePoint.position);
        Vector3 endPosition = firePoint.position + direction * maxLength;
        lineR.SetPosition(1,endPosition);
    }

    public void EnableLaser(){
        lineR.enabled = true;
        if(lineR.gameObject.GetComponent<MeshCollider>()) _collider.enabled = true;
    } 

    public void DisableLaser(){
        lineR.enabled = false;
        if(lineR.gameObject.GetComponent<MeshCollider>()) _collider.enabled = false;
    }

    private void OnTriggerStay(Collider other) {
        if(other.gameObject.GetComponent<LineRenderer>() && other.gameObject.GetComponent<LineRenderer>()!= lineR){
            Debug.Log(other.name);
            isOpen = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log(other.name);
    }
}
