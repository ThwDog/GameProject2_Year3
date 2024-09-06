using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(EnableOnCam))]
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
    Transform player;
    Collider colliders;
    EnableOnCam enableOnCam;
    public bool mapOpen = true;

    private void Start()
    {
        player = GameObject.FindAnyObjectByType<PlayerController>().GetComponent<Transform>();
        enableOnCam = GetComponent<EnableOnCam>();

        if (types == type.mesh) colliders = GetComponent<MeshCollider>();
        else if (types == type.box) colliders = GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (!mapOpen) return;

        if(!enableOnCam._isEnable) return;

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
