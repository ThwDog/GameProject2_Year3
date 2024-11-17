using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneManager : MonoBehaviour , IEnable
{

    // TODO : one manager per stage
    // TODO : Run on Unity Event On Player Scene
    [Header("CutScene On Start")]
    [SerializeField] PlayCutScene playCutSceneOnstart; // Cant use Cut Scene With dialogue
    [SerializeField] bool canPlayOnStart = false;
    [SerializeField] float waitSecForPlay; // Make IEmulator For wait play 
    bool hasPlay = false;
    [Header("Talk Dialogue")]
    [SerializeField] Animator playerAnimator;
    [SerializeField] Animator NPCAnimator;
    [SerializeField] Image playerSpriteRen; 
    [SerializeField] List<NPCAnimator> npcAnimCon;
    [SerializeField] Image npcSpriteRen; 
    [Header("Color Sprite")]
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeDuration;

    // int playerSpriteIndex = 0;
    // int npcSpriteIndex = 0;
    NPCAnimator _foundSprite;

    private void Update() {
        if(hasPlay) return;
        if(!playCutSceneOnstart) return;
        if(!canPlayOnStart) return;
        else{
            if(!hasPlay){
                playCutSceneOnstart._playCutScene(); // Make IEmulator For wait play 
                hasPlay = true;
            }
        }
    }

    public void _PlayerPlayAnimation(){
        playerAnimator.speed = 1;
        NPCAnimator.speed = 0;
        showSpriteRen(playerSpriteRen);
        playAnimationTalk(playerAnimator);
        FadeInNOut("Player");
    }

    // set before play
    public void _NPCSetStart(string name){
        NPCAnimator con  = npcAnimCon.Find(sprite => sprite.controllerName == name);
        NPCSetAnimator(name); // set runtime animator controller

        npcSpriteRen.sprite = con.startSprite;
        npcSpriteRen.SetNativeSize();
    }

    public void _NPCPlayAnimation(){
        playerAnimator.speed = 0;
        NPCAnimator.speed = 1;
        showSpriteRen(npcSpriteRen);
        playAnimationTalk(NPCAnimator);
        FadeInNOut("NPC");
    }

    private void playAnimationTalk(Animator anim){
        anim.SetTrigger("Talk");
    }

    private void NPCSetAnimator(string name){
        NPCAnimator con  = npcAnimCon.Find(sprite => sprite.controllerName == name);
        NPCAnimator.runtimeAnimatorController = con.controller;
    }


    //for set sprite
    public void setSprite(Image img , Sprite sprite){
        // change sprite and set in location
        img.sprite = sprite;
    }

    public void resetSprite(){
        // playerSpriteIndex = 0;
        // npcSpriteIndex = 0;
        npcSpriteRen.color = defaultColor;
        playerSpriteRen.color = defaultColor;
    }
    

    [HideInInspector]
    public void showSpriteRen(Image obj){
        // show sprite render
        if(!obj.gameObject.activeSelf) obj.gameObject.SetActive(true);
    }

    // can use in PlayCutScene
    public void closeSpriteRen(Image obj){
        // close sprite render
        if(obj.gameObject.activeSelf) obj.gameObject.SetActive(false);
    }

    public void closeSpriteAll(){
        closeSpriteRen(playerSpriteRen);
        closeSpriteRen(npcSpriteRen);
    }

    public void _enable()
    {
        canPlayOnStart = true;
    }

    public void FadeInNOut(string sprite) {
        if(sprite == "Player") {
            StartCoroutine(Fade(playerSpriteRen, defaultColor)); // in
            StartCoroutine(Fade(npcSpriteRen, fadeColor)); // out
        }
        else {
            StartCoroutine(Fade(npcSpriteRen, defaultColor)); // in
            StartCoroutine(Fade(playerSpriteRen, fadeColor)); // out
        }
    }

    private IEnumerator Fade(Image sprite, Color endColor) {
        if(!sprite) yield break;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration) {
            sprite.color = Color.Lerp(sprite.color, endColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

       sprite.color = endColor;
    } 

    // fade in to default color
    public void fadeInOtherImg(Image sprite){
        StartCoroutine(Fade(sprite, defaultColor));
    }

    // fade out of default color
    public void fadeOutOtherImg(Image sprite){
        StartCoroutine(Fade(sprite, fadeColor));
    }


    // public void _PlayPlayerSprite(){
    //     // if(playerSprite == null) {Debug.Log("yo"); return;}
    //     if(playerSpriteRen.gameObject.activeSelf && playerSpriteIndex < playerSprite.Count - 1) playerSpriteIndex++;
    //     else if (playerSpriteIndex > playerSprite.Count) playerSpriteIndex = 0;

    //     Sprite foundSprite = playerSprite[playerSpriteIndex]; // find sprite name

    //     // if(foundSprite == null) {
    //     //     Debug.LogError("Don't Found : " + spiteName);
    //     //     return;
    //     // }

    //     setSprite(playerSpriteRen,foundSprite);
    //     showSpriteRen(playerSpriteRen);
    // }

    // public void _PlayNPCSprite(string spiteName){

    //     if(npcSprites == null) return;

    //     // Check in npcSprites that if SpriteName Exist in sprites get it
    //     NPCSprite _foundSprite = npcSprites.Find(sprite => sprite.spriteName == spiteName); 
    //     // Sprite foundSprite = s.sprites.Find(sprite => sprite.name == spiteName); // get sprite from list
    //     if(npcSpriteRen.gameObject.activeSelf && npcSpriteIndex < _foundSprite.sprites.Count - 1) npcSpriteIndex++;
    //     else if (npcSpriteIndex > _foundSprite.sprites.Count) npcSpriteIndex = 0;

    //     Sprite foundSprite = _foundSprite.sprites[npcSpriteIndex]; // get sprite from list

    //     if(foundSprite == null) {
    //         Debug.LogError("Don't Found : " + spiteName);
    //         return;
    //     }

    //     setSprite(npcSpriteRen,foundSprite);
    //     showSpriteRen(npcSpriteRen);
    //     npcSpriteRen.SetNativeSize();
    // }

}

[Serializable] 
public class NPCAnimator{
    public string controllerName;
    public RuntimeAnimatorController controller;
    public Sprite startSprite;
}
