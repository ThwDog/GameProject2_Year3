using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //TODO : Jump , Push obj , inventory (reset when next LEVEL)

    [Header("Setting")]
    [SerializeField] float speed;
    
    private CharacterController controller;
    private InputManager input;
    private Transform camTransform;

    private void Start() 
    {
        Cursor.visible = false;

        controller = GetComponent<CharacterController>();    
        input = GetComponent<InputManager>();
        camTransform = Camera.main.transform;
    }

    private void FixedUpdate() 
    {
        // if(Input.GetKeyDown(KeyCode.Escape))
        //     Cursor.visible = true;
        move();    
    }

    private void move()
    {
        Vector2 movement = input.GetPlayerMovement();
        Vector3 move = new Vector3(movement.x,0,movement.y);
        move = camTransform.forward * move.z + camTransform.right *move.x;
        move.y = 0;

        controller.Move(move * Time.deltaTime * speed);
    }
}
