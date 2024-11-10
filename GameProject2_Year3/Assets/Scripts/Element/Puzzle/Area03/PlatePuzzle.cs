using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// if player fail one player need to play again
public class PlatePuzzle : MonoBehaviour , IRestartable
{
    // TODO : use signal that player know is it win or need to start again 

    [Header("Setting")]
    [SerializeField][Tooltip("ForCheck")] int stage = 1;
    [SerializeField] private GameObject winCheck; // for player to know that win or lose
    [SerializeField] [Range(0,20)] private float delay;
    [SerializeField] private List<plateList> originalPlateLists; // list of plate pattern
    internal List<plateList> usePlateLists; // list of plate pattern but it use
    [SerializeField] private List<string> plateName = new List<string>(); // list of plate that player has step
    public bool canStep;
    private bool canDelay = true;
    private bool canCheck = true;
    [Header("Only for check")]
    [SerializeField] private bool isWin = false;

    private EventScript _event;


    private void Start()
    {
        mirrorList();
        _event = GetComponent<EventScript>();
    }

    private void Update()
    {
        if(isWin) return;

        if (usePlateLists.Count == 0 && !isWin)
        {
            isWin = true;
            _event._FinishEvent();
            return;
        }

        // Check if plate is same
        if (usePlateLists[0].plateNum.Length == plateName.Count && canCheck)
        {
            bool isSame = true;
            for (int i = 0; i < usePlateLists[0].plateNum.Length; i++)
            {
                if (usePlateLists[0].plateNum[i].name != plateName[i])
                {
                    isSame = false;
                    break;
                }
            }

            if (isSame) // finish
            {
                Debug.Log("Win");
                stage++;
                // resetAllPlate();
                if(canDelay) {
                    canDelay = false;
                    StartCoroutine(resetDelay());
                }
                usePlateLists.Remove(usePlateLists[0]);
                StartCoroutine(changeColor(2,Color.green));
            }
            else // fail
            {
                Debug.Log("lose");
                stage = 1;
                // resetAllPlate();
                if(canDelay) {
                    canDelay = false;
                    StartCoroutine(resetDelay());
                }
                mirrorList();
                StartCoroutine(changeColor(2,Color.red));
            }
        }
        
    }

    private void mirrorList(){
        usePlateLists = originalPlateLists.ToList();
    }

    IEnumerator resetDelay(){
        canCheck = false;
        canStep = false;

        yield return new WaitForSeconds(delay);
        resetAllPlate();

        canStep = true;
        canDelay = true;
        canCheck = true;
        yield break;
    }

    // usee when ever player win or lose
    private void resetAllPlate()
    {
        PlateScript[] plateScripts = GetComponentsInChildren<PlateScript>();
        foreach (PlateScript plate in plateScripts)
        {
            plate.resetPlate();
        }
        plateName.Clear();
    }

    public void StepOnPlate(string plateNum)
    {
        if(isWin) return;
        plateName.Add(plateNum);
    }

    public void _Restart()
    {
        resetAllPlate();
        mirrorList();
        canStep = true;
        isWin = false;
    }


    #region just for check
    IEnumerator changeColor(float time ,Color color){
        MeshRenderer mesh = winCheck.GetComponent<MeshRenderer>();
        mesh.material.color = color;
        yield return new WaitForSeconds(time);
        mesh.material.color = Color.white;
    }
    #endregion
}


[Serializable]
public class plateList
{
    public GameObject[] plateNum; // list of plate num
}
