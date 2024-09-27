using System.Collections;
using System.Collections.Generic;
using HeneGames.DialogueSystem;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent (typeof(DialogueManager))]
public class PlayCutScene : MonoBehaviour
{
    // TODO : check key from Dialogue UI and run next sprite CutScene
    [Tooltip("Set it to true if want to play in start")]public bool playCutSceneOnStart = false; // set to true if want to play on start
    [SerializeField] Sprite[] cutSceneSprite;
    [SerializeField] Image cutSceneImage;
    bool hasPlayDialogue = false;
    DialogueManager dialogueManager;
    CutSceneManager cutSceneManager;


    private void Start() {
        dialogueManager = GetComponent<DialogueManager>();
        cutSceneManager = GetComponentInParent<CutSceneManager>();

        // play cut scene on start
        if(playCutSceneOnStart && !hasPlayDialogue){
            if(dialogueManager) dialogueManager.playDialogue();
            hasPlayDialogue = true;
        }
    }

    public void _playCutScene() {
        if(dialogueManager) dialogueManager.playDialogue();
    }

    public void setCutSceneImg(int index){
        cutSceneManager.setSprite(cutSceneImage,cutSceneSprite[index]);
        cutSceneManager.showSpriteRen(cutSceneImage);
    }
}
