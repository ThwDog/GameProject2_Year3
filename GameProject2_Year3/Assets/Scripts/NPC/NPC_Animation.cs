using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO : Use in event script
public class NPC_Animation : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField] Animator anim;

    public  void playAnimOnTrigger(string name){
        anim.SetTrigger(name);
    }

    public void playAnimOnBoolTrue(string name){
        anim.SetBool(name,true);
    }
    
    public void playAnimOnBoolFalse(string name){
        anim.SetBool(name,false);
    }
}
