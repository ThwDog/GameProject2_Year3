using System;
using System.Collections;
using System.Collections.Generic;
using HeneGames.DialogueSystem;
using UnityEngine;

public class NPC_CheckQuest : MonoBehaviour
{
    enum animationType
    { // type of play animation
        Trigger, _bool, none
    }

    internal DialogueManager dialogue;
    [Header("Setting")]
    [SerializeField] string playerPlayAnimationName;
    [SerializeField] animationType _animationType;
    public bool isQuestFinish = false;
    internal ShowUICollision showUI;
    internal EventScript _event;
    internal PlayerController player;

    private void Start()
    {
        if (GetComponentInChildren<DialogueManager>()) dialogue = GetComponentInChildren<DialogueManager>();
        _event = GetComponent<EventScript>();
    }

    public virtual void OnTriggerStay(Collider other)
    {
        if (isQuestFinish) return;

        if (GetComponent<ShowUICollision>())
            showUI = GetComponent<ShowUICollision>();

        if (other.TryGetComponent<PlayerController>(out PlayerController _player))
        {
            if(!dialogue.questIsFinish) showUI.ShowDescription();
            // player must give item then talk to npc 
            if (Input.GetKey(KeyCode.E))
            {
                showUI.CloseDescription();
                dialogue.inventoryCheck(_player.gameObject.GetComponent<InventorySystem>());
                this.player = _player;
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isQuestFinish) return;
        if (other.GetComponent<PlayerController>() && !isQuestFinish)
        {
            showUI.CloseDescription();
        }
    }

    // normally it working on Dialogue manager
    public void playAnimation()
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

    public void _SetQuest(bool _bool)
    {
        isQuestFinish = _bool;
    }
}
