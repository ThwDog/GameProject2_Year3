using UnityEngine;

public class ReciveManager : MonoBehaviour
{
    public void NextChapter(){
        try{
            FindObjectOfType<GameManager>().NextChapter();
        }
        catch{

        }
    }

    public void pauseMenu(){
        try{
            FindObjectOfType<UIManager>().pauseMenu();
        }
        catch{

        }
    }
}

