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
    [SerializeField] private GameObject crystal; // for player to know that win or lose
    [SerializeField] private Material loseMat;
    [SerializeField] [Range(0,20)] private float delay;
    [SerializeField] private List<plateList> originalPlateLists; // list of plate pattern
    internal List<plateList> usePlateLists; // list of plate pattern but it use
    [SerializeField] private List<string> plateName = new List<string>(); // list of plate that player has step
    public bool canStep;
    private bool canDelay = true;
    private bool canCheck = true;
    [Header("Only for check")]
    [SerializeField] private bool isWin = false;
    [SerializeField] ItemScript reward;
    [SerializeField] CollectableItem_Scriptable reward_Sprite;

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

        if(canStep){
            if(crystal.GetComponent<MeshRenderer>().material != usePlateLists[0].stageColor){
                StartCoroutine(changeColor(1,usePlateLists[0].stageColor));
            }
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
                // Debug.Log("Win");
                stage++;
                // resetAllPlate();
                if(canDelay) {
                    canDelay = false;
                    StartCoroutine(resetDelay());
                }
                usePlateLists.Remove(usePlateLists[0]);
                // StartCoroutine(changeColor(2,winMat));
            }
            else // fail
            {
                // Debug.Log("lose");
                stage = 1;
                // resetAllPlate();
                if(canDelay) {
                    canDelay = false;
                    StartCoroutine(resetDelay());
                }
                mirrorList();
                _event._ExitEvent();
                StartCoroutine(changeColor(2,loseMat));
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

    public void setCanStep(bool _bool){
        canStep = _bool;
    }


    #region just for check
    IEnumerator changeColor(float time , Material mat){
        MeshRenderer mesh = crystal.GetComponent<MeshRenderer>();
        yield return new WaitForSeconds(time);
        mesh.material = mat;
        // mesh.material = defaultMat;

    }
    #endregion

    public void setCollect(InventorySystem _inventory){
        if(reward_Sprite._sprite != null) _inventory.player.setShowItemSprite(reward_Sprite._sprite);
        reward.setCollect(_inventory);
    }
}


[Serializable]
public class plateList
{
    public Material stageColor;
    public GameObject[] plateNum; // list of plate num
}
