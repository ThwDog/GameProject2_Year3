using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// TODO : Should set this in npc and npc set to Quest manager
[CreateAssetMenu(fileName = "Quest_Scriptable", menuName = "ScriptableObj/Quest", order = 0)]
public class Quest_Scriptable : ScriptableObject {
    public enum level{
        one , two, three , four 
    }
    [Header("Setting")]
    public level itemArea = level.one ; // quest in level what
    public string description;

    public List<Quest> quests;
}

[Serializable]
public class Quest{
    public enum type{
        itemReqType , CheckType
    }

    public string questTitle;
    public string questDescription;
    public type _checkQuestType; // how dialogue check finish

    [Header("Item Requirement Type")] // go to next Quest by checking player inventory
    public CollectableItem_Scriptable itemReq; 
    // public int itemAmountReq;

    // [Header("Check Type")] // go to next quest by bool
    // public bool checkQuest = false;
}
