using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls playerControls;
    private Vector2 movementInput;
    public float speed = 5f;
    public float minX = -10f;
    public float maxX = 10f;

    public float tiltAngle = 15f; // The maximum angle to tilt the player when moving

    private void Awake()
    {
        // Create a new instance of the PlayerControls class (from your Input Action)
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        // Enable the controls
        playerControls.Enable();

        // Bind input events
        playerControls.Player.Move.performed += OnMove;
        playerControls.Player.Move.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        // Unbind input events and disable controls
        playerControls.Player.Move.performed -= OnMove;
        playerControls.Player.Move.canceled -= OnMoveCanceled;
        playerControls.Disable();
    }

    private void Update()
    {
        // Move only on the X axis, back and forth
        float moveX = movementInput.x * speed * Time.deltaTime;
        Vector3 newPosition = transform.position + new Vector3(moveX, 0f, 0f);

        // Clamp the player's position between minX and maxX
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

        // Apply the position
        transform.position = newPosition;

        // Rotate the player based on movement direction
        RotatePlayerBasedOnMovement();
    }

    private void RotatePlayerBasedOnMovement()
    {
        // Check the direction of movement and apply a tilt
        float targetTilt = -movementInput.x * tiltAngle; // Negative for left, positive for right

        // Apply rotation around the Z-axis
        transform.rotation = Quaternion.Euler(0f, 0f, targetTilt);
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        // Get the 2D input from the InputAction
        movementInput = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        // Reset movement when input is canceled (key released)
        movementInput = Vector2.zero;
    }
}
