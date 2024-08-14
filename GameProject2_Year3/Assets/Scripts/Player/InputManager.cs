using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    internal PlayerAction inputs;

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

    // for disable action 
    public void Disable(InputAction action){
        action.Disable();
    }

    public void Enable(InputAction action){
        action.Enable();
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
