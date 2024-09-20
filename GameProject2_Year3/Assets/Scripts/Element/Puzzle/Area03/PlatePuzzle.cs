using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HeneGames.DialogueSystem;
using UnityEngine;

public class PlatePuzzle : MonoBehaviour , IRestartable
{
    // TODO : use signal that player know is it win or need to start again 

    [Header("Setting")]
    [SerializeField] private GameObject winCheck; // for player to know that win or lose
    [SerializeField] private List<plateList> plateLists; // list of plate pattern
    private List<plateList> usePlateLists; // list of plate pattern but it use
    [SerializeField] private List<string> plateName = new List<string>(); // list of plate that player has step
    [SerializeField] private bool isWin = false;
    public bool canStep;

    private PuzzleEvent _event;


    private void Start()
    {
        mirrorList();
        _event = GetComponent<PuzzleEvent>();
    }

    private void Update()
    {
        if(isWin) return;

        if (usePlateLists.Count == 0 && !isWin)
        {
            isWin = true;
            _event._FinishPuzzle();
            return;
        }

        // Check if plate is same
        if (usePlateLists[0].plateNum.Length == plateName.Count)
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

            if (isSame)
            {
                resetAllPlate();
                usePlateLists.Remove(usePlateLists[0]);
                StartCoroutine(chageColor(2,Color.green));
            }
            else // fail
            {
                resetAllPlate();
                mirrorList();
                StartCoroutine(chageColor(2,Color.red));
            }
        }
        
    }

    private void mirrorList(){
        usePlateLists = plateLists.ToList();
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
    IEnumerator chageColor(float time ,Color color){
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
