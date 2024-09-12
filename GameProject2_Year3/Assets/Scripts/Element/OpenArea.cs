using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenArea : MonoBehaviour
{
    public GameObject[] _Area;
    bool openFistArea; // just of safe

    private void Awake() {
        foreach (var area in _Area) {
            area.SetActive(false);
            // Debug.Log("disable"+area.name);
        }
        StartCoroutine(wait());
    }

    private void Start(){
    }

    public void Open(int areaIndex){
        _Area[areaIndex].SetActive(true);
    }
    public void Close(int areaIndex){
        _Area[areaIndex].SetActive(false);
    }

    // TODO : If load scene is crash check this first
    // wait for open scene
    IEnumerator wait(){
        yield return new WaitForSeconds(0.01f);
        if(!openFistArea){
        Open(0); // open first Area
        openFistArea = true;
        }
    }
    
}
