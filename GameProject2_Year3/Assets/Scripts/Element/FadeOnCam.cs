using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO : Wait for fade obj
public class FadeOnCam : MonoBehaviour
{
    [SerializeField] private List<MeshRenderer> objRenderer;

    public void activate(){
        foreach(var objRenderer in objRenderer){
            objRenderer.enabled = true;
        }
    }

    public void deactivate(){
        foreach(var objRenderer in objRenderer){
            objRenderer.enabled = false;
        }
    }

    // public IEnumerator activateAgain(){
    //     if(objRenderer[0].enabled) yield break;
    //     yield return new WaitForSeconds(5f);
    //     activate();
    // }
}
