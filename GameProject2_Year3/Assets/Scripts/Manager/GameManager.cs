using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonClass<GameManager>
{
    CutSceneManager cutSceneManager; // getCompo every stage
    UIManager uiManager;
    LoadScene loadScene;

    public override void Awake()
    {
        base.Awake();
        uiManager = GetComponent<UIManager>();
        loadScene = GetComponent<LoadScene>();
    }

    private void Update()
    {
        if(loadScene.CheckNextStage() == 1) return;
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("Pause");
            uiManager.pauseMenu();
        }
    }
}
