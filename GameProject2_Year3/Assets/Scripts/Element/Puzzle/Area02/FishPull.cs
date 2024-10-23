using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// TODO : use when finish " pool.finishPool(); "
[RequireComponent(typeof(PoolPuzzle))]
public class FishPull : MonoBehaviour
{
    [SerializeField] private float _playerPer = 50 , _fishPer = 50; // percentPool Default value

    public float playerPer {get{return _playerPer;}set{}}
    public float fishPer {get{return _fishPer;}set{}}

    [SerializeField] private Slider playerBar , FishBar ;
    [SerializeField] private GameObject pullingBar;
    PoolPuzzle pool;

    private void OnEnable() {
        pool = GetComponent<PoolPuzzle>();
        pullingBar.SetActive(true);
        _Reset();
    }

    private void OnDisable() {
        pullingBar.SetActive(false);
        _Reset();
    }

    private void Update() {
        if(!pool.startPuzzle) return;
        playerBar.value = playerPer;
        FishBar.value = fishPer;
    }

    private void _Reset(){
        playerPer = _playerPer;
        fishPer = _fishPer;
    }
}
