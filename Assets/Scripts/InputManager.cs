using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public delegate void OnSpacePressed();
    public static event OnSpacePressed onSpacePressed;

    public static InputManager instance;

    private PlayerInput playerInput;
    public Vector2 moveInput = Vector2.zero;

    private void Awake()
    {
        if(instance == null)
            instance = this;

        playerInput = new PlayerInput();
    }
    private void OnEnable()
    {
        playerInput.Enable();
        playerInput.PlayerAction.Move.performed += ReadMoveDirection;
        playerInput.PlayerAction.Throw.performed += ThrowAction;
    }

    private void OnDestroy()
    {
        playerInput.Disable();
        playerInput.PlayerAction.Move.performed -= ReadMoveDirection;
        playerInput.PlayerAction.Throw.performed -= ThrowAction;


    }
    private void ReadMoveDirection(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }
    private void ThrowAction(InputAction.CallbackContext ctx)
    {
        onSpacePressed?.Invoke();
    }
}
