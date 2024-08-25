using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonClass<GameManager>
{
    public int stageIndex; // what this stage
    CutSceneManager cutSceneManager; // getCompo every stage
}
