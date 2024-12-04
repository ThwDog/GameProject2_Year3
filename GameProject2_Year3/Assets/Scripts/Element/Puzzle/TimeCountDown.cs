using System;
using TMPro;
using UnityEngine;

public class TimeCountDown : MonoBehaviour , Ipauseable , IRestartable
{
    [SerializeField] private float maxMin; // max minute
    private float currentTime {get; set;}
    [SerializeField] private TMP_Text text; // text box that show time
    [SerializeField] private bool isStart = false;
    bool isOpen = false;
    string timeText;

    private void Update() {
        if(!isOpen){
            TimeSpan _timeSpan = TimeSpan.FromMinutes(0);
            timeText = _timeSpan.ToString(@"mm\:ss");
            currentTime = maxMin;
            isOpen = true;
            // Debug.Log(timeText);
        }
        if(!isStart) return;
        // Count Down Time //
        if(currentTime <= 0) return; //TODO: set cause if fail 
        currentTime -= Time.deltaTime;
        TimeSpan timeSpan = TimeSpan.FromMinutes(currentTime);
        timeText = timeSpan.ToString(@"mm\:ss");

        // Debug.Log(timeText);
    }



    public void pause()
    {
        isStart = false;
    }

    public void resume()
    {
        isStart = true;
    }

    public void _Restart()
    {
        currentTime = maxMin;
    }
}
