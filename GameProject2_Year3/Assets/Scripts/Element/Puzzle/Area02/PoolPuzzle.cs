using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolPuzzle : NPC_CheckQuest
{
    [Header("")]
    [SerializeField] ItemScript[] itemFishing; // item that can get in fishing 
    [SerializeField][Range(0,10)] float finishTimeDelay = 5f;

    public override void OnTriggerStay(Collider other)
    {
        if (isQuestFinish) return;

        if (GetComponent<ShowUICollision>())
            showUI = GetComponent<ShowUICollision>();

        if (other.TryGetComponent<PlayerController>(out PlayerController _player))
        {
            dialogue.inventoryCheck(_player.gameObject.GetComponent<InventorySystem>());
            if (!dialogue.questIsFinish) return;

            showUI.ShowDescription();

            if (Input.GetKey(KeyCode.E))
            {
                this.player = _player;
                playAnimation();
                StartCoroutine(waitCollect(finishTimeDelay,_player));

                isQuestFinish = true;
                
                if(isQuestFinish) showUI.CloseDescription();
            }
        }
    }

    IEnumerator waitCollect(float time , PlayerController _player){
        yield return new WaitForSeconds(time);
        itemFishing[0].Collect(_player.gameObject.GetComponent<InventorySystem>()); // can random this
        yield break;
    }
}
