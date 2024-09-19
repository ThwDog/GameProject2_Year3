using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(InputManager) , typeof(InventorySystem))]
public class PlayerController : MonoBehaviour , Ipauseable
{
    //TODO : inventory (reset when next LEVEL) , spite 45 deg player 0 deg 

    [Header("Setting")]
    [SerializeField][Range(0f, 100f)] float speed;
    [Header("")]
    private float verticalSpeed;
    [SerializeField] private float gravityValue = 20f;
    private float current_GValue;
    [SerializeField] private float stickingGravityPro = 20f;
    [SerializeField][Range(-10.0f, 10.0f)] float input_Delay;
    private Collider playerCollider;
    bool isGrounded;

    //Animation
    bool isFishing = false;
    public Animator anim;
    SpriteRenderer sprite;
    private CharacterController controller;
    private InputManager input;
    [SerializeField] List<string> listOfAnimation;

    bool ground()
    {
        float distToGround = playerCollider.bounds.extents.y;
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f);
    }

    [SerializeField] bool paused = false;

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
        if(!paused) paused = !paused;
    }

    public void resume(){
        if(paused) paused = !paused;
    }    

    private void Awake()
    {
        playerCollider = GetComponent<CharacterController>();
    }

    private void Start()
    {
        current_GValue = gravityValue;
        sprite = anim.gameObject.GetComponent<SpriteRenderer>();
        // Cursor.visible = false;

        controller = GetComponent<CharacterController>();
        input = GetComponent<InputManager>();
    }

    private void FixedUpdate()
    {
        if(paused) return;
        // if(Input.GetKeyDown(KeyCode.Escape))
        //     Cursor.visible = true;
        isGrounded = ground();
        move();
        checkGround();
    }

    private void move()
    {

        Vector2 movement = input.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x, 0, movement.y);

        if (move.sqrMagnitude > 1.0f)
            move.Normalize();


        //Movement
        move += verticalSpeed * Vector3.up;
        // move.y = 0;
        if(Input.GetMouseButton(1))return;
        //Animation
        fishingCheck();
        if(isFishing) return;
        if(isAnimPlaying(0,"CollectItem"))return; // if animation collect item is play return
        if(isAnimPlaying(0,"Flute"))return;
        if(isAnimPlaying(0,"Fishing_End"))return;

        if(move.x > 0) {
            flipX(true);
        }
        else flipX(false);
        anim.SetBool("SideWalk",move.x > 0  && move.z == 0 || move.x < 0 && move.z == 0); 
        anim.SetFloat("Vertical",move.z); 


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
    void fishingCheck(){
        if(isAnimPlaying(0,"Fishing_Start")){
            anim.SetBool("IsFishing",true); //set is fishing is true
            if(!isFishing){
                StartCoroutine(fishing_Finish(3f));
            }
        }

        //TODO : if fishing end is play get item
    }

    IEnumerator fishing_Finish(float time){
        isFishing = true;
        yield return new WaitForSeconds(time);
        anim.SetBool("IsFishing",false); //set is fishing is true
        yield return null;
        isFishing = false;
    }
}
