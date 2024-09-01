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

    public GroundType groundType;

    public type types = type.mesh;

    [SerializeField][Range(0, 100)] private float dis = 10; // check distance form player and ground 
    Transform player;
    Collider colliders;
    [SerializeField] Mesh mesh;
    public bool mapOpen = true;

    private void Start()
    {
        //TODO : Change It
        player = GameObject.FindAnyObjectByType<PlayerController>().GetComponent<Transform>();

        if (types == type.mesh) colliders = GetComponent<MeshCollider>();
        else if (types == type.box) colliders = GetComponent<BoxCollider>();

        if (groundType == GroundType.grass)
        {
            Collider[] hit = Physics.OverlapSphere(transform.position, 2 ,8);
            
            if (hit != null)
            {
                GetComponent<MeshFilter>().mesh = mesh;
            }
        }
    }

    private void Update()
    {

        if (!mapOpen) return;

        if (Vector3.Distance(player.position, transform.position) < dis && !colliders.enabled)
        {
            colliders.enabled = true;
        }
        else if (Vector3.Distance(player.position, transform.position) > dis && colliders.enabled)
        {
            colliders.enabled = false;
        }
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
