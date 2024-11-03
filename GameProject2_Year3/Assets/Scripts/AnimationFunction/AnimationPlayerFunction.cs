using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayerFunction : MonoBehaviour
{
    public void playRodHitWaterSFX(){
        SoundManager.instance.PlaySfx("RodHitWater");
    }
}
