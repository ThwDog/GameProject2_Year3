using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PoolPuzzle : NPC_CheckQuest
{
    [Header("")]
    [SerializeField] ItemScript[] itemFishing; // item that can get in fishing 
    [SerializeField][Range(0, 10)] float finishTimeDelay = 5f;
    bool fishing = false;
    internal bool startPuzzle;
    EventScript _events;
    FishPull fish;

    public override void OnTriggerStay(Collider other)
    {
        if (isQuestFinish)
        {
            startPuzzle = false;
            return;
        }

        if (GetComponent<ShowUICollision>())
            showUI = GetComponent<ShowUICollision>();

        if (other.TryGetComponent<PlayerController>(out PlayerController _player))
        {
            startPuzzle = true;

            dialogueCall.inventoryCheck(_player.gameObject.GetComponent<InventorySystem>());
            // if (!dialogue.questIsFinish) return;

            showUI.ShowDescription();

            if (Input.GetKey(KeyCode.E) && !fishing)
            {
                Debug.Log("E");
                if (!dialogueCall.questIsFinish)
                {
                    dialogueCall.playDialogue();
                    return;
                }

                dialogueCall.playDialogue();
                this.player = _player;
                playPlayerAnimation(); // play animation player here
                fishing = true;
                
                //add fish pull script in game obj
                fish = gameObject.GetOrAddComponent<FishPull>();
                fish.enabled = true;

                // StartCoroutine(waitCollect(finishTimeDelay, _player));

                // if use have fishing flute is true 
                showUI.CloseDescription();
            }
        }
    }

    public void finishPool()
    {
        player._SetFishing(false);
        itemFishing[0].Collect(player.gameObject.GetComponent<InventorySystem>());
        fishing = false;
        isQuestFinish = true;
    }

    IEnumerator waitCollect(float time, PlayerController _player)
    {
        yield return new WaitForSeconds(time);
        itemFishing[0].Collect(_player.gameObject.GetComponent<InventorySystem>()); // can random this
        yield break;
    }
}
