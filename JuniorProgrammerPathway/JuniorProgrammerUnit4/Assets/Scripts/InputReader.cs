using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    private Controls controls;
    public float verticalInput;
    public float horizontalInput;

    private void OnEnable()
    {
        controls = new Controls();
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }
    private void OnDisable()
    {
        controls.Player.Disable();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        verticalInput = context.ReadValue<Vector2>().y;
        horizontalInput = context.ReadValue<Vector2>().x;
    }
}
