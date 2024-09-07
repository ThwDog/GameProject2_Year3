using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO : this is not real inventory just for check that player has been collected certain item to 
public class InventorySystem : MonoBehaviour{
    public Inventory_Scriptable checkItemReq; //inventory in level
    public List<string> itemReq = new List<string>(); // get name of item in inventory scriptable // TODO : this value use for check that what item player should collect
    public List<string> inventory = new List<string>(); // item that player has collect    
    internal PlayerController player;

    private void Start() {
        player = GetComponent<PlayerController>();

        if (checkItemReq != null){
        // get name of requirement item
            for(int i = 0; i < checkItemReq.listOfItem.Length ;i++){
                if(itemReq.Contains(checkItemReq.listOfItem[i].name))
                    return;

                itemReq.Add(checkItemReq.listOfItem[i].name);
            }
        }
    }


}
