using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatForStage : MonoBehaviour
{
    enum stage{
        one , two , three
    }

    [SerializeField] stage _stage;
    [Header("Stage 02")]
    [SerializeField] GameObject wall;

    public void cheatStage(){
        switch(_stage){
            case stage.one :
                break;
            case stage.two :
                stageTwo();
                break;
            case stage.three:
                break;
        }
    }

    public void stageTwo(){
        if(GUI.Button(new Rect(10, 700, 110, 50),"Open Area")){
            wall.SetActive(false);
        }
    }
}
