using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // Import the Input System

public class DeathButton : MonoBehaviour
{
    public PlayerRespawn playerRespawn; // Reference to PlayerRespawn script

    // This method is called when the "Death" input action is triggered
    public void OnDeath(InputAction.CallbackContext context)
    {
        // We check if the action was performed (button pressed)
        if (context.performed)
        {
            playerRespawn.TakeDamage(); // Call the TakeDamage method when the input is triggered
        }
    }
}
