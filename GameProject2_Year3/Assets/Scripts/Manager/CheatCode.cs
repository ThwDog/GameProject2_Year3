using UnityEngine;

public class CheatCode : MonoBehaviour
{
    // TODO : Make Cheat code easy to use 
    LoadScene loadScene;
    PlayerController player; 
    CheatForStage cheat;
    public bool cheatEnable = true;

    private void Awake() {
        loadScene = GetComponent<LoadScene>();
    }

    private void OnGUI() {
        areaFindCanCheat();

        if(!cheatEnable) return;
        if(GUI.Button(new Rect(10, 800, 110, 50),"Next scene")){
            loadScene._LoadScene();
        }
        if(player != null)
        {
            if(GUI.Button(new Rect(10, 400, 110, 50),"ItemCheat")){
                InventorySystem inventory = player.gameObject.GetComponent<InventorySystem>();
                inventory.cheat();
            }
            if(GUI.Button(new Rect(10, 880, 110, 50),"ReSpawn")){
                SpawnPlayer spawn = FindObjectOfType<SpawnPlayer>();
                spawn.deSpawn();
            }
            if(GUI.Button(new Rect(10, 960, 110, 50),"Increase Speed")){
                player.speed += 10;
            }
            if(player.speed > 5){
                if(GUI.Button(new Rect(10, 1040, 110, 50),"decrease Speed")){
                    player.speed -= 10;
                }
            }
        }
        if(cheat != null){
            cheat.cheatStage();
        }
    }

    private void areaFindCanCheat(){
        if(!player){
            try{
                if(loadScene.CheckNextStage() - 1 != 0){
                    player = FindObjectOfType<PlayerController>();
                }
            }
            catch{
                return;
            } 
        }
        if(!cheat){
            try{
                if(loadScene.CheckNextStage() - 1 != 0){
                    cheat = FindObjectOfType<CheatForStage>();
                }
            }
            catch{
                return;
            } 
        }
    }
}
