using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatePuzzle : MonoBehaviour
{
    [SerializeField] private List<plateList> plateLists; // list of plate pattern
    [SerializeField] private List<string> plateName = new List<string>(); // list of plate that player has step
    [SerializeField] private bool isWin = false;

    private void Start() {
        // this  is just of check
        for(int i = 0; i < plateLists.Count; i++){
            for(int j = 0; j < plateLists[i].plateNum.Length; j++){
                Debug.Log($"plate list : {i} plate Name : {plateLists[i].plateNum[j].gameObject.name}");
            }
        }
    }

    public void StepOnPlate(string plateNum){
        plateName.Add(plateNum);
    }

    // usee when ever player win or lose
    private void resetAllPlate(){
        PlateScript[] plateScripts = GetComponentsInChildren<PlateScript>();
        foreach (PlateScript plate in plateScripts){
            plate.resetPlate();
        }
        plateName = null;
    }

}


[Serializable]
public class plateList{
    public GameObject[] plateNum; // list of plate num
}
