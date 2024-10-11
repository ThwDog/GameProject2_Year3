using UnityEngine;

[ExecuteInEditMode]
public class GroundRandomMaterial : MonoBehaviour 
{
    [SerializeField] Mesh[] meshes;
    [SerializeField] bool useMat;
    [SerializeField] Material[] material;

    private void Start() {
        random();
    }

    public void random(){
        if(meshes.Length < 2) return;
        if(useMat && material.Length != meshes.Length) return;

        int ran = Random.Range(0, meshes.Length);

        if(meshes[ran] == GetComponent<MeshFilter>().sharedMesh){
            random();
        }
        
        Mesh m = meshes[ran];

        GetComponent<MeshFilter>().mesh = m;
        
        if(useMat) GetComponent<MeshRenderer>().material = material[ran];
    }
}
