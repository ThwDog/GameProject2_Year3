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


    public void _PlayPlayerSprite(string spiteName){
        if(playerSprite == null) {Debug.Log("yo"); return;}

        Sprite foundSprite = playerSprite.Find(sprite => sprite.name == spiteName); // find sprite name

        if(foundSprite == null) {
            Debug.LogError("Don't Found : " + spiteName);
            return;
        }

        setSprite(playerSpriteRen,foundSprite);
        showSpriteRen(playerSpriteRen);
    }

    public void _PlayNPCSprite(string spiteName){
         if(npcSprites == null) return;

        // Check in npcSprites that if SpriteName Exist in sprites get it
        NPCSprite s = npcSprites.Find(npcSprite => npcSprite.sprites.Exists(sprite => sprite.name == spiteName)); 
        Sprite foundSprite = s.sprites.Find(sprite => sprite.name == spiteName); // get sprite from list

        if(foundSprite == null) {
            Debug.LogError("Don't Found : " + spiteName);
            return;
        }

        setSprite(npcSpriteRen,foundSprite);
        showSpriteRen(npcSpriteRen);
        
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
    public void closeSpriteRen(Image obj){
        // close sprite render
        if(obj.gameObject.activeSelf) obj.gameObject.SetActive(false);
    }

    public void _enable()
    {
        canPlayOnStart = true;
    }
}

[Serializable] 
public class NPCSprite{
    public List<Sprite> sprites;
}
