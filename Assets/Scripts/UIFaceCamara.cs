using UnityEngine;

public class UIFaceCamara : MonoBehaviour
{
    [SerializeField] Transform lookAt; // Target or Cam
    [SerializeField] Transform obj; // Obj

    private void Update() {
        if(!obj.gameObject.activeInHierarchy) return; 
        if(lookAt){
            obj.LookAt(2 * obj.position - lookAt.position);
        }
    }
}
