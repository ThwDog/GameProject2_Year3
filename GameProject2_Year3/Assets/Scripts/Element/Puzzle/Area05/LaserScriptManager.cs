using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserScriptManager : MonoBehaviour , IRestartable
{
    public bool isWin = false;
    [SerializeField] GameObject[] ListOfLaser;
    List<Quaternion> originalRotation = new List<Quaternion>();
    private EventScript _event; 

    private void Start() {
        getOriginalRote();
        _event = GetComponent<EventScript>();
    }

    public void Win(){
        isWin = true;
        _event._FinishEvent();
    }


    void getOriginalRote(){
        foreach (var laser in ListOfLaser){
            originalRotation.Add(laser.transform.rotation);
        }
    }

    private void _Reset(){
        foreach (var laser in ListOfLaser){
            laser.transform.localRotation = Quaternion.identity;
        }
    }

    public void _Restart()
    {
        _Reset();
    }

}
