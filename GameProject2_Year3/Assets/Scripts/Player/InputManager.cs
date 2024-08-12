using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerAction inputs;

    public void Awake() 
    {
        inputs = new PlayerAction();    
    }

    private void OnEnable() 
    {
        inputs.Enable();    
    }

    private void OnDisable() 
    {
        inputs.Disable();    
    }

    public Vector2 GetPlayerMovement()
    {
        return inputs.Player.Movement.ReadValue<Vector2>();
    }

    public bool jump(){
        return inputs.Player.Jump.triggered;
    }

    public Vector2 GetMouseDelta()
    {
        return inputs.Player.Look.ReadValue<Vector2>();
    }

}
