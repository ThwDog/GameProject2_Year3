using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayObjAnimation : MonoBehaviour
{
    [SerializeField] string soundName;

    public void playAnimationTrigger(string name){
        GetComponent<Animator>().SetTrigger(name);
        if(soundName != null) SoundManager.instance.PlaySfx(soundName);
    }
}
