using UnityEngine;

public class DetectMouseClick : MonoBehaviour , Ipauseable
{
    [SerializeField] private Camera cam;
    SpawnItemByClick spawn;

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
            if (hit.transform.gameObject.GetComponent<SpawnItemByClick>()){
                spawn = hit.transform.gameObject.GetComponent<SpawnItemByClick>();
                if(spawn.isSpawn) return;
                spawn.ShowDescription();
                if (Input.GetMouseButton(0)){
                    spawn.Spawn();
                    spawn = null;
                }
            }
            else{
                if(!spawn || spawn.isSpawn) return;
                else{
                    spawn.CloseDescription();
                    spawn = null;
                }
            }
        }
    }

    public void pause(){
    }

    public void resume(){
    }
}
