using UnityEngine;

public class PlateScript : MonoBehaviour
{
    [SerializeField]PlatePuzzle platePuzzle;
    [SerializeField] ParticleSystem particle;
    [SerializeField] private bool hasStep = false;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.GetComponent<PlayerController>() && !hasStep){
            if(!platePuzzle) platePuzzle = GetComponentInParent<PlatePuzzle>();
            
            if(!platePuzzle.canStep) return;
            platePuzzle.StepOnPlate(this.gameObject.name);
            particle.gameObject.SetActive(true);
            hasStep = true;
        }
    }

    public void resetPlate(){
        if(hasStep) hasStep = false;
        particle.gameObject.SetActive(false);
        platePuzzle = null;
    }
}
