using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonClass<GameManager>
{
    [Header("SceneManager")]
    CutSceneManager cutSceneManager; // getCompo every stage
}
