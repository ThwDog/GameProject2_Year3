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
}
