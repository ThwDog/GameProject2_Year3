using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory_Scriptable", menuName = "ScriptableObj/Inventory", order = 0)]
public class Inventory_Scriptable : ScriptableObject {
    public int level;
    public CollectableItem_Scriptable[] listOfItem; //the list of item that should be in that level
    public string disc; // this is discription 
}

