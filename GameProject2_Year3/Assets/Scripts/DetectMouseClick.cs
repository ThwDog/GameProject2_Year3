using UnityEngine;

public class DetectMouseClick : MonoBehaviour , Ipauseable
{
    [SerializeField] private Camera cam;
    GrassSpawnItem grass;

    private void Update() {
        mouseDetect();
    }

    // For Detect Grass
    public void mouseDetect(){
        Vector3 mousePosition = Input.mousePosition;
        
        Ray ray= cam.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit)){
            // if have more item that can show ui when hold cursor on add it
            if (hit.transform.gameObject.GetComponent<GrassSpawnItem>()){
                grass = hit.transform.gameObject.GetComponent<GrassSpawnItem>();
                if(grass.isSpawn) return;
                grass.ShowDescription();
                if (Input.GetMouseButton(0)){
                    grass.Spawn();
                    grass = null;
                }
            }
            else{
                if(!grass || grass.isSpawn) return;
                grass.CloseDescription();
                grass = null;
            }
        }
    }

    public void pause(){
    }

    public void resume(){
    }
}
