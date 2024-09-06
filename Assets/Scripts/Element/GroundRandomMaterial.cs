using UnityEngine;

[ExecuteInEditMode]
public class GroundRandomMaterial : MonoBehaviour 
{
    [SerializeField] Mesh[] meshes;

    private void Start() {
        if(meshes.Length < 2) return;
        Mesh m = meshes[Random.Range(0, meshes.Length)];
        GetComponent<MeshFilter>().mesh = m;
        
    }
}
