using UnityEngine;

public class PlateScript : MonoBehaviour
{
    PlatePuzzle platePuzzle;
    [SerializeField] private bool hasStep = false;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<PlayerController>() && !hasStep){
            if(!platePuzzle) platePuzzle = GetComponentInParent<PlatePuzzle>();
            platePuzzle.StepOnPlate(this.gameObject.name);
            hasStep = true;
        }
    }

    public void resetPlate(){
        if(hasStep) hasStep = false;
    }
}
