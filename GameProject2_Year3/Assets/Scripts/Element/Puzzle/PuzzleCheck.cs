using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCheck : MonoBehaviour
{
    [SerializeField]List<GameObject> checkBoolObj;
    [SerializeField]private bool allWin;
    List<checkBool> checks = new List<checkBool>();
    EventScript _event;
    
    private void OnEnable() {
        _event = GetComponent<EventScript>();
        foreach(GameObject obj in checkBoolObj) {
            checkBool check = obj.GetComponent<checkBool>();
            if(check != null){
                checks.Add(check);
                // Debug.Log("Add" + check);
            }
        }
    }

    public void checkAll(){
        foreach(checkBool check in checks){
            if(!check._check()){
                return;
            }
        }
        // Debug.Log("All Win");
        allWin = true;
        _event._FinishEvent();
    } 
}
