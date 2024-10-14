using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUICollision : MonoBehaviour
{
    [SerializeField] GameObject description;
    public bool canOpen;

    public void setOpen(bool _bool){
        canOpen = _bool;
    }

    public bool checkActive(){
        return description.activeSelf;
    }

    public void ShowDescription(){
        if(!canOpen) return;
        if(!description) {
            GetComponentInChildren<Canvas>();
        }
        if(!description) return;
        description.SetActive(true);
    }

    public void CloseDescription(){
        if(!description) {
            GetComponentInChildren<Canvas>();
        }
        if(!description) return;
        description.SetActive(false);
    }
}
