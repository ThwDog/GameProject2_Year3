using System;
using System.Collections;
using System.Collections.Generic;
using HeneGames.DialogueSystem;
using UnityEngine;

public class NPC_CheckQuest : MonoBehaviour
{
    enum type{
        Trigger , _bool
    }

    DialogueManager dialogue;
    [Header("Setting")]
    [SerializeField] string playerPlayAnimationName;
    [SerializeField] type animationType;
    [SerializeField] bool isQuestFinish = false;
    ShowUICollision showUI;

    private void Start() {
        if(GetComponentInChildren<DialogueManager>()) dialogue = GetComponentInChildren<DialogueManager>();
    }

    private void OnTriggerStay(Collider other) {
        if(GetComponent<ShowUICollision>())
        showUI = GetComponent<ShowUICollision>();
        if(other.TryGetComponent<PlayerController>(out PlayerController player) && isQuestFinish){
            showUI.ShowDescription();
            if(!isQuestFinish) return;

            if(Input.GetKey(KeyCode.E)) {
                Debug.Log("play Animation");
                playAnimation(player); // play Player Animation
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.GetComponent<PlayerController>() && isQuestFinish){
            showUI.CloseDescription();
        }
    }

    void playAnimation(PlayerController player){
        switch(animationType){
            case type.Trigger:
                player._PlayAnimation(playerPlayAnimationName);
                break;
            case type._bool:
                player._PlayAnimation(playerPlayAnimationName,isQuestFinish);
                break;
            default:
                break;
        }
    }

    public void _SetQuest(bool _bool){
        isQuestFinish = _bool;
    }
}
