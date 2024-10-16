using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Reference to the auto-generated PlayerControls class
    private PlayerControls controls;
    private Vector2 moveInput;
    public float moveSpeed = 5f; // Speed of the player
    private float screenBoundaryX; // To clamp the player's movement within screen bounds

    private void Awake()
    {
        // Initialize the PlayerControls
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        // Enable the player action map
        controls.player.Enable();

        // Subscribe to the move action
        controls.player.move.performed += OnMove;
        controls.player.move.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        // Disable the player action map
        controls.player.Disable();

        // Unsubscribe from the action when disabled
        controls.player.move.performed -= OnMove;
        controls.player.move.canceled -= OnMoveCanceled;
    }

    private void Start()
    {
        // Calculate the screen boundaries based on the camera
        Camera mainCamera = Camera.main;
        float playerHalfWidth = transform.localScale.x / 2;
        screenBoundaryX = mainCamera.aspect * mainCamera.orthographicSize - playerHalfWidth;
    }

    private void Update()
    {
        // Move the player based on the input
        Vector3 move = new Vector3(moveInput.x, 0, 0) * moveSpeed * Time.deltaTime;
        transform.position += move;

        // Clamp the player's position to stay within screen boundaries
        float clampedX = Mathf.Clamp(transform.position.x, -screenBoundaryX, screenBoundaryX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    // This method gets called when the move input is performed
    private void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // This method gets called when the move input is canceled (like key/button release)
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }
}
