using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSortPuzzle_Script : MonoBehaviour , IRestartable
{
    [Header("setting")]
    [SerializeField] private GameObject canvas;
    [SerializeField] List<Coin_Scriptable> reqCoinList; // sort requirement
    [Tooltip("Size of coin that can sort")][SerializeField] private int coinSortListSize = 3;
    [Tooltip("Coin that player sort")]List<Coin_Scriptable> coinSort = new List<Coin_Scriptable>(3); // show coin that player sort
    
    [Header("")]
    [SerializeField] List<CoinIdentify> listOfCoinSort;
    [SerializeField] bool isWin = false;

    private PuzzleEvent _event;

    PlayerController player;

    private void Start() {
        _event = GetComponent<PuzzleEvent>();
        createCoinListSort(coinSortListSize);
    }

    private void Update() {
        if(isWin) return;

        checkCoin();
    }

    public void _AddCoinButton(GameObject obj){
        if(isWin) return;

        Coin_Scriptable coin = obj.GetComponent<CoinIdentify>().coin; 

        if(coinSort.Contains(coin)) return;


        foreach(var coins in listOfCoinSort){
            if(coins.coin == null){
                coins.changeIden(coin);
                coinSort[0] = coins.coin;
                break;
            }
        }

        _UpdateCoinForSort();
    }

    public void _RemoveCoinButton(GameObject obj){
        if(isWin) return;
        if(obj.GetComponent<CoinIdentify>().coin == null) return;

        Coin_Scriptable coin = obj.GetComponent<CoinIdentify>().coin; 

        for(int i = 0;i < coinSort.Count ;i++){
            if(coinSort[i] == coin){
                coinSort[i] = null;
                break;
            }
        }

        obj.GetComponent<CoinIdentify>()._Reset();
        _UpdateCoinForSort();
    }

    public void resetAll(){
        coinSort.Clear();
        isWin = false;
        createCoinListSort(coinSortListSize);
    }

    public void createCoinListSort(int listSize){
        for(int i = 0; i < listSize ; i++){
            coinSort.Add(null);
        }
    }

    private void _UpdateCoinForSort(){
        for(int i = 0; i < coinSort.Count;i++){
            coinSort[i] = listOfCoinSort[i].coin;
        }
    }

    private void checkCoin(){
        if(coinSort.Count == reqCoinList.Count){
            bool isSame = true;
            for (int i = 0; i < coinSort.Count; i++)
            {
                if (coinSort[i] != reqCoinList[i])
                {
                    isSame = false;
                    break;
                }
            }

            if(isSame){
                isWin = true;
            }
        }
    }

    public void _Restart()
    {
        resetAll();
    }
}
