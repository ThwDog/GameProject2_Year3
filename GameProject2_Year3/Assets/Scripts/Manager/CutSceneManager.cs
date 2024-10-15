using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneManager : MonoBehaviour , IEnable
{

    // TODO : one manager per stage
    // TODO : Run on Unity Event On Player Scene
    [Header("CutScene")]
    [SerializeField] GameObject cutSceneObj;
    [SerializeField] PlayCutScene playCutSceneOnstart;
    [SerializeField] bool canPlayOnStart = false;
    [SerializeField] float waitSecForPlay; // Make IEmulator For wait play 
    bool hasPlay = false;
    [Header("Talk Dialogue")]
    [SerializeField] List<Sprite> playerSprite;
    [SerializeField] Image playerSpriteRen; 
    [SerializeField] List<NPCSprite> npcSprites;
    [SerializeField] Image npcSpriteRen; 
    [Header("Color Sprite")]
    [SerializeField] private Color defaultColor;
    [SerializeField] private Color fadeColor;
    [SerializeField] private float fadeDuration;

    int playerSpriteIndex = 0;
    int npcSpriteIndex = 0;
    NPCSprite _foundSprite;

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


    public void _PlayPlayerSprite(){
        // if(playerSprite == null) {Debug.Log("yo"); return;}
        if(playerSpriteRen.gameObject.activeSelf && playerSpriteIndex < playerSprite.Count - 1) playerSpriteIndex++;
        else if (playerSpriteIndex > playerSprite.Count) playerSpriteIndex = 0;

        Sprite foundSprite = playerSprite[playerSpriteIndex]; // find sprite name

        // if(foundSprite == null) {
        //     Debug.LogError("Don't Found : " + spiteName);
        //     return;
        // }

        setSprite(playerSpriteRen,foundSprite);
        showSpriteRen(playerSpriteRen);
    }

    public void _PlayNPCSprite(string spiteName){

        if(npcSprites == null) return;

        // Check in npcSprites that if SpriteName Exist in sprites get it
        NPCSprite _foundSprite = npcSprites.Find(sprite => sprite.spriteName == spiteName); 
        // Sprite foundSprite = s.sprites.Find(sprite => sprite.name == spiteName); // get sprite from list
        if(npcSpriteRen.gameObject.activeSelf && npcSpriteIndex < _foundSprite.sprites.Count - 1) npcSpriteIndex++;
        else if (npcSpriteIndex > _foundSprite.sprites.Count) npcSpriteIndex = 0;

        Sprite foundSprite = _foundSprite.sprites[npcSpriteIndex]; // get sprite from list

        if(foundSprite == null) {
            Debug.LogError("Don't Found : " + spiteName);
            return;
        }

        setSprite(npcSpriteRen,foundSprite);
        showSpriteRen(npcSpriteRen);
        npcSpriteRen.SetNativeSize();
    }

    public void resetSprite(){
        playerSpriteIndex = 0;
        npcSpriteIndex = 0;
        npcSpriteRen.color = defaultColor;
        playerSpriteRen.color = defaultColor;
    }


    public void setSprite(Image img , Sprite sprite){
        // change sprite and set in location
        img.sprite = sprite;
    }

    [HideInInspector]
    public void showSpriteRen(Image obj){
        // show sprite render
        if(!obj.gameObject.activeSelf) obj.gameObject.SetActive(true);
    }

    // can use in PlayCutScene
    private void closeSpriteRen(Image obj){
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
        if(sprite == "player") {
            StartCoroutine(Fade(playerSpriteRen, defaultColor)); // in
            StartCoroutine(Fade(npcSpriteRen, fadeColor)); // out
        }
        else {
            StartCoroutine(Fade(npcSpriteRen, defaultColor)); // in
            StartCoroutine(Fade(playerSpriteRen, fadeColor)); // out
        }
    }

    private IEnumerator Fade(Image sprite, Color endColor) {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration) {
            sprite.color = Color.Lerp(sprite.color, endColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

       sprite.color = endColor;
    } 
}

[Serializable] 
public class NPCSprite{
    public string spriteName;
    public List<Sprite> sprites;
}
