using System;
using System.Collections;
using System.Collections.Generic;
using HeneGames.DialogueSystem;
using UnityEngine;

// TODO : in dialogue manager use call type

public class NPC_CheckQuest : MonoBehaviour
{
    enum animationType
    { // type of play animation
        Trigger, _bool, none
    }

    public DialogueManager dialogueCall;
    [Header("Setting")]
    [SerializeField] string playerPlayAnimationName;
    [SerializeField] animationType _animationType;
    public bool isQuestFinish = false;
    internal ShowUICollision showUI;
    internal EventScript _event;
    internal PlayerController player;
    [SerializeField] ItemScript giveItem;
    // bool canShowDes;

    private void Start()
    {
        // get dialogue from call type
        // if (GetComponentInChildren<DialogueManager>().triggerState == DialogueManager.TriggerState.Call) dialogueCall = GetComponentInChildren<DialogueManager>();
        _event = GetComponent<EventScript>();
    }

    public virtual void OnTriggerStay(Collider other)
    {
        if(!dialogueCall) return;

        if (isQuestFinish) return;

        if (GetComponent<ShowUICollision>())
            showUI = GetComponent<ShowUICollision>();

        if (other.TryGetComponent<PlayerController>(out PlayerController _player))
        {
            if(!dialogueCall.questIsFinish) showUI.ShowDescription();
            // player must give item then talk to npc 
            if (Input.GetKey(KeyCode.E))
            {
                showUI.CloseDescription();
                dialogueCall.inventoryCheck(_player.gameObject.GetComponent<InventorySystem>());
                // if player doesn't have item call dialogue you doesn't have item
                dialogueCall.playDialogue();
                // if(!dialogueCall.checkStatusQuest() && dialogueCall.checkCanTalk()) dialogueCall.setQuest(true);
                
                this.player = _player;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(!showUI) return;
        if (isQuestFinish) return;
        if (other.GetComponent<PlayerController>() && !isQuestFinish)
        {
            // if(!dialogue.gameObject.activeSelf) dialogue.gameObject.SetActive(true);
            // if(!canShowDes){
            //     canShowDes = true;
            // }
            showUI.CloseDescription();
        }
    }

    // normally it working on Dialogue manager
    public void playPlayerAnimation()
    {
        switch (_animationType)
        {
            case animationType.Trigger:
                player._PlayAnimation(playerPlayAnimationName);
                break;
            case animationType._bool:
                player._PlayAnimation(playerPlayAnimationName, isQuestFinish);
                break;
            case animationType.none:
                break;
            default:
                break;
        }
    }

    public void _giveItem(){
        giveItem.Collect(FindAnyObjectByType<InventorySystem>());
    }

    public void setCollect(){
        giveItem.setCollect();
    }

    public void _SetQuest(bool _bool)
    {
        isQuestFinish = _bool;
    }
}
