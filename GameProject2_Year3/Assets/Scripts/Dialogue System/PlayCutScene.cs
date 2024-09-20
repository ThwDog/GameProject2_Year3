using System.Collections;
using System.Collections.Generic;
using HeneGames.DialogueSystem;
using UnityEngine;


[RequireComponent (typeof(DialogueManager))]
public class PlayCutScene : MonoBehaviour
{
    // TODO : check key from Dialogue UI and run next sprite CutScene
    public bool playCutScene = false; // set to true if want to play on start
    [SerializeField] Sprite[] cutSceneSprite;
    bool hasPlayDialogue = false;
    DialogueManager dialogueManager;

    private void Start() {
        dialogueManager = GetComponent<DialogueManager>();
    }

    private void Update() {
        if(playCutScene && !hasPlayDialogue){
            if(dialogueManager) dialogueManager.playDialogue();
            hasPlayDialogue = true;
        }
    }
}
