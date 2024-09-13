using UnityEngine;

public class PlateScript : MonoBehaviour
{
    [SerializeField]PlatePuzzle platePuzzle;
    [SerializeField] private bool hasStep = false;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<PlayerController>() && !hasStep){
            if(!platePuzzle) platePuzzle = GetComponentInParent<PlatePuzzle>();
            
            if(!platePuzzle.canStep) return;
            platePuzzle.StepOnPlate(this.gameObject.name);
            hasStep = true;
        }
    }

    public void resetPlate(){
        if(hasStep) hasStep = false;
        platePuzzle = null;
    }
}
