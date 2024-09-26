using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    [Header("Quest")]
    [SerializeField] Quest_Scriptable currentQuest;
    [SerializeField] int currentQuestIndex = 0;
    [Header("Setting")]
    [SerializeField] TMP_Text questTitle_Text;
    [SerializeField] TMP_Text questDes_Text;
    [SerializeField] GameObject questCanvas;

    InventorySystem inventory;

    private void Awake()
    {
        inventory = FindAnyObjectByType<InventorySystem>();
    }

    private void Update()
    {
        if (!currentQuest) {
            questCanvas.SetActive(false);
            return;
        }
        else questCanvas.SetActive(true); 


        // currentQuestIndex will alway smaller than currentQuest.quests.Count
        if (currentQuestIndex < currentQuest.quests.Count == false){
            currentQuestIndex--;
            Debug.Log("There are no more quest");
            return;
        }

        questTitle_Text.text = currentQuest.quests[currentQuestIndex].questTitle;
        questDes_Text.text = currentQuest.quests[currentQuestIndex].questDescription;

        changeQuestList(); // it alway check 
    }

    // use most in npc
    public void ChangeQuest(Quest_Scriptable quest)
    {
        resetIndex();
        currentQuest = quest;
    }

    // if quest type is check type it method will skip to next quest
    // use most in npc
    public void finishCheckQuest()
    {
        if (currentQuest.quests[currentQuestIndex]._checkQuestType != Quest.type.CheckType) return;

        if (currentQuestIndex < currentQuest.quests.Count == false)
        {
            currentQuestIndex--;
            return;
        }
        else currentQuestIndex++;
    }

    public void changeQuestList()
    {
        if (currentQuest.quests.Count < 1) return;
        if (currentQuestIndex < currentQuest.quests.Count == false) return;

        if (currentQuest.quests[currentQuestIndex]._checkQuestType != Quest.type.itemReqType) return;

        if (inventory._CheckItemReq(currentQuest.quests[currentQuestIndex].itemReq))
        {
            currentQuestIndex++;
        }
        else return;
    }

    private void resetIndex()
    {
        currentQuestIndex = 0;
    }

    #region Editor Script

    public bool listCanNext
    {
        get { 
            if (!currentQuest) return false;
            return currentQuestIndex < currentQuest.quests.Count; 
        }
    }

    public string currentQuestType
    {
        get { 
            if (!currentQuest) return "";
            return currentQuest.quests[currentQuestIndex]._checkQuestType.ToString(); 
        }
    }

    public string currentQuestItemReq
    {
        get { 
            if (!currentQuest) return "";
            return currentQuest.quests[currentQuestIndex].itemReq.name; 
        }
    }

    public string currentQuestTitle
    {
        get { 
            if (!currentQuest) return "";
            return currentQuest.quests[currentQuestIndex].questTitle; 
        }
    }

    public string currentQuestDes
    {
        get { 
            if (!currentQuest) return "";
            return currentQuest.quests[currentQuestIndex].questDescription; 
        }
    }

    public Quest.type type
    {
        get { 
            if (!currentQuest) return Quest.type.itemReqType;
            return currentQuest.quests[currentQuestIndex]._checkQuestType; 
        }
    }
    #endregion
}

