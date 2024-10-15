using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TerrainSoundCheck : MonoBehaviour
{
    private PlayerController player;
    [SerializeField] TerrainSound[] terrainSound;
    [SerializeField] LayerMask floorLayer;

    private void Start()
    {
        player = GetComponent<PlayerController>();
    }

    public void groundSoundCheck()
    {
        float distToGround = player.playerCollider.bounds.extents.y;
        RaycastHit _hit;
        if (Physics.Raycast(transform.position, Vector3.down, out _hit, distToGround + 0.1f, floorLayer))
        {
            if (_hit.collider.gameObject.TryGetComponent<Terrain>(out Terrain terrain))
            {
                // player.walkSoundName = groundEnable.groundSound;
                soundCheckTerrian(terrain, _hit.point);
            }
            else{
                player.walkSoundName = "";
            }
        }
    }

    private void soundCheckTerrian(Terrain terrain, Vector3 hit)
    {
        Vector3 terrainPos = hit - terrain.transform.position;

        Vector3 splatMapPos = new Vector3(terrainPos.x / terrain.terrainData.size.x,0, terrainPos.z / terrain.terrainData.size.z);

        int x = Mathf.FloorToInt(splatMapPos.x * terrain.terrainData.alphamapWidth);
        int z = Mathf.FloorToInt(splatMapPos.z * terrain.terrainData.alphamapHeight);

        float[,,] alphaMap = terrain.terrainData.GetAlphamaps(x, z, 1, 1);
        // Debug.Log(alphaMap);

        int index = 0;
        for (int i = 1; i < alphaMap.Length; i++)
        {
            if (alphaMap[0, 0, i] > alphaMap[0, 0, index])
            {
                index = i;
            }
        }

        foreach (TerrainSound _terrainSound in terrainSound)
        {
            if (_terrainSound._terrainLayer == terrain.terrainData.terrainLayers[index])
            {
                // Debug.Log(terrain.terrainData.terrainLayers[index].name);
                player.walkSoundName = _terrainSound.soundName;
            }
        }
    }

    [System.Serializable]
    private class TerrainSound
    {
        public TerrainLayer _terrainLayer;
        public string soundName;
    }
}


