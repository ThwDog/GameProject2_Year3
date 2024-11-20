using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayerFunction : MonoBehaviour
{
    [SerializeField] PlayerController playerCon;


    public void playRodHitWaterSFX(){
        SoundManager.instance.PlaySfx("RodHitWater");
    }

    public void enabledShowItemSprite(){
        playerCon.enabledShowItemSprite();
    }

    public void disableShowItemSprite(){
        playerCon.disableShowItemSprite();
    }
}
