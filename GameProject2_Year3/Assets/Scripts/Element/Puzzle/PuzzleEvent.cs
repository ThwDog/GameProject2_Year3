using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Have one per Scene or One Active other not
// TODO : Should be in every puzzle Script
public class PuzzleEvent : MonoBehaviour
{
    public UnityEvent startEvent;
    public UnityEvent finishEvent;
    public UnityEvent exitEvent;

    
    // use when start that puzzle
    // Should start when player stay or enter trigger
    public void _StartPuzzle(){
        startEvent.Invoke();
    }

    // use when finish that puzzle
    public void _FinishPuzzle(){
        finishEvent.Invoke();
    }

    // use when player Exit puzzle
    public void _ExitPuzzle(){
        exitEvent.Invoke();
    }
}
