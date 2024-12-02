using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonClass<GameManager>
{
    CutSceneManager cutSceneManager; // getCompo every stage
    UIManager uiManager;
    LoadScene loadScene;
    public bool paused = false;


    public override void Awake()
    {
        base.Awake();
        uiManager = GetComponent<UIManager>();
        loadScene = GetComponent<LoadScene>();
    }

    private void Update()
    {
        if(loadScene.CheckNextStage() - 1 == 0) return;

        uiManager.updateVolume();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Pause");
            uiManager.pauseMenu();
        }
        if(Input.GetMouseButtonDown(0)){
            SoundManager.instance.PlaySfx("Click");
        }
        if(Input.GetKeyUp(KeyCode.F1)){
            GetComponent<CheatCode>().cheatEnable = !GetComponent<CheatCode>().cheatEnable;
        }
    }

    public void NextChapter(){
        loadScene._LoadScene();
    }

    public void exit(){
        Application.Quit();
    }

    public void toMainMenu(){
        loadScene.toMainMenu();
        uiManager.resume();
    }

}
