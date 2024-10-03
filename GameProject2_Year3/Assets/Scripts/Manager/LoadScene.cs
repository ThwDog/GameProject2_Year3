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
    [SerializeField] GameObject loadScene; // set if  load next scene then get new component obj or just make another obj and add this script
    [SerializeField] Slider progressBar; 
    List<AsyncOperation> sceneLoad = new List<AsyncOperation>();

    // for go to next scene
    public void _LoadScene(){
        loadScene.gameObject.SetActive(true);
        // sceneLoad.Add(SceneManager.LoadSceneAsync(CheckNextStage(),LoadSceneMode.Additive)); // for load multiple scene add once
        sceneLoad.Add(SceneManager.LoadSceneAsync(CheckNextStage(),LoadSceneMode.Single));

        StartCoroutine(LoadingScene());
    }

    public void _UnloadStage(){
        sceneLoad.Add(SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadingScene(){
        float totalProgress = 0;
        for(int i = 0;i < sceneLoad.Count; i++){
            while(!sceneLoad[i].isDone){
                totalProgress += sceneLoad[i].progress;
                Debug.Log("Load" + sceneLoad[i].progress);
                progressBar.value = totalProgress/sceneLoad.Count;
                yield return null;
            }
        }
        if(!loadScene) yield break;
        yield return new WaitForSeconds(1f); // for safe
        loadScene.gameObject.SetActive(false); 
        setEnable();
        progressBar.value = 0f; // reset
    }

    public void setEnable(){
        GameObject[] gameObjects = FindObjectsOfType<GameObject>();
        List<GameObject> objectsWithInterface = new List<GameObject>();

        foreach (GameObject gameObject in gameObjects) {
            IEnable myInterface = gameObject.GetComponent<IEnable>();
            if (myInterface != null) {
                myInterface._enable();
            }
        }
    }

    public int CheckNextStage(){
        int index = SceneManager.GetActiveScene().buildIndex;
        switch(index){
            case 0: // main menu
                return 1;
            case 1: 
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


