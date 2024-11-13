using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HeneGames.DialogueSystem
{
    public class DialogueManager : MonoBehaviour
    {
        // TODO : Use sentence event to setActive NPC Sprite When talk to NPC
        // TODO : Make sentence can change in game
        private int currentSentence; // current character sentence
        private float coolDownTimer;
        private bool dialogueIsOn;
        private DialogueTrigger dialogueTrigger;

        public enum TriggerState
        {
            Collision,
            Input ,
            Call 
        }

        public enum CollisionType{
            repeat,
            oneTime
        }

        [Header("References")]
        [SerializeField] private AudioSource audioSource;

        [Header("Events")]
        public UnityEvent startDialogueEvent;
        public UnityEvent nextSentenceDialogueEvent;
        public UnityEvent endDialogueEvent;
        public bool questIsFinish = false;
        private bool startQuest;
        [Tooltip("Set it if you req item to start dialogue")][SerializeField] private List<CollectableItem_Scriptable> req_item = new List<CollectableItem_Scriptable>();  // check if player inventory and reqment inven is match

        [Header("Dialogue")]
        public TriggerState triggerState;
        [SerializeField] private CollisionType _collisionType;
        bool hasBeenPlay;
        private List<NPC_Centence> sentences = new List<NPC_Centence>();
        [SerializeField] private List<NPC_Centence> NormalSentences = new List<NPC_Centence>(); // normal
        [SerializeField] private List<NPC_Centence> reqSentences = new List<NPC_Centence>(); // requirement  or quest sentence
        [SerializeField] private List<NPC_Centence> talkAgainSentences = new List<NPC_Centence>(); // player has spoke to this npc
        [SerializeField] private bool canTalk = true;
        [SerializeField] private List<NPC_Centence> cantTalkToSentences = new List<NPC_Centence>(); // player doesnt have something

        private void Start() {
            sentences = NormalSentences;
        }

        private void Update()
        {
            if(questIsFinish) sentences = reqSentences;
            else if(!questIsFinish && startQuest){
                if(talkAgainSentences.Count <= 0) return;
                sentences = talkAgainSentences;
            } 
            else if(!canTalk) sentences = cantTalkToSentences;
            else if(canTalk) sentences = NormalSentences;
            //Timer
            if(coolDownTimer > 0f)
            {
                coolDownTimer -= Time.deltaTime;
            }

            // if(DialogueUI.instance.checkSentence()){
            //     DialogueUI.instance.ShowInteractionUI(false);
            // }

            //Start dialogue by input
            if (Input.GetKeyDown(DialogueUI.instance.actionInput) && dialogueTrigger != null && !dialogueIsOn)
            {
                //Trigger event inside DialogueTrigger component
                if (dialogueTrigger != null)
                {
                    dialogueTrigger.startDialogueEvent.Invoke();
                }

                startDialogueEvent.Invoke();

                //If component found start dialogue
                DialogueUI.instance.StartDialogue(this);

                //Hide interaction UI
                DialogueUI.instance.ShowInteractionUI(false);

                dialogueIsOn = true;
            }
        }

        //Start dialogue by trigger
        private void OnTriggerEnter(Collider other)
        {
            if (triggerState == TriggerState.Collision && !dialogueIsOn)
            {
                if(_collisionType == CollisionType.oneTime && hasBeenPlay) return;
                //Try to find the "DialogueTrigger" component in the crashing collider
                if (other.gameObject.TryGetComponent<DialogueTrigger>(out DialogueTrigger _trigger))
                {
                    //Trigger event inside DialogueTrigger component and store refenrece
                    dialogueTrigger = _trigger;
                    dialogueTrigger.startDialogueEvent.Invoke();

                    startDialogueEvent.Invoke();

                    //If component found start dialogue
                    DialogueUI.instance.StartDialogue(this);

                    dialogueIsOn = true;
                }
            }
        }


        //Start dialogue by pressing DialogueUI action input
        private void OnTriggerStay(Collider other)
        {
            if (dialogueTrigger != null)
                return;

            if (triggerState == TriggerState.Input && dialogueTrigger == null)
            {
                //Try to find the "DialogueTrigger" component in the crashing collider
                if (other.gameObject.TryGetComponent<DialogueTrigger>(out DialogueTrigger _trigger))
                {
                    // if(other.gameObject.TryGetComponent<InventorySystem>(out InventorySystem _inventorySystem)){
                    //     inventoryCheck(_inventorySystem);
                    // }
                    //Show interaction UI
                    DialogueUI.instance.ShowInteractionUI(true);

                    //Store refenrece
                    dialogueTrigger = _trigger;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            //Try to find the "DialogueTrigger" component from the exiting collider
            if (other.gameObject.TryGetComponent<DialogueTrigger>(out DialogueTrigger _trigger))
            {
                //Hide interaction UI
                DialogueUI.instance.ShowInteractionUI(false);

                //Stop dialogue
                StopDialogue();
            }
        }

        public void StartDialogue()
        {
            //Start event
            if(dialogueTrigger != null)
            {
                dialogueTrigger.startDialogueEvent.Invoke();
            }
            //Reset sentence index
            currentSentence = 0;

            //Show first sentence in dialogue UI
            ShowCurrentSentence();

            //Play dialogue sound
            PlaySound(sentences[currentSentence].sentenceSound);

            //Cooldown timer
            coolDownTimer = sentences[currentSentence].skipDelayTime;
        }

        public void NextSentence(out bool lastSentence)
        {
            //The next sentence cannot be changed immediately after starting
            if (coolDownTimer > 0f)
            {
                lastSentence = false;
                return;
            }

            //Add one to sentence index
            currentSentence++;

            //Next sentence event
            if (dialogueTrigger != null)
            {
                dialogueTrigger.nextSentenceDialogueEvent.Invoke();
            }

            nextSentenceDialogueEvent.Invoke();

            //If last sentence stop dialogue and return
            if (currentSentence > sentences.Count - 1)
            {
                StopDialogue();

                lastSentence = true;

                return;
            }

            //If not last sentence continue...
            lastSentence = false;

            //Play dialogue sound
            PlaySound(sentences[currentSentence].sentenceSound);

            //Show next sentence in dialogue UI
            ShowCurrentSentence();

            //Cooldown timer
            coolDownTimer = sentences[currentSentence].skipDelayTime;
        }

        public void StopDialogue()
        {
            //Stop dialogue event
            // if (dialogueTrigger != null)
            // {
            //     dialogueTrigger.endDialogueEvent.Invoke();
            // }

            FindObjectOfType<DialogueTrigger>().endDialogueEvent.Invoke();

            endDialogueEvent.Invoke();

            //Hide dialogue UI
            DialogueUI.instance.ClearText();

            //Stop audiosource so that the speaker's voice does not play in the background
            if(audioSource != null)
            {
                audioSource.Stop();
            }

            //Remove trigger refence
            dialogueIsOn = false;
            dialogueTrigger = null;

            hasBeenPlay = true;
        }

        private void PlaySound(AudioClip _audioClip)
        {
            //Play the sound only if it exists
            if (_audioClip == null || audioSource == null)
                return;

            //Stop the audioSource so that the new sentence does not overlap with the old one
            audioSource.Stop();

            //Play sentence sound
            audioSource.PlayOneShot(_audioClip);
        }

        private void ShowCurrentSentence()
        {
            if (sentences[currentSentence].dialogueCharacter != null)
            {
                //Show sentence on the screen
                DialogueUI.instance.ShowSentence(sentences[currentSentence].dialogueCharacter, sentences[currentSentence].sentence);

                //Invoke sentence event
                sentences[currentSentence].sentenceEvent.Invoke();
            }
            else
            {
                DialogueCharacter _dialogueCharacter = new DialogueCharacter();
                _dialogueCharacter.characterName = "";
                _dialogueCharacter.characterPhoto = null;

                DialogueUI.instance.ShowSentence(_dialogueCharacter, sentences[currentSentence].sentence);

                //Invoke sentence event
                sentences[currentSentence].sentenceEvent.Invoke();
            }
        }

        public int CurrentSentenceLenght()
        {
            if(sentences.Count <= 0)
                return 0;

            return sentences[currentSentence].sentence.Length;
        }

        public bool checkStatusQuest(){
            return startQuest;
        }

        public bool checkCanTalk(){
            return canTalk;
        }

        public void setStartQuest(bool _bool){
            startQuest = _bool;
        }

        public void setCanTalk(bool _bool){
            canTalk = _bool;
        }
        
        // Check inventory in player
        public void inventoryCheck(InventorySystem inventory){
            if(req_item == null) return;
            for(int i = 0; i < req_item.Count ;i++){
                if(inventory.inventory.Contains(req_item[i])){
                    questIsFinish = true;
                }
                else {
                    questIsFinish = false;
                    break;
                }
            }
        }

        // use for play dialogue that doesn't need to set trigger
        public void playDialogue(){
            DialogueUI.instance.StartDialogue(this);
            try{
                FindObjectOfType<DialogueTrigger>().startDialogueEvent.Invoke();
            }
            catch{

            }
        }
    }


    [System.Serializable]
    public class NPC_Centence
    {
        [Header("------------------------------------------------------------")]

        public DialogueCharacter dialogueCharacter;

        [TextArea(3, 10)]
        public string sentence;

        public float skipDelayTime = 0.5f;

        public AudioClip sentenceSound;

        public UnityEvent sentenceEvent;
    }
}