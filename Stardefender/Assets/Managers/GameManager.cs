using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerRespawn playerRespawn; // Reference to the PlayerRespawn script

    // This is where you can check game conditions or reset the level
    private void Update()
    {
        // Example of how you might reset the game on Game Over (optional)
        if (playerRespawn.lives <= 0)
        {
            Debug.Log("Reset Game");
            // Reload scene or reset game (you can use SceneManager for this)
        }
    }
}
