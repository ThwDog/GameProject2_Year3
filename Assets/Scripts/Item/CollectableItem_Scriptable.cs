using System;
using UnityEngine;


[CreateAssetMenu(fileName = "CollectableItem_Scriptable", menuName = "ScriptableObj/Item", order = 0)]
public class CollectableItem_Scriptable : ScriptableObject {

    public enum level{
        one , two, three , four 
    }
    
    public string objNam;
    public string disc;// discription of item
    public GameObject obj;
    public level itemlevel = level.one ; // item in level what

}

    

