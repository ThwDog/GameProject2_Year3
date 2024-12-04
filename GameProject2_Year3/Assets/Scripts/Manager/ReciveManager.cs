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
            // Debug.Log("Cant find");
        }
    }

    public void toMainMenu(){
        try{
            FindObjectOfType<GameManager>().toMainMenu();
        }
        catch{

        }
    }

    public void exit(){
        try{
            FindObjectOfType<GameManager>().exit();
        }
        catch{

        }
    }
}

