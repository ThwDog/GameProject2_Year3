using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

// Open Area with call
public class OpenArea : MonoBehaviour
{
    public enum _callType
    {
        collision, range, call
    }
    public enum _openType
    {
        area, scene
    }

    [Header("Setting")]
    public _callType callType;
    public _openType openType;
    [SerializeField][Range(0, 1000)] float loadRange;

    [Header("Load Area")]
    [SerializeField] GameObject[] _Area;

    [Header("Load Scene")]
    [SerializeField] int callByIndex;
    [SerializeField] string callByName;

    [Tooltip("If it load on start set it to true")][SerializeField] bool isLoad;

    CheckCollision checkCollision;
    [SerializeField] PlayerController player;

    private void Update()
    {
        // open scene or area by check range
        if (callType != _callType.range) return;

        if (Vector3.Distance(transform.position, player.gameObject.transform.position) < loadRange)
        {
            // Debug.Log("Open");
            if (openType == _openType.area)
            {
                _OpenArea(callByIndex);
            }
            else
            {
                _LoadScene(callByName);
            }
        }
        else if (Vector3.Distance(transform.position, player.gameObject.transform.position) > loadRange)
        {
            // Debug.Log("close");
            if (openType == _openType.area)
            {
                _CloseArea(callByIndex);
            }
            else
            {
                _CloseScene(callByName);
            }
        }

    }

    public void _OpenArea(int areaIndex)
    {
        if (!isLoad)
        {
            _Area[areaIndex].SetActive(true);
            isLoad = true;
        }
    }
    public void _CloseArea(int areaIndex)
    {
        if (isLoad)
        {
            _Area[areaIndex].SetActive(false);
            isLoad = false;
        }
    }

    public void _LoadScene(string sceneName)
    {
        if (!isLoad)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            isLoad = true;
        }
    }
    public void _CloseScene(string sceneName)
    {
        if (isLoad)
        {
            SceneManager.UnloadSceneAsync(sceneName);
            isLoad = false;
        }
    }

}
