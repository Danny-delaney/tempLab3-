using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // For UI if you want to display lives

public class PlayerRespawn : MonoBehaviour
{
    public int lives = 3; // Number of player lives
    public float invulnerabilityDuration = 2f; // Invulnerability time after respawn
    public Renderer playerRenderer; // Player's Renderer to flash
    public Color invulnerableColor = Color.red; // Flash color during invulnerability
    public Text livesText; // UI Text to display lives (optional)

    private bool isInvulnerable = false;
    private Color originalColor; // Original player color
    private bool isFlashing = false;

    private void Start()
    {
        originalColor = playerRenderer.material.color; // Store original color
        UpdateLivesUI(); // Update UI at start
    }

    // Method to be called when player takes damage
    public void TakeDamage()
    {
        if (!isInvulnerable)
        {
            lives--;
            UpdateLivesUI();

            if (lives > 0)
            {
                Respawn();
            }
            else
            {
                // Handle game over (you can add your own logic here)
                Debug.Log("Game Over!");
                gameObject.SetActive(false); // Disable player on death
            }
        }
    }

    // Method to respawn the player and start invulnerability
    private void Respawn()
    {
        // Move player to initial spawn position or desired location
        transform.position = new Vector3(0, transform.position.y, transform.position.z);

        // Start the invulnerability timer
        StartCoroutine(Invulnerability());
    }

    // Invulnerability coroutine with flashing effect
    private IEnumerator Invulnerability()
    {
        isInvulnerable = true;
        float elapsedTime = 0f;

        while (elapsedTime < invulnerabilityDuration)
        {
            FlashEffect(); // Flashing effect
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        playerRenderer.material.color = originalColor; // Reset color after invulnerability
        isInvulnerable = false;
    }

    // Flashing effect method
    private void FlashEffect()
    {
        if (isFlashing)
        {
            playerRenderer.material.color = originalColor;
        }
        else
        {
            playerRenderer.material.color = invulnerableColor;
        }
        isFlashing = !isFlashing;
    }

    // Update the UI for lives (optional)
    private void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + lives;
        }
    }
}
