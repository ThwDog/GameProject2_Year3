using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class CoinSortPuzzle_Script : MonoBehaviour, IRestartable
{
    [Header("setting")]
    [SerializeField] private GameObject canvas;
    [Tooltip("How should player sort the coin")][SerializeField] List<Coin_Scriptable> reqCoinList; // sort requirement
    [Tooltip("Size of coin that can sort")][SerializeField] private int coinSortListSize = 3;
    [Tooltip("Coin that player sort")] List<Coin_Scriptable> coinSort = new List<Coin_Scriptable>(); // show coin that player sort

    [Header("")]
    [SerializeField] List<CoinIdentify> listOfCoinSort;
    [SerializeField] bool isWin = false;
    [SerializeField] bool canPlay = false;

    private ShowUICollision showUI;
    private EventScript _event;

    PlayerController player;

    private void Start()
    {
        _event = GetComponent<EventScript>();
        createCoinListSort(coinSortListSize);
        showUI = GetComponent<ShowUICollision>();
    }

    private void Update()
    {
        if(!canPlay) return;
        if (isWin)
        {
            _event._FinishEvent();
            canPlay = false;
            return;
        }

        checkCoin();
    }

    public void _AddCoinButton(GameObject obj)
    {
        if (isWin) return;

        Coin_Scriptable coin = obj.GetComponent<CoinIdentify>().coin;

        if (coinSort.Contains(coin)) return;


        foreach (var coins in listOfCoinSort)
        {
            if (coins.coin == null)
            {
                coins.changeIden(coin);
                coinSort[0] = coins.coin;
                break;
            }
        }

        _UpdateCoinForSort();
    }

    public void _RemoveCoinButton(GameObject obj)
    {
        if (isWin) return;
        if (obj.GetComponent<CoinIdentify>().coin == null) return;

        Coin_Scriptable coin = obj.GetComponent<CoinIdentify>().coin;

        for (int i = 0; i < coinSort.Count; i++)
        {
            if (coinSort[i] == coin)
            {
                coinSort[i] = null;
                break;
            }
        }

        obj.GetComponent<CoinIdentify>()._Reset();
        _UpdateCoinForSort();
    }

    public void resetAll()
    {
        coinSort.Clear();
        isWin = false;
        createCoinListSort(coinSortListSize);
    }

    public void createCoinListSort(int listSize)
    {
        for (int i = 0; i < listSize; i++)
        {
            coinSort.Add(null);
        }
    }

    private void _UpdateCoinForSort()
    {
        for (int i = 0; i < coinSort.Count; i++)
        {
            coinSort[i] = listOfCoinSort[i].coin;
        }
    }

    private void checkCoin()
    {
        if (coinSort.Count == reqCoinList.Count)
        {
            bool isSame = true;
            for (int i = 0; i < coinSort.Count; i++)
            {
                if (coinSort[i] != reqCoinList[i])
                {
                    isSame = false;
                    break;
                }
            }

            if (isSame)
            {
                isWin = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(!canPlay) return;
        if (!other.gameObject.GetComponent<PlayerController>()) return;
        if (!canvas.activeSelf)
        {
            showUI.ShowDescription();
            if (Input.GetKey(KeyCode.E))
            {
                showUI.CloseDescription();
                canvas.SetActive(true);
                _event._StartEvent();
            }
        }
    }

    public void exit()
    {
        showUI.ShowDescription();
        resetAll();
        canvas.SetActive(false);
        _event._ExitEvent();
    }

    private void OnTriggerExit(Collider other)
    {
        if(!canPlay) return;
        if (!other.gameObject.GetComponent<PlayerController>()) return;

        showUI.CloseDescription();
    }

    public void _Restart()
    {
        resetAll();
    }
    
    public void canPlaySet(bool _bool){
        canPlay = _bool;
    }
}
