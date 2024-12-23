using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(InputManager) , typeof(InventorySystem))]
public class PlayerController : MonoBehaviour , Ipauseable
{
    //TODO : inventory (reset when next LEVEL) , spite 45 deg player 0 deg 

    [Header("Setting")]
    [Range(0f, 100f)] public float speed;
    [Header("")]
    private float verticalSpeed;
    [SerializeField] private float gravityValue = 20f;
    private float current_GValue;
    [SerializeField] private float stickingGravityPro = 20f;
    [SerializeField][Range(-10.0f, 10.0f)] float input_Delay;

    //Animation
    bool isFishing = false;
    [Header("Animation")]
    public Animator anim;
    [SerializeField] GameObject showItemSprite;
    [SerializeField] bool paused = false;

    SpriteRenderer sprite;
    bool isGrounded;

    internal Collider playerCollider;
    internal CharacterController controller;
    private InputManager input;
    // [SerializeField] List<string> listOfAnimation;
    CamControlAndSetting cam;

    internal string walkSoundName;
    internal bool canPlayWalkSound = true;

    bool ground()
    {
        float distToGround = playerCollider.bounds.extents.y;
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f);
    }


    //check if animation is play
    bool isAnimPlaying(int animLayer, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(animLayer).IsName(stateName) &&
                anim.GetCurrentAnimatorStateInfo(animLayer).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }

    public void pause(){
        // Debug.Log("Player pause");
        if(!paused) paused = !paused;
        setAnimToNormal();
    }

    public void resume(){
        // Debug.Log("Player resume");
        if(paused) paused = !paused;
    }    

    private void Awake()
    {
        playerCollider = GetComponent<CharacterController>();
    }

    private void Start()
    {
        cam = FindObjectOfType<CamControlAndSetting>();
        current_GValue = gravityValue;
        sprite = anim.gameObject.GetComponent<SpriteRenderer>();
        // Cursor.visible = false;

        controller = GetComponent<CharacterController>();
        input = GetComponent<InputManager>();
    }

    private void FixedUpdate()
    {
        fishingCheck();

        if(paused) return;
        // if(Input.GetKeyDown(KeyCode.Escape))
        //     Cursor.visible = true;
        isGrounded = ground();
        move();
        checkGround();
        // groundSoundCheck();
    }

    private void move()
    {
        Vector2 movement = input.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0, movement.y);

        if (move.sqrMagnitude > 1.0f){
            move.Normalize();
        }

        cam.camShake(move.sqrMagnitude);

        //Movement
        move += verticalSpeed * Vector3.up;
        // move.y = 0;
        if(Input.GetMouseButton(1))return;
        //Animation
        // fishingCheck();
        if(isFishing) return;
        if(isAnimPlaying(0,"CollectItem"))return; // if animation collect item is play return
        if(isAnimPlaying(0,"Flute"))return;
        // if(isAnimPlaying(0,"Fishing_End"))return;

        if(move.x > 0) {
            flipX(true);
        }
        else flipX(false);
        anim.SetBool("SideWalk",move.x > 0  && move.z == 0 || move.x < 0 && move.z == 0); 
        anim.SetFloat("Vertical",move.z); 

        // WalkSound
        if(move.sqrMagnitude > 0.3 || move.sqrMagnitude < -0.3){
            GetComponent<TerrainSoundCheck>().groundSoundCheck();
            if(walkSoundName != ""){
                if(canPlayWalkSound) StartCoroutine(walkSound());
            }
        }


        controller.Move(move * Time.deltaTime * speed);
    }

    // play by bool
    public void _PlayAnimation(string AnimationName, bool _bool){
        anim.SetBool(AnimationName,_bool);
    }
    // play by trigger
    public void _PlayAnimation(string AnimationName){
        anim.SetTrigger(AnimationName);
    }

    // flip sprite
    void flipX(bool flip){
        if(flip){
            sprite.flipX = true;
        }
        else{
            sprite.flipX = false;
        }
    }

    void checkGround(){

        if (isGrounded){
            verticalSpeed = -current_GValue * stickingGravityPro;
        }
        else{
            verticalSpeed -= current_GValue;
        }
    }
    // check if start fishing
    public void fishingCheck(){
        if(isAnimPlaying(0,"Fishing_Start")){
            // Debug.Log("Fishing");
            anim.SetBool("IsFishing",true); //set is fishing is true
            _SetFishing(true);
            // if(!isFishing){
            //     // StartCoroutine(fishing_Finish(3f));
            //     _SetFishing(true);
            // }
        }
    }

    // TODO : For animation doesn't freeze
    public void setAnimToNormal(){
        anim.SetFloat("Vertical",0); 
        anim.SetBool("SideWalk",false); 
        cam.camShake(0);
    }

    // IEnumerator fishing_Finish(float time){
    //     isFishing = true;
    //     yield return new WaitForSeconds(time);
    //     anim.SetBool("IsFishing",false); //set is fishing is true
    //     yield return new WaitForSeconds(time);
    //     isFishing = false;
    // }

    public void _SetFishing(bool _bool){
        isFishing = _bool;
        anim.SetBool("IsFishing",_bool); //set is fishing is true
    }

    // TODO : need to change


    IEnumerator walkSound(){
        SoundManager.instance.PlaySfx(walkSoundName);
        canPlayWalkSound = false;
        yield return new WaitForSeconds(0.25f);
        canPlayWalkSound = true;
    }

    public void enabledShowItemSprite(){
        showItemSprite.SetActive(true);
    }

    public void disableShowItemSprite(){
        showItemSprite.SetActive(false);
    }

    public void setShowItemSprite(Sprite _sprite){
        showItemSprite.GetComponent<SpriteRenderer>().sprite = _sprite;
    }
}
