using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnable : MonoBehaviour, MapOpenable
{
    public enum type
    {
        mesh, box
    }
    public enum GroundType
    {
        grass, walk
    }

    [Header("Setting")]
    public GroundType groundType;

    public type types = type.mesh;

    [SerializeField][Range(0, 100)] private float dis = 10; // check distance form player and ground 
    [SerializeField]Transform player;
    [SerializeField]Collider colliders;
    [SerializeField]EnableOnCam enableOnCam;
    public bool mapOpen = true;
    [Header("")]
    public string groundSound; // for store sound of that ground

    private void Start()
    {
    }

    private void Update()
    {
        if (!mapOpen) return;
        assignAllVa();
        // assignAllVa();
        if(!enableOnCam._isEnable) return;

        if (Vector3.Distance(player.position, transform.position) > dis && colliders.enabled)
        {
            colliders.enabled = false;
            return;
        }
        else if (Vector3.Distance(player.position, transform.position) < dis && !colliders.enabled)
        {
            colliders.enabled = true;
        }

    }

    void assignAllVa(){
        try
        {
            if(!player) player = GameObject.FindAnyObjectByType<PlayerController>().GetComponent<Transform>();
            if(!enableOnCam)enableOnCam = GetComponent<EnableOnCam>();
            if(!colliders){
                if (types == type.mesh) colliders = GetComponent<MeshCollider>();
                else if (types == type.box) colliders = GetComponent<BoxCollider>();
            }
        }
        catch{}
    }

    public void Open()
    {
        mapOpen = true;
    }

    public void Close()
    {
        mapOpen = false;
    }
}
