using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    // TODO : Add load Path in scene use additional
    [Header("Loading Scene")]
    [SerializeField] GameObject loadScene; 
    [SerializeField] Slider progressBar; 
    List<AsyncOperation> sceneLoad = new List<AsyncOperation>();

    // for go to new stage or next scene
    public void _LoadStage(){
        loadScene.gameObject.SetActive(true);
        sceneLoad.Add(SceneManager.LoadSceneAsync(CheckNextStage(),LoadSceneMode.Additive));

        StartCoroutine(LoadingScene());
    }

    public void _UnloadStage(){
        sceneLoad.Add(SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadingScene(){
        for(int i = 0;i < sceneLoad.Count; i++){
            while(!sceneLoad[i].isDone){
                Debug.Log("Load" + sceneLoad[i].progress);
                progressBar.value = Mathf.Clamp01(sceneLoad[i].progress / .9f);
                yield return null;
            }
        }
        if(!loadScene) yield break;
        loadScene.gameObject.SetActive(false);
    }

    private int CheckNextStage(){
        int index = SceneManager.GetActiveScene().buildIndex;
        switch(index){
            case 0: // wait scene
                return 1;
            case 1: // main menu
                return 2;
            case 2:
                return 3;
            
            case 3:
                return 4;
            case 4:
                return 0;
        }
        // should not be here
        return 0;
    }
}


