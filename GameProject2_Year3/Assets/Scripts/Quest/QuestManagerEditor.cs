using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(QuestManager))]
public class QuestManagerEditor : Editor {
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();

        QuestManager questManager = (QuestManager)target;
        EditorGUILayout.LabelField("");
        if(questManager.listCanNext){

            if(GUILayout.Button("Check Quest")){
                questManager.finishCheckQuest();
            }

            EditorGUILayout.LabelField("Quest Title",questManager.currentQuestTitle);
            EditorGUILayout.LabelField("Quest Description",questManager.currentQuestDes);

            EditorGUILayout.LabelField("Quest Type",questManager.currentQuestType);
            if(questManager.type == Quest.type.itemReqType){
                EditorGUILayout.LabelField("Quest Item Req",questManager.currentQuestItemReq);
            }
        }
        else{
            EditorGUILayout.LabelField("list is ended");
        }
    }
}

