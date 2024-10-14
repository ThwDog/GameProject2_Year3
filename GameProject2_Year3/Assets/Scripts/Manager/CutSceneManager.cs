using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneManager : MonoBehaviour , IEnable
{
    public enum type{
        player , npc
    }

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
        if(playerSprite == null) {Debug.Log("yo"); return;}

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
        Sprite foundSprite = _foundSprite.sprites[npcSpriteIndex]; // get sprite from list

        if(foundSprite == null) {
            Debug.LogError("Don't Found : " + spiteName);
            return;
        }

        setSprite(npcSpriteRen,foundSprite);
        showSpriteRen(npcSpriteRen);
    }

    public void _nextSprite(type _type){
        if(_type == type.player){
            if(playerSpriteIndex > playerSprite.Count){
                playerSpriteIndex = 0;
            }
            else playerSpriteIndex++;
        }
        else if(_type == type.npc){
            if(npcSpriteIndex > _foundSprite.sprites.Count){
                npcSpriteIndex = 0;
            }
            else npcSpriteIndex++;
        }
    }

    public void resetSprite(){
        playerSpriteIndex = 0;
        npcSpriteIndex = 0;
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
}

[Serializable] 
public class NPCSprite{
    public string spriteName;
    public List<Sprite> sprites;
}
