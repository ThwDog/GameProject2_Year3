using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO : this is not real inventory just for check that player has been collected certain item to 

// TODO : checkItemReq in every level is diff in game manager should have script that can  assign them 
public class InventorySystem : MonoBehaviour
{
    public Inventory_Scriptable checkItemReq; //inventory in level
    public List<CollectableItem_Scriptable> itemReq = new List<CollectableItem_Scriptable>(); // get name of item in inventory scriptable // TODO : this value use for check that what item player should collect
    public List<CollectableItem_Scriptable> inventory = new List<CollectableItem_Scriptable>(); // item that player has collect    
    internal PlayerController player;

    private void Start()
    {
        player = GetComponent<PlayerController>();

        _AssignItemReq();
    }


    public void _AssignItemReq()
    {
        if (checkItemReq != null)
        {
            for (int i = 0; i < checkItemReq.listOfItem.Length; i++)
            {
                if (itemReq.Contains(checkItemReq.listOfItem[i]))
                    return;

                itemReq.Add(checkItemReq.listOfItem[i]);
            }
        }
    }

    public void _AssignItemReq(Inventory_Scriptable inventory)
    {
        itemReq.Clear();
        this.inventory.Clear();
        this.checkItemReq = inventory;
        _AssignItemReq();
    }

    public void resetInventory(){
        inventory = null;
    }

    public void cheat(){
        inventory.AddRange(itemReq);
    }

    // check inventory if have all item require 
    // use in other obj
    public void _CheckItemReq(ref bool check)
    {
        check = true;
        foreach (var item in itemReq)
        {
            if (!inventory.Contains(item))
            {
                check = false;

                Debug.Log("You doesn't have " + item.name);
                break;
            }
        }
    }

    public bool _CheckItemReq(CollectableItem_Scriptable item)
    {
        if (!inventory.Contains(item))
        {
            // Debug.Log("You doesn't have " + item.name);
            return false;
        }
        else return true;
    }


}
