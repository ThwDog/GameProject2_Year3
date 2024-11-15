using System;
using System.Collections;
using System.Collections.Generic;
using HeneGames.DialogueSystem;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent (typeof(DialogueManager))]
public class PlayCutScene : MonoBehaviour
{
    // TODO : check key from Dialogue UI and run next sprite CutScene
    // [Tooltip("Set it to true if want to play in start")]public bool playCutSceneOnStart = false; // set to true if want to play on start
    [SerializeField] Sprite[] cutSceneSprite;
    [SerializeField] Image cutSceneImageUI;
    [SerializeField] float cutSceneDelay;
    [SerializeField] bool playCutSceneWithDialogue = false;
    // bool hasPlayDialogue = false;
    DialogueManager dialogueManager;
    CutSceneManager cutSceneManager;
    EventScript _event;
    int currentIndex = 0;


    private void Start() {
        dialogueManager = GetComponent<DialogueManager>();
        cutSceneManager = GetComponentInParent<CutSceneManager>();
        _event = GetComponent<EventScript>();
        // play cut scene on start
        // if(playCutSceneOnStart && !hasPlayDialogue){
        //     if(dialogueManager) dialogueManager.playDialogue();
        //     hasPlayDialogue = true;
        // }
    }

    public void _playCutScene() {
        if(playCutSceneWithDialogue)playDialogue();
        else{
            setCutSceneImg(0);
            StartCoroutine(playCutSceneSlide());
        }
    }

    IEnumerator playCutSceneSlide()
    {
        if(currentIndex > cutSceneSprite.Length - 1) {
            yield return new WaitForSeconds(cutSceneDelay);
            closeCutScene();
            _event._ExitEvent();
            yield break;
        }
        yield return new WaitForSeconds(cutSceneDelay);
        setCutSceneNextIndex();
        StartCoroutine(playCutSceneSlide());
    }

    private void playDialogue(){
        if(dialogueManager)dialogueManager.playDialogue();
    }

    // Set Cut Scene BG
    public void setCutSceneImg(int index){
        currentIndex = index;
        cutSceneManager.setSprite(cutSceneImageUI,cutSceneSprite[index]);
        cutSceneManager.showSpriteRen(cutSceneImageUI);
    }

    public void setCutSceneNextIndex(){
        cutSceneManager.setSprite(cutSceneImageUI,cutSceneSprite[currentIndex++]);
        // cutSceneManager.showSpriteRen(cutSceneImageUI);
    }

    public void closeCutScene(){
        cutSceneManager.closeSpriteRen(cutSceneImageUI);
    }
}
