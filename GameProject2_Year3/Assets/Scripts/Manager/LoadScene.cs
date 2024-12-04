using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [Header("load Scene")]
    [SerializeField] private Sprite[] loadSceneSprite;
    [SerializeField] private Image image;
    // for go to next scene
    public void _LoadScene(){
        randomLoadScene();
        loadScene.gameObject.SetActive(true);
        // sceneLoad.Add(SceneManager.LoadSceneAsync(CheckNextStage(),LoadSceneMode.Additive)); // for load multiple scene add once
        sceneLoad.Add(SceneManager.LoadSceneAsync(CheckNextStage(),LoadSceneMode.Single));

        StartCoroutine(LoadingScene());
    }

    public void _UnloadStage(){
        sceneLoad.Add(SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex));
    }

    public void toMainMenu(){
        randomLoadScene();
        loadScene.gameObject.SetActive(true);
        // sceneLoad.Add(SceneManager.LoadSceneAsync(CheckNextStage(),LoadSceneMode.Additive)); // for load multiple scene add once
        sceneLoad.Add(SceneManager.LoadSceneAsync(0,LoadSceneMode.Single));

        StartCoroutine(LoadingScene());
    }

    IEnumerator LoadingScene(){
        float totalProgress = 0;
        for(int i = 0;i < sceneLoad.Count; i++){
            while(sceneLoad[i].progress < 0.9f){
                totalProgress += sceneLoad[i].progress;
                // Debug.Log("Load" + sceneLoad[i].progress);
                progressBar.value = totalProgress/sceneLoad.Count;
                yield return null;
            }
        }
        if(!loadScene) yield break;
        yield return new WaitUntil(() => sceneLoad.All(a => a.progress >= 0.9)); // for safe
        loadScene.gameObject.SetActive(false); 
        setEnable();
        progressBar.value = 0f; // reset
    }

    public void setEnable(){
        GameObject[] gameObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject gameObject in gameObjects) {
            IEnable myInterface = gameObject.GetComponent<IEnable>();
            if (myInterface != null) {
                myInterface._enable();
            }
        }
    }

    private void randomLoadScene(){
        int rnd = Random.Range(0, loadSceneSprite.Length - 1);
        image. sprite = loadSceneSprite[rnd];
    }

    public int CheckNextStage(){
        int index = SceneManager.GetActiveScene().buildIndex;
        return index + 1;
    }


}


