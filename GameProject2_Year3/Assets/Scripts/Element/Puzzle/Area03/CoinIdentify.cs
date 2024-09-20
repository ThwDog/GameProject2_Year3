using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinIdentify : MonoBehaviour , IRestartable
{
    public Coin_Scriptable coin;
    internal Image image;

    public void changeIden(Coin_Scriptable coin){
        this.coin = coin;
        image.sprite = coin.sprite;
    }

    private void Start() {
        image = GetComponent<Image>();
        if(coin){
            image.sprite = coin.sprite ;
        }
    }

    public void _Reset(){
        image.sprite = null ;
        coin = null ;
    }

    public void _Restart()
    {
        _Reset();
    }
}
