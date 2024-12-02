using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayerFunction : MonoBehaviour
{
    [SerializeField] PlayerController playerCon;
    [SerializeField] GameObject effect;


    public void playRodHitWaterSFX(){
        SoundManager.instance.PlaySfx("RodHitWater");
    }

    public void enabledShowItemSprite(){
        playerCon.enabledShowItemSprite();
    }

    public void disableShowItemSprite(){
        playerCon.disableShowItemSprite();
    }

    public void enableEffect(){
        effect.SetActive(true);
    }

    public void disableEffect(){
        effect.SetActive(false);
    }
}
