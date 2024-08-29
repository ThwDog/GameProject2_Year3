using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnable : MonoBehaviour
{
    public enum type{
        mesh , box
    }
    public type types  = type.mesh;

    [SerializeField][Range(0,100)] private float dis = 10; // check distance form player and ground 
    Transform player;
    Collider colliders;
    public bool mapOpen = true;

    private void Start() {
        //TODO : Change It
        player = GameObject.Find("Player").GetComponent<Transform>();

        if(types == type.mesh) colliders = GetComponent<MeshCollider>();
        else if(types == type.box) colliders = GetComponent<BoxCollider>();
    }

    private void Update() {
        if(!mapOpen) return;
        if(Vector3.Distance(player.position,transform.position) > dis) return;

        if(Vector3.Distance(player.position,transform.position) < dis && !colliders.enabled){
            colliders.enabled = true;
        }
        else if (Vector3.Distance(player.position,transform.position) > dis && colliders.enabled)
        {
            colliders.enabled = false;
        }
    }
}
