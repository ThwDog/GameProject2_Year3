using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonClass<GameManager>
{
    CutSceneManager cutSceneManager; // getCompo every stage
    UIManager uiManager;

    public override void Awake()
    {
        base.Awake();
        uiManager = GetComponent<UIManager>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("Pause");
            uiManager.pauseMenu();
        }
    }
}
