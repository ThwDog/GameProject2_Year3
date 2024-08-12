using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //TODO : Jump ,Push obj , inventory (reset when next LEVEL) , spite 45 deg player 0 deg 

    [Header("Setting")]
    [SerializeField] float speed;
    //Jump
    [Header("")]
    private float verticalSpeed;
    [SerializeField] private float gravityValue = 20f;
    private float current_GValue;
    [SerializeField] private float stickingGravityPro = 20f;
    [SerializeField][Range(-10.0f, 10.0f)] float input_Delay;
    private Collider playerCollider;
    bool isGrounded;

    private CharacterController controller;
    private InputManager input;

    bool ground()
    {
        float distToGround = playerCollider.bounds.extents.y;
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f);
    }

    private void Awake()
    {
        playerCollider = GetComponent<CharacterController>();
    }

    private void Start()
    {
        current_GValue = gravityValue;

        // Cursor.visible = false;

        controller = GetComponent<CharacterController>();
        input = GetComponent<InputManager>();
    }

    private void FixedUpdate()
    {
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
        
        move += verticalSpeed * Vector3.up;
        // move.y = 0;

        controller.Move(move * Time.deltaTime * speed);
    }

    void checkGround(){

        if (isGrounded){
            verticalSpeed = -current_GValue * stickingGravityPro;
        }
        else{
            verticalSpeed -= current_GValue;
        }
    }


}
